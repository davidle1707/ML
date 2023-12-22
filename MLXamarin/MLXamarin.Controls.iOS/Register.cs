using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MLXamarin.Controls.iOS
{
    public static class Register
    {
        public static void Init()
        {
            CustomCheckBoxRenderer.Register();
            CustomCircleImageRenderer.Register();
            CustomDatePickerRenderer.Register();
            CustomEntryRenderer.Register();
            CustomIconViewRenderer.Register();
            CustomImageButtonRenderer.Register();
            CustomListViewRenderer.Register();
            CustomMapRenderer.Register();
            CustomPickerRenderer.Register();
            CustomShapeViewRenderer.Register();
            CustomTabbedPageRenderer.Register();
            CustomTimePickerRenderer.Register();
        }
    }
}
