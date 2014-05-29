using System;
using System.Collections.Generic;

namespace saper
{
    class Genetics
    {
        Random rand = new Random();

        private List<String> features = new List<String>(new String[] {
            // "rec" means recognition and "dis" means disarm
            "recClaymore", "recS-mine", "recC4", "recSemtex", "recMisfire", "recTNT",
            "recFougasse", "recBoundingMine", "recPicricAcid", "recTrotil", "recHexogen",
            "disClaymore", "disS-mine", "disC4", "disSemtex", "disMisfire", "disTNT",
            "disFougasse", "disBoundingMine", "disPicricAcid", "disTrotil", "disHexogen" });
            
        //private float mutationRate = 0.05;

        public int GetNrOfFeatures()
        {
            return features.Count;
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

        public Chromosome GenerateInitialChromosome()
        {
            List< KeyValuePair<String, bool> > genes = new List< KeyValuePair<String, bool> >();
            
            foreach(String feature in features)
            {
                bool isActive = Convert.ToBoolean(rand.Next() % 3); // mod 3 for higher probability for active gene 
                genes.Add(new KeyValuePair<String, bool>(feature, isActive));
            }
            
            return new Chromosome(genes);
        }
        
        public List<Chromosome> GeneratePopulation(ushort numberOfIndividuals)
        {
            List<Chromosome> population = new List<Chromosome>();
            for(ushort i=0; i<numberOfIndividuals; ++i)
                population.Add(GenerateInitialChromosome());
                
            return population;
        }
        
        public List<Chromosome> GenerateNextGeneration(List<Chromosome> population,
            IParentsSelectionStrategy pss, int mutationRate)
        {   
            int numberOfIndividuals = population.Count;

            List<Chromosome> parents = pss.SelectParents(population);
            List<Chromosome> nextGeneration = new List<Chromosome>();
            Random rand = new Random();

            for (int i = 0; i < parents.Count/2; i += 2)
            {
                KeyValuePair<Chromosome, Chromosome> sibling;

                int crossingPoint = (rand.Next() % (features.Count - 2)) + 1;

                sibling = CrossOver(parents[i], parents[parents.Count/2 - 1 - i], crossingPoint);
                nextGeneration.Add(sibling.Key);
                nextGeneration.Add(sibling.Value);

                crossingPoint = (rand.Next() % (features.Count - 2)) + 1;
                sibling = CrossOver(parents[i], parents[i + 1], crossingPoint);
                nextGeneration.Add(sibling.Key);
                nextGeneration.Add(sibling.Value);
            }

            if (mutationRate > 0)
                nextGeneration = Mutate(nextGeneration, mutationRate);

            return nextGeneration;
        }

        private List<Chromosome> Mutate(List<Chromosome> population, float mutationRate)
        {
            List<Chromosome> mutatedPopulation = new List<Chromosome>();

            foreach (Chromosome individual in population)
            {
                List<KeyValuePair<String, bool>> genes = individual.GetGenes();

                for (int i = 0; i < genes.Count; ++i)
                {
                    int mutationFactor = rand.Next() % 100;

                    if (mutationFactor < mutationRate)
                        genes[i] = new KeyValuePair<String, bool>(genes[i].Key, !genes[i].Value);
                }

                individual.SetGenes(genes);

                mutatedPopulation.Add(individual);
            }

            return mutatedPopulation;
        }
    }
}
