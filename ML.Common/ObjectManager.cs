using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MongoDB.Bson;

namespace ML.Common
{
    public static class ObjectManager
    {
        #region Get & Set Value

        public static T GetValue<T>(this object source, string fullPropertyName, List<CacheObjectValue> cacheValues = null)
        {
            var value = GetValue(source, fullPropertyName, cacheValues);
            return value != null ? (T)value : default(T);
        }

        /// <summary>
        /// Cache: only sub-parent value. Ex: xx.yy.zz -> cache: yy
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fullPropertyName"></param>
        /// <param name="cachedProperties"></param>
        /// <returns></returns>
        public static object GetValue(this object source, string fullPropertyName, List<CacheObjectValue> cachedProperties = null)
        {
            if (source == null || string.IsNullOrWhiteSpace(fullPropertyName))
            {
                return null;
            }

            var fullPropNames = fullPropertyName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            //from cache
            if (GetValueFromCache(source, fullPropNames, cachedProperties, out var outValue))
            {
                return outValue;
            }

            var sourceValue = source;
            var sourcePropType = source.SafeType();

            var cachedPropNames = cachedProperties != null ? new List<string>() : null;

            for (int i = 0; i < fullPropNames.Length; i++)
            {
                var originalPropName = fullPropNames[i];
                var safePropName = originalPropName;
                if (cachedProperties != null) cachedPropNames.Add(originalPropName);

                var propIsList = false;
                var propListIndex = -1;

                if (safePropName.StartsWith("ExtendProp")) // ExtendProp: is indexer, only use in CMS (http://msdn.microsoft.com/en-us/library/2549tw02.aspx)
                {
                    safePropName = "ExtendProp";
                }
                else if (PropNameIsTheList(safePropName, ref safePropName, ref propListIndex))
                {
                    propIsList = true;
                }

                var sourceProp = sourcePropType.GetProperty(safePropName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp == null)
                {
                    return null;
                }

                sourcePropType = sourceProp.PropertyType.GenericTypeArguments?.FirstOrDefault() ?? sourceProp.PropertyType;

                if (safePropName == "ExtendProp")
                {
                    var indexerName = originalPropName.Replace("ExtendProp[\"", "").Replace("\"]", "");
                    propIsList = PropNameIsTheList(indexerName, ref indexerName, ref propListIndex);

                    sourceValue = sourceProp.GetValue(sourceValue, new object[] { indexerName });
                }
                else
                {
                    sourceValue = sourceProp.GetValue(sourceValue, null);
                }

                if (propIsList && propListIndex >= 0 && sourceValue is ICollection)
                {
                    switch (sourceValue)
                    {
                        case IList list:
                            sourceValue = propListIndex < list.Count ? list[propListIndex] : null;
                            break;

                        case IDictionary dictionary:
                            sourceValue = propListIndex < dictionary.Count ? dictionary[propListIndex] : null;
                            break;
                    }
                }

                if (sourceValue == null)
                {
                    return null;
                }

                //cache
                if (cachedProperties != null && i > 0 && i < fullPropNames.Length - 1) //only cache sub-parent value, xx.yy.zz -> cache: yy
                {
                    cachedProperties.Add(new CacheObjectValue { FullName = string.Join(".", cachedPropNames), Value = sourceValue });
                }
            }

            return sourceValue;
        }

        /// <summary>
        /// Cache: only sub-parent value. Ex: xx.yy.zz -> cache: yy
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fullPropertyName"></param>
        /// <param name="cachedProperties"></param>
        /// <returns></returns>
        public static object GetValue(this object source, string fullPropertyName, ref Type valueType, List<CacheObjectValue> cachedProperties = null)
        {
            if (source == null || string.IsNullOrWhiteSpace(fullPropertyName))
            {
                return null;
            }

            var fullPropNames = fullPropertyName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            //from cache
            if (GetValueFromCache(source, fullPropNames, cachedProperties, out var outValue))
            {
                return outValue;
            }

            var sourceValue = source;
            var cachedPropNames = cachedProperties != null ? new List<string>() : null;

