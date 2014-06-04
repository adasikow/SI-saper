using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace saper
{
    class MinePositionsGenerator
    {   
        public List<Point> GenerateMinePositions(ushort nMines, Minefield minefield)
        {
            List<Point> positionsOfMines = new List<Point>(nMines);
            Random rand = new Random();

            for (ushort i = 0; i < nMines; ++i)
            {
                Point newPoint = new Point();
                ushort x = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                ushort y = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                double depth = ((double) (rand.Next() % 101)) / 100;
                while (minefield.fieldArray[x, y].explosive != null || minefield.fieldArray[x, y].type == Frame.FieldType.Scrap)
                {
                    x = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                    y = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                }
                //minefield.placeMineAt(x, y, depth, new Explosive());
                newPoint.X = x;
                newPoint.Y = y;
                positionsOfMines.Add(newPoint);
            }

            return positionsOfMines;
        }
    }
}
