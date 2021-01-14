using GlobalMaximum.GeneticAlgorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalMaximum
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

        public static (Chromosome childOne, Chromosome childTwo) Crossover(this Chromosome parentOne, Chromosome parentTwo, int begin, int end)
        {
            BitArray childOne = new BitArray(parentOne.Lenght);
            BitArray childTwo = new BitArray(parentTwo.Lenght);
            for (int i = 0; i < begin; ++i)
            {
                childOne[i] = parentOne.Genes[i];
                childTwo[i] = parentTwo.Genes[i];
            }
            for (int i = begin; i < end; ++i)
            {
                childOne[i] = parentTwo.Genes[i];
                childTwo[i] = parentOne.Genes[i];
            }
            for (int i = end; i < parentOne.Lenght; ++i)
            {
                childOne[i] = parentOne.Genes[i];
                childTwo[i] = parentTwo.Genes[i];
            }
            return (new Chromosome(childOne), new Chromosome(childTwo));
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
             => selected.FirstOrDefault() ?? PopulationSelector.GenerateChromosome(new Random());
    }
}
