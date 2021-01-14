using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalMaximum.GeneticAlgorithm
{
    public class GeneticAlgorithmExecutor
    {
        public enum CrossoverPoint { One, Two }

        private readonly PopulationSelector populationSelector;
        private readonly GeneticAlgorithmConfiguration configuration;
        private readonly CrossoverPoint crossoverPoint;

        public GeneticAlgorithmExecutor(GeneticAlgorithmConfiguration configuration, CrossoverPoint crossoverPoint = CrossoverPoint.One)
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
            return population.Max();
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
            Random crossoverPointRandom = new Random();

            Random mutationRandom = new Random();
            Random mutationPointRandom = new Random();

            // Do crossover
            foreach ((Chromosome firstParent, Chromosome secondParent) in pairs)
            {
                newPopulation.AddTwo(firstParent, secondParent);

                double randomProbability = crossoverRandom.NextDouble();
                if (randomProbability < configuration.CrossoverProbability)
                {
                    (Chromosome firstChild, Chromosome secondChild) =
                        Crossover(firstParent, secondParent, crossoverPointRandom, crossoverPoint);
                    newPopulation.AddTwo(firstChild, secondChild);
                }
            }

            // Do mutation
            foreach (Chromosome chromosome in newPopulation)
            {
                double randomProbability = mutationRandom.NextDouble();
                if (randomProbability < configuration.MutationProbability)
                    Mutate(chromosome, mutationPointRandom);
            }

            List<Chromosome> distinctIndividuals = newPopulation.Distinct(new ChromosomeEqualityComparer())
                                                                .ToList();

            while (distinctIndividuals.Count < configuration.PopulationSize)
                distinctIndividuals.Add(PopulationSelector.GenerateChromosome(new Random()));

            IEnumerable<Chromosome> elite = distinctIndividuals.OrderBy(x => x.FittnessValue)
                                                               .Take(configuration.EliteCount);

            return elite.Concat(distinctIndividuals)
                        .Take(configuration.PopulationSize)
                        .ToList();
        }

        private (Chromosome, Chromosome) Crossover(Chromosome first, Chromosome second, Random random, CrossoverPoint type)
        {
            int begin = random.Next(0, first.Lenght);
            int end = first.Lenght;
            if (type == CrossoverPoint.Two)
                end = random.Next(0, first.Lenght);
            return first.Crossover(second, begin, end);
        }

        private void Mutate(Chromosome chromosome, Random random)
        {
            int indexX = random.Next(0, chromosome.Lenght);
            chromosome.Genes[indexX] = !chromosome.Genes[indexX];
        }
    }
}
