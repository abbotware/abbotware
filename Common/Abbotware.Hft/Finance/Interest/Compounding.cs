namespace Abbotware.Quant.Finance.Interest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Interest { }

    public class SimpleInterest : Interest { }

    public abstract class CompoundingInterest : Interest { }

    public class ContinousCompounding : CompoundingInterest { }
    public class DiscreteCompounding : CompoundingInterest { }

}
