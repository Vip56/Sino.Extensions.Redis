using System;
using System.Collections.Generic;

namespace Sino.Extensions.Redis.Internal.Utilities
{
    static class RedisArgs
    {
        public static string[] Concat(params object[][] arrays)
        {
            int count = 0;
            foreach (var ar in arrays)
                count += ar.Length;

            int pos = 0;
            string[] output = new string[count];
            for (int i = 0; i < arrays.Length; i++)
            {
                for(int j = 0; j < arrays[i].Length; j++)
                {
                    object obj = arrays[i][j];
                    output[pos++] = obj == null ? string.Empty : obj.ToString();
                }
            }
            return output;
        }

        public static string[] Concat(string str, params object[] arrays)
        {
            return Concat(new[] { str }, arrays);
        }

        public static object[] GetTupleArgs<TItem1,TItem2>(Tuple<TItem1, TItem2>[] tuples)
        {
            List<object> args = new List<object>();
            foreach(var kvp in tuples)
            {
                args.AddRange(new object[] { kvp.Item1, kvp.Item2 });
            }
            return args.ToArray();
        }

        public static string GetScore(double score, bool isExclusive)
        {
            if (double.IsNegativeInfinity(score) || score == double.MinValue)
                return "-inf";
            else if (double.IsPositiveInfinity(score) || score == double.MaxValue)
                return "+inf";
            else if (isExclusive)
                return '(' + score.ToString();
            else
                return score.ToString();
        }

        public static object[] FromDict(Dictionary<string, string> dict)
        {
            var array = new List<object>();
            foreach (var keyValue in dict)
            {
                if (keyValue.Key != null && keyValue.Value != null)
                    array.AddRange(new[] { keyValue.Key, keyValue.Value });
            }
            return array.ToArray();
        }
    }
}
