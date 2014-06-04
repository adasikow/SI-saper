using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;

namespace saper
{
    public enum Directions { Up, Right, Down, Left };

    class Minesweeper
    {
        public Directions facingDirection { get; private set; }
        private int x;
        private int y;
        private int minefieldSize;
        private Chromosome chromosome { get; set; }
        public Image minesweeperImage { get; private set; }
        private Frame.Minefield minefield;
        private List<State> path;
        private int pathIterator;

        public event EventHandler<MinesweeperEventArgs> LocationUpdate;

        //private List<DisarmMethodFrame> disarmMethodsKnowledge;
        //private List<ExplosiveFrame> explosivesKnowledge;
        //private FieldFrame[,] minefieldKnowledge;

        public Minesweeper(Frame.Minefield initialMinefieldKnowledge)
        {
            this.minefieldSize = Settings.MAP_SIZE;
            this.SetX(0);
            this.SetY(0);
            this.facingDirection = Directions.Up;
            this.minesweeperImage = new Image();
            this.minesweeperImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/saper.png"));
            this.minefield = initialMinefieldKnowledge;

            //this.minefieldKnowledge = new FieldFrame[this.minefieldSize, this.minefieldSize];
            //this.disarmMethodsKnowledge = new List<DisarmMethodFrame>();
            //this.explosivesKnowledge = new List<ExplosiveFrame>();

        }

        public Minesweeper(int x, int y)
        {
            this.minefieldSize = Settings.MAP_SIZE;
            this.SetX(x);
            this.SetY(y);
            this.facingDirection = Directions.Up;
            this.minesweeperImage = new Image();
            this.minesweeperImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/saper.png"));
        }

        private void SetLocation(int x, int y)
        {
            this.SetX(x);
            this.SetY(y);
            this.UpdateLocation();
        }

        public void NextMove()
        {
            if (path != null)if (path.Count > 0 && pathIterator < path.Count)
            {
                if (path.Count > 0 && pathIterator < path.Count)
                {
                    this.SetLocation(path[pathIterator].x, path[pathIterator].y);
                    pathIterator++;
                }
            }
        }

        private void UpdateLocation()
        {
            Grid.SetColumn(this.minesweeperImage, this.GetX());
            Grid.SetRow(this.minesweeperImage, this.GetY());
            //OnLocationUpdate(this.GetX(), this.GetY());
            //Thread.Sleep(100);
        }

        private void OnLocationUpdate(int x, int y)
        {
            var handler = LocationUpdate;
            if (handler != null)
            {
                handler(this, new MinesweeperEventArgs(x, y));
            }
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

        private Boolean visited(List<State> visitedStates, State state)
        {
            for(int i = 0; i < visitedStates.Count; ++i)
                if(isStateEqual(visitedStates[i], state))
                    return true;
            return false;
        }

        private Boolean isStateEqual(State a, State b)
        {
            return a.x == b.x && a.y == b.y && a.facingDirection == b.facingDirection;
        }

        private int calculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private int findNearestExplosive(List<Point> explosives)
        {
            int resultIndex = 0;
            int currentMinDistance = calculateDistance(this.GetX(), this.GetY(), Convert.ToInt32(explosives[resultIndex].X), Convert.ToInt32(explosives[resultIndex].Y));
            for (int i = 1; i < explosives.Count; ++i)
            {
                if (calculateDistance(this.GetX(), this.GetY(), Convert.ToInt32(explosives[i].X), Convert.ToInt32(explosives[i].Y)) < currentMinDistance)
                {
                    resultIndex = i;
                    currentMinDistance = calculateDistance(this.GetX(), this.GetY(), Convert.ToInt32(explosives[i].X), Convert.ToInt32(explosives[i].Y));
                }
            }

            return resultIndex;
        }

        public void Search(List<Point> explosives)
        {
            this.pathIterator = 0;
            this.path = new List<State>();
            while(explosives.Count > 0)
            {
                int i = findNearestExplosive(explosives);
                List<State> visitedStates = new List<State>();
                State startState = new State(this.GetX(), this.GetY(), this.minefieldSize, this.facingDirection);
                State currentState = default(State);
                State finalState = new State(Convert.ToInt32(explosives[i].X), Convert.ToInt32(explosives[i].Y), this.minefieldSize);
                PriorityQueue<int, State> queue = new PriorityQueue<int, State>();
                queue.Enqueue(startState.calculateEstimatedDistance(0, finalState), startState);
                while (!queue.IsEmpty)
                {
                    currentState = queue.DequeueValue();
                    if (visited(visitedStates, currentState))
                        continue;
                    if (currentState.isFinalState(finalState))
                        break;

                    visitedStates.Add(currentState);
                    State newState;

                    newState = currentState.MoveForwardAction();
                    if (minefield.fields[newState.x, newState.y].type == Frame.FieldType.Scrap)
                        queue.Enqueue(newState.calculateEstimatedDistance(25, finalState), newState);
                    else //if(minefield.fields[newState.x, newState.y].type == Frame.FieldType.Grass)
                        queue.Enqueue(newState.calculateEstimatedDistance(5, finalState), newState);

                    newState = currentState.RotateLeftAction();
                    queue.Enqueue(newState.calculateEstimatedDistance(1, finalState), newState);

                    newState = currentState.RotateRightAction();
                    queue.Enqueue(newState.calculateEstimatedDistance(1, finalState), newState);
                }
                this.SetX(currentState.x);
                this.SetY(currentState.y);
                this.facingDirection = currentState.facingDirection;
                Stack<State> singlePath = new Stack<State>();
                while (currentState != null)
                {
                    singlePath.Push(currentState);
                    currentState = currentState.parentState;
                }
                while (singlePath.Count != 0)
                    path.Add(singlePath.Pop());
                explosives.RemoveAt(i);
            }

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

public class MinesweeperEventArgs : EventArgs
{
    public int x { get; private set; }
    public int y { get; private set; }
    public MinesweeperEventArgs(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}