using ML.Utils.FusionChart.Abstract;
using System;
using System.Configuration;
using System.Text;
using System.Web.Mvc;

namespace ML.Utils.FusionChart
{
    public static class FusionChartHtmlHelper
    {
        private static string UrlSWF = ConfigurationManager.AppSettings["FusionChartUrl"] + @"/Charts/";
        private static string PrefixSWF = @"";
        private static string SufixSWF = @".swf";

        /// <summary>
        /// Render a Fusion Chart.
        /// </summary>
        /// <param name="helper">Extension</param>
        /// <param name="controlId">Unique Id for the controler.</param>
        /// <param name="chart">An object. Will be cast to a FusionChartBase type.</param>
        /// <param name="width">Charts width.</param>
        /// <param name="height">Charts height</param>
        /// <returns>HTML to show a Fusion Chart</returns>
        public static MvcHtmlString FChart(this HtmlHelper helper, string controlId, object chart, int width, int height)
        {
            FusionChartBase convChart = (FusionChartBase)chart;
            return RenderChart(controlId, convChart.ToXML(), convChart.ChartType, width, height);
        }

        /// <summary>
        /// Render a Fusion Chart.
        /// </summary>
        /// <param name="helper">Extension</param>
        /// <param name="controlId">Unique Id for the controler.</param>
        /// <param name="chart">An object that inherit from FusionChartBase.</param>
        /// <param name="width">Charts width.</param>
        /// <param name="height">Charts height</param>
        /// <returns>HTML to show a Fusion Chart</returns>
        public static MvcHtmlString FChart(this HtmlHelper helper, string controlId, FusionChartBase chart, int width, int height)
        {
            return RenderChart(controlId, chart.ToXML(), chart.ChartType, width, height);
        }

        /// <summary>
        /// Render a Fusion Chart.
        /// </summary>
        /// <param name="helper">Extension</param>
        /// <param name="controlId">Unique Id for the controler.</param>
        /// <param name="xmlData">Data for the Chart in XML format.</param>
        /// <param name="chartType">Chart type, string format.</param>
        /// <param name="width">Charts width.</param>
        /// <param name="height">Charts height</param>
        /// <returns>HTML to show a Fusion Chart</returns>
        public static MvcHtmlString FChart(this HtmlHelper helper, string controlId, string xmlData, string chartType, int width, int height)
        {
            return RenderChart(controlId, xmlData, chartType, width, height);
        }

        private static MvcHtmlString RenderChart(string controlId, string xmlData, string chartType, int width, int height)
        {
            String sControlId = controlId;
            String sJsVarId = "_lib_JS_" + controlId;
            String sDivId = "_lib_DIV_" + controlId;
            String sObjId = "_lib_OBJ_" + controlId;
            String sWidth = width.ToString();
            String sHeight = height.ToString();


            StringBuilder oBuilder = new StringBuilder();

            oBuilder.AppendLine(@"<div id=""" + sDivId + @""" align=""center""></div>");

            oBuilder.AppendLine(@"<script type=""text/javascript"">");

            oBuilder.AppendLine(@"var " + sControlId + @" = (function() {");
            oBuilder.AppendLine(@"    return {");
            oBuilder.AppendLine(@"        containerId: '" + sDivId + "',");
            oBuilder.AppendLine(@"        xmlData: '',");
            oBuilder.AppendLine(@"        chartType: '',");
            oBuilder.AppendLine(@"        showChart: function() {");
            oBuilder.AppendLine();
            oBuilder.AppendFormat(@"          var chartURL = '{0}' + '{1}' + this.chartType.replace('Chart', '{2}');", UrlSWF, PrefixSWF, SufixSWF);
            oBuilder.AppendLine(@"            var " + sJsVarId + @" = new FusionCharts(chartURL, """ + sObjId + @""", """ + sWidth + @""", """ + sHeight + @""");");
            oBuilder.AppendLine(@"            " + sJsVarId + @".addParam('WMode', 'Transparent');");
            oBuilder.AppendLine(@"            " + sJsVarId + @".setDataXML(this.xmlData);");
            oBuilder.AppendLine(@"            " + sJsVarId + @".render(""" + sDivId + @""");");
            oBuilder.AppendLine(@"        }");
            oBuilder.AppendLine(@"    }");
            oBuilder.AppendLine(@"})();");

            oBuilder.AppendLine(@"setTimeout(function(){");
            oBuilder.AppendLine(@"    " + sControlId + @".xmlData = """ + xmlData.Replace(@"""", @"'") + @""";");
            oBuilder.AppendLine(@"    " + sControlId + @".chartType = """ + chartType + @""";");
            oBuilder.AppendLine(@"    " + sControlId + @".showChart();");
            oBuilder.AppendLine(@"},0);");

            oBuilder.AppendLine(@"</script>");

            return new MvcHtmlString(oBuilder.ToString());
        }
    }
}
