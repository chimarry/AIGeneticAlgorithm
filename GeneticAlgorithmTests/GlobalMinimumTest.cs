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
        [DataRow(100, 100, 1, 0.05, 0.85)]
        [DataRow(50, 100, 5, 0.05, 0.85)]
        [DataRow(50, 50, 5, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.1, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
        [DataRow(100, 100, 1, 0.05, 0.85)]
        [DataRow(50, 100, 5, 0.05, 0.85)]
        [DataRow(50, 50, 5, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.1, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
        [DataRow(100, 100, 1, 0.05, 0.85)]
        [DataRow(50, 100, 5, 0.05, 0.85)]
        [DataRow(50, 50, 5, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.1, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
        [DataRow(100, 100, 10, 0.05, 0.85)]
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
            file.WriteLine($"{populationSize},{iterationCount},{eliteCount},{numberOfPoints},{crossover},{mutationProbability},{result.XGenesAsRealNumber},{result.YGenesAsRealNumber},{result.FittnessValue},{stopWatch.ElapsedMilliseconds}");
            file.Flush();
        }
    }
}
