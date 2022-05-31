using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs.Movies
{
    /// <summary>
    /// Represents a range that has start and end indexes.
    /// </summary>
    public class Range<T>
    {
        /// <summary>
        /// Holds the range's lower bound.
        /// </summary>
        public T LowerBound;

        /// <summary>
        /// Holds the range's upper bound.
        /// </summary>
        public T UpperBound;

        public Range(T lowerBound, T upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public Range()
        {

        }
    }
}
