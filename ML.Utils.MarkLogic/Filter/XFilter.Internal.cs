using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.MarkLogic
{
    internal class XStringFilter<TEntity> : XFilter<TEntity>
    {
        private readonly string _filter;

        public XStringFilter(string filter = "")
        {
            _filter = filter;
        }

        public override string Render()
        {
            return _filter;
        }
    }

    internal class XAndOrFilter<TEntity, TEntity2> : XFilter<TEntity>
    {
        private readonly XFilter<TEntity> _filter;
        private readonly XFilter<TEntity2> _filter2;
        private readonly string _andOr;

        public XAndOrFilter(XFilter<TEntity> filter, XFilter<TEntity2> filter2, string andOr)
        {
            _filter = filter;
            _filter2 = filter2;
            _andOr = andOr;
        }

        public override string Render()
        {
            var render1 = _filter.Render();
            var render2 = _filter2.Render();

            if (!string.IsNullOrWhiteSpace(render1) && !string.IsNullOrWhiteSpace(render2))
            {
                return $"({render1} {_andOr} {render2})";
            }

            if (!string.IsNullOrWhiteSpace(render1))
            {
                return render1;
            }

            if (!string.IsNullOrWhiteSpace(render2))
            {
                return render2;
            }

            return string.Empty;
        }
    }

    internal class XAndOrFilter<TEntity> : XFilter<TEntity>
    {
        internal readonly List<XFilter<TEntity>> Filters = new List<XFilter<TEntity>>();
        internal readonly string AndOr;

        public XAndOrFilter(IEnumerable<XFilter<TEntity>> filters, string andOr)
        {
            foreach (var filter in filters)
            {
                var fAndOr = filter as XAndOrFilter<TEntity>;

                if (fAndOr != null && fAndOr.AndOr == andOr)
                {
                    Filters.AddRange(fAndOr.Filters);
                }
                else
                {
                    Filters.Add(filter);
                }
            }

            AndOr = andOr;
        }

        public override string Render()
        {
            var renders = Filters.Select(f => f.Render()).Where(f => !string.IsNullOrWhiteSpace(f)).ToList();

            switch (renders.Count)
            {
                case 0:
                    return string.Empty;

                case 1:
                    return renders[0];

                default:
                    return $"({string.Join($" {AndOr} ", renders)})";
            }
        }
    }
}
