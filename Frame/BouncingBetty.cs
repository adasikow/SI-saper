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
            this.colour = Colour.Brown;
            this.material = Material.Metal;
            this.weight = Weight.Light;
            this.size = Size.Small;
            this.hasLight = false;
            this.makesSound = false;
        }
    }
}
