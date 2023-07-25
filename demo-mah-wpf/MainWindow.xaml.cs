using demo_mah_wpf.Entity;
using MahApps.Metro.Controls;
using Serilog;
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
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModel ViewModel)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("logs/log-.txt",
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}]",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    encoding: Encoding.UTF8,
                    retainedFileCountLimit: 10,
                    fileSizeLimitBytes: 10 * 1024)
                .CreateLogger();

            try
            {
                InitializeComponent();

                Components.Add(new Object());
                Components.Add(new Object());

                this.DisplayCurrentDateTime();

                //this.DataContext = new ViewModel();
                this.DataContext = ViewModel;

                //this.RightToLeftMarquee(20);

                this.curDateTimeTxtBlock.SetBinding(TextBlock.TextProperty, new Binding("Date"));

                Log.Verbose("Hello");
                Log.Debug("Hello");
                Log.Information("Hello");
                Log.Warning("Hello");
                Log.Error("Hello");
                Log.Fatal("Hello");

                //Console.WriteLine("Hello, World!");
            }
            catch (Exception ex)
            {
                // 紀錄你的應用程式中未被捕捉的例外 (Unhandled Exception)
                Log.Error(ex, "Something went wrong");
            }
            finally
            {
                // 將最後剩餘的 Log 寫入到 Sinks 去！
                Log.CloseAndFlush();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var a = Application.Current.MainWindow;
            var b = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            // set full screen, this will break the Mah windows property SaveWindowPosition="True|False"
            //this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;

            //((App)Application.Current).UpdateWindow(sender as Window);

            var senderWindow = sender as Window;
            senderWindow.WindowState = WindowState.Maximized;
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
