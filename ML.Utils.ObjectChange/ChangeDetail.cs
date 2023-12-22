using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ML.Utils.ObjectChange
{
	public class ChangeDetail
	{
		public ChangeDetail()
		{
			Childrens = new List<ChangeDetail>();
		}

        public ChangeDetail(ChangedType type) : this()
        {
            Type = type;
        }

        public ChangeDetail(ChangedType type, MemberInfo member) : this(type)
        {
            ObjInfo = member;
        }

        public ChangedType Type { get; set; }

        public MemberInfo ObjInfo { get; set; }

		public ObjectKindOf ObjKindOf { get; set; } = ObjectKindOf.Simple;

		private string _objFullName;
        public string ObjFullName { get => _objFullName; set => _objFullName = value?.Trim(' ', '.'); }

        public object OldValue { get; set; }

		public object NewValue { get; set; }

        public string ListItemId { get; set; }

        public string ListItemDisplayName { get; set; }

        public List<ChangeDetail> Childrens { get; set; }

		public string GetMemberName() => GetMemberNameByAttribute<ChangeMemberNameAttribute>();

		public string GetMemberNameByAttribute<TAttribute>() where TAttribute : DescriptionAttribute
		{
			if (ObjInfo != null)
			{
				var attr = ObjInfo.GetCustomAttribute<TAttribute>();
				return string.IsNullOrWhiteSpace(attr?.Description) ? ObjInfo.Name:  attr.Description;
			}

            if (!string.IsNullOrWhiteSpace(ListItemDisplayName))
            {
				return ListItemDisplayName;
            }

			var value = OldValue ?? NewValue;

			if (value != null)
			{
				var attr = value.GetType().GetCustomAttribute<TAttribute>();
				return attr != null ? attr.Description : string.Empty;
			}

			return string.Empty;
		}

		public bool IsValid(bool checkInChilds = false)
		{
			var valid = Type != ChangedType.None || (ObjKindOf != ObjectKindOf.Unknown);

			if (checkInChilds && valid)
			{
				valid = Childrens.Any(c => c.IsValid());
			}

			return valid;
		}

	}

	public enum ChangedType
	{
		None,

		Add,

		Remove,

		Modify
	}

	public enum ObjectKindOf
    {
		Unknown,

		Simple,

		Class,

		List,

		ListItem
    }
}
