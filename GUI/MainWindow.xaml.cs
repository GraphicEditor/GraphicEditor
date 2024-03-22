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
using IO;

namespace GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public Document Document { get; set; }
    public int CurrentFigure {  get; set; }
    Stack<string> JsonStack = new Stack<string>();
    private DrawAssistant drawingAssistant = new DrawAssistant(); // Объявление переменной в классе MainWindow

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
        //Document.VisualGeometries.Clear();
        // TODO Получить точки 
        Point PCenter = new Point(100, 100); // <--
        Point POnCircle = new Point(200, 100); // <--
        // Вызвать соответствующий конструктор 
        var Figure = FigureBase.CreateCircle(PCenter, POnCircle);

        // TODO Получить цвет заливки и контура, тощину контура
        // если заливки нет (фигура прозрачная) VisualFigure.BackgroundBrush = Color.FromArgb(0, r, g, b);
        var VisualFigure = VisualGeometryFactory.CreateVisualGeometry(Figure.GetType().Name, Figure);
        VisualFigure.BackgroundBrush = Color.FromArgb(255, 255, 0, 0); // <-- цвет заливки
        VisualFigure.BorderBrush = Color.FromArgb(255, 0, 255, 0); // <-- цвет контура
        VisualFigure.BorderThickness = 1; // <-- толщина контура

        // перед каждым изменением Document.VisualGeometries выполнять  
         
        Document.VisualGeometries.Add(VisualFigure);
        Draw();
    }

    private void Click_Triangle(object sender, RoutedEventArgs e)
    {
        //Document.VisualGeometries.Clear();
        // TODO Получить точки 
        Point Top = new Point(500, 100); // <--
        Point Left = new Point(100, 800); // <--
        Point Right = new Point(1000, 800); // <--
        // Вызвать соответствующий конструктор 
        var Figure = FigureBase.CreateTriangle(Top, Left, Right);

        // TODO Получить цвет заливки и контура, тощину контура
        // если заливки нет (фигура прозрачная) VisualFigure.BackgroundBrush = Color.FromArgb(0, r, g, b);
        var VisualFigure = VisualGeometryFactory.CreateVisualGeometry(Figure.GetType().Name, Figure);
        VisualFigure.BackgroundBrush = Color.FromArgb(255, 255, 0, 0); // <-- цвет заливки
        VisualFigure.BorderBrush = Color.FromArgb(255, 0, 255, 0); // <-- цвет контура
        VisualFigure.BorderThickness = 1; // <-- толщина контура

        // перед каждым изменением Document.VisualGeometries выполнять  
         
        Document.VisualGeometries.Add(VisualFigure);
        Draw();
    }

    private void Click_Rectangle(object sender, RoutedEventArgs e)
    {
        //Document.VisualGeometries.Clear();
        Point a = new Point(10, 600); // <--
        Point d = new Point(700, 700); // <--
        var Figure2 = FigureBase.CreateRectangle(a, d);

        var VisualFigure2 = VisualGeometryFactory.CreateVisualGeometry(Figure2.GetType().Name, Figure2);
        VisualFigure2.BackgroundBrush = Color.FromArgb(128, 99, 0, 255); // <--
        VisualFigure2.BorderBrush = Color.FromArgb(255, 255, 99, 0); // <--
        VisualFigure2.BorderThickness = 1; // <--
        Document.VisualGeometries.Add(VisualFigure2);
        Draw();
         
    }

    private void Click_Polyline(object sender, RoutedEventArgs e)
    {
        //var points = new List<Point> { new Point(0, 0), new Point(50, 50), new Point(100, 0) };
        //var figure = FigureBase.CreatePolyline(points);

        //// Добавляем созданную фигуру в список Figures
        //drawingAssistant.Figures.Add(figure);

        //// Вызываем метод Draw для отображения фигуры
        //figure.Draw(drawingAssistant);

        //// Обновляем отображение на экране
        //Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Move(object sender, RoutedEventArgs e)
    {
        // TODO Получить точки 
        Point MoveTo = new Point(700, 700); // <--

        // Есть два варианта 1 - движемся по вектору (MoveTo.X, MoveTo.Y)
        //                   2 - движемся в точку (MoveTo.X, MoveTo.Y)
        // если 1, то функцию Move нужно вызывать: Figure.Move(MoveTo)
        // если 2: Point Center = Figure.GetCenter()
        //         Figure.Move(new Point(MoveTo.X - Center.X, MoveTo.Y - Center.Y))
        CurrentFigure = 0; // TODO REMOVE
        if ((CurrentFigure != -1) && (CurrentFigure < Document.VisualGeometries.Count))
        {
             
            Point Center = Document.VisualGeometries[CurrentFigure].Figure.GetCenter();
            Document.VisualGeometries[CurrentFigure].Figure.Move(new Point(50,50));
            Draw();
        }
    }

    private void Click_Rotate(object sender, RoutedEventArgs e)
    {
        // TODO Получить угол (в градусах)
        float Angle = 10;
        CurrentFigure = 0; // TODO REMOVE
        if ((CurrentFigure != -1) && (CurrentFigure < Document.VisualGeometries.Count))
        {
             
            Point Center = Document.VisualGeometries[CurrentFigure].Figure.GetCenter();
            Document.VisualGeometries[CurrentFigure].Figure.Rotate(Angle);
            Draw();
        }
    }

    private void Click_Bucket(object sender, RoutedEventArgs e)
    {
        //var fillBrush = new SolidColorBrush(Colors.Red);
        //drawingAssistant.Fill(fillBrush);
        //Screen.Source = new DrawingImage(drawingAssistant.GetDrawing());
    }

    private void Click_Resize(object sender, RoutedEventArgs e)
    {
         
        Document.VisualGeometries[0].Figure.Scale((float)0.9);
        Draw();
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
    public void Draw()
    {
        var drawing = new DrawAssistant();
        foreach (var figure in Document.VisualGeometries)
        {
            drawing.BackgroundBrush = figure.BackgroundBrush;
            drawing.BorderBrush = figure.BorderBrush;
            drawing.BorderThickness = figure.BorderThickness;
            figure.Figure.Draw(drawing);
        }
        Screen.Source = new DrawingImage(drawing.drawing);
    }
}
