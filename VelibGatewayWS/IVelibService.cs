using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VelibGatewayWS
{
    
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract(CallbackContract = typeof( IVelibGatewayCallback))]
    public interface IVelibService
    {
        [OperationContract]
        List<Contract> GetContractsList(bool useCache);
        [OperationContract]
        List<Station> GetStationsByContract(Contract contract,bool useCache);
        [OperationContract]
        List<Contract> SearchContract(string contract, bool useCache);
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        void SubscribeContractEvent(string contract,int T);
        [OperationContract]
        void SubscribeStationEvent(Contract contract, Station station, int T);

        // TODO: ajoutez vos opérations de service ici
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "VelibGatewayWS.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
