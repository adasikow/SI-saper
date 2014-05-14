using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace saper
{
    class MinePositionsGenerator
    {   
        public Point[] GenerateMinePositions(ushort nMines)
        {
            Point[] positionsOfMines = new Point[nMines];
            Random rand = new Random();

            for (ushort i = 0; i < nMines; ++i)
            {
                positionsOfMines[i].X = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                positionsOfMines[i].Y = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
            }

            return positionsOfMines;
        }
    }
}
