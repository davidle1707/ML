using CodeEffects.Rule.Client;
using CodeEffects.Rule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.BusinessRule
{
    public static class RuleModelExtension
    {
        public static string GetRuleText(this RuleModel rule)
        {
            List<string> ruleDisplay = new List<string>();

            foreach (var item in rule.Elements)
            {
                if (item.Type == ElementType.Action
                    && ruleDisplay.Count > 0)
                {
                    if (ruleDisplay.Last() != item.Value)
                    {
                        ruleDisplay.Add(item.Value);
                    }
                }
                else
                {
                    ruleDisplay.Add(ReplaceOperator(item.Value));
                }
            }

            return string.Join(" ", ruleDisplay);
        }

        public static string GetRuleDisplay(this RuleModel rule)
        {
            List<string> ruleDisplay = new List<string>();

            foreach (var item in rule.Elements)
            {
                if (item.Type == ElementType.Action
                    && ruleDisplay.Count > 0)
                {
                    if (ruleDisplay.Last() != ActionStyle(item.Value))
                    {
                        ruleDisplay.Add(ActionStyle(item.Value));
                    }
                }
                else
                {
                    if (item.Type == ElementType.Field
                        || item.Type == ElementType.Function)
                    {
                        ruleDisplay.Add(FieldStyle(item.Value));
                    }
                    else if (item.Type == ElementType.Value)
                    {
                        ruleDisplay.Add(ValueStyle(item.Value));
                    }
                    else if (item.Type == ElementType.Operator)
                    {
                        ruleDisplay.Add(OperatorStyle(ReplaceOperator(item.Value)));
                    }
                    else
                    {
                        ruleDisplay.Add(ClauseStyle(item.Value));
                    }
                }
            }

            return string.Join(" ", ruleDisplay);
        }

        private static string ReplaceOperator(string oper)
        {
            switch (oper.Trim().ToLower())
            { 
                case "equal":
                    return "=";
                case "notequal":
                    return "!=";
                case "less":
                    return "<";
                case "greater":
                    return ">";
                case "lessorequal":
                    return "<=";
                case "greaterorequal":
                    return ">=";
                default:
                    return oper;
            }
        }

        private static string ClauseStyle(string item)
        {
            return SetStyle(item, "font-weight: bold; color: blue;");
        }

        private static string OperatorStyle(string item)
        {
            return SetStyle(item, "font-weight: bold; color: black;");
        }

        private static string FieldStyle(string item)
        {
            return SetStyle(item, "font-weight: bold; color: #2B91AF;");
        }

        private static string ValueStyle(string item)
        {
            return SetStyle(item, "font-weight: bold; color: #FF7F27");
        }

        private static string ActionStyle(string item)
        {
            return SetStyle(item, "font-weight: bold; color: red;");
        }

        private static string SetStyle(string item, string style)
        {
            return string.Format("<span style=\"{0}\">{1}</span>", style, item);
        }
    }
}
