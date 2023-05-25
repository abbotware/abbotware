using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abbotware.Quant.Finance.Rates;

namespace Abbotware.Quant.Finance.Equations
{
    public static class TimeValue
    {
        public static decimal FutureValue(decimal presentValue, CompoundingRate rate, double periods) 
        { 
            if (rate.IsContinuous)
            {

            }


            throw new NotImplementedException();
        }
    }
}
