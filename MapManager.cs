using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using saper.Properties;
using System.Windows.Resources;
using System.Windows.Media;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace saper
{
    class MapManager
    {

        //private Mine mine;

        public Point[] minePositions { get; private set; }

        public MapManager()
        {
        }

        public void DrawMap(Grid pole)
        {
            MinePositionsGenerator mpg = new MinePositionsGenerator();
            minePositions = mpg.GenerateMinePositions(Settings.NR_OF_MINES);

            for (ushort i = 0; i < Settings.NR_OF_MINES; ++i)
            {
                Mine newMine = new Mine();
                Grid.SetColumn(newMine.mineImage, (int)minePositions[i].X);
                Grid.SetRow(newMine.mineImage, (int)minePositions[i].Y);
                pole.Children.Add(newMine.mineImage);
            }
        }
    }
}
