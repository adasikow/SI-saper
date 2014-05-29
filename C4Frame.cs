using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class C4Frame : ExplosiveFrame
    {
        public C4Frame()
        {
            this.material = Material.Fabric;
            this.shape = Shape.Cuboid;
            this.hasLight = true;
            this.hardness = Hardness.Soft;
            this.makesSound = false;
        }
    }
}
