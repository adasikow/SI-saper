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

        ushort explosivesCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.NR_OF_MINES = explosivesCount;
            MinefieldWindow minefieldWindow = new MinefieldWindow();
            minefieldWindow.Show();
            this.Close();
        }

        private void explosivesCountBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UInt16.TryParse(explosivesCountBox.Text, out explosivesCount))
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
