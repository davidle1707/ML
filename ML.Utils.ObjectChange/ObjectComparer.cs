using ML.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ML.Utils.ObjectChange
{
    public class ObjectComparer
    {
        public Func<PropertyInfo, bool> FilterProperties { get; set; }

        public List<ChangeDetail> Compare(object original, object modified, Func<PropertyInfo, bool> extendFilterProps = null)
            => ProcessCompare(string.Empty, original, modified, extendFilterProps: extendFilterProps);

        private List<ChangeDetail> ProcessCompare(string parentName, object original, object modified, Func<PropertyInfo, bool> extendFilterProps = null, bool ignoreList = false)
        {
            if (original == null && modified == null)
            {
                return new List<ChangeDetail>();
            }

            var typeCompare = FindType(original, modified);

            if (original == null)
            {
                return new List<ChangeDetail> { new ChangeDetail(ChangedType.Add) { ObjFullName = parentName, NewValue = modified, ObjKindOf = GetObjeKindOf(typeCompare) } };
            }

            if (modified == null)
            {
                return new List<ChangeDetail> { new ChangeDetail(ChangedType.Remove) { ObjFullName = parentName, OldValue = original, ObjKindOf = GetObjeKindOf(typeCompare) } };
            }

            if (typeCompare != modified.GetType() || typeCompare != original.GetType())
            {
                return new List<ChangeDetail>();
            }

            var queryProps = typeCompare.GetProperties().Where(p => p.CanRead && p.CanWrite);
            if (FilterProperties != null)
            {
                queryProps = queryProps.Where(FilterProperties);
            }
            if (extendFilterProps != null)
            {
                queryProps = queryProps.Where(extendFilterProps);
            }

            var changes = queryProps.SelectMany(p => ProcessCompareRecursive(parentName, p, original, modified, ignoreCheckList: ignoreList)).ToList();

            return changes;
        }

        private IEnumerable<ChangeDetail> ProcessCompareRecursive(string parentName, PropertyInfo property, object original, object modified, bool ignoreCheckList = false)
        {
            var originalValue = original == null ? null : property.GetValue(original, null);
            var modifiedValue = modified == null ? null : property.GetValue(modified, null);

            if (originalValue == null && modifiedValue == null)
            {
                return new List<ChangeDetail>();
            }

            var type = FindType(originalValue, modifiedValue);
            var objType = GetObjeKindOf(type);
            var objFullName = $"{parentName}.{property.Name}";

            if (originalValue == null)
            {
                return new List<ChangeDetail> { new ChangeDetail(ChangedType.Add, property) { ObjFullName = objFullName, ObjKindOf = objType, NewValue = modifiedValue } };
            }

            if (modifiedValue == null)
            {
                //modifiedValue = Activator.CreateInstance(type);
                return new List<ChangeDetail> { new ChangeDetail(ChangedType.Remove, property) { ObjFullName = objFullName, ObjKindOf = objType, OldValue = originalValue } };
            }

            var changes = new List<ChangeDetail>();

            if (type.IsSimpleType())
            {
                var differences = IsDifference(originalValue, modifiedValue);
                if (differences)
                {
                    changes.Add(new ChangeDetail(ChangedType.Modify, property) { ObjFullName = objFullName, OldValue = originalValue, NewValue = modifiedValue });
                }

                goto Final;
            }

            if (!ignoreCheckList && objType == ObjectKindOf.List)
            {
                var gengericTypes = type.GenericTypeArguments;
                if (gengericTypes?.Length > 0 && typeof(ICheckListItem).IsAssignableFrom(gengericTypes[0]))
                {
                    var change = CompareList(objFullName, property, (IList)originalValue, (IList)modifiedValue, type.GenericTypeArguments[0]);
                    if (change != null)
                    {
                        changes.Add(change);
                    }
                }

                goto Final;
            }

            if (type.IsClass)
            {
                var differences = ProcessCompare(objFullName, originalValue, modifiedValue);
                if (differences.Count > 0)
                {
                    changes.Add(new ChangeDetail(ChangedType.Modify)
                    {
                        ObjInfo = property,
                        ObjKindOf = ObjectKindOf.Class,
                        ObjFullName = objFullName,
                        Childrens = differences
                    });
                }

                goto Final;
            }

        Final:
            return changes;
        }

        private Type FindType(object original, object modified)
        {
            return original?.GetType() ?? modified.GetType();
        }

        private bool IsDifference(object originalValue, object modifiedValue)
        {
            //return (originalValue as IComparable).CompareTo(modifiedValue) != 0;
            return !originalValue.Equals(modifiedValue);
        }

        private ObjectKindOf GetObjeKindOf(Type type)
        {
            if (type.IsSimpleType())
            {
                return ObjectKindOf.Simple;
            }

            if (typeof(ICollection).IsAssignableFrom(type) && type.GetInterface("System.Collections.IList", true) != null)
            {
                return ObjectKindOf.List;
            }

            if (type.IsClass)
            {
                return ObjectKindOf.Class;
            }

            return ObjectKindOf.Unknown;
        }

        #region Compare List

        private ChangeDetail CompareList(string propFullName, PropertyInfo propList, IList originalList, IList modifiedList, Type itemType)
        {
            if (originalList?.Count == 0 && modifiedList?.Count == 0)
            {
                return null;
            }

            var change = new ChangeDetail { ObjInfo = propList, ObjKindOf = ObjectKindOf.List, ObjFullName = propFullName, Childrens = new List<ChangeDetail>() };

            var originalListObj = originalList != null ? originalList.Cast<ICheckListItem>() : Enumerable.Empty<ICheckListItem>();
            var modifiedListObj = modifiedList != null ? modifiedList.Cast<ICheckListItem>() : Enumerable.Empty<ICheckListItem>();

            bool equalsItemId(ICheckListItem x, ICheckListItem y) => x.ListItemId == y.ListItemId;

            // add items
            foreach (var addItem in modifiedListObj.Where(o => originalListObj.All(m => !equalsItemId(o, m))))
            {
                change.Childrens.Add(new ChangeDetail(ChangedType.Add)
                {
                    ObjKindOf = ObjectKindOf.ListItem,
                    ListItemId = addItem.ListItemId,
                    ListItemDisplayName = addItem.ListItemName,
                    NewValue = addItem
                });
            }

            // remove items
            foreach (var removeItem in originalListObj.Where(o => modifiedListObj.All(m => !equalsItemId(o, m))))
            {
                change.Childrens.Add(new ChangeDetail(ChangedType.Remove)
                {
                    ObjKindOf = ObjectKindOf.ListItem,
                    ListItemId = removeItem.ListItemId,
                    ListItemDisplayName = removeItem.ListItemName,
                    OldValue = removeItem
                });
            }

            // modify items
            foreach (var orginalItem in originalListObj)
            {
                var modifiedItem = modifiedListObj.FirstOrDefault(a => a.ListItemId == orginalItem.ListItemId);
                if (modifiedItem == null)
                {
                    continue;
                }

                var differences = ProcessCompare(propFullName, orginalItem, modifiedItem, ignoreList: true); // if sub-level = lsst -> not check
                if (differences.Count > 0)
                {
                    var listItemId = orginalItem.ListItemId;
                    var listItemDisplayName = orginalItem.ListItemName ?? modifiedItem.ListItemName;
                    //var listItemIndex = ((IList)modifiedListObj).IndexOf(modifiedItem);

                    differences.ForEach(a =>
                    {
                        a.ListItemId = listItemId;
                        a.ListItemDisplayName = listItemDisplayName;
                    });

                    change.Childrens.Add(new ChangeDetail(ChangedType.Modify)
                    {
                        ObjKindOf = ObjectKindOf.ListItem,
                        ListItemId = listItemId,
                        ListItemDisplayName = listItemDisplayName,
                        Childrens = differences
                    });
                }
            }

            if (change.Childrens.Count == 0)
            {
                return null; // no-change
            }

            change.Type = ChangedType.Modify;

            return change;
        }

        #endregion
    }
}
