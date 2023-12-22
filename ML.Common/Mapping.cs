using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ML.Common
{
    public static class Mapping
    {
        public static TDestination Map<TDestination>(this object source, Action<TDestination> afterMapped = null)
        {
            var dest = Mapper.Map<TDestination>(source);

            if (dest != null)
            {
                afterMapped?.Invoke(dest);
            }

            return dest;
        }

        public static TDestination Map<TSource, TDestination>(this TSource source, Action<TDestination> afterMapped = null)
        {
            var dest = Mapper.Map<TSource, TDestination>(source);

            if (dest != null)
            {
                afterMapped?.Invoke(dest);
            }

            return dest;
        }

        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination, Action<TDestination> afterMapped = null)
        {
            var dest = Mapper.Map<TSource, TDestination>(source, destination);

            if (dest != null)
            {
                afterMapped?.Invoke(dest);
            }

            return dest;
        }

        public static IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(this IEnumerable<TSource> sources, Action<IEnumerable<TDestination>> afterMapped = null)
        {
            var dests = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(sources);

            if (dests != null)
            {
                afterMapped?.Invoke(dests);
            }

            return dests;
        }

        public static IEnumerable<TDestination> MapEnumerable<TDestination>(this IEnumerable sources, Action<IEnumerable<TDestination>> afterMapped = null)
        {
            var dests = Mapper.Map<IEnumerable<TDestination>>(sources);

            if (dests != null)
            {
                afterMapped?.Invoke(dests);
            }

            return dests;
        }

        public static List<TDestination> MapList<TDestination>(this IList sources, Action<List<TDestination>> afterMapped = null)
        {
            var dests = Mapper.Map<IList, List<TDestination>>(sources);

            if (dests != null)
            {
                afterMapped?.Invoke(dests);
            }

            return dests;
        }
    }
}
