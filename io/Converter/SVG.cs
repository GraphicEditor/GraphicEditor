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
using System.Windows.Media;

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
                Fill = new SvgColourServer(System.Drawing.Color.FromArgb(document.BackgroundColor.R, document.BackgroundColor.G, document.BackgroundColor.B)),
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

            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(document.Path + document.Name + ".SVG", System.Text.Encoding.UTF8))
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
                case "Rectangle":
                    return Rectangle(element, document);
                default:
                    throw new Exception();
            }
        }

        private static SvgCircle Circle(IVisualGeometry element, Document document)
        {
            var SvgCircleCoord = element.Figure.Origin;
            var circle = new SvgCircle
            {
                CenterX = new SvgUnit(SvgUnitType.Pixel, (float)SvgCircleCoord.X),
                CenterY = new SvgUnit(SvgUnitType.Pixel, (float)SvgCircleCoord.Y),
                Radius = new SvgUnit(SvgUnitType.Pixel, (float)(element.Figure.Parameters["Point on circle"] - element.Figure.Origin).Length),
                Fill = new SvgColourServer(System.Drawing.Color.FromArgb(element.BackgroundBrush.A, element.BackgroundBrush.R, element.BackgroundBrush.G, element.BackgroundBrush.B)),
                Stroke = new SvgColourServer(System.Drawing.Color.FromArgb(element.BorderBrush.A, element.BorderBrush.R, element.BorderBrush.G, element.BorderBrush.B)),
                StrokeWidth = (float)element.BorderThickness

            };
            return circle;
        }

        private static SvgPolygon Triangle(IVisualGeometry element, Document document)
        {
            var SvgTriangleCoordTop = element.Figure.Parameters["Top Point"];
            var SvgTriangleCoordLeft = element.Figure.Parameters["Left Point"];
            var SvgTriangleCoordRight = element.Figure.Parameters["Right Point"];
            var triangle = new SvgPolygon
            {
                Points = new SvgPointCollection
                {
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordTop.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordTop.Y),
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordLeft.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordLeft.Y),
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordRight.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgTriangleCoordRight.Y)
                },
                Fill = new SvgColourServer(System.Drawing.Color.FromArgb(element.BackgroundBrush.A, element.BackgroundBrush.R, element.BackgroundBrush.G, element.BackgroundBrush.B)),
                Stroke = new SvgColourServer(System.Drawing.Color.FromArgb(element.BorderBrush.A, element.BorderBrush.R, element.BorderBrush.G, element.BorderBrush.B)),
                StrokeWidth = (float)element.BorderThickness
            };
            return triangle;
        }

        private static SvgPolygon Rectangle(IVisualGeometry element, Document document)
        {
            var SvgRectangleCoordTopRight = element.Figure.Parameters["Top Right"];
            var SvgRectangleCoordTopLeft = element.Figure.Parameters["Top Left"];
            var SvgRectangleCoordBottomLeft = element.Figure.Parameters["Bottom Left"];
            var SvgRectangleCoordBottomRight = element.Figure.Parameters["Bottom Right"];
            var rectangle = new SvgPolygon
            {
                Points = new SvgPointCollection
                {
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordTopRight.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordTopRight.Y),
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordTopLeft.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordTopLeft.Y),
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordBottomLeft.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordBottomLeft.Y),
                    new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordBottomRight.X), new SvgUnit(SvgUnitType.Pixel, (float)SvgRectangleCoordBottomRight.Y)
                },
                Fill = new SvgColourServer(System.Drawing.Color.FromArgb(element.BackgroundBrush.A, element.BackgroundBrush.R, element.BackgroundBrush.G, element.BackgroundBrush.B)),
                Stroke = new SvgColourServer(System.Drawing.Color.FromArgb(element.BorderBrush.A, element.BorderBrush.R, element.BorderBrush.G, element.BorderBrush.B)),
                StrokeWidth = (float)element.BorderThickness
            };
            return rectangle;
        }

        private static Point LocalCoordToSvgCoord(Point point, Document document) => new Point(point.X, point.Y); //=> new Point(point.X + document.Width / 2, point.Y + document.Height / 2);
    }
}
