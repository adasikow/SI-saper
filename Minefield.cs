using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Minefield
    {
        public Field[,] fieldArray { get; private set; }
        public double[,] radiationMap { get; private set; }
        private int minefieldSize;

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
        }

        private bool isInRange(int x, int y)
        {
            return x >= 0 && y >= 0 && x < this.minefieldSize && y < this.minefieldSize;
        }

        private void updateRadiationAreaAt(int x, int y)
        {
            for(int row = y - 1; row <= y + 1; ++row)
                for (int column = x - 1; column <= x + 1; ++column)
                {
                    if(isInRange(column, row))
                    {
                        if (Math.Abs(column - x) == 1 || Math.Abs(row - y) == 1) //pole na przekątnej
                            radiationMap[column, row] += 0.25 * (1 - fieldArray[column, row].mineDepth);
                        else if (column == x && row == y) //pole x, y
                            radiationMap[column, row] += 1.0 * (1 - fieldArray[column, row].mineDepth);
                        else if (Math.Abs(column - x) == 2 || Math.Abs(row - y) == 2) //pole odległe o 2
                            radiationMap[column, row] += 0.25 * (1 - fieldArray[column, row].mineDepth);
                        else //pole odległe o jeden
                            radiationMap[column, row] += 0.5 * (1 - fieldArray[column, row].mineDepth);
                    }
                }
        }

        public void disarmAt(int x, int y)
        {
            if (x < minefieldSize && x >= 0 && y < minefieldSize && y >= 0)
            {
                this.fieldArray[x, y].disarmExplosive();
                updateRadiationAreaAt(x, y);
            }
        }

        public void placeMineAt(int x, int y, float depth, Explosive explosive)
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
                    fieldFrame.radiation = radiationMap[i, j];
                    minefieldFrame.fields[i, j] = fieldFrame;
                }

            return minefieldFrame;
        }


    }
}
