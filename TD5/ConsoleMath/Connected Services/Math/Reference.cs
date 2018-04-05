//------------------------------------------------------------------------------
// <généré automatiquement>
//     Ce code a été généré par un outil.
//     //
//     Les changements apportés à ce fichier peuvent provoquer un comportement incorrect et seront perdus si
//     le code est regénéré.
// </généré automatiquement>
//------------------------------------------------------------------------------

namespace Math
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Math.IMathOperations")]
    public interface IMathOperations
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMathOperations/Add", ReplyAction="http://tempuri.org/IMathOperations/AddResponse")]
        System.Threading.Tasks.Task<int> AddAsync(int num1, int num2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMathOperations/Multiply", ReplyAction="http://tempuri.org/IMathOperations/MultiplyResponse")]
        System.Threading.Tasks.Task<int> MultiplyAsync(int num1, int num2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface IMathOperationsChannel : Math.IMathOperations, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class MathOperationsClient : System.ServiceModel.ClientBase<Math.IMathOperations>, Math.IMathOperations
    {
        
    /// <summary>
    /// Implémentez cette méthode partielle pour configurer le point de terminaison de service.
    /// </summary>
    /// <param name="serviceEndpoint">Point de terminaison à configurer</param>
    /// <param name="clientCredentials">Informations d'identification du client</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public MathOperationsClient(EndpointConfiguration endpointConfiguration) : 
                base(MathOperationsClient.GetBindingForEndpoint(endpointConfiguration), MathOperationsClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MathOperationsClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(MathOperationsClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MathOperationsClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(MathOperationsClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MathOperationsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(int num1, int num2)
        {
            return base.Channel.AddAsync(num1, num2);
        }
        
        public System.Threading.Tasks.Task<int> MultiplyAsync(int num1, int num2)
        {
            return base.Channel.MultiplyAsync(num1, num2);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.math1))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.math2))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Le point de terminaison nommé \'{0}\' est introuvable.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.math1))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/MathsLibrary/MathLib1/");
            }
            if ((endpointConfiguration == EndpointConfiguration.math2))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/MathsLibrary/MathLib2/");
            }
            throw new System.InvalidOperationException(string.Format("Le point de terminaison nommé \'{0}\' est introuvable.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            math1,
            
            math2,
        }
    }
}
