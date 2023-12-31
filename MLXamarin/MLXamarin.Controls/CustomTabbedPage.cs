﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
namespace MLXamarin.Controls
{
    /// <summary>
	///     Delegate CurrentPageChangingEventHandler.
	/// </summary>
	public delegate void CurrentPageChangingEventHandler();

    /// <summary>
    /// Delegate CurrentPageChangedEventHandler.
    /// </summary>
    public delegate void CurrentPageChangedEventHandler();

    /// <summary>
    /// Delegate SwipeLeftEventHandler
    /// </summary>
    public delegate void SwipeLeftEventHandler();

    /// <summary>
    /// Delegate SwipeRightEventHandler
    /// </summary>
    public delegate void SwipeRightEventHandler();

    //https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Forms/XLabs.Forms/Controls/ExtendedTabbedPage.cs
    public class CustomTabbedPage : TabbedPage
    {
        #region TintColorProperty

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create("TintColor", typeof(Color), typeof(CustomTabbedPage), Color.White);

        /// <summary>
        /// Gets or sets the color of the tint.
        /// </summary>
        /// <value>The color of the tint.</value>
        public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }

        #endregion

        #region BarTintColorProperty

        /// <summary>
        /// The bar tint color property
        /// </summary>
        public static readonly BindableProperty BarTintColorProperty = BindableProperty.Create("BarTintColor", typeof(Color), typeof(CustomTabbedPage), Color.White);

        /// <summary>
        /// Gets or sets the color of the bar tint.
        /// </summary>
        /// <value>The color of the bar tint.</value>
        public Color BarTintColor
        {
            get { return (Color)GetValue(BarTintColorProperty); }
            set { SetValue(BarTintColorProperty, value); }
        }

        #endregion

        #region BadgesProperty

        /// <summary>
        /// The badges property
        /// </summary>
        public static readonly BindableProperty BadgesProperty = BindableProperty.Create("Badges", typeof(List<string>), typeof(CustomTabbedPage));

        /// <summary>
        /// Gets or sets the badges.
        /// </summary>
        /// <value>The badges.</value>
        public List<string> Badges
        {
            get { return (List<string>)GetValue(BadgesProperty); }
            set { SetValue(BadgesProperty, value); }
        }

        #endregion

        #region TabBarSelectedImageProperty

        /// <summary>
        /// The tab bar selected image property
        /// </summary>
        public static readonly BindableProperty TabBarSelectedImageProperty = BindableProperty.Create("TabBarSelectedImage", typeof(string), typeof(CustomTabbedPage));

        /// <summary>
        /// Gets or sets the tab bar selected image.
        /// </summary>
        /// <value>The tab bar selected image.</value>
        public string TabBarSelectedImage
        {
            get { return (string)GetValue(TabBarSelectedImageProperty); }
            set { SetValue(TabBarSelectedImageProperty, value); }
        }

        #endregion

        #region TabBarBackgroundImageProperty

        /// <summary>
        /// The tab bar background image property
        /// </summary>
        public static readonly BindableProperty TabBarBackgroundImageProperty = BindableProperty.Create("TabBarBackgroundImage", typeof(string), typeof(CustomTabbedPage));

        /// <summary>
        /// Gets or sets the tab bar background image.
        /// </summary>
        /// <value>The tab bar background image.</value>
        public string TabBarBackgroundImage
        {
            get { return (string)GetValue(TabBarBackgroundImageProperty); }
            set { SetValue(TabBarBackgroundImageProperty, value); }
        }

        #endregion

        #region ItemTemplateSelectorProperty

        /// <summary>
        /// The item template selector property
        /// </summary>
        public static readonly BindableProperty ItemTemplateSelectorProperty = BindableProperty.Create<CustomTabbedPage, DataTemplateSelector>(x => x.ItemTemplateSelector, default(DataTemplateSelector), propertyChanged: OnDataTemplateSelectorChanged);

        private static void OnDataTemplateSelectorChanged(BindableObject bindable, DataTemplateSelector oldvalue, DataTemplateSelector newvalue)
        {
            ((CustomTabbedPage)bindable).OnDataTemplateSelectorChanged(oldvalue, newvalue);
        }

        private DataTemplateSelector currentItemSelector;
        /// <summary>
        /// Gets or sets the item template selector.
        /// </summary>
        /// <value>The item template selector.</value>
        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        #endregion
        
