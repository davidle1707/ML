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
    /// <summary>
    /// Anchors (or the marker points) are the polygons which appear at the joint of two consecutive lines. On a line chart, the anchors are the elements which react to the hover caption and link for that particular data point.
    /// </summary>
    public class AnchorPropertySet : PropertySetBase
    {
        #region [ Construtor ]
        public AnchorPropertySet()
        {
            showAnchors = null;
            anchorSides = null;
            anchorRadius = null;
            anchorBorderColor = null;
            anchorBorderThickness = null;
            anchorBgColor = null;
            anchorBgAlpha = null;
            anchorAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private bool? showAnchors;
        [GraphElement]
        private int? anchorSides;
        [GraphElement]
        private int? anchorRadius;
        [GraphElement]
        private string anchorBorderColor;
        [GraphElement]
        private int? anchorBorderThickness;
        [GraphElement]
        private string anchorBgColor;
        [GraphElement]
        private int? anchorBgAlpha;
        [GraphElement]
        private int? anchorAlpha;
        #endregion

        #region [ Propriedades Publicas ]
        /// <summary>
        /// Configuration whether the anchors would be shown on the chart or not. If the anchors are not shown, then the hover caption and link functions won't work.
        /// </summary>
        public bool ShowAnchors
        {
            get
            {
                if (showAnchors == null)
                    throw new NullReferenceException("A propriedade showAnchors não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showAnchors);
            }
            set
            {
                showAnchors = value;
            }
        }

        /// <summary>
        /// "Numeric Value greater than 3": This attribute sets the number of sides the anchor will have. For e.g., an anchor with 3 sides would represent a triangle, with 4 it would be a square and so on.
        /// </summary>
        public int AnchorSides
        {
            get
            {
                if (anchorSides == null)
                    throw new NullReferenceException("A propriedade anchorSides não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(anchorSides);
            }
            set
            {
                if (value < 3)
                    throw new ArgumentOutOfRangeException("A propriedade anchorSides deve ser maior que 3.");
                
                anchorSides = value;
            }
        }

        /// <summary>
        /// "Numeric Value" : This attribute sets the radius (in pixels) of the anchor. Greater the radius, bigger would be the anchor size.
        /// </summary>
        public int AnchorRadius
        {
            get
            {
                if (anchorRadius == null)
                    throw new NullReferenceException("A propriedade anchorRadius não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(anchorRadius);
            }
            set
            {
                anchorRadius = value;
            }
        }

        /// <summary>
        /// "Hex Code" : Border Color of the anchor.
        /// </summary>
        public string AnchorBorderColor
        {
            get { return anchorBorderColor; }
            set { anchorBorderColor = value; }
        }

        /// <summary>
        /// "Numeric Value" : Thickness of the anchor border (in pixels).
        /// </summary>
        public int AnchorBorderThickness
        {
            get
            {
                if (anchorBorderThickness == null)
                    throw new NullReferenceException("A propriedade anchorBorderThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(anchorBorderThickness);
            }
            set
            {
                anchorBorderThickness = value;
            }
        }

        /// <summary>
        /// "Hex Code" : Background color of the anchor.
        /// </summary>
        public string AnchorBgColor
        {
            get { return anchorBgColor; }
            set { anchorBgColor = value; }
        }

        /// <summary>
        /// "Numeric Value" : Alpha of the anchor background.
        /// </summary>
        public int AnchorBgAlpha
        {
            get
            {
                if (anchorBgAlpha == null)
                    throw new NullReferenceException("A propriedade anchorBgAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(anchorBgAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                anchorBgAlpha = value;
            }
        }

        /// <summary>
        /// "Numeric Value" : This function lets you set the tranparency of the entire anchor (including the border). This attribute is particularly useful, when you do not want the anchors to be visible on the chart, but you want the hover caption and link functionality. In that case, you can set anchorAlpha to 0.
        /// </summary>
        public int AnchorAlpha
        {
            get
            {
                if (anchorAlpha == null)
                    throw new NullReferenceException("A propriedade anchorAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(anchorAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorAlpha deve ser um valor inteiro entre 0 e 100.");

                anchorAlpha = value;
            }
        }
        #endregion
    }
}
