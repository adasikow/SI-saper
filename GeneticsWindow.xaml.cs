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
    /// Interaction logic for GeneticsWindow.xaml
    /// </summary>
    public partial class GeneticsWindow : Window
    {
        private ushort individualId;
        private ushort generation;
        private List<Chromosome> population;
        private Genetics genetics;

        public GeneticsWindow()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            cbxMutationRate.DataContext = new List<int>(new int[] {
                0, 5, 10, 20, 30, 40, 50 });

            cbxPopSize.DataContext = new List<ushort>(new ushort[] {
                4, 8, 12, 16});

            cbxPSS.DataContext = new List<String>(new String[] {
                "Roulette selection", "Elite selection" });

            cbxPSS.SelectedIndex = cbxPopSize.SelectedIndex = cbxMutationRate.SelectedIndex = 0;
        }

        private void FillListsOfGenes(ushort individualId)
        {
            List<KeyValuePair<String, bool>> genes = population[individualId].GetGenes();

            List<String> activeGenes = new List<String>();
            List<String> inactiveGenes = new List<String>();

            foreach (KeyValuePair<String, bool> gene in genes)
            {
                if (Convert.ToBoolean(gene.Value))
                    activeGenes.Add(gene.Key);
                else
                    inactiveGenes.Add(gene.Key);
            }

            lstBoxActiveGenes.DataContext = activeGenes;
            lstBoxInactiveGenes.DataContext = inactiveGenes;
        }

        private void btnNewPop_Click(object sender, RoutedEventArgs e)
        {
            genetics = new Genetics();

            ushort poulationSize = Convert.ToUInt16(cbxPopSize.SelectedItem);
            population = genetics.GeneratePopulation(poulationSize);

            individualId = 0;
            FillListsOfGenes(individualId);
            generation = 0;

            btnNextInd.IsEnabled = true;
            btnNextGen.IsEnabled = true;

            Title = "Genetics - generation nr " + generation;
            lblMinesweeperId.Content = "Minesweeper nr " + (individualId + 1);
        }

        private void btnNextGen_Click(object sender, RoutedEventArgs e)
        {
            IParentsSelectionStrategy pss;
            if (cbxPSS.SelectedIndex == 0)
                pss = new RouletteSelection();
            else
                pss = new EliteSelection();

            population = genetics.GenerateNextGeneration(population, pss, Convert.ToUInt16(cbxMutationRate.SelectedItem));
            ++generation;

            FillListsOfGenes(individualId);
            this.Title = "Genetics - generation nr " + generation;
        }

        private void btnNextInd_Click(object sender, RoutedEventArgs e)
        {
            ++individualId;

            if (individualId == population.Count - 1)
                btnNextInd.IsEnabled = false;

            btnPrevInd.IsEnabled = true;
            lblMinesweeperId.Content = "Minesweeper nr " + (individualId + 1);

            FillListsOfGenes(individualId);
        }

        private void btnPrevInd_Click(object sender, RoutedEventArgs e)
        {
            --individualId;

            if (individualId == 0)
                btnPrevInd.IsEnabled = false;

            btnNextInd.IsEnabled = true;
            lblMinesweeperId.Content = "Minesweeper nr " + (individualId + 1);

            FillListsOfGenes(individualId);
        }
    }
}