            for (int i = 0; i < fullPropNames.Length; i++)
            {
                var originalPropName = fullPropNames[i];
                var safePropName = originalPropName;
                if (cachedProperties != null) cachedPropNames.Add(originalPropName);

                var propIsList = false;
                var propListIndex = -1;

                if (safePropName.StartsWith("ExtendProp")) // ExtendProp: is indexer, only use in CMS (http://msdn.microsoft.com/en-us/library/2549tw02.aspx)
                {
                    safePropName = "ExtendProp";
                }
                else if (PropNameIsTheList(safePropName, ref safePropName, ref propListIndex))
                {
                    propIsList = true;
                }

                var sourceProp = sourceValue.SafeType().GetProperty(safePropName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp == null)
                {
                    return null;
                }

                valueType = sourceProp.PropertyType.GenericTypeArguments?.LastOrDefault() ?? sourceProp.PropertyType;

                if (safePropName == "ExtendProp")
                {
                    var indexerName = originalPropName.Replace("ExtendProp[\"", "").Replace("\"]", "");
                    propIsList = PropNameIsTheList(indexerName, ref indexerName, ref propListIndex);

                    sourceValue = sourceProp.GetValue(sourceValue, new object[] { indexerName });
                }
                else
                {
                    sourceValue = sourceProp.GetValue(sourceValue, null);
                }

                if (propIsList && propListIndex >= 0 && sourceValue is ICollection)
                {
                    switch (sourceValue)
                    {
                        case IList list:
                            sourceValue = propListIndex < list.Count ? list[propListIndex] : null;
                            break;

                        case IDictionary dictionary:
                            sourceValue = propListIndex < dictionary.Count ? dictionary[propListIndex] : null;
                            break;
                    }
                }

                if (sourceValue == null)
                {
                    return null;
                }

                //cache
                if (cachedProperties != null && i > 0 && i < fullPropNames.Length - 1) //only cache sub-parent value, xx.yy.zz -> cache: yy
                {
                    cachedProperties.Add(new CacheObjectValue { FullName = string.Join(".", cachedPropNames), Value = sourceValue });
                }
            }

            return sourceValue;
        }

        private static bool GetValueFromCache(object source, string[] propNames, List<CacheObjectValue> cacheValues, out object outValue)
        {
            outValue = null;
            if (cacheValues == null || cacheValues.Count == 0 || propNames.Length <= 2) //only cache sub-parent value
            {
                return false;
            }

            var length = propNames.Length;
            var fullParentName = string.Join(".", propNames.Where((item, index) => index < length - 1));

            var cache = cacheValues.FirstOrDefault(a => a.FullName.Equals(fullParentName, StringComparison.OrdinalIgnoreCase));
            if (cache?.Value != null)
            {
                outValue = GetValue(cache.Value, propNames.Last(), cachedProperties: null);
                return true;
            }

            return false;
        }

        private static bool PropNameIsTheList(string propName, ref string listName, ref int listItemIndex)
        {
            var listMatch = Regex.Match(propName, @"(.+?)\[(\d+)\]");
            if (!listMatch.Success)
            {
                return false;
            }

            listName = listMatch.Groups[1].Value;
            listItemIndex = listMatch.Groups[2].Value.ToInt(-1);

            return true;
        }

