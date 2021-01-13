using GeneticAlgorithm.Chromosomes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.GeneticAlgorithm
{
    public class GeneticAlgorithmExecutor
    {
        public delegate (Chromosome, Chromosome) CrossoverFunction(Chromosome firstParent, Chromosome secondParend, int crossoverIndex);

        private readonly CrossoverFunction crossoverFunction;
        private readonly PopulationSelector populationSelector;
        private readonly GeneticAlgorithmConfiguration configuration;

        public GeneticAlgorithmExecutor(GeneticAlgorithmConfiguration configuration, CrossoverFunction crossoverFunction)
        {
            this.configuration = configuration;
            this.crossoverFunction = crossoverFunction;
            populationSelector = new PopulationSelector(configuration.PopulationSize, configuration.EliteCount);
        }

        public void Execute()
        {
            List<Chromosome> population = populationSelector.GeneratePopulation();
            for (int i = 0; i < configuration.IterationCount; ++i)
            {
                List<Chromosome> selectedIndividuals = populationSelector.SelectFittestIndividuals(population);
                List<Chromosome> newPopulation = Evolve(selectedIndividuals);
                /* if (newPopulation.Any(x => x.Root.GetValue() == configuration.Result))
                     return newPopulation.FirstOrDefault(x => x.Root.GetValue() == configuration.Result);
                 population = newPopulation;
             }
             return null;*/
            }
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
            Random mutationRandom = new Random();

            foreach ((Chromosome firstParent, Chromosome secondParent) in pairs)
            {
                double randomProbability = crossoverRandom.NextDouble();
                if (randomProbability > configuration.CrossoverProbability)
                {
                    // TODO: Crossover
                    /*MathExpressionTree firstCopy = firstParent.Copy();
                    MathExpressionTree secondCopy = secondParent.Copy();
                    Crossover(firstCopy, secondCopy);
                    newPopulation.AddTwo(firstCopy, secondCopy);*/
                }
            }
            pairs.ForEach(x =>
            {
                newPopulation.AddTwo(x.Item1, x.Item2);
            });

            foreach (Chromosome chromosome in newPopulation)
            {
                double randomProbability = mutationRandom.NextDouble();
                if (randomProbability < configuration.MutationProbability)
                {
                    // TODO: Mutate(chromosome);
                }
            }

            List<Chromosome> distinctExpressions = newPopulation.Distinct(new ChromosomeEqualityComparer())
                                                                .ToList();

            while (distinctExpressions.Count < configuration.PopulationSize)
                distinctExpressions.Add(PopulationSelector.GenerateChromosome(new Random(), new Random());

            IEnumerable<Chromosome> elite = distinctExpressions.OrderBy(x => x.FittnessValue)
                                                               .Take(configuration.EliteCount);

            return elite.Concat(distinctExpressions)
                        .Take(configuration.PopulationSize)
                        .ToList();
        }

        private (Chromosome, Chromosome) Crossover(Chromosome first, Chromosome second, int crossoverPoint)
        {
            (BitArray,BitArray) CrossoverByAxis()
            {

            }
        }

        private void Mutate(MathExpressionTree expression)
        {
            MathExpressionNode randomNode = expression.GetRandomNode();
            randomNode.SubstiteValue(randomNode.IsLeaf ?
                                           stohasticGenerator.GetRandomOperand()
                                           : stohasticGenerator.GetRandomOperator(randomNode.LeftChild, randomNode.RightChild));
        }
    }
}
