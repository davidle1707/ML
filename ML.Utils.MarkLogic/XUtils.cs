using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ML.Utils.MarkLogic
{
    public static class XUtils
    {
        public const string FunctionRemoveNamespace = "removeNamespace";

        public static XEntityAttribute Attribute<TEntity>(XEntityAttribute attribute = null)
        {
            var info = attribute ?? System.Attribute.GetCustomAttribute(typeof(TEntity), typeof(XEntityAttribute)) as XEntityAttribute;

            if (info == null)
            {
                throw new Exception("Entity must be has MarkLogicInfo attribute");
            }

            //if (string.IsNullOrEmpty(info.TableName) || string.IsNullOrEmpty(info.EntityName))
            //{
            //	throw new Exception("MarkLogicInfo attribute is invalid");
            //}

            return info;
        }

        /// <summary>
		/// if alias is null, use entity name as alias; attribute only use when alias is null
		/// </summary>
		public static string Alias<TEntity>(string alias = null, XEntityAttribute useAttributeIfAliasNull = null)
        {
            if (alias == null)
            {
                useAttributeIfAliasNull = XUtils.Attribute<TEntity>(useAttributeIfAliasNull);
                alias = !string.IsNullOrEmpty(useAttributeIfAliasNull.NamespaceVarName) ? useAttributeIfAliasNull.NamespaceVarName : useAttributeIfAliasNull.EntityName.ToLower();
            }

            return alias.StartsWith("$") ? alias : "$" + alias;
        }

        public static string NodeName<TEntity>(this Expression<Func<TEntity, object>> field, XEntityAttribute attribute = null)
        {
            var ns = XUtils.Attribute<TEntity>(attribute).NamespaceVarName;
            var names = std.PropFullNames(field);

            return string.Join("/", names.Select(n => $"{ns}:{n}"));
        }

        public static string NodeNameByGeneric<TEntity, TMapping>(this Expression<Func<TEntity, TMapping>> col, XEntityAttribute attribute = null)
        {
            var ns = XUtils.Attribute<TEntity>(attribute).NamespaceVarName;
            var names = std.PropFullNamesByGeneric(col);

            return string.Join("/", names.Select(n => $"{ns}:{n}"));
        }

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public static string NodeNameWithAlias<TEntity>(this Expression<Func<TEntity, object>> col, string alias = null, XEntityAttribute useAttributeIfAliasNull = null)
        {
            var attribute = XUtils.Attribute<TEntity>(useAttributeIfAliasNull);

            return alias == string.Empty ? NodeName(col) : $"{Alias<TEntity>(alias, attribute)}/{NodeName(col, attribute)}";
        }

        public static string NodeNameWithAlias<TEntity, TMapping>(this Expression<Func<TEntity, TMapping>> col, string alias = null, XEntityAttribute attribute = null)
        {
            attribute = XUtils.Attribute<TEntity>(attribute);

            return alias == string.Empty ? NodeNameByGeneric(col) : $"{Alias<TEntity>(alias, attribute)}/{NodeNameByGeneric(col, attribute)}";
        }

        public static string ParamValue(object value, bool lowerCaseIfStringOrGuid = false)
        {
            if (value == null)
            {
                return "()";
            }

            var valueType = value.SafeType();

            if (!valueType.IsSimpleType())
            {
                var objElement = value.SerializeToXElement();
                objElement.Name = "p";

                return objElement.ToString();
            }

            if (valueType == typeof(decimal) || valueType == typeof(decimal?))
            {
                return $"{value.ToDecimal()}";
            }
            if (valueType == typeof(double) || valueType == typeof(double?))
            {
                return $"{value.ToDouble()}";
            }
            if (valueType == typeof(int) || valueType == typeof(int?))
            {
                return $"{value.ToInt()}";
            }
            if (valueType == typeof(long) || valueType == typeof(long?))
            {
                return $"{value.ToLong()}";
            }
            if (valueType == typeof(short) || valueType == typeof(short?))
            {
                return $"{value.ToShort()}";
            }
            if (valueType == typeof(bool) || valueType == typeof(bool?))
            {
                return (bool)value ? "fn:true()" : "fn:false()";
            }
            if (valueType == typeof(DateTime) || valueType == typeof(DateTime?))
            {
                //http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx#KSpecifier
                //return string.Format("xs:dateTime('{0}')", value.ToDateTime().ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ"));

                return $"xs:dateTime('{((DateTime?)value).ToXQueryDateTime()}')";
            }

            var valueAsString = value.ToStr().Replace("'", "''");

            return string.IsNullOrWhiteSpace(valueAsString)
                ? "()"
                : $"'{(lowerCaseIfStringOrGuid ? valueAsString.ToLower() : valueAsString)}'";
        }

        public static string DeclareNamespace<TEntity>(bool useAsDefault = false)
        {
            var attribute = XUtils.Attribute<TEntity>();

            if (!useAsDefault)
            {
                return DeclareNamespace(attribute);
            }

            var query = new StringBuilder();
            query.AppendLine(DeclareDefaultNamespace(attribute));
            query.AppendLine(DeclareNamespace(attribute));

            return query.ToString();
        }

        public static string DeclareNamespace(XEntityAttribute attribute)
        {
            return $"declare namespace {attribute.NamespaceVarName} = '{attribute.NamespaceUrl}';";
        }

        public static string DeclareDefaultNamespace<TEntity>()
        {
            var attribute = XUtils.Attribute<TEntity>();

            return DeclareDefaultNamespace(attribute);
        }

        public static string DeclareDefaultNamespace(XEntityAttribute attribute)
        {
            return $"declare default element namespace '{attribute.NamespaceUrl}';";
        }

        public static string DeclareFunction(string dbPath, string funcName)
        {
            return $"import module namespace {dbPath.ToLower()} = \"{dbPath}\" at \"/{dbPath.ToLower()}.{funcName}.xqy\";";
        }

        public static string GetFunction(string dbPath, string funcName, string paramAsString)
        {
            return $"{dbPath.ToLower()}:{funcName}({paramAsString})";
        }

        public static string GetFunction(string dbPath, string funcName, params object[] paramValues)
        {
            return $"{dbPath.ToLower()}:{funcName}({string.Join(", ", paramValues.Select(p => ParamValue(p, true)))})";
        }

        /// <summary>
        /// ex: 'User_xxx.xml' or doc('User_xxx.xml') or doc('User_xxx.xml')/u:User
        /// </summary>
        public static string GetDocumentFile<TEntity>(object primaryValue, XEntityAttribute attribute = null, bool includeDocumentKeyword = true, bool includeDocumentQuery = true)
        {
            attribute = XUtils.Attribute<TEntity>(attribute);

            var doc = $"'{attribute.EntityName}_{primaryValue}.xml'";

            if (includeDocumentKeyword)
            {
                doc = $"doc({doc})";

                if (includeDocumentQuery)
                {
                    doc += GetDocumentQuery<TEntity>(attribute);
                }
            }

            return doc;
        }

        /// <summary>
        /// ex: /u:User
        /// </summary>
        public static string GetDocumentQuery<TEntity>(XEntityAttribute attribute = null)
        {
            attribute = XUtils.Attribute<TEntity>(attribute);

            return $"/{attribute.NamespaceVarName}:{attribute.EntityName}";
        }

        public static string GetDocumentInListQuery<TEntity, TItem>(Expression<Func<TEntity, IList<TItem>>> colList, XEntityAttribute attribute, IEnumerable<string> conditions = null)
        {
            attribute = XUtils.Attribute<TEntity>(attribute);
            var documentQuery = XUtils.GetDocumentQuery<TEntity>(attribute);

            if (conditions != null && conditions.Any())
            {
                return $"{documentQuery}[{string.Join(" and ", conditions)}]/{colList.NodeNameByGeneric(attribute)}/{attribute.NamespaceVarName}:{typeof(TItem).Name}";
            }

            return $"{documentQuery}/{colList.NodeNameByGeneric(attribute)}/{attribute.NamespaceVarName}:{typeof(TItem).Name}";
        }
    }
}
