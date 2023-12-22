using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ML.Common.Extensions
{
    public static class LinqExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sources)
        {
            if (sources == null)
                return true;

            if (sources is T[] array && array.Length == 0)
                return true;

            if (sources is IList<T> list && list.Count == 0)
                return true;

            return !sources.Any();
        }

        public static List<ObjectId> ToListIds<T>(this IEnumerable<T> sources, Func<T, ObjectId> selector, bool distinct = true) where T : class
        {
            var query = sources.Select(selector);
            return distinct ? query.Distinct().ToList() : query.ToList();
        }

        public static List<ObjectId> ToListIds<T>(this IEnumerable<T> sources, Func<T, ObjectId?> selector, bool distinct = true) where T : class
        {
            var query = sources.Select(selector).Where(a => a.HasValue).Select(a => a.Value);
            return distinct ? query.Distinct().ToList() : query.ToList();
        }

        public  static bool TryFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T outT)
        {
            outT = source != null ? source.FirstOrDefault(predicate) : default;
            return outT != null;
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return getPart(enumerator, size);
            }

            static IEnumerable<T> getPart(IEnumerator<T> _enumerator, int _size)
            {
                do
                {
                    yield return _enumerator.Current;
                } while (--_size > 0 && _enumerator.MoveNext());
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action, int? batchSize = 10)
        {
            if (batchSize > 0)
            {
                foreach (var batch in enumeration.Batch(batchSize.Value))
                {
                    foreach (var item in batch)
                    {
                        action(item);
                    }
                }
            }
            else
            {
                foreach (var item in enumeration)
                {
                    action(item);
                }
            }
        }

        public static IEnumerable<T> ExceptByKey<T, TKey>(this IEnumerable<T> sources, IEnumerable<T> others, Func<T, TKey> keySelector)
        {
            var set = new HashSet<TKey>(others.Select(keySelector));

            return sources.Where(item => set.Add(keySelector(item)));
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> sources, Func<T, TKey> keySelector)
        {
            return sources.GroupBy(keySelector).Select(g => g.First());
        }

        public static IQueryable<T> RangeDate<T>(this IQueryable<T> query, Func<T, DateTime?> keySelector, DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate != null)
            {
                query = query.Where(q => keySelector(q) >= fromDate.Value);
            }
            if (toDate != null)
            {
                query = query.Where(q => keySelector(q) <= toDate.Value);
            }

            return query;
        }

        public static IQueryable<TSource> WhereLike<TSource>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, string>> valueSelector,
           string value)
        {
            return WhereLike(source, valueSelector, value, '*');
        }

        public static IQueryable<TSource> WhereLike<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> valueSelector,
            string value,
            char wildcard)
        {
            return source.Where(BuildLikeExpression(valueSelector, value, wildcard));
        }

        public static Expression<Func<TElement, bool>> BuildLikeExpression<TElement>(
            Expression<Func<TElement, string>> valueSelector,
            string value,
            char wildcard)
        {
            if (valueSelector == null)
                throw new ArgumentNullException("valueSelector");

            var method = GetLikeMethod(value, wildcard);

            value = value.Trim(wildcard);
            var body = Expression.Call(valueSelector.Body, method, Expression.Constant(value));

            var parameter = valueSelector.Parameters.Single();
            return Expression.Lambda<Func<TElement, bool>>(body, parameter);
        }

        private static MethodInfo GetLikeMethod(string value, char wildcard)
        {
            var methodName = "Equals";

            var textLength = value.Length;
            value = value.TrimEnd(wildcard);
            if (textLength > value.Length)
            {
                methodName = "StartsWith";
                textLength = value.Length;
            }

            value = value.TrimStart(wildcard);
            if (textLength > value.Length)
            {
                methodName = (methodName == "StartsWith") ? "Contains" : "EndsWith";
                textLength = value.Length;
            }

            var stringType = typeof(string);
            return stringType.GetMethod(methodName, new Type[] { stringType });
        }
    }
}
