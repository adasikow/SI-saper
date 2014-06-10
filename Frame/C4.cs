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
            this.shape = Shape.Sphere;
            this.material = Material.Fabric;
            this.hardness = Hardness.Soft;
            this.weight = Weight.Heavy;
            this.hasLight = true;
            this.makesSound = false;
        }
    }
}
