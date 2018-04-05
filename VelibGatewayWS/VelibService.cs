using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace VelibGatewayWS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "VelibService" à la fois dans le code et le fichier de configuration.
    [ServiceBehavior(ConcurrencyMode =ConcurrencyMode.Multiple)]
    public class VelibService : IVelibService
    {
        private CacheManager CacheManager;
        private Util util;
        Dictionary<string, Action<Contract>> ContractCB = new Dictionary<string, Action<Contract>>();
        Dictionary<Contract, List<KeyValuePair<string,Action<Station>>>> StationCallback = new Dictionary<Contract, List<KeyValuePair<string, Action<Station>>>>();

        public VelibService()
        {
            util = new Util();
            CacheManager = CacheManager.Instance;
        }

        private List<Contract> verifySubscriber(List<Contract> contrats)
        {
            if (ContractCB.Count < 1) return contrats;
            foreach (KeyValuePair<string, Action<Contract>> entry in ContractCB)
            {
                foreach (Contract c in contrats)
                {
                    if (c.IsThisContract(entry.Key))
                    {
                        entry.Value(c);
                    }
                }
            }
            return contrats;

        }
        
        public List<Contract> GetContractsList(bool useCache)
        {
            if (!useCache)
            {
                return verifySubscriber(JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts")));

            }
            List<Contract> contracts = CacheManager.GetContracts();
            if (contracts == null)
            {
                contracts = JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts"));
                CacheManager.AddContractsCache(contracts);
            }
            return verifySubscriber(contracts);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Station> GetStationsByContract(Contract contract, bool useCache)
        {
            if (useCache)
            {
                List<Station> stations = CacheManager.GetStations(contract);
                if (stations == null)
                {
                    string result = util.ApiRequest("stations?contract=" + contract.name);
                    stations = JsonConvert.DeserializeObject<List<Station>>(result);
                    CacheManager.AddStationsCache(stations, contract);
                }
                return CallbackStation(contract,stations);
            }

            string res = util.ApiRequest("stations?contract=" + contract.name);
            return CallbackStation(contract,JsonConvert.DeserializeObject<List<Station>>(res));
        }

        private List<Station> CallbackStation(Contract contract, List<Station> stations)
        {
            if (StationCallback.ContainsKey(contract))
            {
                foreach (KeyValuePair<string, Action<Station>> el in StationCallback[contract])
                {
                    foreach(Station testStation in stations)
                    {
                        if (el.Key.Equals(testStation.name))
                        {
                            el.Value(testStation);
                            break;
                        }
                    }
                }
            }
            return stations;
        }


        public List<Contract> SearchContract(string contractRequest, bool useCache)
        {
            List<Contract> contracts;
            if (useCache)
            {
                contracts = CacheManager.GetContracts();
                if (contracts == null)
                {
                    contracts = JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts"));
                    CacheManager.AddContractsCache(contracts);
                }
            }
            else
            {

                contracts = JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts"));
            }
            List<Contract> returnedContracts = new List<Contract>();
            foreach (Contract c in contracts)
            {
                if (c.IsThisContract(contractRequest))
                {
                    returnedContracts.Add(c);
                }
            }
            return returnedContracts;
        }

        public void SubscribeStationEvent(Contract contract, Station station, int T)
        {
            IVelibGatewayCallback subscriber = OperationContext.Current.GetCallbackChannel<IVelibGatewayCallback>();
            if (!StationCallback.ContainsKey(contract))
            {
                StationCallback[contract] = new List<KeyValuePair<string, Action<Station>>>();
            }
            var found = false;
            foreach(KeyValuePair<string, Action<Station>> el in StationCallback[contract])
            {
                if (el.Key.Equals(station.name))
                {
                    found = true;
                    Action<Station> actions = el.Value;
                    actions += subscriber.CallBackStations;
                }
            }
            if (!found)
            {
                Action<Station> newAction = delegate { };
                newAction += subscriber.CallBackStations;
                KeyValuePair<string, Action<Station>> i = new KeyValuePair<string, Action<Station>>(station.name, newAction);
                StationCallback[contract].Add(i);
            }
        }

        public void SubscribeContractEvent(string contract, int T)
        {
            IVelibGatewayCallback subscriber = OperationContext.Current.GetCallbackChannel<IVelibGatewayCallback>();
            
            if (!ContractCB.ContainsKey(contract))
            {
                ContractCB[contract] = delegate { };
            }
            ContractCB[contract] += subscriber.CallBackContract;

        }
    }
}
