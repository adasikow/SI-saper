using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public class Claymore : Explosive
    {
        public Claymore()
        {
            this.shape = Shape.Cuboid;
            this.material = Material.Metal;
            this.hardness = Hardness.Hard;
            this.hasLight = false;
            this.makesSound = false;
        }
    }
}
