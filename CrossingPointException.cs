using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    public class CrossingPointException: Exception
    {
        public CrossingPointException()
        {
        }

        public CrossingPointException(string message)
            : base(message)
        {
        }

        public CrossingPointException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
