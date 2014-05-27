using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public enum FieldType { Grass, Scrap }

    public class Field
    {
        public FieldType type { get; set; }
        public Explosive explosive { get; set; }
        public double radiation { get; set; }

        public Field()
        {
            this.explosive = null;
        }

    }
}
