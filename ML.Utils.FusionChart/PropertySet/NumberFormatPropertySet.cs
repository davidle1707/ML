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
using ML.Utils.FusionChart.Abstract;
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart.PropertySet
{
    public class NumberFormatPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public NumberFormatPropertySet()
        {
            numberPrefix = null;
            numberSuffix = null;
            formatNumber = null;
            formatNumberScale = null;
            decimalSeparator = null;
            thousandSeparator = null;
            decimalPrecision = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private string numberPrefix;
        [GraphElement]
        private string numberSuffix;
        [GraphElement]
        private bool? formatNumber;
        [GraphElement]
        private bool? formatNumberScale;
        [GraphElement]
        private string decimalSeparator;
        [GraphElement]
        private string thousandSeparator;
        [GraphElement]
        private int? decimalPrecision;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "$" : Using this attribute, you could add prefix to all the numbers visible on the graph. For example, to represent all dollars figure on the chart, you could specify this attribute to ' $' to show like $40000, $50000.
        /// </summary>
        public string NumberPrefix
        {
            get { return numberPrefix; }
            set { numberPrefix = value; }
        }

        /// <summary>
        /// "p.a" : Using this attribute, you could add prefix to all the numbers visible on the graph. For example, to represent all figure quantified as per annum on the chart, you could specify this attribute to ' /a' to show like 40000/a, 50000/a. 
        /// To use special characters for numberPrefix or numberSuffix, you'll need to URL Encode them. That is, suppose you wish to have numberSuffix as % (like 30%), you'll need to specify it as under:
        /// numberSuffix='%25'
        /// </summary>
        public string NumberSuffix
        {
            get { return numberSuffix; }
            set { numberSuffix = value; }
        }

        /// <summary>
        ///  This configuration determines whether the numbers displayed on the chart will be formatted using commas, e.g., 40,000 if formatNumber='1' and 40000 if formatNumber='0 '
        /// </summary>
        public bool FormatNumber
        {
            get
            {
                if (formatNumber == null)
                    throw new NullReferenceException("A propriedade formatNumber não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(formatNumber);
            }
            set
            {
                formatNumber = value;
            }
        }

        /// <summary>
        ///  Configuration whether to add K (thousands) and M (millions) to a number after truncating and rounding it - e.g., if formatNumberScale is set to 1, 10434 would become 1.04K (with decimalPrecision set to 2 places). Same with numbers in millions - a M will added at the end.
        /// </summary>
        public bool FormatNumberScale
        {
            get
            {
                if (formatNumberScale == null)
                    throw new NullReferenceException("A propriedade formatNumberScale não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(formatNumberScale);
            }
            set
            {
                formatNumberScale = value;
            }
        }

        /// <summary>
        /// "." : This option helps you specify the character to be used as the decimal separator in a number.
        /// </summary>
        public string DecimalSeparator
        {
            get { return decimalSeparator; }
            set { decimalSeparator = value; }
        }

        /// <summary>
        /// "," : This option helps you specify the character to be used as the thousands separator in a number.
        /// </summary>
        public string ThousandSeparator
        {
            get { return thousandSeparator; }
            set { thousandSeparator = value; }
        }

        /// <summary>
        /// Number of decimal places to which all numbers on the chart would be rounded to.
        /// </summary>
        public int DecimalPrecision
        {
            get
            {
                if (decimalPrecision == null)
                    throw new NullReferenceException("A propriedade decimalPrecision não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(decimalPrecision);
            }
            set
            {
                decimalPrecision = value;
            }
        }
        #endregion
    }
}
