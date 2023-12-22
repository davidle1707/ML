namespace ML.Utils.MarkLogic
{
    public class XNodeFunc
    {
        #region IsInUse

        public static string IsInUse(object value, params string[] ignoreEntities)
        {
            return $"xdmp:estimate(cts:search(fn:doc(), cts:and-query('{value}')))";
        }

        #endregion
    }
}
