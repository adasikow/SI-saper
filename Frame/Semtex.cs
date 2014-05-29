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
            this.material = Material.Fabric;
            this.hardness = Hardness.Plastic;
            this.hasLight = false;
            this.makesSound = false;
        }
    }
}
