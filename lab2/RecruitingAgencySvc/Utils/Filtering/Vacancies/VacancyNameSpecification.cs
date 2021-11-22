using ServicesContracts;
using System;

namespace Utils.Filtering.Vacancies
{
    public class VacancyNameSpecification : CompositeSpecification<Vacancy>
    {
        public VacancyNameSpecification(string name)
        {
            _name = name;
        }

        public override bool IsSatisfiedBy(Vacancy candidate)
        {
            return candidate.Name.Contains(_name, StringComparison.InvariantCultureIgnoreCase);
        }

        private readonly string _name;
    }
}
