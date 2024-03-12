using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public struct Color
    {
        public byte r;
        public byte g;
        public byte b;

        public static Color White { get => new Color(255, 255, 255); }

        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}
