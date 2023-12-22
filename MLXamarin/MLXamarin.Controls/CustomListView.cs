using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace MLXamarin.Controls
{
    public class CustomListView : ListView
    {
        #region HasScrollProperty

        public static readonly BindableProperty HasScrollProperty = BindableProperty.Create("HasScroll", typeof(bool), typeof(CustomListView), true);

        public bool HasScroll
        {
            get { return (bool)GetValue(HasScrollProperty); }
            set { SetValue(HasScrollProperty, value); }
        }

        #endregion

        #region LoadMoreCommandProperty

        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create("LoadMoreCommand", typeof(ICommand), typeof(CustomListView));

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        #endregion

        public CustomListView()
        {
            ItemAppearing += OnItemAppearing;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;

            if (items != null && e.Item == items[items.Count - 1])
            {
                var loadMore = LoadMoreCommand;

                if (loadMore != null && loadMore.CanExecute(null))
                {
                    loadMore.Execute(null);
                }
            }
        }
    }
}
