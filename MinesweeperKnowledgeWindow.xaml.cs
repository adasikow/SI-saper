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
        private Frame.Minefield minefield;

        private BitmapImage scrap;

        private BitmapImage explosive;
        private BitmapImage bouncingBetty;
        private BitmapImage c4;
        private BitmapImage claymore;
        private BitmapImage dynamite;
        private BitmapImage misfire;
        private BitmapImage semtex;

        public MinesweeperKnowledgeWindow()
        {
            InitializeComponent();
            InitializeGrid();

            scrap = new BitmapImage(new Uri(@"pack://application:,,,/res/scrap.jpg"));

            explosive = new BitmapImage(new Uri(@"pack://application:,,,/res/explosive.jpg"));
            //bouncingBetty = new BitmapImage(new Uri(@"pack://application:,,,/res/bouncingBetty.png"));
            //c4 = new BitmapImage(new Uri(@"pack://application:,,,/res/c4.png"));
            //claymore = new BitmapImage(new Uri(@"pack://application:,,,/res/claymore.png"));
            //dynamite = new BitmapImage(new Uri(@"pack://application:,,,/res/dynamite.png"));
            //misfire = new BitmapImage(new Uri(@"pack://application:,,,/res/misfire.png"));
            //semtex = new BitmapImage(new Uri(@"pack://application:,,,/res/semtex.png"));
        }

        private void InitializeGrid()
        {
            minesweeperKnowledgeGrid.ColumnDefinitions.Clear();
            minesweeperKnowledgeGrid.RowDefinitions.Clear();
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
            {
                minesweeperKnowledgeGrid.ColumnDefinitions.Add(new ColumnDefinition());
                minesweeperKnowledgeGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private Image getImage(BitmapImage source)
        {
            Image result = new Image();
            result.Source = source;
            return result;
        }

        public void Redraw(Frame.Minefield minefield)
        {
            this.minefield = minefield;
            minesweeperKnowledgeGrid.Children.Clear();
            DrawFieldTypes();
            DrawExplosives();
        }

        private void DrawExplosives()
        {
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
                {
                    if (minefield.fields[i, j].explosive != null)
                    {
                        if (minefield.fields[i, j].explosive.GetType().Equals(typeof(Frame.BouncingBetty)))
                        {
                            DrawImage(i, j, bouncingBetty);
                        }
                        else if (minefield.fields[i, j].explosive.GetType().Equals(typeof(Frame.C4)))
                        {
                            DrawImage(i, j, c4);
                        }
                        else if (minefield.fields[i, j].explosive.GetType().Equals(typeof(Frame.Claymore)))
                        {
                            DrawImage(i, j, claymore);
                        }
                        else if (minefield.fields[i, j].explosive.GetType().Equals(typeof(Frame.Dynamite)))
                        {
                            DrawImage(i, j, dynamite);
                        }
                        else if (minefield.fields[i, j].explosive.GetType().Equals(typeof(Frame.Misfire)))
                        {
                            DrawImage(i, j, misfire);
                        }
                        else if (minefield.fields[i, j].explosive.GetType().Equals(typeof(Frame.Semtex)))
                        {
                            DrawImage(i, j, semtex);
                        }
                        else //if (minefield.fields[i, j].explosive.GetType().Equals(typeof(Frame.Explosive))) - ładunek nierozpoznany
                        {
                            DrawImage(i, j, explosive);
                        }
                    }
                }
        }

        private void DrawFieldTypes()
        {
            for (int i = 0; i < Settings.MAP_SIZE; ++i)
                for (int j = 0; j < Settings.MAP_SIZE; ++j)
                {
                    if (minefield.fields[i, j].type == Frame.FieldType.Scrap)
                    {
                        DrawImage(i, j, scrap);
                    }
                }
        }

        private void DrawImage(int x, int y, BitmapImage source)
        {
            Image image = getImage(source);
            Grid.SetColumn(image, x);
            Grid.SetRow(image, y);
            minesweeperKnowledgeGrid.Children.Add(image);
        }
    }
}
