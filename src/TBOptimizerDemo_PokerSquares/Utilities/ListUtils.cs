using System;
using System.Collections.Generic;
using System.Text;

namespace TrailBlazerDemo.Utilities
{
    public static class ListUtils
    {
        public static IEnumerable<TResult> Zip<TResult, T1, T2> (IEnumerable<T1> a, IEnumerable<T2> b, Func<T1, T2, TResult> combine)
        {
            using (var f = a.GetEnumerator())
            using (var s = b.GetEnumerator())
            {
                while (f.MoveNext() && s.MoveNext())
                    yield return combine(f.Current, s.Current);
            }
        }

        public static IEnumerable<T> SliceRow<T>(this T[,] arrayToSlice, int rowIndex)
        {
            for (int i = 0; i < arrayToSlice.GetLength(0); i++)
            {
                yield return arrayToSlice[rowIndex, i];
            }
        }

        public static IEnumerable<T> SliceCol<T>(this T[,] arrayToSlice, int colIndex)
        {
            for (int i = 0; i < arrayToSlice.GetLength(1); i++)
            {
                yield return arrayToSlice[i, colIndex];
            }
        }
    }
}
