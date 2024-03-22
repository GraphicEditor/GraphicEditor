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
using Logic;

namespace GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public Document Document { get; set; }
    class DrawAssistant : IGraphicBase
    {
        public DrawingGroup drawing = new();
        public Color BackgroundBrush { get; set; }
        public Color BorderBrush { get; set; }
        public double BorderThickness { get; set; }

        public void DrawCircle(Point center, float rad)
        {
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(BackgroundBrush),
                                 new Pen(new SolidColorBrush(BorderBrush), BorderThickness),
                                 new EllipseGeometry(center, rad, rad)));
        }
        public void DrawLine(Point a, Point b)
        {
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(BackgroundBrush),
                                                     new Pen(new SolidColorBrush(BorderBrush), BorderThickness),
                                                     new LineGeometry(a, b)));
        }

        public void DrawTriangle(Point Top, Point Left, Point Right)
        {
            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                // Начинаем фигуру с верхнего угла
                geometryContext.BeginFigure(Top, true, true);

                // Определяем линию влево 
                geometryContext.LineTo(Left, true, false);

                // Определяем линию вправо
                geometryContext.LineTo(Right, true, false);

                // Завершаем фигуру
                geometryContext.Close();
            }
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(BackgroundBrush),
                                                     new Pen(new SolidColorBrush(BorderBrush), BorderThickness),
                                                     streamGeometry));
        }

        public void DrawRectangle(Point TopRight, Point TopLeft, Point BottomLeft, Point BottomRight)
        {
            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                // Начинаем фигуру с верхнего правого угла 
                geometryContext.BeginFigure(TopRight, true, true);

                // Определяем линии против часовой стрелки
                geometryContext.LineTo(TopLeft, true, false);
                geometryContext.LineTo(BottomLeft, true, false);
                geometryContext.LineTo(BottomRight, true, false);

                // Завершаем фигуру
                geometryContext.Close();
            }
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(BackgroundBrush),
                                                     new Pen(new SolidColorBrush(BorderBrush), BorderThickness),
                                                     streamGeometry));
        }
    }


    public MainWindow()
    {
        Document = new Document("output", "", 1095, 720, Color.FromRgb(227, 213, 202));

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
        var drawing = new DrawAssistant();
        figure.Draw(drawing);
        Screen.Source = new DrawingImage(drawing.drawing);
    }


    private void Click_Triangle(object sender, RoutedEventArgs e)
    {
        // TODO Получить точки 
        Point Top = new Point(500, 100); // <--
        Point Left = new Point(100, 800); // <--
        Point Right = new Point(1000, 800); // <--
        // Вызвать соответствующий конструктор 
        var Figure = FigureBase.CreateTriangle(Top, Left, Right);

        // TODO Получить цвет заливки и контура, тощину контура
        // если заливки нет (фигура прозрачная) VisualFigure.BackgroundBrush = Color.FromArgb(0, r, g, b);
        var VisualFigure = VisualGeometryFactory.CreateVisualGeometry(Figure.GetType().Name, Figure);
        VisualFigure.BackgroundBrush = Color.FromArgb(0, 1, 1, 1); // <-- цвет заливки
        VisualFigure.BorderBrush = Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue); // <-- цвет контура
        VisualFigure.BorderThickness = 1; // <-- толщина контура

        // перед каждым изменением Document.VisualGeometries выполнять JSON.JSONPUSH(Document, JsonStack);
        // JSON.JSONPUSH(Document, JsonStack);
        Document.VisualGeometries.Add(VisualFigure);
    }

    private void Click_Rectangle(object sender, RoutedEventArgs e)
    {

    }

    private void Click_Move(object sender, RoutedEventArgs e)
    {

    }

    private void Click_Rotate(object sender, RoutedEventArgs e)
    {

    }

    private void Click_Bucket(object sender, RoutedEventArgs e)
    {

    }

    private void Click_Resize(object sender, RoutedEventArgs e)
    {

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
