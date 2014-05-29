using System;
using System.Collections.Generic;

namespace saper
{
    class RouletteSelection : IParentsSelectionStrategy
    {
        public List<Chromosome> SelectParents(List<Chromosome> candidates)
        {
            int numberOfCandidates = candidates.Count;
            List<Chromosome> parents = new List<Chromosome>();
            
            Random rand = new Random();
            
            for(int i=0; i<numberOfCandidates; ++i)
            {
                int index = rand.Next() % (numberOfCandidates-i);

                parents.Add(candidates[index]);
                candidates.RemoveAt(index);
            }
            
            return parents;
        }
    }
}
