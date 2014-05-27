using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace saper
{
    class Mine
    {
        private int x;
        private int y;
        private Chromosome chromosome;
        public Image mineImage { get; private set; }

        public Mine()
        {
            this.SetX(0);
            this.SetY(0);
            this.mineImage = new Image();
            this.mineImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/mine.jpg"));   
        }

        public Mine(int x, int y)
        {
            this.SetX(x);
            this.SetY(y);
            this.mineImage = new Image();
            this.mineImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/mine.jpg"));
        }

        private void SetLocation(int x, int y)
        {
            this.SetX(x);
            this.SetY(y);
            this.UpdateLocation();
        }

        private void UpdateLocation()
        {
            Grid.SetColumn(this.mineImage, this.GetX());
            Grid.SetRow(this.mineImage, this.GetY());
        }

        private void SetX(int x)
        {
            if (x < 0)
                this.x = 0;
            else if (x >= Settings.MAP_SIZE)
                this.x = Settings.MAP_SIZE - 1;
            else
                this.x = x;
        }

        private void SetY(int y)
        {
            if (y < 0)
                this.y = 0;
            else if (y >= Settings.MAP_SIZE)
                this.y = Settings.MAP_SIZE - 1;
            else
                this.y = y;
        }

        public int GetX()
        {
            return this.x;
        }

        public int GetY()
        {
            return this.y;
        }

    }
}