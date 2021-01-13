using GeneticAlgorithm.Chromosomes;
using GeneticAlgorithm.GeneticAlgorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static class ListExtensionMethods
    {
        public static void AddTwo(this List<Chromosome> list, Chromosome first, Chromosome second)
        {
            list.Add(first);
            list.Add(second);
        }

        public static Chromosome FirstOrNew(this IEnumerable<Chromosome> selected)
             => selected.FirstOrDefault() ?? PopulationSelector.GenerateChromosome(new Random(), new Random());
    }
}