        public static SetValueResult SetValue(this object target, string fullPropertyName, object value, bool createInstanceIfNull = false)
        {
            if (string.IsNullOrWhiteSpace(fullPropertyName))
            {
                return new SetValueResult { Success = false };
            }

            var safePropName = "";
            var propListIndex = -1;

            var propertyNames = fullPropertyName.Trim().Split('.');
            var targetValue = target;

            for (var i = 0; i < propertyNames.Length - 1; i++)
            {
                PropertyInfo targetProp;
                if (PropNameIsTheList(propertyNames[i], ref safePropName, ref propListIndex))
                {
                    targetProp = targetValue.SafeType().GetProperty(safePropName);

                    switch (targetProp.GetValue(targetValue))
                    {
                        case IList list when propListIndex < list.Count:
                            targetValue = list[propListIndex];
                            break;

                        case IDictionary dictionary when propListIndex < dictionary.Count:
                            targetValue = dictionary[propListIndex];
                            break;

                        default:
                            return null;
                    }

                    if (targetValue == null && createInstanceIfNull)
                    {
                        targetValue = Activator.CreateInstance(targetProp.PropertyType.GetGenericArguments()[0]);
                    }

                    continue;
                }

                targetProp = targetValue.SafeType().GetProperty(propertyNames[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (targetProp == null)
                {
                    return new SetValueResult { Success = false };
                }

                var tmpTarget = targetValue;
                targetValue = targetProp.GetValue(targetValue, null);

                if (targetValue == null && createInstanceIfNull)
                {
                    targetValue = Activator.CreateInstance(targetProp.PropertyType);
                    targetProp.SetValue(tmpTarget, targetValue, null);
                }
            }

            var propToSet = targetValue.SafeType().GetProperty(propertyNames.Last());
            if (propToSet == null || !propToSet.CanWrite)
            {
                return new SetValueResult { Success = false };
            }

            var result = new SetValueResult
            {
                Success = true,
                OldValue = propToSet.GetValue(targetValue),
                NewValue = value,
                PropertyInfo = propToSet
            };

            propToSet.SetValue(targetValue, value, null);

            return result;
        }

        #endregion

        #region Type

        public static Type GetType<TParent>(string childFullPropNames, bool getItemTypeIfList = false) => GetType(typeof(TParent), childFullPropNames, getItemTypeIfList);

        public static Type GetType(Type parentType, string childFullPropNames, bool getItemTypeIfList = false)
        {
            if (string.IsNullOrWhiteSpace(childFullPropNames))
            {
                return null;
            }

            Type curPropType = null;

            var props = childFullPropNames.Split('.');
            var propsLength = props.Length;

            for (int i = 0; i < propsLength; i++)
            {
                curPropType = (curPropType ?? parentType).GetProperty(props[i])?.PropertyType;
                if (curPropType == null)
                {
                    return null;
                }

                // list??
                if ((i < propsLength - 1 || getItemTypeIfList) && curPropType.IsGenericType && curPropType.GenericTypeArguments?.Length > 0)
                {
                    curPropType = curPropType.GenericTypeArguments[0];
                }
            }

            return curPropType;
        }

        public static Type GetSafeType(Type parentType, string childFullPropNames, bool getItemTypeIfList = false)
        {
            var curPropType = GetType(parentType, childFullPropNames, getItemTypeIfList);

            if (curPropType.IsNullableType())
            {
                return Nullable.GetUnderlyingType(curPropType);
            }

            return curPropType;
        }

        public static IEnumerable<object> ChangeTypes<T>(this IEnumerable<T> values, Type type)
        {
            return values.Select(val =>
            {
                try
                {
                    return Convert.ChangeType(val, type);
                }
                catch
                {
                    return null;
                }
            });
        }

        public static Type SafeType(this object obj)
        {
            var valueType = obj.GetType();

            if (valueType.IsNullableType())
            {
                valueType = Nullable.GetUnderlyingType(valueType);
            }

            return valueType;
        }

        public static bool IsNullableType(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsSimpleType(this Type type) => type.IsValueType();

        public static bool IsValueType(this Type type)
        {
            if (type.IsNullableType())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return type.IsPrimitive || type.IsEnum
                || type == typeof(DateTime)
                || type == typeof(TimeSpan)
                || type == typeof(decimal)
                || type == typeof(double)
                || type == typeof(float)
                || type == typeof(long)
                || type == typeof(int)
                || type == typeof(short)
                || type == typeof(string)
                || type == typeof(bool)
                || type == typeof(Guid)
                || type == typeof(ObjectId);
        }

        public static bool IsType<T>(this Type type)
        {
            if (type.IsNullableType())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return type == typeof(T);
        }

        public static string GetTypeName(this Type type, bool shortName = false)
        {
            var name = type.Name;
            if (type.IsGenericType)
            {
                var nullableType = Nullable.GetUnderlyingType(type);
                if (nullableType == null)
                {
                    return name;
                }

                name = nullableType.Name;
            }

            if (shortName)
            {
                name = name switch
                {
                    "String" => "string",
                    "Boolean" => "bool",
                    "Int16" => "short",
                    "Int32" => "int",
                    "Int64" => "long",
                    "Single" => "float",
                    "Double" => "double",
                    "Decimal" => "decimal",
                    _ => name
                };
            }

            return $"{name}{(type.IsGenericType ? "?" : "")}";
        }

        #endregion

        #region PropertyInfo

        public static PropertyInfo GetProperty<TParent>(string childFullPropNames) => GetProperty(typeof(TParent), childFullPropNames);

        public static PropertyInfo GetProperty(Type parentType, string childFullPropNames)
        {
            if (string.IsNullOrWhiteSpace(childFullPropNames))
            {
                return null;
            }

            PropertyInfo curProp = null;

            foreach (var propName in childFullPropNames.Split('.'))
            {
                var type = curProp != null ? curProp.PropertyType : parentType;
                curProp = type.GetProperty(propName);

                if (curProp == null)
                {
                    return null;
                }
            }

            return curProp;
        }

        #endregion

        #region Serialize & Deserialize

        /// <summary>
        /// @namespace is null -> ignore namespace; typeSerialize is null -> typeof(T)
        /// </summary>
        public static XElement SerializeToXElement<T>(this T toSerialize, string @namespace, Type typeSerialize = null)
        {
            if (ReferenceEquals(toSerialize, null))
            {
                return null;
            }

            var stringXml = toSerialize.Serialize(@namespace, typeSerialize);

            return XElement.Parse(stringXml);
        }

        /// <summary>
        /// @namespace is null -> ignore namespace; typeSerialize is null -> typeof(T)
        /// </summary>
        public static string Serialize<T>(this T toSerialize, string @namespace, Type typeSerialize = null, bool formatIndent = true)
        {
            if (ReferenceEquals(toSerialize, null))
            {
                return string.Empty;
            }

            var safeType = typeSerialize ?? typeof(T);
            var xmlSerializer = @namespace != null ? new XmlSerializer(safeType, @namespace) : new XmlSerializer(safeType);

            using (var textWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { Indent = formatIndent }))
                {
                    xmlSerializer.Serialize(xmlWriter, toSerialize);
                }

                return textWriter.ToString();
            }
        }

        /// <summary>
        /// typeSerialize is null -> typeof(T)
        /// </summary>
        public static XElement SerializeToXElement<T>(this T toSerialize, bool emptyNamespace = false, Type typeSerialize = null)
        {
            if (ReferenceEquals(toSerialize, null))
            {
                return null;
            }

            var stringXml = toSerialize.Serialize(emptyNamespace, typeSerialize);

            return XElement.Parse(stringXml);
        }

        /// <summary>
        /// typeSerialize is null -> typeof(T)
        /// </summary>
        public static string Serialize<T>(this T toSerialize, bool emptyNamespace = false, Type typeSerialize = null, bool formatIndent = true)
        {
            if (ReferenceEquals(toSerialize, null))
            {
                return string.Empty;
            }

            var xmlSerializer = new XmlSerializer(typeSerialize ?? typeof(T));

            using (var textWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { Indent = formatIndent }))
                {
                    if (emptyNamespace)
                    {
                        var @namespace = new XmlSerializerNamespaces();
                        @namespace.Add("", "");

                        xmlSerializer.Serialize(xmlWriter, toSerialize, @namespace);
                    }
                    else
                    {
                        xmlSerializer.Serialize(xmlWriter, toSerialize);
                    }
                }

                return textWriter.ToString();
            }
        }

