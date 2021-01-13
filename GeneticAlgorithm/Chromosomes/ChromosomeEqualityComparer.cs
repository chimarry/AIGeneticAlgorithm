using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GeneticAlgorithm.Chromosomes
{
    public class ChromosomeEqualityComparer : IEqualityComparer<Chromosome>
    {
        public bool Equals([AllowNull] Chromosome x, [AllowNull] Chromosome y)
             => x != null && y != null && x.GetHashCode() == y.GetHashCode();

        public int GetHashCode([DisallowNull] Chromosome obj)
             => obj.XGenes.GetHashCode() + obj.YGenes.GetHashCode();
    }
}
