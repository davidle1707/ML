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
    public class BarsVisibilityPropertySet : PropertySetBase
    {
        #region[ Construtor ]
        public BarsVisibilityPropertySet()
        {
            showLimits = null;
            rotateNames = null;
            showBarShadow = null;
            showColumnShadow = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private bool? showLimits;
        [GraphElement]
        private bool? rotateNames;
        [GraphElement]
        private bool? showBarShadow;
        [GraphElement]
        private bool? showColumnShadow;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// Option whether to show/hide the chart limit textboxes.
        /// </summary>
        public bool ShowLimits
        {
            get
            {
                if (showLimits == null)
                    throw new NullReferenceException("A propriedade showLimits não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showLimits);
            }
            set
            {
                showLimits = value;
            }
        }

        /// <summary>
        /// Configuration that sets whether the category name text boxes would be rotated or not.
        /// </summary>
        public bool RotateNames
        {
            get
            {
                if (rotateNames == null)
                    throw new NullReferenceException("A propriedade rotateNames não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(rotateNames);
            }
            set
            {
                rotateNames = value;
            }
        }

        /// <summary>
        /// Whether the 2D shadow for the bars would be shown or not.
        /// </summary>
        public bool ShowBarShadow
        {
            get
            {
                if (showBarShadow == null)
                    throw new NullReferenceException("A propriedade showBarShadow não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showBarShadow);
            }
            set
            {
                showBarShadow = value;
            }
        }

        /// <summary>
        ///  Whether the 2D shadow for the columns would be shown or not.
        /// </summary>
        public bool ShowColumnShadow
        {
            get
            {
                if (showColumnShadow == null)
                    throw new NullReferenceException("A propriedade showColumnShadow não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showColumnShadow);
            }
            set
            {
                showColumnShadow = value;
            }
        }

        #endregion
    }
}
