using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelibGatewayWS
{
    class CacheManager
    {
        public int CacheSecondLimit
        {
            get{
                return int.Parse(ConfigurationManager.AppSettings["cacheLimit"]);
            }
        }
        private static CacheManager _cacheManger;
        public static CacheManager Instance
        {
            get
            {
                if(_cacheManger == null)
                {
                    _cacheManger = new CacheManager();
                }
                return _cacheManger;
            }
        }
        private DataTable contracts;
        private DataTable stations;

        private CacheManager()
        {
            contracts = new DataTable();
            contracts.Columns.Add("contracts", typeof(List<Contract>));
            contracts.Columns.Add("timestamp", typeof(DateTime));
            stations = new DataTable();

            stations.Columns.Add("contract", typeof(Contract));
            stations.Columns.Add("stations", typeof(List<Station>));
            stations.Columns.Add("timestamp", typeof(DateTime));
        }

        internal void AddContractsCache(List<Contract> newContracts)
        {
            DataRow row = contracts.NewRow();
            row[0] = newContracts;
            row[1] = DateTime.Now;
        }

        public List<Contract> GetContracts()
        {
            DateTime currentTime = DateTime.Now;
            List<Contract> list = new List<Contract>();
            if (contracts.Select().Length == 0)
            {
                return null;
            }

            DataRow latestData = contracts.Rows[contracts.Rows.Count - 1];
            if ((currentTime- (DateTime)latestData[1]).TotalSeconds <= CacheSecondLimit)
            {
                return (List<Contract>)latestData[0];
            }
            else
            {
                return null;
            }
           
        }

        internal List<Station> GetStations(Contract contract)
        {
            DataRow[] foundRows = stations.Select("contract = '" + contract+"'");
            if(foundRows.Length != 0)
            {
                DataRow last = foundRows[foundRows.Length - 1];
                if ((DateTime.Now - (DateTime)last[2]).TotalSeconds <= CacheSecondLimit)
                {
                    return (List<Station>)last[1];
                }
            }
            return null;
        }

        internal void AddStationsCache(List<Station> stationsCache, Contract contract)
        {
            DataRow row = stations.NewRow();
            row[0] = contract;
            row[1] = stationsCache;
            row[2] = DateTime.Now;

        }
    }
}
