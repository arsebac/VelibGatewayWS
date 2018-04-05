using ConsoleApp1.ServiceReference1;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal class CalcServiceCallbackSink: ServiceReference1.IVelibServiceCallback
    {
        private Program program;

       

        public CalcServiceCallbackSink(Program program)
        {
            this.program = program;
        }

        public void CallBackContract(Contract contract)
        {
            Console.WriteLine("Il y a un nouveau résultat : "+contract.name);
            Task.Factory.StartNew(() => program.checkStations(contract));
        }

        public void CallBackStations(Station stations)
        {
            Console.WriteLine("Mise à jour :Il y a " + stations.available_bikes + " dans la station " + stations.name);
        }
    }
}