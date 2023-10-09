using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concentrix__Common.Modal
{
    public class PagingParams
    {
        public PagingParams(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
