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
    public class AnimationPropertySet : PropertySetBase
    {
        #region[ Construtor ]
        public AnimationPropertySet()
        {
            animation = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private bool? animation;

        #endregion

        #region [ Propriedades Publicas ]

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
