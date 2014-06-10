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

        private GeneticsWindow geneticsWindow;
        private MinesweeperKnowledgeWindow minesweeperKnowledgeWindow;

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
            minefield = new Minefield();
            minesweeper = null;
            DestroyGeneticsWindow();
            DestroyKnowledgeWindow();
        }

        private void Redraw()
        {
            minefieldGrid.Children.Clear();
            drawFieldTypes();
            drawMines();
            drawMinesweeper();
            if (minesweeperKnowledgeWindow != null)
                minesweeperKnowledgeWindow.Redraw(minesweeper.minefield);
        }

        private void InitializeGeneticsWindow()
        {
            if (geneticsWindow == null)
            {
                geneticsWindow = new GeneticsWindow();
                geneticsWindow.Show();
            }
        }

        private void InitializeKnowledgeWindow()
        {
            if (minesweeperKnowledgeWindow == null)
            {
                minesweeperKnowledgeWindow = new MinesweeperKnowledgeWindow();
                minesweeperKnowledgeWindow.Show();
            }
        }

        private void DestroyGeneticsWindow()
        {
            if (geneticsWindow != null)
                geneticsWindow.Close();
            geneticsWindow = null;
        }

        private void DestroyKnowledgeWindow()
        {
            if (minesweeperKnowledgeWindow != null)
                minesweeperKnowledgeWindow.Close();
            minesweeperKnowledgeWindow = null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if (minesweeper != null)
                    {
                        int code = minesweeper.NextMove();
                        if (code == 1)
                        {
                            minefield.disarmAt(minesweeper.GetX(), minesweeper.GetY());
                        }
                        else if (code == 2 && minefield.fieldArray[minesweeper.GetX(), minesweeper.GetY()].explosive != null)
                        {
                            minesweeper.Recognize(minefield.fieldArray[minesweeper.GetX(), minesweeper.GetY()].explosive.frame);
                        }
                    }
                    break;


                case Key.R:
                    Reset();
                    break;

                case Key.G:
                    InitializeGeneticsWindow();
                    break;

                case Key.Enter:
                    if(geneticsWindow != null)
                    {
                        InitializeKnowledgeWindow();
                        minesweeper = new Minesweeper(geneticsWindow.getCurrentChromosome());
                        minesweeper.minefield = minefield.generateMinefieldFrame();
                        minefield.WriteToFile("diag");
                        minesweeper.Search();
                    }
                    break;
            }
            Redraw();
        }
    }
}
