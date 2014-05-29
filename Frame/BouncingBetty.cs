using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public class BouncingBetty : Explosive
    {
        public BouncingBetty()
        {
            this.shape = Shape.Cylinder;
            this.material = Material.Metal;
            this.hardness = Hardness.Hard;
            this.hasLight = false;
            this.makesSound = false;
        }
    }
}
