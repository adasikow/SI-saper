using System;
using System.Collections.Generic;

namespace saper
{
    interface IParentsSelectionStrategy
    {
        List<Chromosome> SelectParents(List<Chromosome> candidates);
    }
}
