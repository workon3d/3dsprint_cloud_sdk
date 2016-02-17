using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Threading;
using _3DSPRINTCloudDB;

namespace _3DSPRINTCloudAgent
{
    class ServiceInstall
    {
        static string SERVICE_NAME = "3DSPRINTCloudAgent";

        private static bool IsInstalled()
        {
            using (ServiceController controller = 
                new ServiceController(SERVICE_NAME)) {
                try {
                    ServiceControllerStatus status = controller.Status;
                } catch {
                    return false;
                }
                return true;
            }
        }

        private static bool IsRunning()
        {
            using (ServiceController controller = 
                new ServiceController(SERVICE_NAME)) {
                if (!IsInstalled()) return false;
                return (controller.Status == ServiceControllerStatus.Running);
            }
        }

        private static AssemblyInstaller GetInstaller()
        {
            AssemblyInstaller installer = new AssemblyInstaller(
                typeof(ProjectInstaller).Assembly, null);
            installer.UseNewContext = true;
            return installer;
        }

        public static void InstallService()
        {
            if (IsInstalled()) return;

            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Install(state);
                        installer.Commit(state);
                        Logger.Instance.log("service installed");
                    }
                    catch(Exception e)
                    {
                        try
                        {
                            installer.Rollback(state);
                        }
                        catch { }
                        Logger.Instance.error(e);
                        throw e;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static void UninstallService()
        {
            if (!IsInstalled()) return;
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Uninstall(state);
                    }
                    catch
                    {
                        throw;
                    }
                }

                //int max_try = 100;
                //int count = 0;
                //while (count < max_try && IsInstalled())
                //{
                //    count++;
                //    Thread.Sleep(1);
                //    Logger.Instance.log("waiting uninstalling: {0}", count.ToString());
                //}
                Logger.Instance.log("service uninstalled");
            }
            catch
            {
                throw;
            }
        }

        public static void StartService()
        {
            if (!IsInstalled()) return;

            using (ServiceController controller =
                new ServiceController(SERVICE_NAME))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running,
                            TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public static void StopService()
        {
            if (!IsInstalled()) return;
            using (ServiceController controller =
                new ServiceController(SERVICE_NAME))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Stopped)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped,
                             TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
