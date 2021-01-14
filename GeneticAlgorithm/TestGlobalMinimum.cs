using GlobalMinimum.GeneticAlgorithm.Chromosomes;
using GlobalMinimum.GeneticAlgorithm.GeneticAlgorithm;
using System;

namespace GeneticAlgorithm
{
    public static class TestGlobalMinimum
    {
        public static void FindGlobalMinimum()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            GeneticAlgorithmConfiguration configuration = new GeneticAlgorithmConfiguration(eliteCount: 10, populationSize: 200, iterationCount: 100);
            GeneticAlgorithmExecutor executor = new GeneticAlgorithmExecutor(configuration);
            Chromosome chromosome = executor.Execute();
            Console.WriteLine(chromosome);
            Console.WriteLine("Potrebno vrijeme: "+)
        }
    }
}
