using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Minefield
    {
        private Field[,] fieldArray;
        public double[,] radiationMap { get; private set; }
        private int minefieldSize;

        public Minefield(int size)
        {
            this.minefieldSize = size;
            this.fieldArray = new Field[minefieldSize, minefieldSize];
            this.radiationMap = new double[minefieldSize, minefieldSize];
            Array.Clear(this.radiationMap, 0, this.minefieldSize);
        }

        private void updateRadiationAreaAt(int x, int y)
        {
            for(int row = (y - 1 < 0 ? 0 : y - 1); row <= y + 1; ++row)
                for (int column = (x - 1 < 0 ? 0 : x - 1); column <= x + 1; ++column)
                {
                    if (Math.Abs(column - x) == 1 || Math.Abs(row - y) == 1) //pole na przekątnej
                        radiationMap[column, row] += 0.25 * fieldArray[column, row].mineDepth;
                    else if (column == x && row == y) //pole x, y
                        radiationMap[column, row] += 1.0 * fieldArray[column, row].mineDepth;
                    else if (Math.Abs(column - x) == 2 || Math.Abs(row - y) == 2) //pole odległe o 2
                        radiationMap[column, row] += 0.25 * fieldArray[column, row].mineDepth;
                    else //pole odległe o jeden
                        radiationMap[column, row] += 0.5 * fieldArray[column, row].mineDepth;
                }
        }

        public void disarmAt(int x, int y)
        {
            if (x < minefieldSize && x >= 0 && y < minefieldSize && y >= 0)
            {
                this.fieldArray[x, y].disarm();
                updateRadiationAreaAt(x, y);
            }
        }

        public void placeMineAt(int x, int y, float depth)
        {
            if (x < minefieldSize && x >= 0 && y < minefieldSize && y >= 0)
            {
                this.fieldArray[x, y].placeMine(depth);
                updateRadiationAreaAt(x, y);
            }
        }

        public void addField(int x, int y, FieldType type)
        {
            if (!(x < 0 || x >= minefieldSize || y < 0 || y >= minefieldSize || fieldArray[x, y] == null))
                fieldArray[x, y] = new Field(type);
        }


    }
}
