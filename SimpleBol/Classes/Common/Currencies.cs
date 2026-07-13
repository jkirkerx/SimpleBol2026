namespace SimpleBol.Classes.Common
{
    public class Currency
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class Currencies
    {
        public static List<Currency> GetCurrencyCodes()
        {
            List<Currency> currencies = new List<Currency>
            {
                new Currency { Code = "USD", Name = "United States Dollar" },
                new Currency { Code = "EUR", Name = "Euro" },
                new Currency { Code = "GBP", Name = "British Pound" },
                new Currency { Code = "JPY", Name = "Japanese Yen" },
                new Currency { Code = "CAD", Name = "Canadian Dollar" },
                // Add more currencies here...
            };


            return currencies;
        }
    }


}
