using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using _3DSPRINTCloudDB;

namespace _3DSPRINTCloudAgent
{
    public partial class Service : ServiceBase
    {
        Thread _WorkerThread;
        Worker _Worker;

        public Service()
        {
            _WorkerThread = null;
            _Worker = null;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Instance.log("Service started");
            _Worker = new Worker();
            _WorkerThread = new Thread(_Worker.Start);
            try {
                _WorkerThread.Start();
            }
            catch(Exception e)
            {
                Logger.Instance.error(e);
            }
        }

        protected override void OnStop()
        {
            if (_WorkerThread != null)
            {
                try {
                    _Worker.Stop();
                    _WorkerThread.Abort();
                }
                catch(Exception e)
                {
                    Logger.Instance.error(e);
                }
            }
            Logger.Instance.log("Service Stopped");
        }

        protected override void OnCustomCommand(int command)
        {
            switch (command)
            {
                case 128:
                    Logger.Instance.log("Command " + command + " successfully called.");
                    break;
                default:
                    break;
            }
        }
    }
}
