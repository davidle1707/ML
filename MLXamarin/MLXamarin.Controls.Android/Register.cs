using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MLXamarin.Controls.Android
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
            CustomMapRenderer.Register();
            CustomShapeViewRenderer.Register();
            CustomTimePickerRenderer.Register();
        }
    }
}
