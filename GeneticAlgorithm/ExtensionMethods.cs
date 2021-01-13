using System;
using System.Collections;
using System.Text;

namespace GeneticAlgorithm
{
    public static class BitArrayExtensionMethods
    {
        public static string ToBitString(this BitArray bits)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bits.Count; i++)
                sb.Append(bits[i] ? '1' : '0');
            return sb.ToString();
        }

        public static int ToInt(this BitArray bits)
        {
            int[] array = new int[1];
            bits.CopyTo(array, 0);
            return array[0];
        }
    }

    public static class RandomExtensionMethods
    {
        public static double NextDouble(this Random random, int lower, int upper)
            => Math.Round(random.NextDouble() * (upper - lower) + lower, Constants.Precision);
    }
}
