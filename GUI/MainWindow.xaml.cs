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
    class DrawAssistant:IGraphicBase
    {
        public  DrawingGroup drawing = new();
        public void DrawLine(Point a, Point b)
        {
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(Colors.Black),new Pen(new SolidColorBrush(Colors.Red),1), new LineGeometry(a, b)));
        }
        public void DrawCircle(Point center, float rad)
        {
            drawing.Children.Add(new GeometryDrawing(new SolidColorBrush(Colors.Black), new Pen(new SolidColorBrush(Colors.Red), 1),
                new EllipseGeometry(center,rad,rad)));
        }
    }


    public MainWindow()
    {
        InitializeComponent();
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

}

    private void Click_Triangle(object sender, RoutedEventArgs e)
    {


    }

    private void Click_Rectangle(object sender, RoutedEventArgs e)
    {

    }

    private void Click_Polyline(object sender, RoutedEventArgs e)
    {

    }

}
