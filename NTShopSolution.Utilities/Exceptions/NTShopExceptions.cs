using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Utilities.Exceptions
{
    public class NTShopExceptions : Exception
    {
        public NTShopExceptions()
        {

        }

        public NTShopExceptions(string message): base(message)
        {
             
        }

        public NTShopExceptions(string message,Exception inner) : base (message,inner)
        {

        }
    }
}
