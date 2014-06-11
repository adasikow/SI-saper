using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace saper
{
    class Explosive
    {
        public Frame.Explosive frame { get; private set; }

        private Random random;
        public Image image { get; protected set; }
        public Explosive(Random rand)
        {
            //random = new Random();
            frame = new Frame.Explosive();
            image = new Image();
            image.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/explosive.jpg"));
            GenerateFrame(rand);

        }

        private void GenerateFrame(Random random)
        {

            Array values;

            values = Enum.GetValues(typeof(Frame.Colour));
            frame.colour = (Frame.Colour)values.GetValue(random.Next(values.Length));

            values = Enum.GetValues(typeof(Frame.Shape));
            frame.shape = (Frame.Shape)values.GetValue(random.Next(values.Length));

            values = Enum.GetValues(typeof(Frame.Material));
            frame.material = (Frame.Material)values.GetValue(random.Next(values.Length));

            frame.hasLight = Convert.ToBoolean(random.Next(2));

            frame.makesSound = Convert.ToBoolean(random.Next(2));

            float weight = ((float)(random.Next(401) + 100)) / 100;
            float size = ((float)(random.Next(401) + 100)) / 100;
            float hardness = ((float)(random.Next(401) + 100)) / 100;

            values = Enum.GetValues(typeof(Frame.Weight));
            frame.weight = (Frame.Weight)values.GetValue(max(examineWeight(weight)));

            values = Enum.GetValues(typeof(Frame.Size));
            frame.size = (Frame.Size)values.GetValue(max(examineSize(size)));

            values = Enum.GetValues(typeof(Frame.Hardness));
            frame.hardness = (Frame.Hardness)values.GetValue(max(examineHardness(hardness)));

        }

        private float[] examineWeight(float weight)
        {
            float[] rates; //tablica rates przechowuje 3 wartości - stopień przynależności do pojęcia 'lekki', 'średni' i 'ciężki'
            rates = new float[4];

            if (weight >= 3) rates[1] = 0;
            else rates[1] = (3 - weight) / (3 - 1); // dla pojęcia 'lekki' rozpatrujemy przedział (1, 3), forma diagramu: "\_

            if (weight <= 2 || weight >= 4) rates[2] = 0;
            else if (weight > 2 && weight <= 3) rates[2] = (1 - (3 - weight)) / (3 - 2); // dla pojęcia 'średni' rozpatrujemy przedział (2, 3), forma diagramu: _/"...
            else if (weight > 3 && weight < 4) rates[2] = (1 + (3 - weight)) / (3 - 2); // ... oraz przedział (3, 4), forma diagramu: "\_

            if (weight <= 3) rates[3] = 0;
            else rates[3] = (weight - 3) / (5 - 3); // dla pojęcia 'ciężki' rozpatrujemy przedział (3, 5), forma diagramu: _/"

            return rates;
        }

        private float[] examineHardness(float hardness)
        {
            float[] rates; //tablica rates przechowuje 3 wartości - stopień przynależności do pojęcia 'miękki', 'średni' i 'twardy'
            rates = new float[4];

            if (hardness >= 3) rates[1] = 0;
            else rates[1] = (3 - hardness) / (3 - 1); // dla pojęcia 'miękki' rozpatrujemy przedział (1, 3), forma diagramu: "\_

            if (hardness <= 2 || hardness >= 4) rates[2] = 0;
            else if (hardness > 2 && hardness <= 3) rates[2] = (1 - (3 - hardness)) / (3 - 2); // dla pojęcia 'średni' rozpatrujemy przedział (2, 3), forma diagramu: _/"...
            else if (hardness > 3 && hardness < 4) rates[2] = (1 + (3 - hardness)) / (3 - 2); // ... oraz przedział (3, 4), forma diagramu: "\_

            if (hardness <= 3) rates[3] = 0;
            else rates[3] = (hardness - 3) / (5 - 3); // dla pojęcia 'twardy' rozpatrujemy przedział (3, 5), forma diagramu: _/"

            return rates;
        }

        private float[] examineSize(float size)
        {
            float[] rates; //tablica rates przechowuje 3 wartości - stopień przynależności do pojęcia 'mały', 'średni' i 'wielki'
            rates = new float[4];

            if (size >= 3) rates[1] = 0;
            else rates[1] = (3 - size) / (3 - 1); // dla pojęcia 'mały' rozpatrujemy przedział (1, 3), forma diagramu: "\_

            if (size <= 2 || size >= 4) rates[2] = 0;
            else if (size > 2 && size <= 3) rates[2] = (1 - (3 - size)) / (3 - 2); // dla pojęcia 'średni' rozpatrujemy przedział (2, 3), forma diagramu: _/"...
            else if (size > 3 && size < 4) rates[2] = (1 + (3 - size)) / (3 - 2); // ... oraz przedział (3, 4), forma diagramu: "\_

            if (size <= 3) rates[3] = 0;
            else rates[3] = (size - 3) / (5 - 3); // dla pojęcia 'wielki' rozpatrujemy przedział (3, 5), forma diagramu: _/"

            return rates;
        }

        private int max(float[] values)
        {
            int max = 1;
            for (int i = 1; i < values.Length; ++i)
            {
                if (values[i] > values[max])
                    max = i;
            }
            return max - 1;
        }

    }
}
