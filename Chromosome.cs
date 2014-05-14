using System;
using System.Collections.Generic;

namespace saper
{
    class Chromosome
    {
        private List< KeyValuePair<String, bool> > genes = new List< KeyValuePair<String, bool> >(Settings.NR_OF_GENES);
        
        public Chromosome()
        {
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
        
        public KeyValuePair<Chromosome, Chromosome> CrossOver(Chromosome ParentA, Chromosome ParentB, int crossingPoint)
        {
            List< KeyValuePair<String, bool> > genesA, genesB, genesAB, genesBA;
            genesA = new List< KeyValuePair<String, bool> >(ParentA.GetGenes());
            genesB = new List< KeyValuePair<String, bool> >(ParentB.GetGenes());

            int genesASize = genesA.Count;
            int genesBSize = genesB.Count;
            
            if (genesASize != genesBSize)
                throw new ChromosomeLengthException();
            
            // checking if crossPosition is correnct. 
            // Offspring must take at least one gene from each parent
            if (crossingPoint < 1 || crossingPoint > genesASize - 2 )
                throw new CrossingPointException();
            
            //initilizing
            genesAB = new List< KeyValuePair<String, bool> >(genesB);
            genesBA = new List< KeyValuePair<String, bool> >(genesA);

            for(int i=0; i<crossingPoint; ++i)
            {
                genesAB[i] = genesA[i];
                genesBA[i] = genesB[i];
            }
            
            Chromosome chromosomeAB, chromosomeBA;
            
            chromosomeAB = new Chromosome(genesAB);
            chromosomeBA = new Chromosome(genesBA);
            
            return new KeyValuePair<Chromosome, Chromosome>(chromosomeAB, chromosomeBA);
        }
    }
}
