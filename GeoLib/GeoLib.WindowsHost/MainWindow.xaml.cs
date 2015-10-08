using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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
using GeoLib.Services;
using GeoLib.WindowsHost.Services;

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost _hostGeoManager = null;
        ServiceHost _hostMessageManager = null;

        public static MainWindow MainUI { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            MainUI = this;          

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager = new ServiceHost(typeof(GeoManager));
            _hostGeoManager.Open();

            _hostMessageManager = new ServiceHost(typeof(MessageManager));
            _hostMessageManager.Open();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            if (_hostGeoManager != null)
            _hostGeoManager.Close();

            if (_hostMessageManager != null)
                _hostMessageManager.Close();

        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            lblMessage.Content = message + Environment.NewLine + "Thread ID: " + threadId + " | Process: " + Process.GetCurrentProcess().Id.ToString();
        }

        //#region Fixes Entity Framework Error

        //private void EFBugFix()
        //{
        //    var instanceExists = System.Data.Entity.SqlServer.SqlProviderServices.Instance != null;
        //}
        //#endregion
    }
}
