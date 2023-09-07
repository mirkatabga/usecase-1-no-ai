using AutoMapper;
using UseCaseOneNoAI.Domain.ValueObjects;

namespace UseCaseOneNoAI.Infrastructure.Mapping
{
    internal class CoordinatesValueConverter : IValueConverter<IEnumerable<double>, Coordinates?>
    {
        public Coordinates? Convert(IEnumerable<double> sourceMember, ResolutionContext context)
        {
            if (sourceMember is null)
            {
                return null;
            }

            var latLng = sourceMember.ToArray();
            var expectedCount = 2;

            if (latLng.Length != expectedCount)
            {
                throw new Exception($"Unecpected items count. Expected {expectedCount}, but found {latLng.Length}");
            }

            return new Coordinates(Latitude: latLng[0], Longitude: latLng[1]);
        }
    }
}
