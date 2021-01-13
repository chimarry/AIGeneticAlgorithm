using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
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
            {
                double x = xRandom.NextDouble(Constants.XLower, Constants.XUpper);
                double y = yRandom.NextDouble(Constants.YLower, Constants.YUpper);
                population.Add(new Chromosome(x, y));
            }
            return population;
        }

        public 
    }
}
