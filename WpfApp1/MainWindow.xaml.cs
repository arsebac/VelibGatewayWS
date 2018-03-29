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
using WpfApp1.VelibServiceReference;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Contract[] contracts;
        private Station[] stations;

        public MainWindow()
        {
            Client = new VelibServiceReference.VelibServiceClient();
            contracts = Client.GetContractsList(true);
            InitializeComponent();
            comboBox.ItemsSource = contracts;
            comboStation.ItemsSource = stations;
        }

        private VelibServiceClient Client { get; set; }

        
        private void stationSelected(object sender, SelectionChangedEventArgs e)
        {
            if(comboStation.SelectedItem != null)
            {
                ab.Content = ((Station)comboStation.SelectedItem).available_bikes+"";
            }
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stations = Client.GetStationsByContract((Contract)comboBox.SelectedItem,true);
            comboStation.ItemsSource = stations;

        }
    }
}
