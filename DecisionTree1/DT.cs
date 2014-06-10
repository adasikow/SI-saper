using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;

namespace DecisionTree
{
    public class DecisionTree
    {
        private DataTable sampleData;
        private int mTotalPositives = 0;
        private int mTotal = 0;
        private string mTargetAttribute = "explosive";
        private double mEntropySet = 0.0;

        private int countTotalPositives(DataTable samples)
        {
            int result = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if (aRow[mTargetAttribute].ToString().ToUpper().Trim() == "TRUE")
                    result++;
            }

            return result;
        }

        private double calculateEntropy(int positives, int negatives)
        {
            int total = positives + negatives;
            double positive = (double)positives / total;
            double negative = (double)negatives / total;

            if (positive != 0)
                positive = -(positive) * System.Math.Log(positive, 2);
            if (negative != 0)
                negative = -(negative) * System.Math.Log(negative, 2);

            double result = positive + negative;

            return result;
        }

        private void getValuesToAttribute(DataTable samples, TreeAttribute attribute, string value, out int positives, out int negatives)
        {
            positives = 0;
            negatives = 0;

            foreach (DataRow aRow in samples.Rows)
            {
                if (((string)aRow[attribute.AttributeName] == value))
                    if (aRow[mTargetAttribute].ToString().Trim().ToUpper() == "TRUE")
                        positives++;
                    else
                        negatives++;
                
            }
        }

        private double gain(DataTable samples, TreeAttribute attribute)
        {
            PossibleValueCollection values = attribute.PossibleValues;
            double sum = 0.0;

            for (int i = 0; i < values.Count; i++)
            {
                int positives, negatives;

                positives = negatives = 0;

                getValuesToAttribute(samples, attribute, values[i], out positives, out negatives);

                double entropy = calculateEntropy(positives, negatives);
                sum += -(double)(positives + negatives) / mTotal * entropy;
            }
            return mEntropySet + sum;
        }

        private TreeAttribute getBestAttribute(DataTable samples, TreeAttributeCollection attributes)
        {
            double maxGain = 0.0;
            TreeAttribute result = null;

            foreach (TreeAttribute attribute in attributes)
            {
                double aux = gain(samples, attribute);
                if (aux > maxGain)
                {
                    maxGain = aux;
                    result = attribute;
                }
            }
            return result;
        }

        private bool allSamplesArePositive(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if (row[targetAttribute].ToString().ToUpper().Trim() == "FALSE")
                    return false;
            }

            return true;
        }

        private bool allSamplesAreNegative(DataTable samples, string targetAttribute)
        {
            foreach (DataRow row in samples.Rows)
            {
                if (row[targetAttribute].ToString().ToUpper().Trim() == "TRUE")
                    return false;
            }

            return true;
        }

        private ArrayList getDistinctValues(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = new ArrayList(samples.Rows.Count);

            foreach (DataRow row in samples.Rows)
            {
                if (distinctValues.IndexOf(row[targetAttribute]) == -1)
                    distinctValues.Add(row[targetAttribute]);
            }

            return distinctValues;
        }

        private object getMostCommonValue(DataTable samples, string targetAttribute)
        {
            ArrayList distinctValues = getDistinctValues(samples, targetAttribute);
            int[] count = new int[distinctValues.Count];

            foreach (DataRow row in samples.Rows)
            {
                int index = distinctValues.IndexOf(row[targetAttribute]);
                count[index]++;
            }

            int MaxIndex = 0;
            int MaxCount = 0;

            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > MaxCount)
                {
                    MaxCount = count[i];
                    MaxIndex = i;
                }
            }

            return distinctValues[MaxIndex];
        }

        private TreeNode buildTree(DataTable samples, string targetAttribute, TreeAttributeCollection attributes)
        {
            if (samples == null)
                return new TreeNode(new OutcomeTreeAttribute(targetAttribute));

            if (allSamplesArePositive(samples, targetAttribute) == true)
                return new TreeNode(new OutcomeTreeAttribute(true));

            if (allSamplesAreNegative(samples, targetAttribute) == true)
                return new TreeNode(new OutcomeTreeAttribute(false));

            if (attributes.Count == 0)
                return new TreeNode(new OutcomeTreeAttribute(getMostCommonValue(samples, targetAttribute)));

            mTotal = samples.Rows.Count;
            mTargetAttribute = targetAttribute;
            mTotalPositives = countTotalPositives(samples);

            mEntropySet = calculateEntropy(mTotalPositives, mTotal - mTotalPositives);

            TreeAttribute bestAttribute = getBestAttribute(samples, attributes);

            TreeNode root = new TreeNode(bestAttribute);

            if (bestAttribute == null)
                return root;

            DataTable aSample = samples.Clone();

            foreach (string value in bestAttribute.PossibleValues)
            {			
                aSample.Rows.Clear();

                DataRow[] rows = samples.Select(bestAttribute.AttributeName + " = " + "'" + value + "'");

                foreach (DataRow row in rows)
                {
                    aSample.Rows.Add(row.ItemArray);
                }
                
                TreeAttributeCollection aAttributes = new TreeAttributeCollection();
                //ArrayList aAttributes = new ArrayList(attributes.Count - 1);
                for (int i = 0; i < attributes.Count; i++)
                {
                    if (attributes[i].AttributeName != bestAttribute.AttributeName)
                        aAttributes.Add(attributes[i]);
                }

                if (aSample.Rows.Count == 0)
                {
                    return new TreeNode(new OutcomeTreeAttribute(getMostCommonValue(aSample, targetAttribute)));
                }
                else
                {
                    DecisionTree dc3 = new DecisionTree();
                    TreeNode ChildNode = dc3.mountTree(aSample, targetAttribute, aAttributes);
                    root.AddTreeNode(ChildNode, value);
                }
            }

            return root;
        }

        public TreeNode mountTree(DataTable samples, string targetAttribute, TreeAttributeCollection attributes)
        {
            sampleData = samples;
            return buildTree(sampleData, targetAttribute, attributes);
        }
    }

    public class DecisionTreeImplementation
    {

        string _sourceFile;

        public string GetTree(string sourceFile)
        {
            _sourceFile = sourceFile;
            RawDataSource samples = new RawDataSource(_sourceFile);

            TreeAttributeCollection attributes = samples.GetValidAttributeCollection();

            DecisionTree id3 = new DecisionTree();
            TreeNode root = id3.mountTree(samples, "explosive", attributes);

            return PrintNode(root, "");

        }

        public string PrintNode(TreeNode root, string tabs)
        {
            Console.WriteLine(root.GetChildByBranchName("Black"));
            string returnString = String.Empty;
            string prefix = "Atrybut o najwiêkszym przyroœcie informacji : ";

            if (tabs != String.Empty)
                prefix = " --> ";
            returnString += (tabs + prefix + root.Attribute ) + Environment.NewLine;

            if (root != null && root.Attribute != null && root.Attribute.PossibleValues != null)
            {
                for (int i = 0; i < root.Attribute.PossibleValues.Count; i++)
                {
                    returnString += (Environment.NewLine + tabs + "\t" + "wejœcie:  " + root.Attribute.PossibleValues[i]) + Environment.NewLine;
                    TreeNode childNode = root.GetChildByBranchName(root.Attribute.PossibleValues[i]);
                    returnString += PrintNode(childNode, "\t" + tabs);
                }
            }
            
            return returnString;
        }

    }
}
