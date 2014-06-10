using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DecisionTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void onBuildTreeButtonClicked(object sender, EventArgs e)
        {
            if (_txtSourceFile.Text == String.Empty)
            {
                MessageBox.Show("Please select a valid source file");
                return;
            }

            try
            {
                DecisionTreeImplementation sam = new DecisionTreeImplementation();
                _rtxtOutput.Text = sam.GetTree(_txtSourceFile.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - there has been some problem parsing your input file.  Below is information about the error: " + Environment.NewLine + ex.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + ex.StackTrace);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void _btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _txtSourceFile.Text = fileDialog.FileName;
            }
        }
    }
}