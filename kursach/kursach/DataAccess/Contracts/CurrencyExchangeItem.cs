namespace kursach.DataAccess.Contracts
{
    public class CurrencyExchangeItem
    {
        public string Currency { get; set; }
        public double? Buy { get; set; }
        public double? Sale { get; set; }
    }
}
