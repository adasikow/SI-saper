using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace saper
{
    public enum Directions { Up, Right, Down, Left };

    class Minesweeper
    {
        public Directions facingDirection { get; private set; }
        private int x;
        private int y;
        private int minefieldSize;
        public Image minesweeperImage { get; private set; }

        private List<DisarmMethodFrame> disarmMethodsKnowledge;
        private List<ExplosiveFrame> explosivesKnowledge;
        private FieldFrame[,] minefieldKnowledge;

        public Minesweeper(int minefieldSize)
        {
            this.minefieldSize = minefieldSize;
            this.SetX(0);
            this.SetY(0);
            this.facingDirection = Directions.Up;
            this.minesweeperImage = new Image();
            this.minesweeperImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/saper.jpg"));

            this.minefieldKnowledge = new FieldFrame[this.minefieldSize, this.minefieldSize];
            this.disarmMethodsKnowledge = new List<DisarmMethodFrame>();
            this.explosivesKnowledge = new List<ExplosiveFrame>();

        }

        public Minesweeper(int x, int y, int minefieldSize)
        {
            this.minefieldSize = minefieldSize;
            this.SetX(x);
            this.SetY(y);
            this.facingDirection = Directions.Up;
            this.minesweeperImage = new Image();
            this.minesweeperImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/saper.jpg"));
        }

        private void SetLocation(int x, int y)
        {
            this.SetX(x);
            this.SetY(y);
            this.UpdateLocation();
        }

        private void UpdateLocation()
        {
            Grid.SetColumn(this.minesweeperImage, this.GetX());
            Grid.SetRow(this.minesweeperImage, this.GetY());
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


        //wyszukiwanie rozwiazania
        //dopoki (liczba_bomb > 0)
        //  wyznacz kolejna bombe do rozbrojenia
        //  utworz stan koncowy
        //  utworz stan obecny
        //  dodaj do kolejki/stosu obecny stan
        //  dopoki stan obecny != stan koncowy
        //      pobierz stan
        //      wygeneruj wszystkie mozliwe nastepne stany
        //      te stany, ktore jeszcze nie zostaly odwiedzone dodaj do kolejki/stosu stanow do przeszukania
        //  odtworz sciezke za pomoca stanow - rodzicow i zapapamietaj
        //zwroc sciezke
    }
}