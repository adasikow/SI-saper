using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public class Semtex : Explosive
    {
        public Semtex()
        {
            this.shape = Shape.Cuboid;
            this.colour = Colour.Brown;
            this.material = Material.Polymer;
            this.weight = Weight.Medium;
            this.hasLight = false;
            this.makesSound = false;
        }
    }
}
