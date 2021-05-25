using System.Collections.Generic;

namespace IntegrationTests
{
    public class PaginatedListTest<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
