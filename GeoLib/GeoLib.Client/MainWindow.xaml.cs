using System;
using System.Collections.Generic;
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
using GeoLib.Client.Contracts;
using GeoLib.Contracts;
using GeoLib.Proxies;

namespace GeoLib.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();         
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text == "") return;

            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
            NetTcpBinding binding = new NetTcpBinding();
            binding.MaxReceivedMessageSize = 2097152;

            using (GeoClient client = new GeoClient(binding, address))
            {
                IEnumerable<ZipCodeData> data = client.GetZips(txtState.Text);

                if (data == null) return;

                lstZips.ItemsSource = data;

            }
        }

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text == "") return;

            using (GeoClient proxy = new GeoClient("httpEP"))
            {
                ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);

                if (data == null) return;

                lblCity.Content = data.City;
                lblState.Content = data.State;
            }   
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8010/MessageService");
            NetTcpBinding binding = new NetTcpBinding();
            binding.MaxReceivedMessageSize = 2097152;

            //ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            using (ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>(binding, address))
            {
                IMessageService proxy = factory.CreateChannel();
                proxy.DisplayMessage(txtMessage.Text);
            }
            
        }       
    }
}
