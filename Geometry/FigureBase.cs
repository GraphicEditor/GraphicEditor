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
        public static IFigure CreateTriangle(Point Top, Point Left, Point Right)
        {
            return new Triangle(Top, Left, Right);
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
        void DrawTriangle(Point Top, Point Left, Point Right);
        void DrawSquare(Point topleft, Point sidelength)
    }
    public interface IFigure
    {
        IDictionary<string, Point> Parameters { get; }
        Point Origin { get; }
        bool IsInternal(Point p);
        void Draw(IGraphicBase window);
        void Move(Point p);
        Point GetCenter();
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
        public void Move(Point p) // p - diff
        {
            Vector vp = new Vector(p.X, p.Y);
            Parameters["Center"] += vp;
            Parameters["Point on circle"] += vp;
        }
        public Point GetCenter()
        {
            return Origin;
        }
    }
    public class Triangle : IFigure 
    {
        public IDictionary<string, Point> Parameters { get; } = new Dictionary<string, Point>()
        {
            ["Top Point"] = new Point(0, 2),
            ["Left Point"] = new Point(-1, 0),
            ["Right Point"] = new Point(1, 0)
        };
        public Point Top => Parameters["Top Point"];
        public Point Left => Parameters["Left Point"];
        public Point Right => Parameters["Right Point"];

        public Point Origin => Top;

        public void Draw(IGraphicBase window)
        {
            window.DrawTriangle(Top, Left, Right)
        }

        public void Move(Point p) // p - diff
        {
            Vector vp = new Vector(p.X, p.Y);
            Parameters["Center"] += vp;
            Parameters["Point on circle"] += vp;
        }
        public Point GetCenter()
        {
            return new Point((Top.X + Left.X + Right.X) / 3, (Top.Y + Left.Y + Right.Y) / 3);
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

}
