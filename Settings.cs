using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Settings
    {
        public const ushort MAP_SIZE = 15;   // grid has size: MAP_SIZE x MAP_SIZE
        public const ushort MAP_MARGIN = 2;  // MAP_MARGIN is number of safety lines (without mines) of fields at grid border
        public static ushort NR_OF_MINES = 5;

        private Settings()
        {

        }
    }
}
