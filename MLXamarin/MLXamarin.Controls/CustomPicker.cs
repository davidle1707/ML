using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MLXamarin.Controls
{
    //https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Forms/XLabs.Forms/Controls/BindablePicker.cs
    public class CustomPicker : Picker
    {
        /// <summary>
		/// The HasBorder property
		/// </summary>
		public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create<CustomPicker, bool>(p => p.HasBorder, true);

        /// <summary>
        /// The font property
        /// </summary>
        public static readonly BindableProperty FontProperty =
            BindableProperty.Create<CustomPicker, Font>(p => p.Font, new Font());

        /// <summary>
        /// The TextColor property
        /// </summary>
        public static readonly BindableProperty TextColorProperty = 
            BindableProperty.Create<CustomPicker, Color>(p => p.TextColor, Color.Black);
        
        /// <summary>
        /// The PlaceholderTextColor property
        /// </summary>
        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create<CustomPicker, Color>(p => p.PlaceholderTextColor, Color.Default);

        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Font
        /// </summary>
        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Sets color for placeholder text color
        /// </summary>
        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

        public string DisplayMember { get; set; }

        public string ValueMember { get; set; }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public Object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                if (SelectedItem != value)
                {
                    SetValue(SelectedItemProperty, value);
                    ProcessSelectedItemChanged();
                }
            }
        }

        public Object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set
            {
                SetValue(SelectedValueProperty, value);
                ProcessSelectedValueChanged();
            }
        }

        public CustomPicker()
        {
            this.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CustomPicker),
                null, propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(CustomPicker),
                null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty SelectedValueProperty = BindableProperty.Create("SelectedValue", typeof(object), typeof(CustomPicker),
                null, BindingMode.TwoWay, propertyChanged: OnSelectedValueChanged);

        bool _disableNestedCalls;

        void ProcessSelectedItemChanged()
        {
            if (_disableNestedCalls)
            {
                return;
            }

            var selectedIndex = -1;
            Object selectedValue = null;
            if (ItemsSource != null)
            {
                var index = 0;
                var hasSelectedValuePath = !String.IsNullOrWhiteSpace(ValueMember);
                foreach (var item in ItemsSource)
                {
                    if (item != null && item.Equals(SelectedItem))
                    {
                        selectedIndex = index;
                        if (hasSelectedValuePath)
                        {
                            var type = item.GetType();
                            var prop = type.GetRuntimeProperty(ValueMember);
                            selectedValue = prop.GetValue(item);
                        }
                        break;
                    }
                    index++;
                }
            }
            _disableNestedCalls = true;
            SelectedValue = selectedValue;
            SelectedIndex = selectedIndex;
            _disableNestedCalls = false;
        }

        void ProcessSelectedValueChanged()
        {
            if (_disableNestedCalls)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(ValueMember))
            {
                return;
            }

            var selectedIndex = -1;
            var selectedItem = (object)null;

            if (ItemsSource != null)
            {
                var index = 0;
                foreach (var item in ItemsSource)
                {
                    if (item != null)
                    {
                        var prop = item.GetType().GetRuntimeProperty(ValueMember);
                        var itemValue = prop.GetValue(item);

                        if (Equals(itemValue, SelectedValue))
                        {
                            selectedIndex = index;
                            selectedItem = item;
                            break;
                        }
                    }

                    index++;
                }
            }

            _disableNestedCalls = true;
            SelectedItem = selectedItem;
            SelectedIndex = selectedIndex;
            _disableNestedCalls = false;
        }

        static void OnItemsSourceChanged(BindableObject bindable, Object oldValue, Object newValue)
        {
            if (Equals(newValue, null) && Equals(oldValue, null))
            {
                return;
            }

            var picker = (CustomPicker)bindable;
            picker.Items.Clear();

            if (!Equals(newValue, null))
            {
                var hasDisplayMemberPath = !String.IsNullOrWhiteSpace(picker.DisplayMember);

                foreach (var item in (IEnumerable)newValue)
                {
                    if (hasDisplayMemberPath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(picker.DisplayMember);
                        picker.Items.Add(prop.GetValue(item).ToString());
                    }
                    else
                    {
                        picker.Items.Add(item.ToString());
                    }
                }

                picker._disableNestedCalls = true;
                picker.SelectedIndex = -1;
                picker._disableNestedCalls = false;

                if (picker.SelectedItem != null)
                {
                    picker.ProcessSelectedItemChanged();
                }
                else if (hasDisplayMemberPath && picker.SelectedValue != null)
                {
                    picker.ProcessSelectedValueChanged();
                }
            }
            else
            {
                picker._disableNestedCalls = true;
                picker.SelectedIndex = -1;
                picker.SelectedItem = null;
                picker.SelectedValue = null;
                picker._disableNestedCalls = false;
            }
        }

        void OnSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (_disableNestedCalls)
            {
                return;
            }

            if (SelectedIndex < 0 || ItemsSource == null || !ItemsSource.GetEnumerator().MoveNext())
            {
                _disableNestedCalls = true;
                if (SelectedIndex != -1)
                {
                    SelectedIndex = -1;
                }
                SelectedItem = null;
                SelectedValue = null;
                _disableNestedCalls = false;
                return;
            }

            _disableNestedCalls = true;

            var index = 0;
            var hasSelectedValuePath = !String.IsNullOrWhiteSpace(ValueMember);
            foreach (var item in ItemsSource)
            {
                if (index == SelectedIndex)
                {
                    SelectedItem = item;
                    if (hasSelectedValuePath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(ValueMember);
                        SelectedValue = prop.GetValue(item);
                    }

                    break;
                }
                index++;
            }

            _disableNestedCalls = false;
        }

        static void OnSelectedItemChanged(BindableObject bindable, Object oldValue, Object newValue)
        {
            var boundPicker = (CustomPicker)bindable;
            boundPicker.ItemSelected?.Invoke(boundPicker, new SelectedItemChangedEventArgs(newValue));
            boundPicker.ProcessSelectedItemChanged();
        }

        static void OnSelectedValueChanged(BindableObject bindable, Object oldValue, Object newValue)
        {
            var boundPicker = (CustomPicker)bindable;
            boundPicker.ProcessSelectedValueChanged();
        }


    }
}
