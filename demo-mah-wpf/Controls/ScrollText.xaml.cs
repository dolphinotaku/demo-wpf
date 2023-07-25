using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo_mah_wpf.Controls
{
    /// <summary>
    /// Interaction logic for ScrollText.xaml
    /// </summary>
    public partial class ScrollText : UserControl
    {
        public ScrollText()
        {
            InitializeComponent();
        }

        private void tb1_Loaded(object sender, RoutedEventArgs e)
        {
            tb1.Width = tb1.ActualWidth;
        }

        // hints, simple type propdp then press tab x 2

        /// <summary>
        /// 
        /// </summary>
        public object Text
        {
            get { return (object)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(object), typeof(ScrollText), new PropertyMetadata(null));

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(ScrollText), new PropertyMetadata(null));

    }
}
