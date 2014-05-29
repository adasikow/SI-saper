using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Settings
    {
        public static ushort MAP_SIZE = 25;   // grid has size: MAP_SIZE x MAP_SIZE
        public const ushort MAP_MARGIN = 2;  // MAP_MARGIN is number of safety lines (without mines) of fields at grid border
        public const ushort NR_OF_MINES = 5;

        private Settings()
        {

        }
    }
}
