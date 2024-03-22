using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Geometry;

namespace GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DrawAssistant drawingAssistant = new DrawAssistant(); // Объявление переменной в классе MainWindow

    class DrawAssistant : IGraphicBase
    {
        public DrawingGroup drawing = new DrawingGroup();
        public List<IFigure> Figures { get; } = new List<IFigure>(); // Добавляем список фигур

        public void DrawLine(Point a, Point b)
        {
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(new SolidColorBrush(Colors.Red), 1), new LineGeometry(a, b)));
        }

        public void DrawCircle(Point center, float rad)
        {
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(new SolidColorBrush(Colors.Red), 1), new EllipseGeometry(center, rad, rad)));
        }

        public void DrawTriangle(Point top, Point left, Point right)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            figure.StartPoint = top;
            figure.Segments.Add(new LineSegment(left, true));
            figure.Segments.Add(new LineSegment(right, true));
            figure.Segments.Add(new LineSegment(top, true));
            geometry.Figures.Add(figure);
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(new SolidColorBrush(Colors.Red), 1), geometry));
        }

        public void DrawRectangle(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            figure.StartPoint = topLeft;
            figure.Segments.Add(new LineSegment(topRight, true));
            figure.Segments.Add(new LineSegment(bottomRight, true));
            figure.Segments.Add(new LineSegment(bottomLeft, true));
            figure.Segments.Add(new LineSegment(topLeft, true));
            geometry.Figures.Add(figure);
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(new SolidColorBrush(Colors.Red), 1), geometry));
        }

        public void Move(Point vector)
        {
            // Перемещение каждой фигуры на заданный вектор
            foreach (var figure in Figures)
            {
                figure.Move(vector);
            }
        }

        public void Rotate(double angle)
        {
            // Поворот каждой фигуры на заданный угол
            foreach (var figure in Figures)
            {
                figure.Rotate((float)angle);
            }
        }

        public void Fill(Brush brush)
        {
            // Применение заливки для каждой фигуры
            /*foreach (var figure in Figures)
            {
                figure.Fill(brush);
            }*/
        }

        public void Resize(float scaleFactor)
        {
            // Изменение размера каждой фигуры на заданный коэффициент
            /*foreach (var figure in Figures)
            {
                figure.Resize(scaleFactor);
            }*/
        }

        public DrawingGroup GetDrawing()
        {
            return drawing;
        }
    }

    

    public MainWindow()
    {
        InitializeComponent();
        mcolor = new ColorRBG();
        mcolor.red = 0;
        mcolor.green = 0;
        mcolor.blue = 0;
        this.lbl1.Background = new SolidColorBrush(Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue));
    }

    private void Click_Save(object sender, RoutedEventArgs e)
    {
        
    }

    private void Click_Circle(object sender, RoutedEventArgs e)
    {
        var figure = FigureBase.CreateCircle();
        figure.Parameters["Center"] = new Point(10, 10);
        figure.Parameters["Point on circle"] = new Point(100, 100);

        // Добавляем созданную фигуру в список Figures
        drawingAssistant.Figures.Add(figure);

        // Вызываем метод Draw для отображения фигуры
        figure.Draw(drawingAssistant);

        // Обновляем отображение на экране
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Triangle(object sender, RoutedEventArgs e)
    {
        var figure = FigureBase.CreateTriangle(new Point(0, 0), new Point(50, 100), new Point(100, 0));

        // Добавляем созданную фигуру в список Figures
        drawingAssistant.Figures.Add(figure);

        // Вызываем метод Draw для отображения фигуры
        figure.Draw(drawingAssistant);

        // Обновляем отображение на экране
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Rectangle(object sender, RoutedEventArgs e)
    {
        var figure = FigureBase.CreateRectangle(new Point(0, 0), new Point(100, 100));

        // Добавляем созданную фигуру в список Figures
        drawingAssistant.Figures.Add(figure);

        // Вызываем метод Draw для отображения фигуры
        figure.Draw(drawingAssistant);

        // Обновляем отображение на экране
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Polyline(object sender, RoutedEventArgs e)
    {
        var points = new List<Point> { new Point(0, 0), new Point(50, 50), new Point(100, 0) };
        var figure = FigureBase.CreatePolyline(points);

        // Добавляем созданную фигуру в список Figures
        drawingAssistant.Figures.Add(figure);

        // Вызываем метод Draw для отображения фигуры
        figure.Draw(drawingAssistant);

        // Обновляем отображение на экране
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Move(object sender, RoutedEventArgs e)
    {
        // Перемещение фигуры на заданный вектор
        var moveVector = new Vector(10, 10);
        var movePoint = new Point(moveVector.X, moveVector.Y);
        drawingAssistant.Move(movePoint); // Перемещение фигуры
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Rotate(object sender, RoutedEventArgs e)
    {
        var angle = 45;
        drawingAssistant.Rotate(angle);
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Bucket(object sender, RoutedEventArgs e)
    {
        var fillBrush = new SolidColorBrush(Colors.Red);
        drawingAssistant.Fill(fillBrush);
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Resize(object sender, RoutedEventArgs e)
    {
        var scaleFactor = 1.5f;
        drawingAssistant.Resize(scaleFactor);
        Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }




    public class ColorRBG
    {
        public byte red { get; set; }
        public byte green { get; set; }
        public byte blue { get; set; }
    }

    public ColorRBG mcolor { get; set; }
    public Color clr { get; set; }

    private void sld_color_valueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) 
    {
        var slider = sender as Slider;
        string crlName = slider.Name;
        double value = slider.Value;

        if(crlName.Equals("sld_RedColor"))
        {
            mcolor.red = Convert.ToByte(value);
        }
        if (crlName.Equals("sld_GreenColor"))
        {
            mcolor.green = Convert.ToByte(value);
        }
        if (crlName.Equals("sld_BlueColor"))
        {
            mcolor.blue = Convert.ToByte(value);
        }

        clr = Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue);

        this.lbl1.Background = new SolidColorBrush(Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue));

        this.inkCanvas1.DefaultDrawingAttributes.Color = clr;
    }
    Color SelectedColor=Colors.Red;
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        SelectedColor=((sender as Button)?.Background as SolidColorBrush)?.Color??SelectedColor;
    }
}
