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
using System.Data;
using System.Collections;

namespace ML.Utils.FusionChart
{
    internal class SSDataSourceParser
    {
        internal string DataTextField { set; private get; }
        internal string DataValueField { set; private get; }

        internal List<SSSetElement> Parse(object dataSource)
        {
            if (dataSource is DataTable)
                return Parse((DataTable)dataSource);

            if (dataSource is IList)
                return Parse((IList)dataSource);

            throw new ArgumentException("The parameter must be a DataTable or a member thar implements IList.");
        }

        private List<SSSetElement> Parse(DataTable dataTable)
        {
            List<SSSetElement> retorno = new List<SSSetElement>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SSSetElement set = new SSSetElement();
                set.Name = dataTable.Rows[i][this.DataTextField].ToString();
                set.Value = Convert.ToDecimal(dataTable.Rows[i][this.DataValueField]);
                retorno.Add(set);
            }

            return retorno;
        }

        private List<SSSetElement> Parse(IList iList)
        {
            List<SSSetElement> retorno = new List<SSSetElement>();

            for (int i = 0; i < iList.Count; i++)
            {
                Type type = iList[i].GetType();

                SSSetElement set = new SSSetElement();
                set.Name = type.GetProperty(this.DataTextField).GetValue(iList[i], null).ToString();
                set.Value = Convert.ToDecimal(
                    type.GetProperty(this.DataValueField).GetValue(iList[i], null)
                    );
                retorno.Add(set);
            }

            return retorno;
        }


    }
}
