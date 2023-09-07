namespace UseCaseOneNoAI.Infrastructure.Models
{
    internal class CountryDataModel
    {
        public NameDataModel? Name { get; set; }

        public double? Area { get; set; }

        public long? Population { get; set; }

        public string? CCA2 { get; set; }

        public string? CCA3 { get; set; }

        public Dictionary<string, CurrencyDataModel>? Currencies { get; set; }

        public IEnumerable<string>? Capital { get; set; }

        public IEnumerable<double>? LatLng { get; set; }

        internal class NameDataModel
        {
            public string? Common { get; set; }

            public NameDataModel? NativeName { get; set; }

            public string? Official { get; set; }
        }

        internal class CurrencyDataModel
        {
            public string? Name { get; set; }

            public string? Symbol { get; set; }
        }
    }
}
