using CodeEffects.Rule.Common;
using CodeEffects.Rule.Core;
using CodeEffects.Rule.Models;
using CodeEffects.Rule.Mvc;
using ML.BusinessRule.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ML.BusinessRule
{
    public static class BusinessRuleHelper
    {
        public static RuleEditor GetRuleEditor(string id, Type type)
        {
            return GetRuleEditor(id, type, null);
        }

        public static RuleEditor GetRuleEditor(string id, Type type, string ruleXml, RuleType ruleType = RuleType.Execution, bool clientMode = false, bool showToolbar = false, bool showHelpString = false)
        {
            RuleEditor editor = new RuleEditor(id);

            editor.ClientOnly = clientMode;
            editor.ShowToolBar = showToolbar;
            editor.ShowHelpString = showHelpString;

            editor.Mode = ruleType;

            if (ruleXml == null)
                editor.Rule = GetRuleModel(type);
            else
                editor.Rule = GetRuleModel(type, ruleXml);

            return editor;
        }

        public static RuleModel GetRuleModel(Type type, string ruleXml = null)
        {
            if (string.IsNullOrWhiteSpace(ruleXml))
            {
                return RuleModel.Create(type);
            }
            else
            {
                return RuleModel.Create(ruleXml, type);
            }
        }

        public static string GetRuleXml(this Rule rule)
        {
            return rule.Rules.ToString();
        }

        public static string GetPropertyValue(this string ruleXml, string propertyName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ruleXml);

            XmlNode node = doc.SelectSingleNode(string.Format("//condition/property[@name='{0}']", propertyName));
            if (node != null)
            {
                foreach(XmlNode item in node.ChildNodes)
                {
                    if (item.Name == "value")
                    {
                        return item.Value;
                    }
                }
            }

            return null;
        }

        public static bool Evaluate<T>(this T obj, string ruleXml) where T : class
        {
            Evaluator<T> eval = new Evaluator<T>(ruleXml);
            var result = eval.Evaluate(obj);

            return result;
        }
    }
}