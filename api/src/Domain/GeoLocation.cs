using Domain.Common;
using System.Collections.Generic;

namespace Domain
{
    public class GeoLocation : ValueObject
    {
        public string Lat { get; set; }
        public string Lng { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Lat;
            yield return Lng;
        }
    }
}
