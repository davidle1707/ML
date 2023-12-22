using System;

namespace CMS.Common.Attributes
{
    public class CmsFilterAttribute : Attribute
    {
        public bool InPipeline { get; set; }

        public bool InReporting { get; set; }
    }
}
