using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    public class ChromosomeLengthException : Exception
    {
        public ChromosomeLengthException()
        {
        }

        public ChromosomeLengthException(string message)
            : base(message)
        {
        }

        public ChromosomeLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
