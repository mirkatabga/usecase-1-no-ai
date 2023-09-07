using UseCaseOneNoAI.Domain.ValueObjects;

namespace UseCaseOneNoAI.Application.Resources
{
    public class Country
    {
        public string? Name { get; set; }

        public string? CountryCodeTwoLetters { get; set; }

        public string? CountryCodeThreeLetters { get; set; }

        public double? Area { get; set; }

        public long? Population { get; set; }

        public Currency? Currency { get; set; }

        public string? Capital { get; set; }

        public Coordinates? Coordinates { get; set; }
    }
}
