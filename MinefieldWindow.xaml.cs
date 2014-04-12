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
        int minefieldSize;

        public MinefieldWindow(int minefieldSize)
        {
            InitializeComponent();
            this.minefieldSize = minefieldSize;
            for (int i = 0; i < this.minefieldSize; ++i)
            {
                minefieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
                minefieldGrid.RowDefinitions.Add(new RowDefinition());
            }
            minesweeper = new Minesweeper(this.minefieldSize);
            minefieldGrid.Children.Add(minesweeper.minesweeperImage);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                minesweeper.Move();
            if (e.Key == Key.Right)
                minesweeper.RotateRight();
            if (e.Key == Key.Left)
                minesweeper.RotateLeft();

        }
    }
}