        public static string SerializeToPureString<T>(this T toSerialize, bool ignoreNamespace = true, bool removeXmlDeclare = true)
        {
            if (ReferenceEquals(toSerialize, null))
            {
                return string.Empty;
            }

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = removeXmlDeclare,
                Indent = true,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };
            var ms = new MemoryStream();

            var xmlWriter = XmlWriter.Create(ms, settings);

            var xmlSerializer = new XmlSerializer(typeof(T));

            if (ignoreNamespace)
            {
                var emptyNamespace = new XmlSerializerNamespaces();
                emptyNamespace.Add("", "");

                xmlSerializer.Serialize(xmlWriter, toSerialize, emptyNamespace);
            }
            else
            {
                xmlSerializer.Serialize(xmlWriter, toSerialize);
            }

            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            var sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// @namespace is null -> ignore namespace
        /// </summary>
        public static T Deserialize<T>(this string xmlContent, string @namespace = null)
        {
            if (string.IsNullOrEmpty(xmlContent))
            {
                return default(T);
            }

            var serializer = @namespace != null ? new XmlSerializer(typeof(T), @namespace) : new XmlSerializer(typeof(T));
            var reader = new StringReader(xmlContent);

            var instance = serializer.Deserialize(reader);

            return instance != null ? (T)instance : default(T);
        }

        public static T DeserializeFromXElement<T>(this XElement xmlContent)
        {
            if (xmlContent == null)
            {
                return default(T);
            }

            var stringXml = xmlContent.ToString();

            return stringXml.Deserialize<T>();
        }

