using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }


        public PaginatedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageIndex)
       {
            Items = items;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ItemsFrom = pageSize * (pageIndex - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
        }
    }
}
