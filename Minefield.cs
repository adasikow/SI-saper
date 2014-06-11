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

        private double radiationMax;

        public Minefield()
        {
            Random rand = new Random();

            this.fieldArray = new Field[Settings.MAP_SIZE, Settings.MAP_SIZE];
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
                {
                    int nextRand = (int)(rand.Next() % 101);
                    double type = (double)nextRand / 100;
                    if(type < 0.2)
                        addField(i, j, Frame.FieldType.Scrap);
                    else
                        addField(i, j, Frame.FieldType.Grass);
                }

            generateMinePositions();

        }

        private void generateMinePositions()
        {
            Random rand = new Random();

            for (ushort i = 0; i < Settings.NR_OF_MINES; ++i)
            {
                ushort x = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                ushort y = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                double[] depths = { 0.0, 0.2, 0.4 };
                double depth = depths[rand.Next() % 3];
                while (!(fieldArray[x, y].explosive == null && fieldArray[x, y].type == Frame.FieldType.Grass))
                {
                    x = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                    y = (ushort)rand.Next(Settings.MAP_MARGIN, Settings.MAP_SIZE - Settings.MAP_MARGIN);
                }
                placeMineAt(x, y, depth, new Explosive(rand));
            }
        }

        private bool isInRange(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Settings.MAP_SIZE && y < Settings.MAP_SIZE;
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
            if (x < Settings.MAP_SIZE && x >= 0 && y < Settings.MAP_SIZE && y >= 0)
            {
                this.fieldArray[x, y].disarmExplosive();
            }
        }

        public void placeMineAt(int x, int y, double depth, Explosive explosive)
        {
            if (x < Settings.MAP_SIZE && x >= 0 && y < Settings.MAP_SIZE && y >= 0)
            {
                this.fieldArray[x, y].placeExplosive(explosive, depth);
            }
        }

        public void addField(int x, int y, Frame.FieldType type)
        {
            if (!(x < 0 || x >= Settings.MAP_SIZE || y < 0 || y >= Settings.MAP_SIZE || fieldArray[x, y] != null))
                fieldArray[x, y] = new Field(type);
        }

        public void WriteToFile(string filename)
        {
            Frame.Minefield minefieldFrame = generateFakeFrame();
            List<string> lines = new List<string>();
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
            {
                string line = "";
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
                {
                    line += minefieldFrame.fields[j, i].radiation.ToString() + " ";
                }
                lines.Add(line);
            }
            System.IO.File.WriteAllLines(@filename + ".txt", lines);
        }

        private void generateRadiationMap()
        {
            this.radiationMap = new double[Settings.MAP_SIZE, Settings.MAP_SIZE];
            Array.Clear(this.radiationMap, 0, Settings.MAP_SIZE);
            radiationMax = 0.0;
            for(int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
                {
                    if(fieldArray[i, j].explosive != null)
                        updateRadiationAreaAt(i, j);
                }
        }

        public Frame.Minefield generateMinefieldFrame()
        {
            Frame.Minefield minefieldFrame = new Frame.Minefield(Settings.MAP_SIZE);
            generateRadiationMap();
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
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

        
        private Frame.Minefield generateFakeFrame()
        {
            Frame.Minefield minefieldFrame = new Frame.Minefield(Settings.MAP_SIZE);

            for (int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
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
            for(int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
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
