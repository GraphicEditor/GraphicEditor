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
            throw new NotImplementedException();
            //            return new Rectangle();
        }
        public static IFigure CreateTriangle()
        {
            return new Triangle();
        }
        public static IFigure CreatePolyline()
        {
            throw new NotImplementedException();
            //            return new Polyline(); 
        }
        public static IFigure CreateCircle()
        {
            return new Circle();
        }
    }
    public interface IGraphicBase
    {
        void DrawLine(Point a, Point b);
        void DrawCircle(Point center, float rad);
        void DrawTriangle(Point a, Point b, Point c);
    }
    public interface IFigure
    {
        IDictionary<string, Point> Parameters { get; }
        Point Origin { get; }
        bool IsInternal(Point p);
        void Draw(IGraphicBase window);
    }
    public class Circle : IFigure
    {
        public IDictionary<string, Point> Parameters { get; } = new Dictionary<string, Point>()
        {
            ["Center"] = new Point(0, 0),
            ["Point on circle"] = new Point(1, 0)
        };
        public Point Origin => Parameters["Center"];
        double Radius => (Parameters["Point on circle"] - Origin).Length;
        public bool IsInternal(Point p)
        {
            return (p - Origin).Length <= Radius;
        }
        public void Draw(IGraphicBase window)

        {
            window.DrawCircle(Origin, (float)Radius);
        }
    }
    public class Triangle : IFigure 
    {
        public IDictionary<string, Point> Parametrs { get; } = new Dictionary<string, Point>()
        {
            ["Top Point"] = new Point(0, 2),
            ["Left Point"] = new Point(-1, 0),
            ["Right Point"] = new Point(1, 0)
        };
        public Point Top => Parametrs["Top Point"];
        public Point Left => Parametrs["Left Point"];
        public Point Right => Parametrs["Right Point"];
        
        
        public void Draw(IGraphicBase window)
        {
            window.DrawTriangle(Top, Left, Right)
        }
    }

}
