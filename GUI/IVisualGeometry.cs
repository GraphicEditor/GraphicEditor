using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Geometry;

namespace Logic
{
    /// <summary>
    /// Contains information about geometry with visual representation
    /// </summary>
    public interface IVisualGeometry
    {
        /// <summary>
        /// Name for geometry
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Geometry base
        /// </summary>
        IFigure Figure { get; }
        /// <summary>
        /// Background brush for geometry
        /// </summary>
        Color BackgroundBrush { get; set; }
        /// <summary>
        /// Border brush for geometry
        /// </summary>
        Color BorderBrush { get; set; }
        /// <summary>
        /// Thickness of border
        /// </summary>
        double BorderThickness { get; set; }
    }

    public class VisualGeometry : IVisualGeometry
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
        private IFigure figure;
        public IFigure Figure { get => figure; }
        private Color backgroundBrush = Colors.White;
        public Color BackgroundBrush
        {
            get
            {
                return backgroundBrush;
            }
            set
            {
                backgroundBrush = value;
            }
        }
        private Color borderBrush = Colors.White;
        public Color BorderBrush
        {
            get
            {
                return borderBrush;
            }
            set
            {
                borderBrush = value;
            }
        }
        private double borderThickness = 1;
        public double BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                borderThickness = value;
            }
        }
        public VisualGeometry(string name, IFigure figure)
        {
            this.name = name;
            this.figure = figure;
        }
    }

    public static class VisualGeometryFactory
    {
        public static IVisualGeometry CreateVisualGeometry(string name, IFigure geometry)
        {
            return new VisualGeometry(name, geometry);
        }
    }
}
