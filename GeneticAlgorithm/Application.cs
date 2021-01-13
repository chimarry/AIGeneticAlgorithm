using GeneticAlgorithm.GeneticAlgorithm;
using System;
using System.Collections;

namespace GeneticAlgorithm
{
    public class Application
    {
        public static void Main(string[] args)
        {
            PopulationSelector populationSelector = new PopulationSelector(30, 4);
            populationSelector.GeneratePopulation();
            //    double number = 1.214234;
            //    byte[] data = BitConverter.GetBytes(number);
            //    BitArray array = new BitArray(data);
            //    Console.WriteLine("Bits from byte array: " + array);
            //    Chromosome chromosome = new Chromosome(2.2441, 1.35325);
            //    (double x, double y) = chromosome.ToRealNumber();

            //    Console.WriteLine("Real x = " + 2.2441 + "Real y = " + 1.35325);
            //    Console.WriteLine("XGenes = " + chromosome.XGenes.ToBitString());
            //    Console.WriteLine("YGenes = " + chromosome.YGenes.ToBitString());
            //    Console.WriteLine("x = " + x + "y = " + y);
        }
    }
}
