using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using _3DSPRINTCloudDB;

namespace _3DSPRINTCloudAgent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ServiceBase[] ServicesToRun = new ServiceBase[] { new Service() };
                ServiceBase.Run(ServicesToRun);
            }
            else if (args.Length >= 1)
            {
                switch (args[0])
                {
                    case "-install":
                        CloudDBAgent.SetupDBLocation();
                        ServiceInstall.InstallService();
                        ServiceInstall.StartService();
                        break;
                    case "-uninstall":
                        ServiceInstall.StopService();
                        ServiceInstall.UninstallService();
                        break;
                    case "-stop":
                        ServiceInstall.StopService();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
