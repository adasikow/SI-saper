using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace saper
{
    class MinePositionsGenerator
    {   
        public void GenerateMinePositions(ushort nMines, Minefield minefield)
        {
            Random rand = new Random();

            for (ushort i = 0; i < nMines; ++i)
            {
                ushort x = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                ushort y = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                double depth = ((double) (rand.Next() % 101)) / 100;
                while (!(minefield.fieldArray[x, y].explosive == null && minefield.fieldArray[x, y].type == Frame.FieldType.Grass))
                {
                    x = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                    y = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                }
                minefield.placeMineAt(x, y, depth, new Explosive());
            }
        }
    }
}
