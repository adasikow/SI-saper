using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class BouncingBettyFrame : ExplosiveFrame
    {
        public BouncingBettyFrame()
        {
            this.shape = Shape.Cylinder;
            this.material = Material.Metal;
            this.hardness = Hardness.Hard;
            this.hasLight = false;
            this.makesSound = false;
        }
    }
}
