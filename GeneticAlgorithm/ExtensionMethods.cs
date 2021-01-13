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
}
