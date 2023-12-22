namespace ML.Utils.MarkLogic
{
    public class XFilterOption
    {
        public bool LowerCaseIfString { get; set; } = true;

        public bool OnlyDateIfDateTime { get; set; }
    }
}
