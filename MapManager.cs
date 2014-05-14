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

        public MapManager()
        {
        }

        private UIElement DeepCopy(UIElement element)
       {
           string shapestring = XamlWriter.Save(element);
           StringReader stringReader = new StringReader(shapestring);
           XmlTextReader xmlTextReader = new XmlTextReader(stringReader);
           UIElement DeepCopyobject = (UIElement)XamlReader.Load(xmlTextReader);
           return DeepCopyobject;
       }

        public void DrawMap(Grid pole)
        {
            MinePositionsGenerator mpg = new MinePositionsGenerator();
            Point[] minePositions = mpg.GenerateMinePositions(Settings.NR_OF_MINES);

            for (ushort i = 0; i < Settings.NR_OF_MINES; ++i)
            {
                Mine newMine = new Mine();
              //  UIElement newMine = DeepCopy(mine.mineImage);
                Grid.SetColumn(newMine.mineImage, (int)minePositions[i].X);
                Grid.SetRow(newMine.mineImage, (int)minePositions[i].Y);
                pole.Children.Add(newMine.mineImage);
            }
        }
    }
}
