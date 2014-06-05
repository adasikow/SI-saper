using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace saper
{
    class Explosive
    {
        public Frame.Shape shape { get; private set; }
        public Frame.Colour colour { get; private set; }
        public Frame.Material material { get; private set; }
        public Frame.Hardness hardness { get; private set; }
        public float weight { get; set; }
        public bool makesSound { get; set; }
        public bool hasLight { get; set; }
        public Image image { get; protected set; }
        public Explosive()
        {
            image = new Image();
            image.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/explosive.jpg"));
        }
    }
}
