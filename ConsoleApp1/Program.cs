using ConsoleApp1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Program
    {
        private static VelibServiceClient client;

        static void Main(string[] args)
        {
            CalcServiceCallbackSink objsink = new CalcServiceCallbackSink(new Program());
            InstanceContext iCntxt = new InstanceContext(objsink);
            client = new ServiceReference1.VelibServiceClient(iCntxt);
            Console.WriteLine("Entrez le nom d'un contrat");
            String contractRequest;
            contractRequest = Console.ReadLine();
            client.SubscribeContractEvent(contractRequest, 3);
            Task.Factory.StartNew(() =>
            {
                client.GetContractsList(true);
            });
            
            while (true)
            {
                Thread.Sleep(200);
            }
        }

        public void checkStations(Contract contract)
        {
            Station[] stations = client.GetStationsByContract(contract, true);
            String request;
            bool end = false;
            Console.WriteLine("Vous avez sélectionné le contrat " + contract.name + ". " + stations.Length + " stations disponibles\n");
            do
            {
                Console.WriteLine("Entrez le nom d'une station, '?' pour la liste des stations ou laissez vide pour quitter");
                request = Console.ReadLine();
                switch (request)
                {
                    case "":
                        end = true;
                        break;
                    case "?":
                        foreach (Station station in stations)
                        {
                            Console.WriteLine(station.name);
                        }
                        break;
                    default:
                        bool find = false;
                        foreach (Station station in stations)
                        {

                            if (station.name.ToLower().Contains(request))
                            {
                                client.SubscribeStationEvent(contract, station, 0);

                                Console.WriteLine($"Il y a actuellement {station.available_bikes} vélos disponible à la station {station.name}");

                                Task.Factory.StartNew(() => client.GetStationsByContract(contract, false));
                                find = true;
                                break;
                            }
                        }
                            if (!find)
                            {
                                Console.WriteLine("Aucunes stations pour la recherche '" + request + "'");
                            }
                            break;

                }
            } while (!end);

        }
    }
}
