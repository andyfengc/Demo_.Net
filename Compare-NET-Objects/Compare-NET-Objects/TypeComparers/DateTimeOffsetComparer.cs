using System;

namespace KellermanSoftware.CompareNetObjects.TypeComparers
{
    public class DateTimeOffSetComparer : BaseTypeComparer
    {
        /// <summary>
        /// Constructor that takes a root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        public DateTimeOffSetComparer(RootComparer rootComparer) : base(rootComparer)
        {
        }

        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return TypeHelper.IsDateTimeOffset(type1) && TypeHelper.IsDateTimeOffset(type2);
        }

        public override void CompareType(CompareParms parms)
        {
            //This should never happen, null check happens one level up
            if (parms.Object1 == null || parms.Object2 == null)
                return;

            DateTimeOffset date1 = (DateTimeOffset)parms.Object1;
            DateTimeOffset date2 = (DateTimeOffset)parms.Object2;

            if (Math.Abs(date1.DateTime.Subtract(date2.DateTime).TotalMilliseconds) > parms.Config.MaxMillisecondsDateDifference)
                AddDifference(parms);

        }
    }
}