        public static string ToXmlSchema<T>()
        {
            return ToXmlSchema(typeof(T));
        }

        public static string ToXmlSchema(this Type type)
        {
            var textWriter = new StringWriter();

            var importer = new XmlReflectionImporter();
            var schemas = new XmlSchemas();
            var exporter = new XmlSchemaExporter(schemas);
            var map = importer.ImportTypeMapping(type);
            exporter.ExportTypeMapping(map);

            schemas[0].Write(textWriter);
            textWriter.Close();

            return textWriter.ToString();
        }

        #endregion

        #region Clone

        public static T Clone<T>(this T source, Action<T> afterClone = null)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);

                var clone = (T)formatter.Deserialize(stream);

                afterClone?.Invoke(clone);

                return clone;
            }
        }

        #endregion

        #region Compare

        public static bool IsDiff(this object source, object compare, bool stringIgnoreNull = false)
        {
            if (source == null)
            {
                if (stringIgnoreNull && compare is string str_compare)
                {
                    return !string.IsNullOrEmpty(str_compare);
                }

                return compare != null;
            }

            if (compare == null)
            {
                if (stringIgnoreNull && source is string str_source)
                {
                    return !string.IsNullOrEmpty(str_source);
                }

                return source != null;
            }

            //var jSource = JsonConvert.SerializeObject(source);
            //var jCompare = JsonConvert.SerializeObject(compare);
            //return jSource != jCompare;

            return !source.Equals(compare);
        }

        public static bool IsTrueOperator<T>(this T source, string @operator, T compare)
        {
            if (source == null && compare == null && @operator == "=")
            {
                return true;
            }

            if (source == null || compare == null)
            {
                return true;
            }

            var type = typeof(T);
            if (type.IsNullableType()) type = Nullable.GetUnderlyingType(type);

            return IsTrueOperator(type, source, @operator, compare);
        }

        /// <summary>
        /// source, compare must be same type
        /// </summary>
        public static bool IsTrueOperator(Type type, object source, string @operator, object compare)
        {
            if (source == null && compare == null && @operator == "=")
            {
                return true;
            }

            if (source == null || compare == null)
            {
                return true;
            }

            if (!typeof(IComparable).IsAssignableFrom(type))
            {
                return false;
            }

            var compareValue = ((IComparable)source).CompareTo(compare);

            switch (@operator)
            {
                case ">":
                    return compareValue == 1;

                case ">=":
                    return compareValue == 1 || compareValue == 0;

                case "<":
                    return compareValue == -1;

                case "<=":
                    return compareValue == -1 || compareValue == 0;

                case "=":
                    return compareValue == 0;

                case "!=":
                    return compareValue != 0;
            }

            return false;
        }

        #endregion

        /// <summary>
        /// Currently only apply for simple object (properties are not list, array, dictionary)
        /// </summary>
        public static Dictionary<string, object> ToDictionary(this object source, string prefix = "")
        {
            var dictionary = new Dictionary<string, object>();
            if (source == null)
            {
                return dictionary;
            }

            foreach (var prop in source.GetType().GetProperties())
            {
                var propValue = prop.GetValue(source);
                FetchValues(prop, propValue, prefix);
            }

            return dictionary;

            void FetchValues(PropertyInfo objProp, object objValue, string prefixKey)
            {
                if (objValue == null)
                {
                    return;
                }

                var key = !string.IsNullOrEmpty(prefixKey) ? $"{prefixKey}.{objProp.Name}" : objProp.Name;

                if (objProp.PropertyType.IsValueType || objProp.PropertyType.FullName == "System.String")
                {
                    dictionary.Add(key, objValue);
                }
                else if (objProp.PropertyType.IsClass)
                {
                    foreach (var subProp in objProp.PropertyType.GetProperties())
                    {
                        var subObj = subProp.GetValue(objValue);
                        FetchValues(subProp, subObj, key);
                    }
                }
            }
        }

        public class SetValueResult
        {
            public bool Success { get; set; }

            public object OldValue { get; set; }

            public object NewValue { get; set; }

            public PropertyInfo PropertyInfo { get; set; }
        }

        public class CacheObjectValue
        {
            public string FullName { get; set; }

            public object Value { get; set; }

            public Type Type { get; set; }
        }
    }
}
