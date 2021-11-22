using ServicesContracts;

namespace Utils.Filtering.Vacancies
{
    public class VacancySalarySpecification : CompositeSpecification<Vacancy>
    {
        public VacancySalarySpecification(int salary)
        {
            _salary = salary;
        }

        public override bool IsSatisfiedBy(Vacancy candidate)
        {
            return candidate.Salary >= _salary;
        }

        private readonly int _salary;
    }
}
