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
        private Chromosome chromosome { get; set; }
        public Image image { get; private set; }
        private Frame.Minefield minefield;
        private List<Action> path;
        private int pathIterator;
        private int code;

        public Minesweeper(int x, int y, Frame.Minefield initialMinefieldKnowledge)
        {
            this.minefieldSize = Settings.MAP_SIZE;
            this.SetX(x);
            this.SetY(y);
            this.facingDirection = Directions.Down;
            this.image = new Image();
            this.image.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/saper.png"));
            this.minefield = initialMinefieldKnowledge;
        }

        public Minesweeper(Frame.Minefield initialMinefieldKnowledge) : this(0, 0, initialMinefieldKnowledge) { }

        public void AddExplosivesLocations(List<Point> location)
        {
            for (int i = 0; i < location.Count; ++i)
                minefield.fields[Convert.ToInt32(location[i].X), Convert.ToInt32(location[i].Y)].explosive = new Frame.Explosive();
        }

        public int NextMove()
        {
            code = 0;
            if (path != null && path.Count > 0 && pathIterator < path.Count)
                this.path[pathIterator++]();

            return code;
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
            SetX(GetX());
            SetY(GetY() - 1);
        }

        private void MoveDown()
        {
            SetX(GetX());
            SetY(GetY() + 1);
        }

        private void MoveLeft()
        {
            SetX(GetX() - 1);
            SetY(GetY());
        }

        private void MoveRight()
        {
            SetX(GetX() + 1);
            SetY(GetY());
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

        public void Recognize()
        {
            return;
        }

        public void Disarm()
        {
            minefield.fields[GetX(), GetY()].explosive = null;
            code = 1;
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

        private int findNearestExplosive(State startState, List<Point> explosives)
        {
            int resultIndex = 0;
            int currentMinDistance = calculateDistance(startState.x, startState.y, Convert.ToInt32(explosives[resultIndex].X), Convert.ToInt32(explosives[resultIndex].Y));
            for (int i = 1; i < explosives.Count; ++i)
            {
                if (calculateDistance(startState.x, startState.y, Convert.ToInt32(explosives[i].X), Convert.ToInt32(explosives[i].Y)) < currentMinDistance)
                {
                    resultIndex = i;
                    currentMinDistance = calculateDistance(startState.x, startState.y, Convert.ToInt32(explosives[i].X), Convert.ToInt32(explosives[i].Y));
                }
            }

            return resultIndex;
        }

        private List<Point> generateExplosivesLocationsList()
        {
            List<Point> result = new List<Point>();
            for(int i = 0; i < minefieldSize; ++i)
                for (int j = 0; j < minefieldSize; ++j)
                {
                    if (minefield.fields[i, j].explosive != null)
                    {
                        Point explosiveLocation = new Point(i, j);
                        result.Add(explosiveLocation);
                    }
                }
            return result;
        }

        public void Search()
        {
            this.pathIterator = 0;
            this.path = new List<Action>();
            State startState = new State(this.GetX(), this.GetY(), this.minefieldSize, this.facingDirection);
            List<Point> explosives = generateExplosivesLocationsList();
            while(explosives.Count > 0)
            {
                int i = findNearestExplosive(startState, explosives);
                List<State> visitedStates = new List<State>();
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

                    newState = currentState.MoveForwardAction(Move);
                    if (minefield.fields[newState.x, newState.y].type == Frame.FieldType.Scrap)
                        queue.Enqueue(newState.calculateEstimatedDistance(25, finalState), newState);
                    else //if(minefield.fields[newState.x, newState.y].type == Frame.FieldType.Grass)
                        queue.Enqueue(newState.calculateEstimatedDistance(5, finalState), newState);

                    newState = currentState.RotateLeftAction(RotateLeft);
                    queue.Enqueue(newState.calculateEstimatedDistance(1, finalState), newState);

                    newState = currentState.RotateRightAction(RotateRight);
                    queue.Enqueue(newState.calculateEstimatedDistance(1, finalState), newState);
                }
                startState = new State(currentState.x, currentState.y, this.minefieldSize, currentState.facingDirection);
                Stack<Action> singlePath = new Stack<Action>();
                while (currentState.lastActionPerformed != null)
                {
                    singlePath.Push(currentState.lastActionPerformed);
                    currentState = currentState.parentState;
                }
                while (singlePath.Count != 0)
                    path.Add(singlePath.Pop());
                //path.Add(Recognize);
                path.Add(Disarm);
                explosives.RemoveAt(i);
            }

        }
    }
}