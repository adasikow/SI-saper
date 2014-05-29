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
            this.makesSound = true;
            this.hasLight = true;
            this.hardness = Hardness.Hard;
            this.colour = Colour.Red;
        }
    }
}
