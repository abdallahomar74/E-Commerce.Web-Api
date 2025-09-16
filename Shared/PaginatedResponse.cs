using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginatedResponse<TEntity>
    {
        public PaginatedResponse(int pageIndex, int pagesize, int totalCount, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            Pagesize = pagesize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int Pagesize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
