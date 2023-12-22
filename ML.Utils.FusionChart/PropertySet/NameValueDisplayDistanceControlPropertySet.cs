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
    public class NameValueDisplayDistanceControlPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public NameValueDisplayDistanceControlPropertySet()
        {
            slicingDistance = null;
            nameTBDistance = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? slicingDistance;
        [GraphElement]
        private int? nameTBDistance;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "Numeric Value" : If you've opted to slice a particular pie, using this attribute you can control the distance between the sliced pie and the center of other pies.
        /// </summary>
        public int SlicingDistance
        {
            get
            {
                if (slicingDistance == null)
                    throw new NullReferenceException("A propriedade slicingDistance não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(slicingDistance);
            }
            set
            {
                slicingDistance = value;
            }
        }

        /// <summary>
        /// "Numeric Value" : This attribute helps you set the distance of the name/value text boxes from the pie edge.
        /// </summary>
        public int NameTBDistance
        {
            get
            {
                if (nameTBDistance == null)
                    throw new NullReferenceException("A propriedade nameTBDistance não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(nameTBDistance);
            }
            set
            {
                nameTBDistance = value;
            }
        }
        #endregion
    }
}
