
namespace WcfServiceClient
{
    using System;
    using System.ServiceModel;

    using WcfServiceClient.Service_References.ServiceReference;

    public class MyServiceClient : IDisposable
    {
        private readonly ServiceClient _serviceClient;

        public MyServiceClient()
        {
            _serviceClient = this.CreateClient();
        }

        private ServiceClient CreateClient()
        {
            var binding = new BasicHttpBinding();
            var serviceAddress = new EndpointAddress("http://localhost:9001/Service");

            return new ServiceClient(binding, serviceAddress);
        }

        public string GetData(int value)
        {
            return _serviceClient.GetData(value);
        }

        public void Dispose()
        {
            _serviceClient.Close();
        }
    }
}
