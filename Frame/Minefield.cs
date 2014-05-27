using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public class Minefield
    {
        public int size { get; set; }
        public Field[,] fields;

        public Minefield(int size)
        {
            this.size = size;
            fields = new Field[this.size, this.size];
        }
    }
}
