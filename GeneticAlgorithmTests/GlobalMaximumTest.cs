using GlobalMaximum;
using GlobalMaximum.GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using static GlobalMaximum.GeneticAlgorithm.GeneticAlgorithmExecutor;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GlobalMaximumTest
    {
        [DataRow(10, 100, 1, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
        [DataRow(200, 100, 15, 0.1, 0.85)]

        [DataRow(100, 10, 1, 0.05, 0.85)]
        [DataRow(100, 100, 0, 0.05, 0.85)]
        [DataRow(100, 100, 0, 0.05, 0.95)]
        [DataRow(100, 100, 0, 0.10, 0.95)]
        [DataRow(100, 1000, 0, 0.05, 0.95)]
        [DataRow(100, 1000, 0, 0.1, 0.95)]
        [DataRow(100, 500, 5, 0.05, 0.85)]
        [DataRow(100, 1000, 5, 0.05, 0.85)]
        [DataRow(100, 10000, 5, 0.05, 0.85)]
        [DataRow(100, 50000, 5, 0.05, 0.85)]

        [DataRow(100, 100, 1, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(100, 100, 5, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(200, 100, 5, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(100, 1000, 5, 0.1, 0.95, CrossoverPoint.Two)]
        [DataRow(100, 1000, 5, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(100, 1000, 5, 0.40, 0.85, CrossoverPoint.Two)]
        [DataRow(100, 1000, 5, 0.40, 0.85, CrossoverPoint.One)]

        [DataRow(50, 100, 10, 0.05, 0.85)]
        [DataRow(100, 100, 30, 0.05, 0.85)]
        [DataRow(100, 100, 30, 0.05, 0.50)]
        [DataRow(100, 100, 10, 0.40, 0.85)]
        [DataRow(100, 10000, 10, 0.40, 0.85)]
        [DataRow(100, 10000, 10, 0.05, 0.85)]
        [DataRow(100, 10000, 10, 0.05, 0.95)]
        [DataRow(100, 100, 0, 0.05, 0.85)]
        [DataTestMethod]
        public void FindMaximum(int populationSize, int iterationCount = 100, int eliteCount = 1, double mutationProbability = 0.05, double crossover = 0.85, CrossoverPoint point = CrossoverPoint.One)
        {
            using StreamWriter file = new StreamWriter(File.Open("GlobalMaximum.csv", FileMode.Append));
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var configuration = new GeneticAlgorithmConfiguration(mutationProbability, crossover, populationSize, eliteCount, iterationCount);
            GeneticAlgorithmExecutor geneticAlgorithm = new GeneticAlgorithmExecutor(configuration, point);
            Chromosome result = geneticAlgorithm.Execute();
            stopWatch.Stop();
            string numberOfPoints = point == CrossoverPoint.One ? "Jedna" : "Dvije";
            file.WriteLine($"{populationSize},{eliteCount},{iterationCount},{numberOfPoints},{crossover},{mutationProbability},{result.GenesAsRealNumber},{result.FittnessValue},{stopWatch.ElapsedMilliseconds}");
            file.Flush();
        }
    }
}
