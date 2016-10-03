
namespace WcfWindowsService
{
    using System;
    using System.ServiceProcess;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                var service = new WindowsService();
                service.TestStartupAndStop();
            }
            else
            {
                var servicesToRun = new ServiceBase[]
                                              {
                                                  new WindowsService()
                                              };

                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
