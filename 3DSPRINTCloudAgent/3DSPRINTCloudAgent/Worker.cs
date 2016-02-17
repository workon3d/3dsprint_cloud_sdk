using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.IO;
using _3DSPRINTCloudDB;
using TDSPRINT.Cloud.SDK;
using Newtonsoft.Json.Linq;

namespace _3DSPRINTCloudAgent
{
    class Worker
    {
        CloudDBAgent _DBContext;
        TSCloud _TSCloud;
        PrinterClient _PrinterClient;
        DateTime _LastLogTime;
        bool _Running;
        private Object _Locker = new Object();
        
        TDSPRINT.Cloud.SDK.Datas.User _CloudUser;

        public Worker()
        {
            _DBContext = null;
            _TSCloud = null;
            _PrinterClient = null;
            _LastLogTime = DateTime.Now;
            _Running = false;
        }

        public void Start()
        {
            lock (_Locker) {
                _Running = true;
            }
            
            while (true)
            {
                lock (_Locker)
                {
                    if (!_Running)
                        return;
                }
            
                Thread.Sleep(TimeSpan.FromSeconds(10));
                try
                {
                    BatchUpdate();
                }
                catch (Exception e)
                {
                    Logger.Instance.error(e);
                }
            }
        }

        public void Stop()
        {
            lock (_Locker)
            {
                _Running = false;
            }
            try
            {
                _TSCloud.Dispose();
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }
        }

        private void ConnectDB()
        {
            _DBContext = new CloudDBAgent();
        }

        private CloudDBAgent DBContext
        {
            get
            {
                if (_DBContext == null)
                    ConnectDB();
                return _DBContext;
            }
        }

        private JToken GetEnvValue(string key)
        {
            if (DBContext == null)
                return null;
            JObject env = DBContext.GetEnv();
            if (env == null)
                return null;
            return env[key];
        }

        private TSCloud CloudClient
        {
            get
            {
                if (_TSCloud == null && DBContext != null)
                {
                    string api_host = (string)GetEnvValue("api_host");
                    if (!string.IsNullOrEmpty(api_host))
                        _TSCloud = new TSCloud(api_host);
                }
                return _TSCloud;
            }
        }

        private PrinterClient CloudPrinter
        {
            get
            {
                if (_PrinterClient == null && CloudClient != null)
                {
                    _PrinterClient = new PrinterClient(CloudClient);
                }
                return _PrinterClient;
            }
        }

        private bool Authenticate(JObject user)
        {
            if (user != null && user["api_token"] != null)
            {
                _CloudUser = CloudClient.AuthenticateByApiToken((string)user["email"], (string)user["api_token"], (string)user["refresh_token"], (long)user["token_expiration"]);
                if (_CloudUser.Email.ToLower() != user["email"].ToString().ToLower())
                    _CloudUser = null;
            }
            return _CloudUser != null;
        }

        private bool UpdateDBUser()
        {
            if (CloudClient != null && _CloudUser != null && DBContext != null)
            {
                return DBContext.UpdateUserInfo(_CloudUser.Email, CloudClient.ApiToken, CloudClient.RefreshToken, CloudClient.Expiration);
            }
            return false;
        }

        private bool CheckUser()
        {
            if (DBContext != null && CloudClient != null)
            {
                JObject DBUser = DBContext.GetUserInfo();
                if (_CloudUser == null && DBUser != null)
                {
                    Authenticate(DBUser);
                }

                if (DBUser == null)
                {
                    _CloudUser = null;
                }

                if (_CloudUser != null && DBUser != null)
                {
                    if (_CloudUser.Email != (string)DBUser["email"])
                    {
                        if (Authenticate(DBUser))
                            UpdateDBUser();
                    }
                    else if (CloudClient.ApiToken != (string)DBUser["api_token"]) // token refreshed.
                    {
                        UpdateDBUser();
                    }
                }
            }

            return _CloudUser != null;
        }


