using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.FusionChart.Abstract
{
    public interface ICombinationChart
    {
        /// <summary>
        /// Atribui a um data set o tipo de eixo o qual ele sera renderizado.
        /// </summary>
        /// <param name="dataSetName">Nome do data set</param>
        /// <param name="axisType">Tipo do eixo, primario ou secundario</param>
        void SetAxisType(String dataSetName, CombinationAxisType axisType);

        /// <summary>
        /// Retorna o dicionario de tipos de eixo.
        /// </summary>
        /// <returns>Dicionario de tipos de eixo.</returns>
        Dictionary<String, CombinationAxisType> GetAxisTypeDictionary();
    }
}
