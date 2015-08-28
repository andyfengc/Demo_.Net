using System;
using System.Linq;

namespace KellermanSoftware.CompareNetObjects.TypeComparers
{
    /// <summary>
    /// Logic to compare two LINQ enumerators
    /// </summary>
    public class EnumerableComparer :BaseTypeComparer
    {
        private readonly ListComparer _compareIList;

        /// <summary>
        /// Constructor that takes a root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        public EnumerableComparer(RootComparer rootComparer) : base(rootComparer)
        {
            _compareIList = new ListComparer(rootComparer);
        }

        /// <summary>
        /// Returns true if either object is of type LINQ Enumerator
        /// </summary>
        /// <param name="type1">The type of the first object</param>
        /// <param name="type2">The type of the second object</param>
        /// <returns></returns>
        public override bool IsTypeMatch(Type type1, Type type2)
        {
            if (type1 == null || type2 == null)
                return false;

            return TypeHelper.IsEnumerable(type1) || TypeHelper.IsEnumerable(type2);
        }

        /// <summary>
        /// Compare two objects that implement LINQ Enumerator
        /// </summary>
        public override void CompareType(CompareParms parms)
        {
            Type t1 = parms.Object1.GetType();
            Type t2 = parms.Object2.GetType();

            var l1 = TypeHelper.IsEnumerable(t1) ? ConvertEnumerableToList(parms.Object1) : parms.Object1;
            var l2 = TypeHelper.IsEnumerable(t2) ? ConvertEnumerableToList(parms.Object2) : parms.Object2;

            parms.Object1 = l1;
            parms.Object2 = l2;

            _compareIList.CompareType(parms);
        }

        private object ConvertEnumerableToList(object source)
        {
            var type = source.GetType();

            if (type.IsArray)
                return source;

            var genArgs = type.GetGenericArguments();
            var toList = typeof(Enumerable).GetMethod("ToList");
            var constructedToList = toList.MakeGenericMethod(genArgs[0]);
            var resultList = constructedToList.Invoke(null, new[] { source });

            return resultList;
        }
    }
}
