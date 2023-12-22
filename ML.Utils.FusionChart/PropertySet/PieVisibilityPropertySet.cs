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
    public class PieVisibilityPropertySet : PropertySetBase
    {
        #region[ Construtor ]
        public PieVisibilityPropertySet()
        {
            showPercentageValues = null;
            showPercentageInLabel = null;
            animation = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private bool? showPercentageValues;
        [GraphElement]
        private bool? showPercentageInLabel;
        [GraphElement]
        private bool? animation;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        ///  If you've opted to show the data value, this attribute helps you control whether to show percentage values or actual values.
        /// </summary>
        public bool ShowPercentageValues
        {
            get
            {
                if (showPercentageValues == null)
                    throw new NullReferenceException("A propriedade showPercentageValues não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showPercentageValues);
            }
            set
            {
                showPercentageValues = value;
            }
        }

        /// <summary>
        ///  If you've opted to show the data value, this attribute helps you control whether to show percentage values or actual values in the pie labels.
        /// </summary>
        public bool ShowPercentageInLabel
        {
            get
            {
                if (showPercentageInLabel == null)
                    throw new NullReferenceException("A propriedade showPercentageInLabel não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showPercentageInLabel);
            }
            set
            {
                showPercentageInLabel = value;
            }
        }

        /// <summary>
        /// This attribute sets whether the animation is to be played or whether the entire chart would be rendered at one go.
        /// </summary>
        public bool Animation
        {
            get
            {
                if (animation == null)
                    throw new NullReferenceException("A propriedade animation não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(animation);
            }
            set
            {
                animation = value;
            }
        }

        #endregion
    }
}
