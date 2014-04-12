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
        List<String> problemSolveMethods;

        int minefieldSize = 0;

        public MainWindow()
        {
            InitializeComponent();
            problemSolveMethods = new List<String>();
            problemSolveMethods.Add("Przeszukanie przestreni stanów");
            problemSolveMethods.Add("Drzewa decyzyjne");
            problemSolveMethods.Add("Algorytmy genetyczne");
            problemSolveMethods.Add("Algorytmy uczenia symbolicznego");
            problemSolveMethods.Add("Sieci neuronowe");
            methodSelectionBox.ItemsSource = problemSolveMethods;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            MinefieldWindow minefieldWindow = new MinefieldWindow(minefieldSize);
            MinesweeperKnowledgeWindow minesweeperKnowledgeWindow = new MinesweeperKnowledgeWindow();
            minefieldWindow.Show();
            minesweeperKnowledgeWindow.Show();
        }

        private void minefieldSizeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Int32.TryParse(minefieldSizeBox.Text, out minefieldSize))
            {
                wrongInputLabel.Visibility = System.Windows.Visibility.Visible;
                startButton.IsEnabled = false;
            }
            else
            {
                wrongInputLabel.Visibility = System.Windows.Visibility.Hidden;
                startButton.IsEnabled = true;
            }
        }
    }
}
