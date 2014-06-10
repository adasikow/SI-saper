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
    }
}
