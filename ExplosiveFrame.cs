using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class ExplosiveFrame
    {
        public string shape { get; private set; }
        public string colour { get; private set; }
        public float weight { get; private set; }
        public bool makesSound { get; private set; }
        public bool hasLight { get; private set; }

        private List<ExplosiveFrame> connectedExplosives;

        public ExplosiveFrame(string shape, string colour, float weight, bool makesSound, bool hasLight)
        {
            this.shape = shape;
            this.colour = colour;
            this.weight = weight;
            this.makesSound = makesSound;
            this.hasLight = hasLight;
            this.connectedExplosives = new List<ExplosiveFrame>();
        }

        public void addConnectedExplosive(ExplosiveFrame explosive)
        {
            if (!connectedExplosives.Contains(explosive))
            {
                connectedExplosives.Add(explosive);
                explosive.addConnectedExplosive(this);
            }
        }

        public void removeConnected(ExplosiveFrame explosive)
        {
            if (connectedExplosives.Remove(explosive))
                explosive.removeConnected(this);
        }
    }
}
