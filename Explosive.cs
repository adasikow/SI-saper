using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Explosive
    {
        public Frame.Shape shape { get; private set; }
        public Frame.Colour colour { get; private set; }
        public Frame.Material material { get; private set; }
        public Frame.Hardness hardness { get; private set; }
        public float weight { get; set; }
        public bool makesSound { get; set; }
        public bool hasLight { get; set; }
    }
}
