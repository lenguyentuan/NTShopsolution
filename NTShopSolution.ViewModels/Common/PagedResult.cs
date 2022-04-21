﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.ViewModels.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { set; get; }

        public int TotalRecord { set; get; }

    }
}
