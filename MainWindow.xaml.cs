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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace saper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        Minesweeper minesweeper;

        public MainWindow()
        {
            InitializeComponent();
            minesweeper = new Minesweeper(Saper);
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
