using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class CurrencyConversionHelper
    {
        public static int ToEuro(this decimal value)
        {
            return (int)(value * 100);
        }
    }
}
