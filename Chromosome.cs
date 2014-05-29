using System;
using System.Collections.Generic;

namespace saper
{
    class Chromosome
    {
        private List< KeyValuePair<String, bool> > genes;
        
        public Chromosome(int nrOfGenes)
        {
            genes = new List< KeyValuePair<String, bool> >(nrOfGenes);
        }
        
        public Chromosome(List< KeyValuePair<String, bool> > genes)
        {
            this.genes = genes;
        }
        
        public List< KeyValuePair<String, bool> > GetGenes()
        {
            return genes;
        }
        
        public void SetGenes(List< KeyValuePair<String, bool> > genes)
        {
            this.genes = genes;
        }
        
        public short CountActiveGenes()
        {
            short counter = 0;
            
            foreach(KeyValuePair<String, bool> gene in genes)
                if(gene.Value)
                    ++counter;
                    
            return counter;
        }
    }
}