        private JObject ReadLog()
        {
            string log_file = (string)GetEnvValue("agent_log_location");
            if (string.IsNullOrEmpty(log_file))
                return null;
            if (!File.Exists(log_file))
                return null;
            DateTime log_time = new System.IO.FileInfo(log_file).LastWriteTime;
            //if (_LastLogTime == log_time)
            //    return null;
            _LastLogTime = log_time;

            DateTime _timeout = DateTime.Now.AddSeconds(10);
            while (_timeout > DateTime.Now)
            {
                try
                {
                    return JObject.Parse(File.ReadAllText(log_file));
                }
                catch (Exception e)
                {
                    Logger.Instance.error(e.Message);
                }
            }
            return null;
        }

        private JObject PurePrinter(JObject printer)
        {
            JObject pure_printer = (JObject)printer.DeepClone();
            pure_printer.Remove("queue");
            return pure_printer;
        }

        private JObject FindJob(JArray queue, string id)
        {
            if (queue == null || queue.Count == 0)
                return null;
            foreach (JObject job in queue)
            {
                if ((string)job["id"] == id)
                    return job;
            }
            return null;
        }

        private JArray DiffQueue(JArray queue1, JArray queue2)
        {
            if (queue2 == null || queue2.Count == 0)
                return null;
            if (queue1 == null || queue1.Count == 0)
                return queue2;

            JArray diff = new JArray();

            foreach (JObject job in queue2)
            {
                if (job["id"] == null)
                    continue;
                JObject found_job = FindJob(queue1, (string)job["id"]);
                if (found_job == null)
                    diff.Add(job);
                else if (found_job.ToString(Newtonsoft.Json.Formatting.None) != job.ToString(Newtonsoft.Json.Formatting.None))
                    diff.Add(job);
            }
            return diff;
        }

        private void BatchUpdate()
        {
            if (!CheckUser())
                return;
            if (DBContext == null)
                return;
            JObject log = ReadLog();
            List<JToken> printers = null;
            try
            {
                printers = log["printers"].Children().ToList();
            }
            catch
            {
                printers = null;
            }

            if (printers == null)
            {
                Logger.Instance.error("no changed printers in json");
                return;
            }

            JArray updated_printers = new JArray();

            Dictionary<string, int> updated_printer_index = new Dictionary<string, int>();
            foreach (JObject printer in printers)
            {
                string id = (string)printer["id"];
                if (string.IsNullOrEmpty(id))
                    continue;
                JObject DBPrinter = DBContext.GetPrinter(id);
                JObject printer_only = PurePrinter(printer);
                if (DBPrinter == null)
                {
                    try
                    {
                        CloudPrinter.Create((string)printer["name"], printer);
                        DBContext.UpdatePrinter(id, printer_only);
                        Logger.Instance.log("printer created: {0}({1})", (string)printer["name"], id);
                        updated_printers.Add(printer_only);
                        updated_printer_index.Add(id, updated_printers.Count - 1);
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.error(e);
                    }
                }
                else if (DBPrinter.ToString(Newtonsoft.Json.Formatting.None) != printer_only.ToString(Newtonsoft.Json.Formatting.None))
                {
                    DBContext.UpdatePrinter(id, printer_only);
                    updated_printers.Add(printer_only);
                    updated_printer_index.Add(id, updated_printers.Count - 1);
                }
            }

            foreach (JObject printer in printers)
            {
                string id = (string)printer["id"];
                if (string.IsNullOrEmpty(id))
                    continue;
                JArray DBQueue = DBContext.GetQueue(id);
                JArray diff = DiffQueue(DBQueue, (JArray)printer["queue"]);
                if (diff == null || diff.Count == 0)
                    continue;

                JObject printer_only = null;
                int index = -1;
                if (!updated_printer_index.TryGetValue(id, out index))
                {
                    printer_only = PurePrinter(printer);
                    updated_printers.Add(printer_only);
                    index = updated_printers.Count - 1;
                    updated_printer_index.Add(id, index);
                }
                updated_printers[index]["queue"] = diff;
                DBContext.UpdateQueue(id, (JArray)printer["queue"]);
            }

            Logger.Instance.log(updated_printers.ToString());

            if (updated_printers.Count == 0)

                return;
            JObject update_data = new JObject();
            update_data["printers"] = updated_printers;
            CloudPrinter.BatchUpdate(update_data.ToString(Newtonsoft.Json.Formatting.None));
        }
    }
}
