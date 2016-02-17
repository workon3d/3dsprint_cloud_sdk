using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace _3DSPRINTCloudDB
{
    public class CloudDBAgent : IDisposable
    {
        public CloudDBAgent()
        {
        }

        public void Dispose()
        {
        }

        ~CloudDBAgent()
        {
            Dispose();
        }

        static public void SetupDBLocation()
        {
            new Env().SetupDB();
            new User().SetupDB();
            new Printer().SetupDB();
        }

        public bool UpdateUserInfo(string email, string api_token, string refresh_token, long token_expiration)
        {
            try
            {
                using (User user = new User(false, true))
                {
                    if (string.IsNullOrEmpty(api_token))
                    {
                        user.json.RemoveAll();
                    }
                    else
                    {
                        user.json["email"] = email;
                        user.json["api_token"] = api_token;
                        user.json["refresh_token"] = refresh_token;
                        user.json["token_expiration"] = token_expiration;
                    }
                }
            }
            catch(Exception e)
            {
                Logger.Instance.error(e);
                return false;
            }
            return true;   
        }

        public JObject GetUserInfo()
        {
            try
            {
                using (User user = new User(true))
                {
                    if (user.json.Count == 0)
                        return null;
                    return (JObject)user.json.DeepClone();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }

            return null;
        }

        public bool UpdateEnv(JObject env_json)
        {
            try
            {
                using (Env env = new Env(false, true))
                {
                    env.Set(env_json);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
                return false;
            }
            return true;    
        }

        public JObject GetEnv()
        {
            try
            {
                using (Env env = new Env(true))
                {
                    if (env.json.Count == 0)
                        return null;
                    return (JObject)env.json.DeepClone();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }

            return null;
        }

        public JObject GetPrinter(string printer_id)
        {
            try
            {
                using (Printer printers = new Printer(true))
                {
                    foreach (JObject printer in printers.List)
                    {
                        if ((string)printer["id"] == printer_id)
                            return (JObject)printer["meta"].DeepClone();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }

            return null;
        }

        public void UpdatePrinter(string printer_id, JObject meta)
        {
            try
            {
                using (Printer printers = new Printer(true, true))
                {
                    JObject printer = null;
                    foreach (JObject p in printers.List)
                    {
                        if ((string)p["id"] == printer_id)
                        {
                            printer = p;
                            break;
                        }
                    }

                    if (printer == null)
                    {
                        printer = new JObject();
                        printer["id"] = printer_id;
                        printer["meta"] = meta;
                        printers.List.Add(printer);
                    }
                    else
                    {
                        printer["id"] = printer_id;
                        printer["meta"] = meta;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }
        }

        public JArray GetQueue(string printer_id)
        {
            try
            {
                using (Queue queue = new Queue(printer_id, true))
                {
                    return (JArray)queue.List.DeepClone();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }

            return null;
        }

        public void UpdateQueue(string printer_id, JArray queue_array)
        {
            try
            {
                using (Queue queue = new Queue(printer_id, false, true))
                {
                    queue.List = queue_array;
                }
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }
        }
    }
}
