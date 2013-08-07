using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace NyaWatch.Windows.WPF
{
    /// <summary>
    /// TextBlock with FontAwesome Family.
    /// </summary>
    public class AwesomeText : TextBlock
    {
        static Uri _applicationUri = new Uri("pack://application:,,,/");

        public AwesomeText()
        {
            FontFamily = new FontFamily(_applicationUri, "/Resources/Fonts/#FontAwesome");
        }
    }
}
