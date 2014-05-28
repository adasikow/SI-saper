using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Field
    {
        private const double MAX_DEPTH = 0.5;
        public Explosive explosive { get; private set; }
        public double mineDepth { get; private set; }
        public Frame.FieldType type { get; private set; }

        public Field(Frame.FieldType fieldType)
        {
            this.explosive = null;
            this.mineDepth = 0.0;
            this.type = fieldType;
        }

        public void placeExplosive(Explosive explosive, double depth)
        {
            this.explosive = explosive;
            if (depth < 0.0)
                this.mineDepth = 0.0;
            else if (depth > MAX_DEPTH)
                this.mineDepth = MAX_DEPTH;
            else
                this.mineDepth = depth;
        }

        public void disarmExplosive()
        {
            if (explosive != null)
            {
                explosive = null;
            }
        }

    }
}
