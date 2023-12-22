/**
 * 
 *    Libero API for Fusion Charts to Asp.Net (Webforms & MVC)
 *    by: Roberto Barbedo (November, 2010) 
 *    email: r_barbedo@yahoo.com.br
 * 
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart
{
    /// <summary>
    /// Cada elemento 'Multi Series Set Element' representa uma disposição de dados no eixo Y
    /// dentro da serie 'DataSet' onde ele está contido.
    /// </summary>
    public class MSSetElement
    {
        public MSSetElement()
        {
            //Default values
            this.Value = 0;
            this.Alpha = 80;
        }

        public MSSetElement(decimal value)
        {
            //Default values
            this.Value = value;
            this.Alpha = 80;
        }

        [SetElementMS]
        public decimal Value { get; set; }
        [SetElementMS]
        public string Color { get; set; }
        [SetElementMS]
        public string Link { get; set; }
        [SetElementMS]
        public int Alpha { get; set; }
    }
}