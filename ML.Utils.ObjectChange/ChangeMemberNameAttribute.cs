using System.ComponentModel;

namespace ML.Utils.ObjectChange
{
	public class ChangeMemberNameAttribute : DescriptionAttribute
	{
		public ChangeMemberNameAttribute(string memberName) : base(memberName)
		{
		}
	}
}
