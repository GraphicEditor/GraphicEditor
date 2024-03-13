using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Document : IDocument
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == "")
                    throw new ArgumentException("Имя файла не может быть пустым.");
                name = value;
            }
        }
        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Путь должен быть определен.");
                path = value;
            }
        }
        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Width", "Ширина должна быть положительной.");
                width = value;
            }
        }
        private int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Height", "Высота должна быть положительной.");
                height = value;
            }
        }
        private Color backgroundColor = Color.White;
        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
            }
        }
    }
}
