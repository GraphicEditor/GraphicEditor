using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Logic;
using System.Drawing.Imaging;
using System.Drawing;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Media3D;

namespace IO
{
    public static class Rust
    {
        // PNGsave(Screen, Document)
        public static void PNGsave(FrameworkElement visual, Document document) 
        {
            // Создание RenderTargetBitmap для сохранения содержимого элемента
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)visual.Width, (int)visual.Height, 96d, 96d, PixelFormats.Default);
            renderBitmap.Render(visual);

            // Создание кодировщика для формата PNG
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // Сохранение изображения в файл
            using (FileStream file = File.Create(document.Path + document.Name + ".PNG"))
            {
                encoder.Save(file);
            }
        }

        // PNGsave(Screen, Document)
        public static void Jpegsave(FrameworkElement visual, Document document)
        {
            // Создание RenderTargetBitmap для сохранения содержимого элемента
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)visual.Width, (int)visual.Height, 96d, 96d, PixelFormats.Default);
            renderBitmap.Render(visual);

            // Создание кодировщика для формата JPEG
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // Сохранение изображения в файл
            using (FileStream file = File.Create(document.Path + document.Name + ".jpg"))
            {
                encoder.Save(file);
            }
        }

        public static void Gifsave(FrameworkElement visual, Document document)
        {
            // Создание RenderTargetBitmap для сохранения содержимого элемента
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)visual.Width, (int)visual.Height, 96d, 96d, PixelFormats.Default);
            renderBitmap.Render(visual);

            // Создание кодировщика для формата GIF
            GifBitmapEncoder encoder = new GifBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // Сохранение изображения в файл
            using (FileStream file = File.Create(document.Path + document.Name + ".gif"))
            {
                encoder.Save(file);
            }
        }
    }
}
