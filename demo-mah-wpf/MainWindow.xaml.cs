using demo_mah_wpf.Entity;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace demo_mah_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Components.Add(new Object());
            Components.Add(new Object());

            //// The Work to perform on another thread 
            //ThreadStart start = delegate () {
            //    // ... 
            //    // This will work as its using the dispatcher 
            //    DispatcherOperation op = Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            //      new Action<string>(SetStatus), "From Other Thread (Async)");
            //    DispatcherOperationStatus status = op.Status;
            //    while (status != DispatcherOperationStatus.Completed)
            //    {
            //        status = op.Wait(TimeSpan.FromMilliseconds(1000));
            //        if (status == DispatcherOperationStatus.Aborted)
            //        {
            //            // Alert Someone 
            //        }
            //    }
            //};
            //// Create the thread and kick it started! 
            //new Thread(start).Start();

            this.DisplayCurrentDateTime();

            this.DataContext = new ViewModel();

            //this.RightToLeftMarquee(20);

            this.curDateTimeTxtBlock.SetBinding(TextBlock.TextProperty, new Binding("Date"));

            // set full screen, this will break the Mah windows property SaveWindowPosition="True|False"
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

        private void DisplayCurrentDateTime()
        {
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += DateTimeElementTick;
            LiveTime.Start();
        }

        private void DateTimeElementTick(object sender, EventArgs e)
        {
            this.curDateTimeTxtBlock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");
        }

        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            // Launch the GitHub site...
        }

        private void DeployCupCakes(object sender, RoutedEventArgs e)
        {
            // deploy some CupCakes...
        }


        private ObservableCollection<Object> components;
        public ObservableCollection<Object> Components
        {
            get
            {
                if (components == null)
                    components = new ObservableCollection<Object>();
                return components;
            }
        }


        public void StartRightToLeftMarquee(Panel container, Control labelOrTextBox, Duration duration)
        {
            //TimeSpan.FromSeconds(5)

            //var s = new StackPanel();
            //var t = new TextBox() { Text = "test" };
            //var b = new Button() { Content = "abcd" };
            container.Children.Add(labelOrTextBox);

            var fade = new DoubleAnimation()
            {
                From = 0,
                To = container.Width,
                Duration = duration,
            };

            Storyboard.SetTarget(fade, labelOrTextBox);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.OpacityProperty));

            var sb = new Storyboard();
            sb.Children.Add(fade);

            sb.Begin();
        }
        //private void btnClickMe_Click(object sender, RoutedEventArgs e)
        //{
        //    // use panel resource
        //    lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
        //    // use windows resource
        //    lbResult.Items.Add(this.FindResource("strWindow").ToString());
        //    // use app resource
        //    lbResult.Items.Add(Application.Current.FindResource("strApp").ToString());
        //}

        //private void LeftToRightMarquee(int _marqueeTimeInSeconds)
        //{
        //    double height = canMain.ActualHeight - tbmarquee.ActualHeight;
        //    tbmarquee.Margin = new Thickness(0, height / 2, 0, 0);
        //    DoubleAnimation doubleAnimation = new DoubleAnimation();
        //    doubleAnimation.From = -tbmarquee.ActualWidth;
        //    doubleAnimation.To = canMain.ActualWidth;
        //    doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
        //    doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds));
        //    tbmarquee.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        //}
        //private void RightToLeftMarquee(int _marqueeTimeInSeconds)
        //{
        //    double height = canMain.ActualHeight - tbmarquee.ActualHeight;
        //    tbmarquee.Margin = new Thickness(0, height / 2, 0, 0);
        //    DoubleAnimation doubleAnimation = new DoubleAnimation();
        //    doubleAnimation.From = -tbmarquee.ActualWidth;
        //    doubleAnimation.To = canMain.ActualWidth;
        //    doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
        //    doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds));
        //    tbmarquee.BeginAnimation(Canvas.RightProperty, doubleAnimation);
        //}

    }

}
