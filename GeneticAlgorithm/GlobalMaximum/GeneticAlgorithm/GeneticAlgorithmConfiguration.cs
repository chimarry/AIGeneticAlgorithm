namespace GlobalMaximum.GeneticAlgorithm
{
    public class GeneticAlgorithmConfiguration
    {
        public double MutationProbability { get; }

        public double CrossoverProbability { get; }

        public int PopulationSize { get; }

        public int EliteCount { get; }

        public int IterationCount { get; }

        public GeneticAlgorithmConfiguration(double mutationProbability = 0.05, double crossoverProbability = 0.85, int populationSize = 100, int eliteCount = 1, int iterationCount = 100)
        {
            MutationProbability = mutationProbability;
            CrossoverProbability = crossoverProbability;
            PopulationSize = populationSize;
            EliteCount = eliteCount;
            IterationCount = iterationCount;
        }
    }
}
