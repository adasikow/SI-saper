using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Diagnostics;

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
        private Image upImage;
        private Image downImage;
        private Image leftImage;
        private Image rightImage;
        public Frame.Minefield minefield { get; set; }
        private Queue<Action> path;
        private int code;

        public Minesweeper(int x, int y, Chromosome chromosome)
        {
            this.minefieldSize = Settings.MAP_SIZE;
            this.SetX(x);
            this.SetY(y);
            this.facingDirection = Directions.Down;
            this.image = new Image();
            this.upImage = new Image();
            this.downImage = new Image();
            this.leftImage = new Image();
            this.rightImage = new Image();
            this.image.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/saper.png"));
            this.upImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/up.png"));
            this.downImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/down.png"));
            this.leftImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/left.png"));
            this.rightImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/right.png"));
            this.chromosome = chromosome;
        }

        public Minesweeper(Chromosome chromosome) : this(0, 0, chromosome) { }

        public Image GetDirectionImage()
        {
            if (this.facingDirection == Directions.Up)
                return this.upImage;
            else if (this.facingDirection == Directions.Down)
                return this.downImage;
            else if (this.facingDirection == Directions.Left)
                return this.leftImage;
            else //if (this.facingDirection == Directions.Right)
                return this.rightImage;
        }

        public int NextMove()
        {
            code = 0;
            if (path != null && path.Count > 0)
                this.path.Dequeue()();

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


        // ********************************************************* UCZENIE SYMBOLICZNE *******************************************************************************************
        public bool recognizeBouncingBetty(Frame.Shape? shape, Frame.Colour? colour, Frame.Material? material,
            Frame.Hardness? hardness, Frame.Weight? weight, Frame.Size? size, bool hasLight, bool makesSound)
        {
            Frame.BouncingBetty bouncingbetty = new Frame.BouncingBetty();

            if (colour == bouncingbetty.colour || colour == null)
            {
                if (material == bouncingbetty.material || material == null)
                {
                    if (weight == bouncingbetty.weight || weight == null)
                    {
                        if (size == bouncingbetty.size || size == null)
                        {
                            if (hasLight == bouncingbetty.hasLight || hasLight == null)
                            {
                                if (makesSound == bouncingbetty.makesSound || makesSound == null)
                                {
                                    return true;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public bool recognizeC4(Frame.Shape? shape, Frame.Colour? colour, Frame.Material? material,
            Frame.Hardness? hardness, Frame.Weight? weight, Frame.Size? size, bool hasLight, bool makesSound)
        {
            Frame.C4 c4 = new Frame.C4();

            if (shape == c4.shape || shape == null)
            {
                if (material == c4.material || material == null)
                {
                    if (hardness == c4.hardness || hardness == null)
                    {
                        if (weight == c4.weight || weight == null)
                        {
                            if (hasLight == c4.hasLight || hasLight == null)
                            {
                                if (makesSound == c4.makesSound || makesSound == null)
                                {
                                    return true;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public bool recognizeClaymore(Frame.Shape? shape, Frame.Colour? colour, Frame.Material? material,
            Frame.Hardness? hardness, Frame.Weight? weight, Frame.Size? size, bool hasLight, bool makesSound)
        {
            Frame.Claymore claymore = new Frame.Claymore();

            if (shape == claymore.shape || shape == null)
            {
                if (material == claymore.material || material == null)
                {
                    if (hardness == claymore.hardness || hardness == null)
                    {
                        if (size == claymore.size || size == null)
                        {
                            if (hasLight == claymore.hasLight || hasLight == null)
                            {
                                if (makesSound == claymore.makesSound || makesSound == null)
                                {
                                    return true;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public bool recognizeDynamite(Frame.Shape? shape, Frame.Colour? colour, Frame.Material? material,
            Frame.Hardness? hardness, Frame.Weight? weight, Frame.Size? size, bool hasLight, bool makesSound)
        {
            Frame.Dynamite dynamite = new Frame.Dynamite();

            if (shape == dynamite.shape || shape == null)
            {
                if (material == dynamite.material || material == null)
                {
                    if (hardness == dynamite.hardness || hardness == null)
                    {
                        if (weight == dynamite.weight || weight == null)
                        {
                            if (size == dynamite.size || size == null)
                            {
                                if (hasLight == dynamite.hasLight || hasLight == null)
                                {
                                    return true;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public bool recognizeSemtex(Frame.Shape? shape, Frame.Colour? colour, Frame.Material? material,
            Frame.Hardness? hardness, Frame.Weight? weight, Frame.Size? size, bool hasLight, bool makesSound)
        {
            Frame.Semtex semtex = new Frame.Semtex();

            if (shape == semtex.shape || shape == null)
            {
                if (colour == semtex.colour || colour == null)
                {
                    if (material == semtex.material || material == null)
                    {
                        if (weight == semtex.weight || weight == null)
                        {
                            if (hasLight == semtex.hasLight || hasLight == null)
                            {
                                if (makesSound == semtex.makesSound || makesSound == null)
                                {
                                    return true;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public void Recognize(Frame.Explosive explosive)
        {
            Frame.BouncingBetty bouncingbetty = new Frame.BouncingBetty();
            Frame.C4 c4 = new Frame.C4();
            Frame.Claymore claymore = new Frame.Claymore();
            Frame.Dynamite dynamite = new Frame.Dynamite();
            Frame.Semtex semtex = new Frame.Semtex();
            Frame.Explosive unknownExplosive = new Frame.Explosive();

            bool isBouncingBetty, isC4, isClaymore, isDynamite, isSemtex,
                recBouncingBetty = false, recC4 = false, recClaymore = false, recDynamite = false, recSemtex = false;

            if (chromosome.GetGenes() != null)
            {
                foreach (KeyValuePair<String, bool> gene in chromosome.GetGenes())
                {
                    if (gene.Key == "recBouncingBetty" && gene.Value) recBouncingBetty = true;
                    if (gene.Key == "recC4" && gene.Value) recC4 = true;
                    if (gene.Key == "recClaymore" && gene.Value) recClaymore = true;
                    if (gene.Key == "recDynamite" && gene.Value) recDynamite = true;
                    if (gene.Key == "recSemtex" && gene.Value) recSemtex = true;
                }
            }

            //TODO: Zaimplementować logikę: za każdym razem, 
            //      przy przeszukiwaniu pola jeden parametr ładunku (losowo) 
            //      będzie zakrywany (będzie przyjmował wartość null)

            isBouncingBetty = recognizeBouncingBetty(explosive.shape,
                explosive.colour, explosive.material,
                explosive.hardness, explosive.weight,
                explosive.size, explosive.hasLight,
                explosive.makesSound);
            if (isBouncingBetty && recBouncingBetty)
            {
                minefield.fields[GetX(), GetY()].explosive = bouncingbetty;
                return;
            }

            isC4 = recognizeC4(explosive.shape,
                explosive.colour, explosive.material,
                explosive.hardness, explosive.weight,
                explosive.size, explosive.hasLight,
                explosive.makesSound);
            if (isC4 && recC4)
            {
                minefield.fields[GetX(), GetY()].explosive = c4;
                return;
            }

            isClaymore = recognizeClaymore(explosive.shape,
                explosive.colour, explosive.material,
                explosive.hardness, explosive.weight,
                explosive.size, explosive.hasLight,
                explosive.makesSound);
            if (isClaymore && recClaymore)
            {
                minefield.fields[GetX(), GetY()].explosive = claymore;
                return;
            }

            isDynamite = recognizeDynamite(explosive.shape,
                explosive.colour, explosive.material,
                explosive.hardness, explosive.weight,
                explosive.size, explosive.hasLight,
                explosive.makesSound);
            if (isDynamite && recDynamite)
            {
                minefield.fields[GetX(), GetY()].explosive = dynamite;
                return;
            }

            isSemtex = recognizeSemtex(explosive.shape,
                explosive.colour, explosive.material,
                explosive.hardness, explosive.weight,
                explosive.size, explosive.hasLight,
                explosive.makesSound);
            if (isSemtex && recSemtex)
            {
                minefield.fields[GetX(), GetY()].explosive = semtex;
                return;
            }

            minefield.fields[GetX(), GetY()].explosive = unknownExplosive;
            return;
        }

        // ********************************************************* KONIEC UCZENIA SYMOBILCZNEGO *******************************************************************************************

        public void RecognizeRequest()
        {
            code = 2;
        }

        private bool canDo(string skill)
        {
            foreach(KeyValuePair<String, Boolean> pair in chromosome.GetGenes())
                if(pair.Key.Equals(skill) && pair.Value)
                    return true;

            return false;
        }

        private Type getExplosiveType()
        {
            return minefield.fields[GetX(), GetY()].explosive.GetType();
        }

        public void Disarm()
        {
            if (getExplosiveType().Equals(typeof(Frame.BouncingBetty)) && canDo("disBouncingBetty"))
                code = 1;
            else if (getExplosiveType().Equals(typeof(Frame.C4)) && canDo("disC4"))
                code = 1;
            else if (getExplosiveType().Equals(typeof(Frame.Claymore)) && canDo("disClaymore"))
                code = 1;
            else if (getExplosiveType().Equals(typeof(Frame.Dynamite)) && canDo("disDynamite"))
                code = 1;
            else if (getExplosiveType().Equals(typeof(Frame.Semtex)) && canDo("disSemtex"))
                code = 1;
            else if (getExplosiveType().Equals(typeof(Frame.Misfire)) && canDo("disMisfire"))
                code = 1;
            else
                code = 0;
            
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

        private void WriteToFile(string filename)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < minefield.size; ++i)
            {
                string line = "";
                for (int j = 0; j < minefield.size; ++j)
                {
                    line += minefield.fields[j, i].radiation.ToString() + " ";
                }
                lines.Add(line);
            }
            System.IO.File.WriteAllLines(@filename + ".txt", lines);
        }

        private List<Point> ReadFromFile(string filename)
        {
            List<Point> result = new List<Point>();
            string[] lines = System.IO.File.ReadAllLines(@filename + ".txt");
            foreach(string line in lines)
            {
                string[] data = line.Split();
                int x = Convert.ToInt32(data[1]);
                int y = Convert.ToInt32(data[0]);
                Point point = new Point(x, y);
                result.Add(point);
            }
            return result;

        }

        private void ResetMinefieldFrame()
        {
            for(int i = 0; i < minefieldSize; ++i)
                for (int j = 0; j < minefieldSize; ++j)
                {
                    minefield.fields[i, j].explosive = null;
                }
        }

        private void RunNeuralNetwork()
        {
            ResetMinefieldFrame();

            WriteToFile("input");

            Process p = new Process();
            p.StartInfo.FileName = "C:\\Python27\\python.exe";
            p.StartInfo.Arguments = "trustMe.py";
            p.Start();
            p.WaitForExit();

            List<Point> locations = ReadFromFile("output");
            foreach(Point point in locations)
            {
                int x = Convert.ToInt32(point.X);
                int y = Convert.ToInt32(point.Y);
                minefield.fields[x, y].explosive = new Frame.Explosive();
            }
        }

        public void Search()
        {
            RunNeuralNetwork();

            this.path = new Queue<Action>();
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
                    path.Enqueue(singlePath.Pop());
                path.Enqueue(RecognizeRequest);
                path.Enqueue(Disarm);
                explosives.RemoveAt(i);
            }

        }
    }
}