using GlobalMaximum;
using GlobalMaximum.GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GlobalMaximumTest
    {
        public void FindMaximum(int populationSize, int iterationCount = 100, int eliteCount = 1, double mutationProbability = 0.05, double crossover = 0.15)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var configuration = new GeneticAlgorithmConfiguration(mutationProbability, crossover, populationSize, eliteCount, iterationCount);
            GeneticAlgorithmExecutor geneticAlgorithm = new GeneticAlgorithmExecutor(configuration);
            Chromosome result = geneticAlgorithm.Execute();
            stopWatch.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"  ** time needed for calculation: {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}
