using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VelibGatewayWS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "VelibService" à la fois dans le code et le fichier de configuration.
    public class VelibService : IVelibService
    {
        private Util util;
        public VelibService()
        {
            util = new Util();
        }

        public List<Contract> GetContractsList()
        {
            return JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts"));
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

        public List<Station> GetStationsByContract(Contract contract)
        {
            string res = util.ApiRequest("stations?contract=" + contract.name);
            Console.WriteLine(res);
            return JsonConvert.DeserializeObject<List<Station>>(res);
        }

        public List<Contract> SearchContract(string contractRequest)
        {
            List<Contract> contracts = JsonConvert.DeserializeObject<List<Contract>>(util.ApiRequest("contracts"));
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
