using GlobalMinimum.GeneticAlgorithm.Chromosomes;
using GlobalMinimum.GeneticAlgorithm.GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using static GlobalMinimum.GeneticAlgorithm.GeneticAlgorithm.GeneticAlgorithmExecutor;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GlobalMinimumTest
    {
        // Change population
        [DataRow(10, 50, 5, 0.05, 0.85)]
        [DataRow(100, 50, 5, 0.05, 0.85)]
        [DataRow(1000, 100, 10, 0.1, 0.85)]
        // Change iteration count
        [DataRow(100, 10, 1, 0.05, 0.85)]
        [DataRow(100, 100, 1, 0.05, 0.85)]
        [DataRow(100, 500, 1, 0.05, 0.85)]
        [DataRow(100, 1000, 1, 0.05, 0.85)]
        [DataRow(100, 10000, 5, 0.05, 0.85)]
        [DataRow(100, 50000, 5, 0.05, 0.85)]
        // Change iteration count with population
        [DataRow(10, 500, 10, 0.05, 0.85)]
        [DataRow(10, 10000, 10, 0.05, 0.85)]
        [DataRow(1000, 10000, 10, 0.05, 0.85)]
        // Change number of crosspoints
        [DataRow(10, 100, 1, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(100, 100, 5, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(1000, 100, 5, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(100, 1000, 5, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(10, 1000, 5, 0.05, 0.85, CrossoverPoint.Two)]
        // Change crosspoint probability
        [DataRow(10, 100, 1, 0.05, 0.95)]
        [DataRow(10, 100, 1, 0.05, 0.95, CrossoverPoint.Two)]
        [DataRow(1000, 100, 1, 0.05, 0.95)]
        [DataRow(100, 1000, 1, 0.05, 0.95)]
        [DataRow(1000, 100, 1, 0.05, 0.95, CrossoverPoint.Two)]
        [DataRow(100, 1000, 1, 0.05, 0.95, CrossoverPoint.Two)]
        [DataRow(100, 100, 5, 0.05, 0.85, CrossoverPoint.Two)]
        [DataRow(100, 100, 5, 0.05, 0.50)]
        [DataRow(100, 100, 5, 0.05, 0.50, CrossoverPoint.Two)]
        [DataRow(1000, 1000, 5, 0.05, 0.50)]
        [DataRow(1000, 100, 5, 0.05, 0.50)]
        // Change mutation probability
        [DataRow(10, 1000, 1, 0.15, 0.85)]
        [DataRow(100, 100, 10, 0.15, 0.85)]
        [DataRow(100, 100, 5, 0.15, 0.85)]
        [DataRow(1000, 100, 5, 0.15, 0.85)]
        [DataRow(10000, 100, 5, 0.15, 0.85)]
        [DataRow(100, 10000, 5, 0.15, 0.85)]
        [DataRow(100, 100, 10, 0.40, 0.85)]
        [DataRow(1000, 100, 10, 0.40, 0.85)]
        [DataRow(1000, 10000, 10, 0.40, 0.85)]
        // Change elitecount
        [DataRow(100, 100, 0, 0.15, 0.85)]
        [DataRow(100, 100, 10, 0.15, 0.85)]
        [DataRow(100, 100, 1, 0.05, 0.85)]
        [DataRow(100, 100, 5, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
        [DataRow(50, 10000, 5, 0.05, 0.85)]
        [DataRow(100, 10000, 15, 0.05, 0.85)]
        [DataTestMethod]
        public void FindMinimum(int populationSize, int iterationCount = 100, int eliteCount = 1, double mutationProbability = 0.05, double crossover = 0.85, CrossoverPoint point = CrossoverPoint.One)
        {
            using StreamWriter file = new StreamWriter(File.Open("GlobalMinimum.csv", FileMode.Append));
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var configuration = new GeneticAlgorithmConfiguration(mutationProbability, crossover, populationSize, eliteCount, iterationCount);
            GeneticAlgorithmExecutor geneticAlgorithm = new GeneticAlgorithmExecutor(configuration, point);
            Chromosome result = geneticAlgorithm.Execute();
            stopWatch.Stop();
            string numberOfPoints = point == CrossoverPoint.One ? "Jedna" : "Dvije";
            file.WriteLine($"{populationSize},{eliteCount},{iterationCount},{numberOfPoints},{crossover},{mutationProbability},{result.XGenesAsRealNumber},{result.YGenesAsRealNumber},{result.FittnessValue},{stopWatch.ElapsedMilliseconds}");
            file.Flush();
        }
    }
}