        /// <summary>
        /// Called when [data template selector changed].
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="System.ArgumentException">Cannot set both ItemTemplate and ItemTemplateSelector;ItemTemplateSelector</exception>
        protected virtual void OnDataTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            // check to see we don't have an ItemTemplate set
            if (ItemTemplate != null && newValue != null)
                throw new ArgumentException("Cannot set both ItemTemplate and ItemTemplateSelector", "ItemTemplateSelector");

            // cache value locally
            currentItemSelector = newValue;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomTabbedPage" /> class.
        /// </summary>
        public CustomTabbedPage()
        {
            PropertyChanging += OnPropertyChanging;
            PropertyChanged += OnPropertyChanged;
            OnSwipeLeft += SwipeLeft;
            OnSwipeRight += SwipeRight;

            this.SwipeEnabled = false;

            Badges = new List<string>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [swipe enabled].
        /// </summary>
        /// <value><c>true</c> if [swipe enabled]; otherwise, <c>false</c>.</value>
        public bool SwipeEnabled { get; set; }

        /// <summary>
        ///     Occurs when [current page changing].
        /// </summary>
        public event CurrentPageChangingEventHandler CurrentPageChanging;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        /// <summary>
        /// Occurs when [current page changed].
        /// </summary>
        public event CurrentPageChangedEventHandler CurrentPageChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        /// <summary>
        /// Occurs when the TabbedPage is swipped Right
        /// </summary>
        public event EventHandler OnSwipeRight;

        /// <summary>
        /// Occurs when the TabbedPage is swipped Left
        /// </summary>
        public event EventHandler OnSwipeLeft;

        /// <summary>
        /// Invokes the item SwipeRight event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item">Item.</param>
        public void InvokeSwipeRightEvent(object sender, object item)
        {
            if (OnSwipeRight != null)
            {
                OnSwipeRight.Invoke(sender, new EventArgs());
            }
        }

        /// <summary>
        /// Invokes the SwipeLeft event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item">Item.</param>
        public void InvokeSwipeLeftEvent(object sender, object item)
        {
            if (OnSwipeLeft != null)
            {
                OnSwipeLeft.Invoke(sender, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the <see cref="E:PropertyChanging" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Xamarin.Forms.PropertyChangingEventArgs" /> instance containing the event data.</param>
        private void OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "CurrentPage")
            {
                RaiseCurrentPageChanging();
            }
        }

        /// <summary>
        ///     Handles the <see cref="E:PropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPage")
            {
                RaiseCurrentPageChanged();
            }
        }

        /// <summary>
        /// Move to the previous Tabbed Page
        /// </summary>
        /// <param name="a"></param>
        /// <param name="e"></param>
        private void SwipeLeft(object a, EventArgs e)
        {
            if (SwipeEnabled)
            {
                PreviousPage();
            }
        }

        /// <summary>
        /// Move to the next Tabbed Page
        /// </summary>
        /// <param name="a"></param>
        /// <param name="e"></param>
        private void SwipeRight(object a, EventArgs e)
        {
            if (SwipeEnabled)
            {
                NextPage();
            }
        }
        /// <summary>
        ///     Raises the current page changing.
        /// </summary>
        private void RaiseCurrentPageChanging()
        {
            var handler = CurrentPageChanging;

            if (handler != null)
            {
                handler();
            }
        }

        /// <summary>
        ///     Raises the current page changed.
        /// </summary>
        private void RaiseCurrentPageChanged()
        {
            var handler = CurrentPageChanged;

            if (handler != null)
            {
                handler();
            }
        }

        /// <summary>
        /// Move to the next page.
        /// Restart at the first page should you try 
        /// to move past the last page.
        /// </summary>
        private void NextPage()
        {
            var currentPage = Children.IndexOf(CurrentPage);

            currentPage++;

            if (currentPage > Children.Count - 1)
            {
                currentPage = 0;
            }

            CurrentPage = Children[currentPage];
        }

        /// <summary>
        /// Move to the previous page.
        /// If you are on the first page then return 
        /// the last page in the list
        /// </summary>
        private void PreviousPage()
        {
            var currentPage = Children.IndexOf(CurrentPage);

            currentPage--;

            if (currentPage < 0)
            {
                currentPage = Children.Count - 1;
            }

            CurrentPage = Children[currentPage];
        }

        /// <summary>
        /// Creates the default.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Page.</returns>
        protected override Page CreateDefault(object item)
        {
            var view = this.ViewFor(item, currentItemSelector);

            if (view != null)
            {
                var cp = new ContentPage
                {
                    BindingContext = item,
                    Content = view,
                };
                return cp;
            }


            return base.CreateDefault(item);
        }

    }
}
