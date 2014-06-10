using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public enum Shape { Cube, Cuboid, Cylinder, Sphere }
    public enum Material { Metal, Plastic, Fabric, Polymer }
    public enum Colour { Black, Gray, Brown, Red }
    public enum Hardness { Soft, Medium, Hard }
    public enum Weight { Light, Medium, Heavy }
    public enum Size { Small, Medium, Big }

    public class Explosive
    {
        public Shape? shape { get; set; }
        public Colour? colour { get; set; }
        public Material? material { get; set; }
        public Hardness? hardness { get; set; }
        public Weight? weight { get; set; }
        public Size? size { get; set; }
        public bool makesSound { get; set; }
        public bool hasLight { get; set; }

        public Explosive()
        {
            this.shape = null;
            this.colour = null;
            this.material = null;
            this.hardness = null;
            this.weight = null;
            this.size = null;
            this.hasLight = false;
            this.makesSound = false;
        }

        public float[] examineWeight(float weight)
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

        public float[] examineHardness(float hardness)
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

        public float[] examineSize(float size)
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
    }
}
