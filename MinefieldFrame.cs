using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class MinefieldFrame
    {
        public int size { get; set; }
        public FieldFrame[,] fields;
        
        public MinefieldFrame(int size)
        {
            this.size = size;
            fields = new FieldFrame[this.size, this.size];
        }
    }
}
