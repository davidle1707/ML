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
    public class Canvas3DPropertySet : PropertySetBase
    {
        #region [ Construtor ]
        public Canvas3DPropertySet()
        {
            canvasBgColor = null;
            canvasBaseColor = null;
            canvasBaseDepth = null;
            canvasBgDepth = null;
            showCanvasBg = null;
            showCanvasBase = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private string canvasBgColor;
        [GraphElement]
        private string canvasBaseColor;
        [GraphElement]
        private int? canvasBaseDepth;
        [GraphElement]
        private int? canvasBgDepth;
        [GraphElement]
        private bool? showCanvasBg;
        [GraphElement]
        private bool? showCanvasBase;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "HexColorCode" : This attribute helps you set the background color of the canvas. The background of the canvas is the one behind the columns.
        /// </summary>
        public string CanvasBgColor
        {
            get
            {
                return canvasBgColor;
            }
            set
            {

                canvasBgColor = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : This attribute helps you set the color of the canvas base. The canvas abse is the on which the base of the columns are placed..
        /// </summary>
        public string CanvasBaseColor
        {
            get
            {
                return canvasBaseColor;
            }
            set
            {

                canvasBaseColor = value;
            }
        }

        /// <summary>
        /// "Numerical Value" : This attribute helps you set the height (3D Depth) of the canvas base.
        /// </summary>
        public int CanvasBaseDepth
        {
            get
            {
                if (canvasBaseDepth == null)
                    throw new NullReferenceException("A propriedade CanvasBaseDepth não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(canvasBaseDepth);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade CanvasBaseDepth deve ser um valor inteiro entre 0 e 100.");

                canvasBaseDepth = value;
            }
        }

        /// <summary>
        /// "Numerical Value" : This attribute helps you set the 3D Depth of the canvas background.
        /// </summary>
        public int CanvasBgDepth
        {
            get
            {
                if (canvasBgDepth == null)
                    throw new NullReferenceException("A propriedade CanvasBgDepth não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(canvasBaseDepth);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade CanvasBgDepth deve ser um valor inteiro entre 0 e 100.");

                canvasBgDepth = value;
            }
        }

        /// <summary>
        /// This attribute helps us set whether we need to show the canvas background.
        /// </summary>
        public bool ShowCanvasBg
        {
            get
            {
                if (showCanvasBg == null)
                    throw new NullReferenceException("A propriedade ShowCanvasBg não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showCanvasBg);
            }
            set
            {
                showCanvasBg = value;
            }
        }

        /// <summary>
        /// This attribute helps us set whether we need to show the canvas base.
        /// </summary>
        public bool ShowCanvasBase
        {
            get
            {
                if (showCanvasBase == null)
                    throw new NullReferenceException("A propriedade showCanvasBase não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showCanvasBase);
            }
            set
            {
                showCanvasBase = value;
            }
        }

        #endregion
    }
}
