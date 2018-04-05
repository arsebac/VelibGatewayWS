using System.Collections.Generic;
using System.ServiceModel;

namespace VelibGatewayWS
{
    public interface IVelibGatewayCallback
    {

        [OperationContract(IsOneWay = true)]
        void CallBackContract(Contract contract);

        [OperationContract(IsOneWay = true)]
        void CallBackStations(Station stations);


    }
}