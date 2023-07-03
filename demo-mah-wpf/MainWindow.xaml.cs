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

            this.DataContext = new TaskViewModel();
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

        private void btnClickMe_Click(object sender, RoutedEventArgs e)
        {
            lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
            lbResult.Items.Add(this.FindResource("strWindow").ToString());
            lbResult.Items.Add(Application.Current.FindResource("strApp").ToString());
        }
    }

}
