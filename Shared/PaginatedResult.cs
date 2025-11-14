using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS;

namespace Shared
{

    public class PaginatedResult<Dto>
    {
        public PaginatedResult(int pageIndex,int pagesize, int totalcount , IEnumerable<Dto> Date) 
        {
            PageIndex = pageIndex;
            PageSize = pagesize;
            TotalCount = totalcount;
            Data = Date;

        }

        public  int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; private set; }
        public IEnumerable<Dto> Data { get; set; }


    }
}
