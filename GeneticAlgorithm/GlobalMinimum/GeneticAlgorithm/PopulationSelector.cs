﻿using GlobalMinimum.GeneticAlgorithm.Chromosomes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalMinimum.GeneticAlgorithm.GeneticAlgorithm
{
    public class PopulationSelector
    {
        private readonly int eliteCount;
        private readonly int populationSize;

        public PopulationSelector(int populationSize, int eliteCount)
        {
            this.eliteCount = eliteCount;
            this.populationSize = populationSize;
        }

        public List<Chromosome> GeneratePopulation()
        {
            Random xRandom = new Random();
            Random yRandom = new Random();
            List<Chromosome> population = new List<Chromosome>(populationSize);
            for (int i = 0; i < populationSize; ++i)
                population.Add(GenerateChromosome(xRandom, yRandom));

            return population;
        }

        public static Chromosome GenerateChromosome(Random xRandom, Random yRandom)
        {
            double x = xRandom.NextDouble(Constants.XLower, Constants.XUpper);
            double y = yRandom.NextDouble(Constants.YLower, Constants.YUpper);
            return new Chromosome(x, y);
        }

        public List<Chromosome> SelectFittestIndividuals(List<Chromosome> population)
        {
            List<Chromosome> individuals = population.ToList();
            List<Chromosome> selectedIndividuals = new List<Chromosome>();

            double max = population.Max(x => x.FittnessValue);
            double min = population.Min(x => x.FittnessValue);

            individuals = individuals.Select(chromosome => (chromosome, max - chromosome.FittnessValue))
                                                     .OrderByDescending(x => x.Item2)
                                                     .Select(x => x.chromosome)
                                                     .ToList();

            selectedIndividuals.AddRange(individuals.Take(eliteCount));

            individuals = individuals.Skip(eliteCount).ToList();

            Random selectionRandom = new Random();
            foreach (Chromosome chromosome in individuals)
            {
                double randomProbability = selectionRandom.NextDouble();
                double probabilityToBeSelected = Math.Abs((max - chromosome.FittnessValue - min) / (double)(max - min));
                if (probabilityToBeSelected > randomProbability)
                    selectedIndividuals.Add(chromosome);
            }
            return selectedIndividuals.AsParallel()
                                      .Distinct(new ChromosomeEqualityComparer())
                                      .ToList();
        }
    }
}
