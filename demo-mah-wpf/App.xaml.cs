using demo_mah_wpf.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Unity;
using Unity.Resolution;
using Forms = System.Windows.Forms;
using Screen = System.Windows.Forms.Screen;

namespace demo_mah_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<string> Args { get; set; }
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public SingletonAppConfig singleton;

        private WindowCollection WindowCollection;
        private IUnityContainer container;
        private readonly Color[] _screenColors =
        {
            Colors.LightGray, Colors.DarkGray, Colors.Gray, Colors.SlateGray, Colors.DarkSlateGray
        };

        public void App_Startup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(e);

            this.singleton = SingletonAppConfig.GetInstance();
            // If no command line arguments were provided, don't process them if (e.Args.Length == 0) return;  
            if (e.Args.Length > 0)
            {
                //this.Args = e.Args.Where(parameter => { return parameter.Contains("="); }).Select(parameter=> { return sWhitespace.Replace(parameter.ToLower(), "").Replace(",", ""); }).ToList();
                singleton.SetApplicationArgs(e.Args);
            }

            //this.ExtractArgs(container, this.singleton.ArgsDict);

            this.container = new UnityContainer();
            InitContainer(this.container);

            this.UpdateWindow();
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
        private void InitContainer(IUnityContainer container)
        {
            container.RegisterType<IVoiceService, VoiceService>();
            container.RegisterType<IAutoMapperService, AutoMapperService>();
        }


        public void UpdateWindow(Window window = null)
        {
            Dictionary<string, object> keyValuePairs = this.singleton.ArgsDict;

            if (keyValuePairs.ContainsKey("monitor"))
            {
                window = this.ExtractArgMonitor(int.Parse(keyValuePairs["monitor"].ToString()));

                if (keyValuePairs.ContainsKey("screenmode")) this.ExtractArgScreenMode(window, keyValuePairs["screenmode"].ToString());
            }
            else if (!keyValuePairs.ContainsKey("monitor"))
            {
                window = this.ExtractArgMonitor(1);
                this.ExtractArgScreenMode(window, "window");
            }
        }

        public void ExtractArgScreenMode(Window window, string value)
        {
            //Application.Current.MainWindow.WindowState = WindowState.Maximized;
            //Application.Current.MainWindow.WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;
            if (window == null || String.IsNullOrEmpty(value)) return;


            if (value == "full")
            {
                window.WindowState = WindowState.Maximized;
                window.WindowStyle = System.Windows.WindowStyle.None;
            }
            else if (value == "window")
            {
                window.WindowState = WindowState.Normal;
                window.WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;
            }
        }
        public Window ExtractArgMonitor(int screenNum)
        {
            //int screenNum = int.Parse(value);
            Screen[] allScreen = Screen.AllScreens;

            // calculates text size in that main window (i.e. 100%, 125%,...)
            var ratio = Math.Max(Screen.PrimaryScreen.WorkingArea.Width / SystemParameters.PrimaryScreenWidth,
                                Screen.PrimaryScreen.WorkingArea.Height / SystemParameters.PrimaryScreenHeight);
            var pos = 0;

            Screen screen = allScreen[0];
            if (allScreen != null && allScreen.Length >= screenNum)
            {
                screen = allScreen[screenNum - 1];
            }

            // Show automata at all screen
            var mainViewModel = container.Resolve<ViewModel>(
                    new ParameterOverride("backgroundColor", _screenColors[Math.Min(pos++, _screenColors.Length - 1)]),
                    new ParameterOverride("primary", screen.Primary),
                    new ParameterOverride("displayName", screen.DeviceName));

            var window = new MainWindow(mainViewModel);
            if (screen.Primary)
                Current.MainWindow = window;

            window.Left = screen.WorkingArea.Left / ratio;
            window.Top = screen.WorkingArea.Top / ratio;
            window.Width = screen.WorkingArea.Width / ratio;
            window.Height = screen.WorkingArea.Height / ratio;
            window.Show();
            //window.WindowState = WindowState.Maximized;

            //ServiceProvider serviceProvider = services.BuildServiceProvider();
            //window = serviceProvider.GetService<MainWindow>();

            return window;
        }
    }

    /// <summary>
    /// The Singleton should always be a 'sealed' class to prevent class
    /// inheritance through external classes and also through nested classes.
    /// </summary>
    public sealed class SingletonAppConfig
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        private static readonly Lazy<SingletonAppConfig> lazy = new Lazy<SingletonAppConfig>(() => new SingletonAppConfig());

        /// <summary>
        /// The Singleton class defines the `GetInstance` method that serves as an
        /// alternative to constructor and lets clients access the same instance of
        /// this class over and over.
        /// </summary>
        public static SingletonAppConfig GetInstance()
        {
            return lazy.Value;
        }

        private List<string> args;
        private Dictionary<string, object> argsDict;
        public Dictionary<string, object> ArgsDict
        {
            get { return this.argsDict; }
            set { this.argsDict = value; }
        }

        private SingletonAppConfig()
        {
            this.args = new List<string>();
            this.ArgsDict = new Dictionary<string, object>();
        }
        private SingletonAppConfig(string[] appArgs) : base()
        {
            this.SetApplicationArgs(appArgs);
        }
        public void SetApplicationArgs(string[] appArgs)
        {
            this.args = this.CheckArgs(appArgs);
            this.ExtractArgs(this.args);
        }
        public List<string> CheckArgs(string[] appArgs)
        {
            List<string> args = new List<string>();
            if (appArgs.Length > 0)
            {
                args = appArgs.Where(parameter => { return parameter.Contains("="); }).Select(parameter => { return sWhitespace.Replace(parameter.ToLower(), "").Replace(",", ""); }).ToList();
            }
            return args;
        }
        public void ExtractArgs(List<string> _args)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            foreach (string _arg in _args)
            {
                List<string> split = _arg.Split('=').ToList();
                if (split.Count < 2) continue;

                keyValuePairs.Add(split[0], split[1]);
            }
            this.ArgsDict = keyValuePairs;
        }
    }

}

