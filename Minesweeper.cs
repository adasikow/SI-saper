using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace saper
{
    class Minesweeper
    {
        private int x;
        private int y;
        private const int MAP_SIZE = 25;
        private UIElement minesweeper;

        public Minesweeper(UIElement uielement)
        {
            this.minesweeper = uielement;
            this.SetX(0);
            this.SetY(0);
        }

        public Minesweeper(UIElement uielement, int x, int y)
        {
            this.minesweeper = uielement;
            this.SetX(x);
            this.SetY(y);
        }

        private void SetLocation(int x, int y)
        {
            this.SetX(x);
            this.SetY(y);
            this.UpdateLocation();
        }

        private void UpdateLocation()
        {
            Grid.SetColumn(this.minesweeper, this.GetX());
            Grid.SetRow(this.minesweeper, this.GetY());
        }

        private void SetX(int x)
        {
            if (x < 0)
                this.x = 0;
            else if (x >= MAP_SIZE)
                this.x = MAP_SIZE - 1;
            else
                this.x = x;
        }

        private void SetY(int y)
        {
            if (y < 0)
                this.y = 0;
            else if (y >= MAP_SIZE)
                this.y = MAP_SIZE - 1;
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


        public void MoveUp()
        {
            SetLocation(this.GetX(), this.GetY() - 1);
        }

        public void MoveDown()
        {
            SetLocation(this.GetX(), this.GetY() + 1);
        }

        public void MoveLeft()
        {
            SetLocation(this.GetX() - 1, this.GetY());
        }

        public void MoveRight()
        {
            SetLocation(this.GetX() + 1, this.GetY());
        }
    }
}