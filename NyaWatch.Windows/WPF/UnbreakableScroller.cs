using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace NyaWatch.Windows.WPF
{
    public class UnbreakableScroller : ScrollViewer
    {
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            var oldScrollable = oldContent as Control;
            var newScrollable = newContent as Control;

            if (oldScrollable != null)
            {
                oldScrollable.PreviewMouseWheel -= CaptureVerticalScroll;
            }

            if (newScrollable != null)
            {
                newScrollable.PreviewMouseWheel += CaptureVerticalScroll;
            }
        }

        void CaptureVerticalScroll(object sender, EventArgs e)
        {
            var mouse = e as MouseWheelEventArgs;
            if (e != null)
            {
                ScrollToVerticalOffset(VerticalOffset - mouse.Delta);
            }
        }
    }
}
