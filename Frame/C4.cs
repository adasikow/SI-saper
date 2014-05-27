using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public class C4 : Explosive
    {
        public C4()
        {
            this.material = Material.Fabric;
            this.shape = Shape.Cuboid;
            this.hasLight = true;
            this.hardness = Hardness.Soft;
            this.makesSound = false;
        }
    }
}
