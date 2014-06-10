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
    /// Interaction logic for MinesweeperKnowledgeWindow.xaml
    /// </summary>
    public partial class MinesweeperKnowledgeWindow : Window
    {
        public MinesweeperKnowledgeWindow()
        {
            InitializeComponent();
        }

        public void Redraw(Frame.Minefield minefield)
        {
            return;
        }
    }
}
