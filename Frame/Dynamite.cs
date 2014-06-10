using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public class Dynamite : Explosive
    {
        public Dynamite()
        {
            this.shape = Shape.Cylinder;
            this.material = Material.Plastic;
            this.hardness = Hardness.Medium;
            this.weight = Weight.Medium;
            this.size = Size.Small;
            this.hasLight = true;
        }
    }
}