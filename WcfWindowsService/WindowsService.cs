
namespace WcfWindowsService
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.ServiceProcess;

    using WcfServiceLibrary;

    public partial class WindowsService : ServiceBase
    {
        private ServiceHost _svcHost;

        public WindowsService()
        {
            this.InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this._svcHost?.Close();

            var httpAddress = "http://localhost:9001/Service";
            var tcpAddress = "net.tcp://localhost:9002/Service";

            Uri[] addressBase = { new Uri(httpAddress), new Uri(tcpAddress) };
            this._svcHost = new ServiceHost(typeof(Service), addressBase);

            var metadataBehaviour = new ServiceMetadataBehavior();
            this._svcHost.Description.Behaviors.Add(metadataBehaviour);

            var httpBinding = new BasicHttpBinding();
            this._svcHost.AddServiceEndpoint(typeof(IService), httpBinding, httpAddress);
            this._svcHost.AddServiceEndpoint(typeof(IMetadataExchange),
            MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            var tcpBinding = new NetTcpBinding();
            this._svcHost.AddServiceEndpoint(typeof(IService), tcpBinding, tcpAddress);
            this._svcHost.AddServiceEndpoint(typeof(IMetadataExchange),
            MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

            this._svcHost.Open();
        }

        protected override void OnStop()
        {
            if (this._svcHost != null)
            {
                this._svcHost.Close();
                this._svcHost = null;
            }
        }

        public void TestStartupAndStop()
        {
            this.OnStart(null);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
