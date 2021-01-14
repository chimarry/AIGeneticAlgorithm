using GeneticAlgorithm.Chromosomes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm.GeneticAlgorithm
{
    public class GeneticAlgorithmExecutor
    {
        public enum CrossoverPoint { One, Two }

        private readonly PopulationSelector populationSelector;
        private readonly GeneticAlgorithmConfiguration configuration;
        private readonly CrossoverPoint crossoverPoint;

        public GeneticAlgorithmExecutor(GeneticAlgorithmConfiguration configuration, CrossoverPoint crossoverPoint)
        {
            this.configuration = configuration;
            this.crossoverPoint = crossoverPoint;
            populationSelector = new PopulationSelector(configuration.PopulationSize, configuration.EliteCount);
        }

        public Chromosome Execute()
        {
            List<Chromosome> population = populationSelector.GeneratePopulation();
            for (int i = 0; i < configuration.IterationCount; ++i)
            {
                List<Chromosome> selectedIndividuals = populationSelector.SelectFittestIndividuals(population);
                List<Chromosome> newPopulation = Evolve(selectedIndividuals);

                population = newPopulation;
            }
            return population.Min();
        }

        private List<Chromosome> Evolve(List<Chromosome> selectedIndividuals)
        {
            List<(Chromosome, Chromosome)> pairs = selectedIndividuals.OrderBy(x => new Guid())
                                                                      .Take(selectedIndividuals.Count)
                                                                      .Select((individual, i) => new { index = i, individual })
                                                                      .GroupBy(x => x.index / 2, x => x.individual)
                                                                      .Select(g => (g.First(), g.Skip(1).FirstOrNew()))
                                                                      .ToList();

            List<Chromosome> newPopulation = new List<Chromosome>();

            Random crossoverRandom = new Random();
            Random crossoverPointXRandom = new Random();
            Random crossoverPointYRandom = new Random();

            Random mutationRandom = new Random();
            Random mutationPointXRandom = new Random();
            Random mutationPointYRandom = new Random();

            // Do crossover
            foreach ((Chromosome firstParent, Chromosome secondParent) in pairs)
            {
                newPopulation.AddTwo(firstParent, secondParent);

                double randomProbability = crossoverRandom.NextDouble();
                if (randomProbability > configuration.CrossoverProbability)
                {
                    (Chromosome firstChild, Chromosome secondChild) =
                        Crossover(firstParent, secondParent, crossoverPointXRandom, crossoverPointYRandom, crossoverPoint);
                    newPopulation.AddTwo(firstChild, secondChild);
                }
            }

            // Do mutation
            foreach (Chromosome chromosome in newPopulation)
            {
                double randomProbability = mutationRandom.NextDouble();
                if (randomProbability < configuration.MutationProbability)
                    Mutate(chromosome, mutationPointXRandom, mutationPointYRandom);
            }

            List<Chromosome> distinctIndividuals = newPopulation.Distinct(new ChromosomeEqualityComparer())
                                                                .ToList();

            while (distinctIndividuals.Count < configuration.PopulationSize)
                distinctIndividuals.Add(PopulationSelector.GenerateChromosome(new Random(), new Random()));

            IEnumerable<Chromosome> elite = distinctIndividuals.OrderBy(x => x.FittnessValue)
                                                               .Take(configuration.EliteCount);

            return elite.Concat(distinctIndividuals)
                        .Take(configuration.PopulationSize)
                        .ToList();
        }

        private (Chromosome, Chromosome) Crossover(Chromosome first, Chromosome second, Random xRandom, Random yRandom, CrossoverPoint type)
        {
            (BitArray, BitArray) crossoverByAxis(BitArray firstParameter, BitArray secondParameter, Random random)
            {
                int begin = random.Next(0, firstParameter.Length);
                int end = firstParameter.Length;
                if (type == CrossoverPoint.Two)
                    end = random.Next(0, firstParameter.Length);
                return firstParameter.Crossover(secondParameter, begin, end);
            }
            (BitArray childOneX, BitArray childTwoX) = crossoverByAxis(first.XGenes, second.XGenes, xRandom);
            (BitArray childOneY, BitArray childTwoY) = crossoverByAxis(first.YGenes, second.YGenes, yRandom);
            return (new Chromosome(childOneX, childOneY), new Chromosome(childTwoX, childTwoY));
        }

        private void Mutate(Chromosome chromosome, Random xRandom, Random yRandom)
        {
            int indexX = xRandom.Next(0, chromosome.Lenght.x);
            int indexY = yRandom.Next(0, chromosome.Lenght.y);

            chromosome.XGenes[indexX] = !chromosome.XGenes[indexX];
            chromosome.YGenes[indexY] = !chromosome.YGenes[indexY];
        }
    }
}
