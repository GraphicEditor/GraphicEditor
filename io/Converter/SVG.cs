using System;
using System.Collections.Generic;
using System.Xml;
using Svg;

using Logic;

namespace IO {

public static class SVG
{
    public static SvgDocument CreateSvgDocument(Idocument document)
    {
        SvgDocument svgDocument = new SvgDocument();

        svgDocument.Width = document.Width;
        svgDocument.Height = document.Height;

        foreach (var figure in document.Figures)
        {
            //
        }

        return svgDocument;
    }

    public static void SaveSvgDoucument(Idocument document, SvgDocument svgDocument)
    {
        using (XmlTextWriter xmlTextWriter = new XmlTextWriter(document.Path, System.Text.Encoding.UTF8))
        {
            xmlTextWriter.Formatting = Formatting.Indented;
            svgDocument.Write(xmlTextWriter);
        }
    }
}
}