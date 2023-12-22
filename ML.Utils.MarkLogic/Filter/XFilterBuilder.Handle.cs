using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ML.Utils.MarkLogic
{
    public sealed class XElemFilterHandleBuilder<TEntity, TItem, TField> : XFilterHandleBuilder<TItem, TField>
    {
        private readonly Expression<Func<TEntity, IList<TItem>>> _colList;

        public XElemFilterHandleBuilder(Expression<Func<TEntity, IList<TItem>>> colList, Expression<Func<TItem, TField>> colItem, string alias = null, XFilterOption option = null)
            : base(colItem, alias, option)
        {
            _colList = colList;
        }

        protected override string NodeNameByVirtualMethod<T, TF>(Expression<Func<T, TF>> col, string alias)
        {
            var attribute = XUtils.Attribute<TEntity>();
            var name = _colList.NodeNameWithAlias(alias, attribute) + "/" + _col.NodeNameByGeneric(attribute);

            return name;
        }
    }

    public class XFilterHandleBuilder<TEntity, TField> : HandleBuilder<TEntity, TField>
    {
        private Func<string> _render;

        protected readonly Expression<Func<TEntity, TField>> _col;
        private readonly string _alias;
        private readonly XFilterOption _option;

        protected XFilterHandleBuilder()
        {
        }

        internal XFilterHandleBuilder(Expression<Func<TEntity, TField>> col, string alias = null, XFilterOption option = null)
        {
            _col = col;
            _alias = alias;
            _option = option ?? new XFilterOption();
        }

        internal override void CompareValue(TField value, string @operator)
        {
            _render = () =>
            {
                var left = GetFieldName(_col, _alias, _option, false);
                var right = GetFieldValue(_col, value, @operator, _option);

                if (@operator == "Contains" || @operator == "StartsWith" || @operator == "EndsWith")
                {
                    return $"matches({left}, {right})";
                }

                return $"{left} {@operator} {right}";
            };
        }

        internal override void RangeValue(TField fromValue, TField toValue)
        {
            _render = () =>
            {
                var col = GetFieldName(_col, _alias, _option, false);
                var from = fromValue != null ? GetFieldValue(_col, fromValue, string.Empty, _option) : null;
                var to = toValue != null ? GetFieldValue(_col, toValue, string.Empty, _option) : null;

                if (!string.IsNullOrWhiteSpace(from) & !string.IsNullOrWhiteSpace(to))
                {
                    return $"({col} >= {from} and {col} <= {to})";
                }

                if (!string.IsNullOrWhiteSpace(from))
                {
                    return $"{col} >= {from}";
                }

                if (!string.IsNullOrWhiteSpace(to))
                {
                    return $"{col} <= {to}";
                }

                return string.Empty;
            };
        }

        internal override void CompareField<TEntity2>(Expression<Func<TEntity2, TField>> col2, string @operator, string alias2 = null)
        {
            _render = () =>
            {
                var left = GetFieldName(_col, _alias, _option, true);
                var right = GetFieldName(col2, alias2, _option, true, false);

                return $"{left} {@operator} {right}";
            };
        }

        internal override void InOrNotIn(IEnumerable<TField> values, bool isNotIn)
        {
            _render = () =>
            {
                var name = GetFieldName(_col, string.Empty, _option, false);
                var _operator = isNotIn ? "!=" : "=";

                var conditions = values.Select(v => $"{name} {_operator} {GetFieldValue(_col, v, _operator, _option)}");

                return $"{XUtils.Alias<TEntity>(_alias)}[{string.Join(isNotIn ? " and " : " or ", conditions)}]";
            };
        }

        internal override void NullOrNotNull(bool isNotNull)
        {
            _render = () => $"not({GetFieldName(_col, _alias, _option, false)}/node()) = {(isNotNull ? "fn:false()" : "fn:true()")}";
        }

        internal override void EmptyOrNotEmpty(bool isNotEmpty)
        {
            _render = () => $"fn:string-length(fn:replace({GetFieldName(_col, _alias, _option, false)}, ' ', '')) {(isNotEmpty ? ">" : "=")} 0";
        }

        internal override void ExistsOrNotExists(bool isNotExists)
        {
            _render = () => $"empty({GetFieldName(_col, _alias, _option, false)}) = {(isNotExists ? "fn:true()" : "fn:false()")}";
        }

        public override string Render()
        {
            return _render?.Invoke() ?? string.Empty;
        }

        #region Private

        protected virtual string NodeNameByVirtualMethod<T, TF>(Expression<Func<T, TF>> col, string alias) => col.NodeNameWithAlias(alias);

        private string GetFieldName<T, TF>(Expression<Func<T, TF>> col, string alias, XFilterOption opt, bool castXQueryType, bool nodeNameByVirtualMethod = true)
        {
            var name = nodeNameByVirtualMethod ? NodeNameByVirtualMethod(col, alias) : col.NodeNameWithAlias(alias);

            var t = typeof(TF);

            if (t == typeof(object))
            {
                var member = std.MemberInfo(col);

                if (member.MemberType == MemberTypes.Property)
                {
                    t = ((PropertyInfo)member).PropertyType;
                }
            }

            if (t == typeof(decimal) || t == typeof(decimal?))
            {
                return castXQueryType ? $"xs:decimal({name})" : name;
            }
            if (t == typeof(double) || t == typeof(double?))
            {
                return castXQueryType ? $"xs:double({name})" : name;
            }
            if (t == typeof(int) || t == typeof(int?))
            {
                return castXQueryType ? $"xs:int({name})" : name;
            }
            if (t == typeof(long) || t == typeof(long?))
            {
                return castXQueryType ? $"xs:long({name})" : name;
            }
            if (t == typeof(short) || t == typeof(short?))
            {
                return castXQueryType ? $"xs:short({name})" : name;
            }
            if (t == typeof(bool) || t == typeof(bool?))
            {
                return castXQueryType ? $"xs:boolean({name})" : name;
            }
            if (t == typeof(DateTime) || t == typeof(DateTime?))
            {
                return castXQueryType
                    ? opt.OnlyDateIfDateTime ? $"xs:dateTime({name}) cast as xs:date" : $"xs:dateTime({name})"
                    : name;
            }
            if (t == typeof(Guid) || t == typeof(Guid?))
            {
                return name;
            }
            if (t == typeof(string) || t == typeof(char) || t == typeof(char?))
            {
                return opt.LowerCaseIfString ? $"fn:lower-case({name})" : name;
            }

            return name;
        }

        private string GetFieldValue<T, TF>(Expression<Func<T, TF>> col, TF value, string @operator, XFilterOption opt)
        {
            if (value == null)
            {
                return "()";
            }

            var t = typeof(TF);

            if (t == typeof(object))
            {
                var member = std.MemberInfo(col);

                if (member.MemberType == MemberTypes.Property)
                {
                    t = ((PropertyInfo)member).PropertyType;
                }
            }

            if (t == typeof(decimal) || t == typeof(decimal?))
            {
                return $"{value}";
            }
            if (t == typeof(double) || t == typeof(double?))
            {
                return $"{value}";
            }
            if (t == typeof(int) || t == typeof(int?))
            {
                return $"{value}";
            }
            if (t == typeof(long) || t == typeof(long?))
            {
                return $"{value}";
            }
            if (t == typeof(short) || t == typeof(short?))
            {
                return $"{value}";
            }
            if (t == typeof(bool) || t == typeof(bool?))
            {
                return value.ToBool() ? "fn:true()" : "fn:false()";
            }
            if (t == typeof(DateTime) || t == typeof(DateTime?))
            {
                var dateTimeValue = value as DateTime?;

                return opt.OnlyDateIfDateTime ? $"xs:date('{dateTimeValue.Value.Date.ToString("yyyy-MM-dd")}Z')" : $"xs:dateTime('{std.ToXQueryDateTime(dateTimeValue)}')";
            }
            if (t == typeof(Guid) || t == typeof(Guid?))
            {
                return $"'{value}'";
            }
            if (t == typeof(string) || t == typeof(char) || t == typeof(char?))
            {
                var regexStart = string.Empty; var regexEnd = string.Empty;

                switch (@operator)
                {
                    case "Contains": regexStart = regexEnd = ".*"; break;
                    case "StartsWith": regexEnd = ".*"; break;
                    case "EndsWith": regexStart = ".*"; break;
                }

                var stringValue = $"'{regexStart}{value.ToStr().Replace("'", "''")}{regexEnd}'";

                if (opt.LowerCaseIfString)
                {
                    stringValue = stringValue.ToLower();
                }

                return stringValue;
            }

            return null;
        }

        #endregion
    }

    public abstract class HandleBuilder<TEntity, TField> : XFilter<TEntity>
    {
        internal abstract void CompareValue(TField value, string @operator);

        internal abstract void RangeValue(TField fromValue, TField toValue);

        internal abstract void CompareField<TEntity2>(Expression<Func<TEntity2, TField>> col2, string @operator, string alias2 = null);

        internal abstract void InOrNotIn(IEnumerable<TField> values, bool isNotIn);

        internal abstract void NullOrNotNull(bool isNotNull);

        internal abstract void EmptyOrNotEmpty(bool isNotEmpty);

        internal abstract void ExistsOrNotExists(bool isNotExists);
    }
}
