using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace IO
{
    [Serializable]
    public class BadFileExtensionError : Exception
    {
        public BadFileExtensionError(string message) : base(message)
        { }

        protected BadFileExtensionError(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        { }
    }

    public static class ExportFile
    {
    
        public static void ToSVG(string filename, object document)
        {
            var svgDoc = SVG.GetSvgDocumentFromDocument(document);
            string svgString = SVG.GetStringFromSvgDocument(svgDoc);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(svgString);
            }
        }


        public static void ToPNG(string filename, object document)
        {
            var svgDoc = SVG.GetSvgDocumentFromDocument(document);

            svgDoc.Draw().Save(filename, ImageFormat.Png);
        }

        public static void ToPNGFromBitmap(string filename, Bitmap bitmap)
        {
            bitmap.Save(filename, ImageFormat.Png);
        }

        public static void ToJPEGFromBitmap(string filename, Bitmap bitmap)
        {
            bitmap.Save(filename, ImageFormat.Jpeg);
        }
    }
}