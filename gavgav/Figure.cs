using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gavgav
{
    class Figure
    {
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Figure() { }

        public Figure(string name, double width, double height)
        {
            Name = name;
            Width = width;
            Height = height;
        }
    }
}
