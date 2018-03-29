using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VelibGatewayWS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "VelibService" à la fois dans le code et le fichier de configuration.
    public class VelibService : IVelibService
    {
        private CacheManager CacheManager;
        private Util util;
        public VelibService()
        {
            util = new Util();
            CacheManager = CacheManager.Instance;
        }

        public List<Contract> GetContractsList(bool useCache)
        {
            if (!useCache)
            {
                return JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts"));

            }
            List<Contract>  contracts = CacheManager.GetContracts();
            if(contracts == null)
            {
                contracts = JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts"));
                CacheManager.AddContractsCache(contracts);
            }
            return contracts;
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

        public List<Station> GetStationsByContract(Contract contract,bool useCache)
        {
            if (useCache)
            {
                List<Station> stations = CacheManager.GetStations(contract);
                if(stations == null)
                {
                    string result = util.ApiRequest("stations?contract=" + contract.name);
                    stations =  JsonConvert.DeserializeObject<List<Station>>(result);
                    CacheManager.AddStationsCache(stations,contract);
                }
                return stations;
            }

            string res = util.ApiRequest("stations?contract=" + contract.name);
            return JsonConvert.DeserializeObject<List<Station>>(res);
        }

        public List<Contract> SearchContract(string contractRequest,bool useCache)
        {
            List<Contract> contracts;
            if (useCache)
            {
                contracts = CacheManager.GetContracts();
                if(contracts == null)
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
    }
}
