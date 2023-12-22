namespace ML.Utils.MarkLogic
{
    public abstract class XFilter<TEntity> : XFilter
    {
        public static XFilter<TEntity> Empty { get; } = new XStringFilter<TEntity>();

        public static XFilter<TEntity> operator &(XFilter<TEntity> left, XFilter<TEntity> right)
        {
            return new XAndOrFilter<TEntity>(new[] { left, right }, "and");
        }

        public static XFilter<TEntity> operator &(XFilter<TEntity> left, string right)
        {
            return new XAndOrFilter<TEntity>(new[] { left, new XStringFilter<TEntity>(right) }, "and");
        }

        public static XFilter<TEntity> operator |(XFilter<TEntity> left, XFilter<TEntity> right)
        {
            return new XAndOrFilter<TEntity>(new[] { left, right }, "or");
        }

        public static XFilter<TEntity> operator |(XFilter<TEntity> left, string right)
        {
            return new XAndOrFilter<TEntity>(new[] { left, new XStringFilter<TEntity>(right) }, "or");
        }
    }

    public abstract class XFilter
    {
        public abstract string Render();
    }
}
