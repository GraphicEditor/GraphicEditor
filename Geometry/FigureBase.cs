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
        public static IFigure CreateSquare()
        {
            return new Square();
        }
    }
    public interface IGraphicBase
    {
        void DrawLine(Point a, Point b);
        void DrawCircle(Point center, float rad);
        void DrawTriangle(Point a, Point b, Point c);
        void DrawSquare(Point topleft, Point sidelength)
    }
    public interface IFigure
    {
        IDictionary<string, Point> Parameters { get; }
        IDictionary<string, Point> ParametersTriangle { get; }
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
        public IDictionary<string, Point> ParametersTriangle { get; } = new Dictionary<string, Point>()
        {
            ["Top Point"] = new Point(0, 2),
            ["Left Point"] = new Point(-1, 0),
            ["Right Point"] = new Point(1, 0)
        };
        public Point Top => ParametersTriangle["Top Point"];
        public Point Left => ParametersTriangle["Left Point"];
        public Point Right => ParametersTriangle["Right Point"];
        
        
        public void Draw(IGraphicBase window)
        {
            window.DrawTriangle(Top, Left, Right)
        }
    }

    public class Square : IFigure
    {
        public IDictionary<string, Point> Parameters { get; } = new Dictionary<string, Point>()
        {
            ["TopLeftCorner"] = new Point(0, 0),
            ["BottomRightCorner"] = new Point(1, 1)
        };

        public Point Origin => Parameters["TopLeftCorner"];

        public double SideLength => Parameters["BottomRightCorner"].X - Parameters["TopLeftCorner"].X;

        public bool IsInternal(Point p)
        {
            double x = p.X - Origin.X;
            double y = p.Y - Origin.Y;

            return x >= 0 && x <= SideLength && y >= 0 && y <= SideLength;
        }

        public void Draw(IGraphicBase window)
        {
            window.DrawSquare(Origin, SideLength);
        }
    }
}

public class Position : IFigure
    {
        public void NewPosition(Point NewPointA, Point NewPointB)
        {
            Parameters["Center"] = NewPointA;
            Parameters["Point on circle"] = NewPointB;
        }
    }

}
