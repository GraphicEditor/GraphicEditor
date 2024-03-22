using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;


namespace Geometry
{
    public static class FigureBase
    {
        public static IFigure CreateRectangle(Point BottomLeft, Point TopRight)
        {
            return new Rectangle(BottomLeft, TopRight);
        }
        public static IFigure CreateTriangle(Point Top, Point Left, Point Right)
        {
            return new Triangle(Top, Left, Right);
        }
        public static IFigure CreatePolyline(List<Point> points)
        {
            return new Polyline(points);
        }
        public static IFigure CreateCircle(Point center, Point circle)
        {
            return new Circle(center, circle);
        }
    }
    public interface IGraphicBase
    {
        void DrawLine(Point a, Point b);
        void DrawCircle(Point center, float rad);
        void DrawTriangle(Point top, Point left, Point right); // Добавляем метод для отрисовки треугольников
        void DrawRectangle(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight); // Добавляем метод для отрисовки прямоугольников
       // void Move(Point vector); // Добавляем метод перемещения фигуры
        //void Rotate(double angle); // Добавляем метод поворота фигуры
        //void Fill(Brush brush); // Добавляем метод заливки фигуры
        //void Resize(float scaleFactor); // Добавляем метод изменения размера фигуры
    }
    public interface IFigure
    {
        IDictionary<string, Point> Parameters { get; }
        Point Origin { get; }
        bool IsInternal(Point p);
        void Draw(IGraphicBase window);
        void Move(Point p);
        Point GetCenter();
        void Scale(float Scale);
        void Rotate(float Angle);
        //void Fill(Brush brush);
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
        public Circle(Point center, Point circle)
        {
            Parameters["Center"] = center;
            Parameters["Point on circle"] = circle;
        }
        public void Draw(IGraphicBase window)
        {
            window.DrawCircle(Origin, (float)Radius);
        }
        public void Move(Point p)
        {
            Vector vp = new Vector(p.X, p.Y);
            Parameters["Center"] += vp;
            Parameters["Point on circle"] += vp;
        }
        public Point GetCenter()
        {
            return Origin;
        }

        public void Scale(float Scale)
        {
            Point centerPoint = this.GetCenter();

            Matrix translationMatrix1 = Matrix.Identity;
            translationMatrix1.Translate(-centerPoint.X, -centerPoint.Y);
            Matrix scaleMatrix = new Matrix();
            scaleMatrix.Scale(Scale, Scale);
            Matrix translationMatrix2 = Matrix.Identity;
            translationMatrix2.Translate(centerPoint.X, centerPoint.Y);

            foreach (var pair in Parameters)
                Parameters[pair.Key] = translationMatrix2.Transform(scaleMatrix.Transform(translationMatrix1.Transform(pair.Value)));
        }
        public void Rotate(float Angle)
        {
            
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

        public Triangle(Point top, Point left, Point right)
        {
            Parameters["Top Point"] = top;
            Parameters["Left Point"] = left;
            Parameters["Right Point"] = right;
        }

        public void Draw(IGraphicBase window)
        {
            window.DrawTriangle(Top, Left, Right);
        }

        public bool IsInternal(Point p)
        {
            return true;
        }

        public void Move(Point p)
        {
            Vector vp = new Vector(p.X, p.Y);
            Parameters["Top Point"] += vp;
            Parameters["Left Point"] += vp;
            Parameters["Right Point"] += vp;
        }

        public Point GetCenter()
        {
            return new Point((Top.X + Left.X + Right.X) / 3, (Top.Y + Left.Y + Right.Y) / 3);
        }

        public void Scale(float Scale)
        {
            Point centerPoint = this.GetCenter();

            Matrix translationMatrix1 = Matrix.Identity;
            translationMatrix1.Translate(-centerPoint.X, -centerPoint.Y);
            Matrix scaleMatrix = new Matrix();
            scaleMatrix.Scale(Scale, Scale);
            Matrix translationMatrix2 = Matrix.Identity;
            translationMatrix2.Translate(centerPoint.X, centerPoint.Y);

            foreach (var pair in Parameters)
                Parameters[pair.Key] = translationMatrix2.Transform(scaleMatrix.Transform(translationMatrix1.Transform(pair.Value)));
        }

        public void Rotate(float Angle)
        {
            Point centerPoint = this.GetCenter();

            Matrix translationMatrix1 = Matrix.Identity;
            translationMatrix1.Translate(-centerPoint.X, -centerPoint.Y);
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.Rotate(Angle);
            Matrix translationMatrix2 = Matrix.Identity;
            translationMatrix2.Translate(centerPoint.X, centerPoint.Y);

            foreach (var pair in Parameters)
            {
                Parameters[pair.Key] = translationMatrix2.Transform(rotationMatrix.Transform(translationMatrix1.Transform(pair.Value)));
            }
        }
    }

