using System;

namespace ServicesContracts
{
    public class Vacancy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Requirements { get; set; }
        public int Salary { get; set; }
        public string SocialPackage { get; set; }
        public string EmployerId { get; set; }
        public string EmployerName { get; set; }
        public DateTime OpeningDate { get; set; }
    }
}
