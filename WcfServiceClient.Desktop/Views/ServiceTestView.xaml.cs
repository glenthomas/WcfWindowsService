
namespace WcfServiceClient.Desktop.Views
{
    using System;
    using System.Windows;

    using WcfServiceClient;

    /// <summary>
    /// Interaction logic for ServiceTestView.xaml
    /// </summary>
    public partial class ServiceTestView
    {
        public ServiceTestView()
        {
            InitializeComponent();
        }

        private void OnTestButtonClick(object sender, RoutedEventArgs e)
        {
            using (var serviceClient = new MyServiceClient())
            {
                Output.AppendText($"{Environment.NewLine}{serviceClient.GetData(5)}");
            }
        }
    }
}
