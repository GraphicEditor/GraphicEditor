using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Svg;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using Logic;
using PdfSharp;
using System.Xml.Linq;

namespace IO
{
    public static class PDF
    {
        public static PdfDocument CreatePdfDocument(Document document)
        {
            
            // Создаем новый PDF документ
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage page = pdfDocument.AddPage();
            page.Width = document.Width;
            page.Height = document.Height;

            // Используем XGraphics для отрисовки SVG на странице PDF
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // background
            XRect rectangle = new XRect(0, 0, document.Width, document.Height); // Координаты и размеры прямоугольника
            XPen pen = new XPen(XColor.FromArgb(document.BackgroundColor.R, document.BackgroundColor.G, document.BackgroundColor.B), 1);
            XSolidBrush brush = new XSolidBrush(XColor.FromArgb(document.BackgroundColor.R, document.BackgroundColor.G, document.BackgroundColor.B));
            gfx.DrawRectangle(pen, brush, rectangle);

            foreach (var figure in document.VisualGeometries)
            {
                ConvertToPvgElement(gfx, figure, document);
            }

            return pdfDocument;
        }

        public static void ConvertToPvgElement(XGraphics gfx, IVisualGeometry element, Document document)
        {
            switch (element.Name)
            {
                case "Circle":
                    Circle(gfx, element, document);
                    break;
                case "Triangle":
                    Triangle(gfx, element, document);
                    break;
                case "Rectangle":
                    Rectangle(gfx, element, document);
                    break;
                default:
                    throw new Exception();
            }
        }

        private static void Circle(XGraphics gfx, IVisualGeometry element, Document document)
        {
            double rad = (element.Figure.Parameters["Point on circle"] - element.Figure.Origin).Length;
            XRect circleRect = new XRect(element.Figure.Origin.X - rad,
                                         element.Figure.Origin.Y - rad, 
                                         2*rad, 2 * rad);
            XPen pen = new XPen(XColor.FromArgb(element.BorderBrush.A, element.BorderBrush.R, element.BorderBrush.G, element.BorderBrush.B), element.BorderThickness);
            XSolidBrush brush = new XSolidBrush(XColor.FromArgb(element.BackgroundBrush.A, element.BackgroundBrush.R, element.BackgroundBrush.G, element.BackgroundBrush.B));
            gfx.DrawEllipse(pen, brush, circleRect);
        }

        private static void Triangle(XGraphics gfx, IVisualGeometry element, Document document)
        {
            XPoint[] points = new XPoint[]
            {
                new XPoint(element.Figure.Parameters["Top Point"].X,element.Figure.Parameters["Top Point"].Y), // Вершина 1
                new XPoint(element.Figure.Parameters["Left Point"].X,element.Figure.Parameters["Left Point"].Y), // Вершина 2
                new XPoint(element.Figure.Parameters["Right Point"].X,element.Figure.Parameters["Right Point"].Y)  // Вершина 3
            };
            XPen pen = new XPen(XColor.FromArgb(element.BorderBrush.A, element.BorderBrush.R, element.BorderBrush.G, element.BorderBrush.B), element.BorderThickness);
            XSolidBrush brush = new XSolidBrush(XColor.FromArgb(element.BackgroundBrush.A, element.BackgroundBrush.R, element.BackgroundBrush.G, element.BackgroundBrush.B));
            gfx.DrawPolygon(pen, brush, points, XFillMode.Winding);
        }

        private static void Rectangle(XGraphics gfx, IVisualGeometry element, Document document)
        {
            XPoint[] points = new XPoint[]
            {
                new XPoint(element.Figure.Parameters["Top Right"].X,element.Figure.Parameters["Top Right"].Y), // Вершина 1
                new XPoint(element.Figure.Parameters["Top Left"].X,element.Figure.Parameters["Top Left"].Y), // Вершина 2
                new XPoint(element.Figure.Parameters["Bottom Left"].X,element.Figure.Parameters["Bottom Left"].Y),  // Вершина 3
                new XPoint(element.Figure.Parameters["Bottom Right"].X,element.Figure.Parameters["Bottom Right"].Y)  // Вершина 4
            };
            XPen pen = new XPen(XColor.FromArgb(element.BorderBrush.A, element.BorderBrush.R, element.BorderBrush.G, element.BorderBrush.B), element.BorderThickness);
            XSolidBrush brush = new XSolidBrush(XColor.FromArgb(element.BackgroundBrush.A, element.BackgroundBrush.R, element.BackgroundBrush.G, element.BackgroundBrush.B));
            gfx.DrawPolygon(pen, brush, points, XFillMode.Winding);
        }

        public static void SavePdfDoucument(Document document, PdfDocument pdfDocument)
        {
            string pdfFilePath = document.Path + document.Name + ".pdf";
            pdfDocument.Save(pdfFilePath);
        }
    }
}
