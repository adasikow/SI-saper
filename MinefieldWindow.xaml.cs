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
using System.Windows.Threading;

namespace saper
{
    /// <summary>
    /// Interaction logic for MinefieldWindow.xaml
    /// </summary>
    public partial class MinefieldWindow : Window
    {
        private MapManager mapManager;
        private Minesweeper minesweeper;
        private Minefield minefield;
        //private Image minesweeperImage;
        //private Image mineImage;
        private MinePositionsGenerator mpg;

        public MinefieldWindow()
        {
            InitializeComponent();
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
            {
                minefieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
                minefieldGrid.RowDefinitions.Add(new RowDefinition());
            }
            mapManager = new MapManager();
            //minesweeper.LocationUpdate += HandleLocationUpdate;
            minefield = new Minefield(Settings.MAP_SIZE);
            minesweeper = new Minesweeper(minefield.generateInitialMinefieldFrame());
            ResetMineField();
            mpg = new MinePositionsGenerator();
        }

        /*
        private void HandleLocationUpdate(object sender, MinesweeperEventArgs e)
        {
            minefieldGrid.Dispatcher.Invoke(new Action((() =>
            {
                Grid.SetColumn(minesweeper.minesweeperImage, e.x);
                Grid.SetRow(minesweeper.minesweeperImage, e.y);
            })), new object[] { this } );
        }
        */

        public void ResetMineField()
        {
            minefieldGrid.Children.Clear();
            minefieldGrid.Children.Add(minesweeper.minesweeperImage);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    minesweeper.Move();
                    break;
                case Key.Left:
                    minesweeper.RotateLeft();
                    break;
                case Key.Right:
                    minesweeper.RotateRight();
                    break;
                case Key.G:
                    mapManager.DrawMap(minefieldGrid);
                    break;
                case Key.R:
                    ResetMineField();
                    break;
                case Key.Enter:
                    minesweeper.Search(mapManager.minePositions);
                    break;
                case Key.N:
                    minesweeper.NextMove();
                    break;
            }
        }
    }
}
