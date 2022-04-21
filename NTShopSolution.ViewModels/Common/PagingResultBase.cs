using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.ViewModels.Common
{
    public class PagingResultBase
    {
        public int PageIndex { set; get; }

        public int PageSize { get; set; }
    }
}
