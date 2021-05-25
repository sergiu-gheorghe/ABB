using Domain.Common;
using System.Collections.Generic;

namespace Domain
{
    public class Company : ValueObject
    {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string BS { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return CatchPhrase;
            yield return BS;
        }
    }
}
