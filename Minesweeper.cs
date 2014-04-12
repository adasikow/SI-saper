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
        public enum Directions { Up, Right, Down, Left };
        public Directions facingDirection { get; private set; }
        private int x;
        private int y;
        private int minefieldSize;
        private UIElement minesweeper;

        public Minesweeper(UIElement uielement, int minefieldSize)
        {
            this.minesweeper = uielement;
            this.SetX(0);
            this.SetY(0);
            this.facingDirection = Directions.Up;
            this.minefieldSize = minefieldSize;
        }

        public Minesweeper(UIElement uielement, int x, int y, int minefieldSize)
        {
            this.minesweeper = uielement;
            this.SetX(x);
            this.SetY(y);
            this.facingDirection = Directions.Up;
            this.minefieldSize = minefieldSize;
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
            else if (x >= this.minefieldSize)
                this.x = this.minefieldSize - 1;
            else
                this.x = x;
        }

        private void SetY(int y)
        {
            if (y < 0)
                this.y = 0;
            else if (y >= this.minefieldSize)
                this.y = this.minefieldSize - 1;
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

        private void MoveUp()
        {
            SetLocation(this.GetX(), this.GetY() - 1);
        }

        private void MoveDown()
        {
            SetLocation(this.GetX(), this.GetY() + 1);
        }

        private void MoveLeft()
        {
            SetLocation(this.GetX() - 1, this.GetY());
        }

        private void MoveRight()
        {
            SetLocation(this.GetX() + 1, this.GetY());
        }

        public void RotateLeft()
        {
            if (this.facingDirection == Directions.Up)
                this.facingDirection = Directions.Left;
            else
                this.facingDirection--;
        }

        public void RotateRight()
        {
            if (this.facingDirection == Directions.Left)
                this.facingDirection = Directions.Up;
            else
                this.facingDirection++;
        }

        public void Move()
        {
            if (this.facingDirection == Directions.Up)
                this.MoveUp();
            else if (this.facingDirection == Directions.Down)
                this.MoveDown();
            else if (this.facingDirection == Directions.Left)
                this.MoveLeft();
            else if (this.facingDirection == Directions.Right)
                this.MoveRight();
        }
    }
}