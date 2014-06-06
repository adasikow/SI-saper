using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace saper
{
    /// <summary>
    /// Interaction logic for MinefieldWindow.xaml
    /// </summary>
    public partial class MinefieldWindow : Window
    {
        private Minesweeper minesweeper;
        private Minefield minefield;
        private MinePositionsGenerator mpg;

        public MinefieldWindow()
        {
            InitializeComponent();
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            InitializeGrid();
            Reset();
            Redraw();
        }

        private void drawFieldTypes()
        {
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
                {
                    if (minefield.fieldArray[i, j].type == Frame.FieldType.Scrap)
                    {
                        Grid.SetColumn(minefield.fieldArray[i, j].scrapImage, i);
                        Grid.SetRow(minefield.fieldArray[i, j].scrapImage, j);
                        minefieldGrid.Children.Add(minefield.fieldArray[i, j].scrapImage);
                    }
                }
        }

        private void drawMines()
        {
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
                {
                    if (minefield.fieldArray[i, j].explosive != null)
                    {
                        Grid.SetColumn(minefield.fieldArray[i, j].explosive.image, i);
                        Grid.SetRow(minefield.fieldArray[i, j].explosive.image, j);
                        minefieldGrid.Children.Add(minefield.fieldArray[i, j].explosive.image);
                    }
                }
        }

        private void drawMinesweeper()
        {
            if (minesweeper != null)
            {
                Grid.SetColumn(minesweeper.GetDirectionImage(), minesweeper.GetX());
                Grid.SetRow(minesweeper.GetDirectionImage(), minesweeper.GetY());
                minefieldGrid.Children.Add(minesweeper.GetDirectionImage());

                Grid.SetColumn(minesweeper.image, minesweeper.GetX());
                Grid.SetRow(minesweeper.image, minesweeper.GetY());
                minefieldGrid.Children.Add(minesweeper.image);
            }
        }

        private void InitializeGrid()
        {
            minefieldGrid.ColumnDefinitions.Clear();
            minefieldGrid.RowDefinitions.Clear();
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
            {
                minefieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
                minefieldGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void Reset()
        {
            mpg = new MinePositionsGenerator();
            minefield = new Minefield(Settings.MAP_SIZE);
            minesweeper = null;
        }

        private void Redraw()
        {
            minefieldGrid.Children.Clear();
            drawFieldTypes();
            drawMines();
            drawMinesweeper();
        }

        private void WriteToFile(Frame.Minefield minefield, string filename)
        {
            List<string> lines = new List<string>();
            for(int i = 0; i < minefield.size; ++i)
            {
                string line = "";
                for (int j = 0; j < minefield.size; ++j)
                {
                    line += minefield.fields[j, i].radiation.ToString() + " ";
                }
                lines.Add(line);
            }
            System.IO.File.WriteAllLines(@"D:\" + filename + ".txt", lines);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if (minesweeper != null)
                    {
                        if (minesweeper.NextMove() == 1)
                            minefield.disarmAt(minesweeper.GetX(), minesweeper.GetY());
                    }
                    break;

                case Key.G:
                    mpg.GenerateMinePositions(Settings.NR_OF_MINES, minefield);
                    break;

                case Key.P:
                    WriteToFile(minefield.generateInitialMinefieldFrame(), "input");
                    WriteToFile(minefield.generateFakeFrame(), "output");
                    break;

                case Key.R:
                    Reset();
                    break;

                case Key.Enter:
                    if (minesweeper == null)
                    {
                        minesweeper = new Minesweeper(minefield.generateInitialMinefieldFrame());
                    }
                    minesweeper.AddExplosivesLocations(minefield.GetExplosivesLocations());
                    minesweeper.Search();
                    break;
            }
            Redraw();
        }
    }
}
