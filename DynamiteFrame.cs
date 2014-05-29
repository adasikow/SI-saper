using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class DynamiteFrame : ExplosiveFrame
    {
        public DynamiteFrame()
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
