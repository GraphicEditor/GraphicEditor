using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace Geometry
{
    public static class FigureBase
    {
        public static IFigure CreateRectangle()
        {
            return new Rectangle();
        }
        public static IFigure CreateTriangle()
        {
            return new Triangle();
        }
        public static IFigure CreatePolyline()
        {
            return new Polyline();
        }
    }
    public interface IFigure : IAttachmentConsumer, IGeometricObject
    {
        IDictionary<vector> Parameters(get; set)
    }
}
