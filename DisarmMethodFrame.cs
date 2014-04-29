using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class DisarmMethodFrame
    {
        public ExplosiveFrame explosive { get; private set; }
        public string firstCable { get; private set; }
        public string secondCable { get; private set; }
        public string thirdCable { get; private set; }
        public string fourthCable { get; private set; }

        public DisarmMethodFrame(ExplosiveFrame explosive, string firstCable, string secondCable, string thirdCable, string fourthCable)
        {
            this.explosive = explosive;
            this.firstCable = firstCable;
            this.secondCable = secondCable;
            this.thirdCable = thirdCable;
            this.fourthCable = fourthCable;
        }

    }
}
