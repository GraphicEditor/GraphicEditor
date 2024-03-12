using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Содержит информацию о документе
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }
        public string Path { get; set; }
        /// <summary>
        /// Ширина в пикселях
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Высота в пикселях
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Цвет фона
        /// </summary>
        public Color BackgroundColor { get; set; }
    }
}
