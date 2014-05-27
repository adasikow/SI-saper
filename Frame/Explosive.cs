using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame
{
    public enum Shape { Cuboid, Cylinder }
    public enum Material { Metal, Plastic, Fabric }
    public enum Colour { Black, Gray, Brown, Red }
    public enum Hardness { Soft, Hard, Plastic }

    public class Explosive
    {
        public Shape shape { get; set; }
        public Colour colour { get; set; }
        public Material material { get; set; }
        public Hardness hardness { get; set; }
        public float weight { get; set; }
        public bool makesSound { get; set; }
        public bool hasLight { get; set; }
    }
}
