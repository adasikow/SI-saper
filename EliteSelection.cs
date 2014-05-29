using System;
using System.Collections.Generic;
using System.Linq;

namespace saper
{
    class EliteSelection : IParentsSelectionStrategy
    {
        public List<Chromosome> SelectParents(List<Chromosome> candidates)
        {
            int numberOfCandidates = candidates.Count;
            List<Chromosome> parents = candidates.OrderByDescending(x=>x.CountActiveGenes()).ToList();
            
            return parents;
        }
    }
}
