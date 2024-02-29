using System.IO;

namespace IO
{
    public static class ExportFile
    {
        public static void ToPNG(string filename, IDocument document)
        {
            FileValidator.CheckParentDirectory(filename);
            FileValidator.CheckExtension(filename, FileValidator.PNG_EXTENSION);
            var svgDoc = SVG.GetSvgDocumentFromDocument(document);
            svgDoc.Draw().Save(filename, ImageFormat.Png);
        }
    }
}