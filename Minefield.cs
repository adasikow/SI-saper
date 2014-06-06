using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace saper
{
    class Minefield
    {
        public Field[,] fieldArray { get; private set; }
        public double[,] radiationMap { get; private set; }
        private int minefieldSize;
        private double radiationMax;

        public Minefield(int size)
        {
            this.minefieldSize = size;
            Random rand = new Random();

            this.fieldArray = new Field[minefieldSize, minefieldSize];
            for (int i = 0; i < minefieldSize; ++i)
                for (int j = 0; j < minefieldSize; ++j)
                {
                    int nextRand = (int)(rand.Next() % 101);
                    double type = (double)nextRand / 100;
                    if(type < 0.2)
                        addField(i, j, Frame.FieldType.Scrap);
                    else
                        addField(i, j, Frame.FieldType.Grass);
                }

            this.radiationMap = new double[minefieldSize, minefieldSize];
            Array.Clear(this.radiationMap, 0, this.minefieldSize);
            radiationMax = 0.0;
        }

        private bool isInRange(int x, int y)
        {
            return x >= 0 && y >= 0 && x < this.minefieldSize && y < this.minefieldSize;
        }

        private void updateRadiationAreaAt(int x, int y)
        {
            for(int i = x - 2; i <= x + 2; ++i)
                for (int j = y - 2; j <= y + 2; ++j)
                {
                    if(isInRange(i, j))
                    {
                        if (Math.Abs(i - x) == 1 && Math.Abs(j - y) == 1) //pole na przekątnej
                            increaseRadiation(i, j, 0.25 * (1 - fieldArray[x, y].mineDepth));
                        else if (i == x && j == y) //pole x, y
                            increaseRadiation(i, j, 1.0 * (1 - fieldArray[x, y].mineDepth));
                        else if ((Math.Abs(i - x) == 0 && Math.Abs(j - y) == 2) || (Math.Abs(i - x) == 2 && Math.Abs(j - y) == 0)) //pole odległe o 2
                            increaseRadiation(i, j, 0.25 * (1 - fieldArray[x, y].mineDepth));
                        else if ((Math.Abs(i - x) == 0 && Math.Abs(j - y) == 1) || (Math.Abs(i - x) == 1 && Math.Abs(j - y) == 0)) //pole odległe o jeden
                            increaseRadiation(i, j, 0.5 * (1 - fieldArray[x, y].mineDepth));
                    }
                }
        }


        private void increaseRadiation(int x, int y, double radiation)
        {
            radiationMap[x, y] += radiation;
            if (radiationMap[x, y] > radiationMax)
                radiationMax = radiationMap[x, y];
        }

        public void disarmAt(int x, int y)
        {
            if (x < minefieldSize && x >= 0 && y < minefieldSize && y >= 0)
            {
                this.fieldArray[x, y].disarmExplosive();
                //updateRadiationAreaAt(x, y);
            }
        }

        public void placeMineAt(int x, int y, double depth, Explosive explosive)
        {
            if (x < minefieldSize && x >= 0 && y < minefieldSize && y >= 0)
            {
                this.fieldArray[x, y].placeExplosive(explosive, depth);
                updateRadiationAreaAt(x, y);
            }
        }

        public void addField(int x, int y, Frame.FieldType type)
        {
            if (!(x < 0 || x >= minefieldSize || y < 0 || y >= minefieldSize || fieldArray[x, y] != null))
                fieldArray[x, y] = new Field(type);
        }

        public Frame.Minefield generateInitialMinefieldFrame()
        {
            Frame.Minefield minefieldFrame = new Frame.Minefield(this.minefieldSize);

            for(int i = 0; i < this.minefieldSize; ++i)
                for (int j = 0; j < this.minefieldSize; ++j)
                {
                    Frame.Field fieldFrame = new Frame.Field();
                    fieldFrame.type = fieldArray[i, j].type;

                    if (radiationMax != 0.0)
                        fieldFrame.radiation = radiationMap[i, j] / radiationMax;
                    else
                        fieldFrame.radiation = radiationMap[i, j];

                    minefieldFrame.fields[i, j] = fieldFrame;
                }

            return minefieldFrame;
        }

        public Frame.Minefield generateFakeFrame()
        {
            Frame.Minefield minefieldFrame = new Frame.Minefield(this.minefieldSize);

            for (int i = 0; i < this.minefieldSize; ++i)
                for (int j = 0; j < this.minefieldSize; ++j)
                {
                    Frame.Field fieldFrame = new Frame.Field();

                    if (fieldArray[i, j].explosive == null)
                        fieldFrame.radiation = 0;
                    else
                        fieldFrame.radiation = 1;

                    minefieldFrame.fields[i, j] = fieldFrame;
                }

            return minefieldFrame;
        }

        public List<Point> GetExplosivesLocations()
        {
            List<Point> result = new List<Point>();
            for(int i = 0; i < minefieldSize; ++i)
                for (int j = 0; j < minefieldSize; ++j)
                {
                    if (fieldArray[i, j].explosive != null)
                    {
                        Point explosiveLocation = new Point(i, j);
                        result.Add(explosiveLocation);
                    }
                }
            return result;
        }
    }
}
