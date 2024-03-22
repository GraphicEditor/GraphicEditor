using System;
using System.Collections.Generic;
using System.Xml;
using Svg;

using Logic;
using Geometry;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Documents;
using System.Xml.Linq;

namespace IO
{
    public static class SVG
    {
        public static SvgDocument CreateSvgDocument(Document document)
        {
            SvgDocument svgDocument = new SvgDocument();

            svgDocument.Width = document.Width;
            svgDocument.Height = document.Height;

            // background 
            var backgroundRect = new SvgRectangle
            {
                X = new SvgUnit(SvgUnitType.Pixel, 0),
                Y = new SvgUnit(SvgUnitType.Pixel, 0),
                Width = document.Width,
                Height = document.Height,
                Fill = new SvgColourServer(System.Drawing.Color.FromArgb(document.BackgroundColor.r, document.BackgroundColor.g, document.BackgroundColor.b)),
            };
            svgDocument.Children.Add(backgroundRect);

            foreach (var figure in document.VisualGeometries)
            {
                svgDocument.Children.Add(ConvertToSvgElement(figure, document));
            }
            return svgDocument;
        }

        public static void SaveSvgDoucument(Document document, SvgDocument svgDocument)
        {

            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(document.Path+document.Name, System.Text.Encoding.UTF8))
            {
                xmlTextWriter.Formatting = Formatting.Indented;
                svgDocument.Write(xmlTextWriter);
            }
        }

        public static SvgElement ConvertToSvgElement(IVisualGeometry element, Document document)
        {
            switch (element.Name)
            {
                case "Circle":
                    return Circle(element, document);
                case "Triangle":
                    return Triangle(element, document);
                default:
                    throw new Exception();
            }
        }

        private static SvgCircle Circle(IVisualGeometry element, Document document)
        {
            var SvgCircleCoord = LocalCoordToSvgCoord(element.Figure.Origin, document);
            var circle = new SvgCircle
            {
                CenterX = new SvgUnit(SvgUnitType.Pixel, (float)SvgCircleCoord.X),
                CenterY = new SvgUnit(SvgUnitType.Pixel, (float)SvgCircleCoord.Y),
                Radius = new SvgUnit(SvgUnitType.Pixel, (float)(element.Figure.Parameters["Point on circle"] - element.Figure.Origin).Length),
                Fill = new SvgColourServer(System.Drawing.Color.FromArgb(element.BackgroundBrush.r, element.BackgroundBrush.g, element.BackgroundBrush.b)),
                Stroke = new SvgColourServer(System.Drawing.Color.FromArgb(element.BorderBrush.r, element.BorderBrush.g, element.BorderBrush.b)),
                StrokeWidth = (float)element.BorderThickness

            };
            return circle;
        }

        private static SvgPolygon Triangle(IVisualGeometry element, Document document)
        {
            var SvgTriangleCoordTop = LocalCoordToSvgCoord(element.Figure.Parameters["Top Point"], document);
            var SvgTriangleCoordLeft = LocalCoordToSvgCoord(element.Figure.Parameters["Left Point"], document);
            var SvgTriangleCoordRight = LocalCoordToSvgCoord(element.Figure.Parameters["Right Point"], document);
            var triangle = new SvgPolygon
            {
                Points = new SvgPointCollection
                {
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordTop.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordTop.Y),
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordLeft.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordLeft.Y),
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordRight.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordRight.Y)
                },
                Fill = new SvgColourServer(System.Drawing.Color.FromArgb(element.BackgroundBrush.r, element.BackgroundBrush.g, element.BackgroundBrush.b)),
                Stroke = new SvgColourServer(System.Drawing.Color.FromArgb(element.BorderBrush.r, element.BorderBrush.g, element.BorderBrush.b)),
                StrokeWidth = (float)element.BorderThickness
            };
            return triangle;
        }

        private static Point LocalCoordToSvgCoord(Point point, Document document) => new Point(point.X + document.Width / 2, point.Y + document.Height / 2);

        public static Document doc_create()
        {
            var document = new Document();
            document.Name = "output.SVG";
            document.Width = 800;
            document.Height = 450;
            document.Path = "";
            document.BackgroundColor = new Color(169, 169, 169);
            
            Circle a = new Circle();
            a.Parameters["Center"] = new Point(10, 10);
            a.Parameters["Point on circle"] = new Point(100, 100);
            var _a = new VisualGeometry("Circle", a);
            _a.BackgroundBrush = new Color(255, 0, 0);
            _a.BorderBrush = new Color(255, 255, 0);
            _a.BorderThickness = 10; 

            List<IVisualGeometry> aaa = new List<IVisualGeometry>();
            aaa.Add(_a);


            document.VisualGeometries = aaa;


            return document;
        }


        public static void ttt(Document document)
        {
            var a = CreateSvgDocument(document);
            SaveSvgDoucument(document, a);

        }
    }
}
