using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class FieldFrame
    {
        public string fieldType { get; private set; }
        public ExplosiveFrame explosive { get; private set; }
        public bool upBorderConnection { get; private set; }
        public bool downBorderConnection { get; private set; }
        public bool leftBorderConnection { get; private set; }
        public bool rightBorderConnection { get; private set; }

        public FieldFrame(string fieldType)
        {
            this.fieldType = fieldType;
            this.explosive = null;
            this.upBorderConnection = false;
            this.downBorderConnection = false;
            this.leftBorderConnection = false;
            this.rightBorderConnection = false;
        }

        public void addExplosive(ExplosiveFrame explosive)
        {
            this.explosive = explosive;
        }

        public void addConnection(bool up = false, bool right = false, bool down = false, bool left = false)
        {
            this.upBorderConnection = up;
            this.downBorderConnection = down;
            this.leftBorderConnection = left;
            this.rightBorderConnection = right;
        }

    }
}