    public class Rectangle : IFigure
    {
        public IDictionary<string, Point> Parameters { get; } = new Dictionary<string, Point>()
        {
            ["Top Right"] = new Point(0, 2),
            ["Top Left"] = new Point(-1, 0),
            ["Bottom Left"] = new Point(1, 0),
            ["Bottom Right"] = new Point(1, 0)
        };

        Point TopRight => Parameters["Top Right"];
        Point TopLeft => Parameters["Top Left"];
        Point BottomLeft => Parameters["Bottom Left"];
        Point BottomRight => Parameters["Bottom Right"];
        public Rectangle(Point BottomLeft, Point TopRight)
        {
            Parameters["Top Right"] = TopRight;
            Parameters["Top Left"] = new Point(BottomLeft.X, TopRight.Y);
            Parameters["Bottom Left"] = BottomLeft;
            Parameters["Bottom Right"] = new Point(TopRight.X, BottomLeft.Y);
        }

        public Point Origin => TopRight;

        public void Draw(IGraphicBase window)
        {
            window.DrawRectangle(TopRight, TopLeft, BottomLeft, BottomRight);
        }

        public Point GetCenter()
        {
            return new Point((TopRight.X + TopLeft.X + BottomLeft.X + BottomRight.X) / 4, (TopRight.Y + TopLeft.Y + BottomLeft.Y + BottomRight.Y) / 4);
        }

        public bool IsInternal(Point p)
        {
            return true;
        }

        public void Move(Point p)
        {
            Vector vp = new Vector(p.X, p.Y);
            foreach (var pair in Parameters)
                Parameters[pair.Key] += vp;
        }

        public void Scale(float Scale)
        {
            Point centerPoint = this.GetCenter();

            Matrix translationMatrix1 = Matrix.Identity;
            translationMatrix1.Translate(-centerPoint.X, -centerPoint.Y);
            Matrix scaleMatrix = new Matrix();
            scaleMatrix.Scale(Scale, Scale);
            Matrix translationMatrix2 = Matrix.Identity;
            translationMatrix2.Translate(centerPoint.X, centerPoint.Y);

            foreach (var pair in Parameters)
                Parameters[pair.Key] = translationMatrix2.Transform(scaleMatrix.Transform(translationMatrix1.Transform(pair.Value)));
        }

        public void Rotate(float Angle)
        {
            Point centerPoint = this.GetCenter();

            Matrix translationMatrix1 = Matrix.Identity;
            translationMatrix1.Translate(-centerPoint.X, -centerPoint.Y);
            Matrix rotationMatrix = new Matrix();
            rotationMatrix.Rotate(Angle);
            Matrix translationMatrix2 = Matrix.Identity;
            translationMatrix2.Translate(centerPoint.X, centerPoint.Y);

            foreach (var pair in Parameters)
            {
                Parameters[pair.Key] = translationMatrix2.Transform(rotationMatrix.Transform(translationMatrix1.Transform(pair.Value)));
            }
        }
    }

    public class Polyline : IFigure
    {
        public IDictionary<string, Point> Parameters { get; } = new Dictionary<string, Point>();

        private List<Point> Points { get; }

        public Polyline(List<Point> points)
        {
            Points = points;
        }

        public Point Origin => Points.FirstOrDefault();

        public bool IsInternal(Point p)
        {
            // Логика проверки, находится ли точка внутри полилинии
            return false;
        }

        public void Draw(IGraphicBase window)
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                window.DrawLine(Points[i], Points[i + 1]);
            }
        }

        public void Move(Point p)
        {
            // Логика перемещения полилинии
        }

        public Point GetCenter()
        {
            // Логика определения центра полилинии
            return new Point(0, 0);
        }

        public void Scale(float Scale)
        {
            // Логика масштабирования полилинии
        }

        public void Rotate(float Angle)
        {
            // Логика поворота полилинии
        }
    }


}
