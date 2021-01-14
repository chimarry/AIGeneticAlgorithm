using GlobalMaximum;
using GlobalMaximum.GeneticAlgorithm;
using System;

namespace GeneticAlgorithm
{
    public static class TestGlobalMaximum
    {
        public static void FindGlobalMaximum()
        {
            GeneticAlgorithmConfiguration configuration = new GeneticAlgorithmConfiguration(eliteCount: 10, populationSize: 200, iterationCount: 100);
            GeneticAlgorithmExecutor executor = new GeneticAlgorithmExecutor(configuration);
            Chromosome chromosome = executor.Execute();
            Console.WriteLine(chromosome);
        }
    }
}
