using ML.BusinessRule.Condition;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.BusinessRule
{
    public class BusinessRuleManager<T> where T : class
    {
        public IfTag<T> Rule { get; set; }

        public BusinessRuleManager()
        {
            Rule = new IfTag<T>();
        }
    }
}
