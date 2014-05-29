using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class SemtexFrame : ExplosiveFrame
    {
        public SemtexFrame()
        {
            this.material = Material.Fabric;
            this.hardness = Hardness.Plastic;
            this.hasLight = false;
            this.makesSound = false;
        }
    }
}
