using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using Svg.Transforms;

namespace IO
{
    public static class SVG
    {
        /// <summary>
        /// Переводит Idocument в SVG-строку
        /// </summary>
        /// <param name="document">документ в формате IDocument</param>
        /// <returns>SvgDoc</returns>
        public static SvgDocument GetSvgDocumentFromDocument(object document)
        {
            // Создадим документ
            SvgDocument svgDoc = GenerateDocument(document);

            // Добавим в него фигуры
            // В цикле перебираем всем объекты из IDocument
            // Разделяем их по группам
            // Накладываем на них трансформации
            // Возвращаем SVG строку


            SvgGroup group = new SvgGroup();
            svgDoc.Children.Add(group);

            SvgRectangle asdf = new SvgRectangle {
                X = 100,
                Y = 100,
                Width = 200,
                Height = 50,
                Fill = new SvgColourServer(Color.Red),
                Transforms = new SvgTransformCollection()
            };

            asdf.Transforms.Add(new SvgRotate(45));

            group.Children.Add(asdf);

            return svgDoc;
        }

        public static string GetStringFromSvgDocument(SvgDocument svgDoc)
        {
            MemoryStream stream = new MemoryStream();
            svgDoc.Write(stream);
            return Encoding.UTF8.GetString(stream.GetBuffer());
        }

        private static SvgDocument GenerateDocument(object document)
        {
            return new SvgDocument
            {
                Width = 500,
                Height = 500,
                ViewBox = new SvgViewBox(-250, -250, 500, 500)
            };
        }
    }
}
