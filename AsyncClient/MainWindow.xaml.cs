using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Threading;
using AsyncClient.VelibServiceReference;
namespace AsyncClient
{
    public class MTObservableCollection<T> : ObservableCollection<T>
    {
        public override event NotifyCollectionChangedEventHandler CollectionChanged;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var eh = CollectionChanged;
            if (eh != null)
            {
                Dispatcher dispatcher = (from NotifyCollectionChangedEventHandler nh in eh.GetInvocationList()
                                         let dpo = nh.Target as DispatcherObject
                                         where dpo != null
                                         select dpo.Dispatcher).FirstOrDefault();

                if (dispatcher != null && dispatcher.CheckAccess() == false)
                {
                    dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => OnCollectionChanged(e)));
                }
                else
                {
                    foreach (NotifyCollectionChangedEventHandler nh in eh.GetInvocationList())
                        nh.Invoke(this, e);
                }
            }
        }
    }
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MTObservableCollection<Contract> observableContracts;
        private MTObservableCollection<Station> observableStations;

        public MainWindow()
        {
            observableContracts = new MTObservableCollection<Contract>();
            observableStations = new MTObservableCollection<Station>();
            InitializeComponent();
            
            comboBox.ItemsSource = observableContracts;
            comboStation.ItemsSource = observableStations;
            Client = new VelibServiceClient();
            Client.GetContractsListAsync(true).ContinueWith(c=> contratLoaded(c.Result));
        }

        private void contratLoaded(Contract[] result)
        {
            foreach(Contract res in result)
            {
                if (!observableContracts.Contains(res))
                {
                    observableContracts.Add(res);
                }
            } 
        }

        private VelibServiceClient Client { get; set; }

        private void stationSelected(object sender, SelectionChangedEventArgs e)
        {
            if (comboStation.SelectedItem != null)
            {
                ab.Content = ((Station)comboStation.SelectedItem).available_bikes + "";
            }
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            for (int i = observableStations.Count - 1; i >= 0; i--) observableStations.RemoveAt(i);
            Client.GetStationsByContractAsync((Contract)comboBox.SelectedItem,true).ContinueWith(e => { foreach (Station s in e.Result) observableStations.Add(s); });
        }
    }
}
