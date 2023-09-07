using UseCaseOneNoAI.Domain.ValueObjects;

namespace UseCaseOneNoAI.Domain.Entities
{
    public class CountryEntity
    {
        public CountryEntity(string name, string countryCodeTwoLetters, string countryCodeThreeLetters)
        {
            Name = name;
            CountryCodeTwoLetters = countryCodeTwoLetters;
            CountryCodeThreeLetters = countryCodeThreeLetters;
        }

        public string Name { get; }

        public string CountryCodeTwoLetters { get; }

        public string CountryCodeThreeLetters { get; }
        
        public double? Area { get; set; }

        public long? Population { get; set; }

        public Currency? Currency { get; set; }

        public string? Capital { get; set; }

        public Coordinates? Coordinates { get; set; }
    }
}
