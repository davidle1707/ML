using System;
using System.IO;
using iTextSharpV5510.text;
using iTextSharpV5510.text.pdf;
using iTextSharpV5510.tool.xml;
using iTextSharpV5510.tool.xml.css;
using iTextSharpV5510.tool.xml.html;
using iTextSharpV5510.tool.xml.parser;
using iTextSharpV5510.tool.xml.pipeline.css;
using iTextSharpV5510.tool.xml.pipeline.end;
using iTextSharpV5510.tool.xml.pipeline.html;

namespace ML.Utils.Pdf
{
    public class PdfBuilder
    {
        public static byte[] FromHtml(string html, Rectangle pageSize, Action<ICSSResolver> processCss = null)
        {
            var doc = new Document(pageSize);

            return FromHtmlProcessing(html, doc, processCss);
        }

        public static byte[] FromHtml(string html, Rectangle pageSize, Margin margin, Action<ICSSResolver> processCss = null)
        {
            var doc = new Document(pageSize, margin.Left, margin.Right, margin.Top, margin.Bottom);

            return FromHtmlProcessing(html, doc, processCss);
        }

        public static byte[] FromHtmlProcessing(string html, Document doc, Action<ICSSResolver> processCss)
        {
            var pdf = new ByteBuffer();

            var writer = PdfWriter.GetInstance(doc, pdf);
            writer.CloseStream = false;

            doc.Open();

            var htmlContext = new HtmlPipelineContext(null);
            htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

            var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);

            processCss?.Invoke(cssResolver);

            var pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(doc, writer)));

            var worker = new XMLWorker(pipeline, true);
            var parser = new XMLParser(worker);

            using (var sr = new StringReader(html))
            {
                parser.Parse(sr);
            }

            doc.Close();

            return pdf.ToByteArray();
        }
    }
}
