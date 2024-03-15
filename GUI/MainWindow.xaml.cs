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


    }

    private void Click_Rectangle(object sender, RoutedEventArgs e)
    {

    }

    private void Click_Polyline(object sender, RoutedEventArgs e)
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
}
