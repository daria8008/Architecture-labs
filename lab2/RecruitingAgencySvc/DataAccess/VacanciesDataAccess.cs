using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using ServicesContracts;
using ServicesLocator;
using ServicesLocator.Clients;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Filtering.Vacancies;

namespace DataAccess
{
    public class VacanciesDataAccess
    {
        public VacanciesDataAccess()
        {
            _serviceClientFactory = new();
            _mongo = new();
        }

        public async Task<List<Vacancy>> GetVacanciesByNameAndSalaryAsync(string name, int minSalary)
        {
            var res = new List<Vacancy>();

            // get data from svc1
            //var svc1 = _serviceClientFactory.ResolveClient<SupplierSvc1Client>();
            //var filtersSvc1 = new Dictionary<string, string>()
            //{
            //    { nameof(Vacancy.Name), name },
            //    { nameof(Vacancy.Salary), minSalary.ToString() }
            //};
            //res.AddRange(await svc1.GetVacanciesByFilterAsync(filtersSvc1));

            // get data from svc2
            var service2 = _serviceClientFactory.ResolveClient<SupplierSvc2Client>();
            var filtersSvc2 = new VacancyNameSpecification(name).And(new VacancySalarySpecification(minSalary));
            var pageNumber = 0;
            while(true)
            {
                var allVacanciesSvc2 = await service2.GetVacanciesAsync(pageNumber);
                if (allVacanciesSvc2.Count == 0)
                {
                    break;
                }
                res.AddRange(allVacanciesSvc2.Where(x => filtersSvc2.IsSatisfiedBy(x)).ToArray());
                pageNumber++;
            }
            

            // get data from db
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Regex(nameof(Vacancy.Name), new BsonRegularExpression($".*{name}.*", "i")) & filterBuilder.Gte(nameof(Vacancy.Salary), minSalary);
            res.AddRange(await _mongo.GetAsync<Vacancy>("vacancies", filter));
            return res;
        }

        private readonly ServiceClientFactory _serviceClientFactory;
        private readonly MongodbClient _mongo;
    }
}
