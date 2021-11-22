using ServicesContracts;

namespace SupplierSvc1.Filtering
{
    public static class FilterConfigurator
    {
        public static FilterRules DefaultSettings { get; } = Configure(new FilterBuilder());

        public static FilterRules Configure(FilterBuilder builder)
        {
            builder.For<Vacancy>()
                .StringEqualsStringIgnoreCase(nameof(Vacancy.Id))
                .StringEqualsStringIgnoreCase(nameof(Vacancy.EmployerId))
                .StringContainsStringIgnoreCase(nameof(Vacancy.Name))
                .StringContainsStringIgnoreCase(nameof(Vacancy.EmployerName))
                .StringContainsStringIgnoreCase(nameof(Vacancy.Requirements))
                .StringContainsStringIgnoreCase(nameof(Vacancy.SocialPackage))
                .IntIsEqualOrGreaterThan(nameof(Vacancy.Salary));

            return builder.Build();
        }
    }
}
