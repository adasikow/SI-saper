using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Field
    {
        private const double MAX_DEPTH = 0.5;
        public bool hasMine { get; private set; }
        public double mineDepth { get; private set; }
        public Frame.FieldType type { get; private set; }

        public Field(Frame.FieldType fieldType)
        {
            this.hasMine = false;
            this.mineDepth = 0.0;
            this.type = fieldType;
        }

        public void placeMine(double depth)
        {
            this.hasMine = true;
            if (depth < 0.0)
                this.mineDepth = 0.0;
            else if (depth > MAX_DEPTH)
                this.mineDepth = MAX_DEPTH;
            else
                this.mineDepth = depth;
        }

        public void placeMine()
        {
            this.hasMine = true;
            this.mineDepth = 0.0;
        }

        public void disarm()
        {
            this.hasMine = false;
            this.mineDepth = 0.0;
        }


    }
}
