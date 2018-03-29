using ConsoleClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Comparaisons
    {
        /// <summary>
        /// Compare two strings and return if one contains another
        /// </summary>
        /// <param name="a">the first string to compare</param>
        /// <param name="b">the second string to compare</param>
        /// <returns></returns>
        public bool CompareFormatted(string a,string b)
        {
            string formattedA = a.ToLower();
            string formattedB = b.ToLower();
            return formattedA.Contains(formattedB);
        }
        public bool ContractCorrespond(Contract contract,string query)
        {
            return CompareFormatted(contract.name,query) || CompareFormatted(contract.commercial_name,query);
        }
        public bool StationCorrespond(Station station, string query)
        {
            return CompareFormatted(station.name, query);
        }
    }
    class Program
    {
        static Comparaisons comparaisons = new Comparaisons();
        static ServiceReference1.VelibServiceClient client = new VelibServiceClient();
        static void Main(string[] args)
        {
            Contract[] contracts = client.GetContractsList(true);
            bool end = false;
            do
            {
                Console.WriteLine("Entrez le nom d'un contrat, entrez '?' pour avoir la liste, ou laissez blanc pour quitter");
                String contractRequest;
                contractRequest = Console.ReadLine();
                if (contractRequest.Equals("?"))
                {
                    foreach (Contract c in contracts)
                    {
                        Console.WriteLine(c.commercial_name + "(" + c.name + ")");
                    }
                }
                else if (contractRequest.Length == 0)
                {
                    end = true;
                }
                else
                {
                    foreach (Contract c in contracts)
                    {
                        if (comparaisons.ContractCorrespond(c,contractRequest))
                        {
                            checkStations(c);
                            break;
                        }
                    }

                }
            } while (!end);
        }
        public static void checkStations(Contract contract)
        {
            Station[] stations = client.GetStationsByContract(contract,true);
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

                            if (comparaisons.StationCorrespond(station,request))
                            {
                                Console.WriteLine($"Il y a actuellement {station.available_bikes} vélos disponible à la station {station.name}");
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
