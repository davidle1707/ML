using System;

namespace ML.Utils.MarkLogic
{
    [Serializable]
    public enum XFuncCollection : short
    {
        None = 0,

        List = 1,

        First = 2,

        Elems = 3
    }

    [Serializable]
    public enum XFuncAggregation : short
    {
        None = 0,

        Count = 1,

        Sum = 2,

        Min = 3,

        Max = 4
    }
}
