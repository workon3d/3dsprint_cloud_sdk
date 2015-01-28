using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

// External Library
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TDSPRINT.Cloud.SDK.Datas;

namespace TDSPRINT.Cloud.SDK
{
    public class PrinterClient : TSCloud
    {
        #region member variables
        private Hash Configuration;
        #endregion

        #region constructor
        public PrinterClient()
        {
        }
        public PrinterClient(TSCloud TSCloud)
            : this()
        {
            RestClient = new RestClient(TSCloud.Hostname);
            ApiToken = TSCloud.ApiToken;
            CurrentUser = TSCloud.CurrentUser;
        }
        public PrinterClient(TSCloud TSCloud, Hash Configuration)
            : this(TSCloud)
        {
            this.Configuration = Configuration;
        }
        #endregion

        #region public method
        [Obsolete("Deprecated: This method is for only development")]
        public List<Printer> GetAllPrinters()
        {
            return GetPrinters(0).Contents;
        }
        [Obsolete("Deprecated: This method is for only development")]
        public List<Queue> GetAllQueues(int PrinterId)
        {
            return GetQueues(PrinterId, 0).Contents;
        }

        public Printers GetPrinters(int Page)
        {
            RestRequest request = new RestRequest(String.Format("{0}/printers", ApiPath), Method.GET);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("root", "true");
            if (Page != 0)
                request.AddParameter("page", Page);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Printers printers = JsonConvert.DeserializeObject<Printers>(httpResponse.Content, TSCloud.serializer_settings());

                    return printers;
                }
                else
                {
                    return new Printers(httpResponse.Content);
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        public Queues GetQueues(int PrinterId, int Page)
        {
            RestRequest request = new RestRequest(String.Format("{0}/queues", ApiPath), Method.GET);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("parent_id", PrinterId);
            if (Page != 0)
                request.AddParameter("page", Page);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Queues queues = JsonConvert.DeserializeObject<Queues>(httpResponse.Content, TSCloud.serializer_settings());

                    return queues;
                }
                else
                {
                    return new Queues(httpResponse.Content);
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public Printer GetPrinter(int PrinterId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/printers/{1}", ApiPath, PrinterId), Method.GET);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Printer printer = JsonConvert.DeserializeObject<Printer>(httpResponse.Content, TSCloud.serializer_settings());

                    return printer;
                }
                else
                {
                    return new Printer(httpResponse.Content);
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        public Queue GetQueue(int QueueId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/queues/{1}", ApiPath, QueueId), Method.GET);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Queue queue = JsonConvert.DeserializeObject<Queue>(httpResponse.Content, TSCloud.serializer_settings());

                    return queue;
                }
                else
                {
                    return new Queue(httpResponse.Content);
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        #endregion

        #region private method
        private List<Printer> index(int Page)
        {
            RestRequest request = new RestRequest(String.Format("{0}/printers", ApiPath), Method.GET);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("root", "true");
            if (Page != 0)
                request.AddParameter("page", Page);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Printers printers = JsonConvert.DeserializeObject<Printers>(httpResponse.Content, TSCloud.serializer_settings());

                int num_pages = printers.Pagination.NumPages;
                if (Page == 0)
                {
                    List<Printer> printer_list = new List<Printer>();

                    for (int i = 1; i <= num_pages; i++)
                    {
                        printer_list.AddRange(index(i));
                    }

                    return printer_list;
                }
                else
                {
                    return printers.Contents;
                }
            }
            catch
            {
                return new List<Printer>();
            }
        }
        #endregion


        #region Legacy
        private RestRequest CreateRequest(string PrinterName, object MetaJson)
        {
            RestRequest request = new RestRequest(String.Format("{0}/printers", ApiPath), Method.POST);
            Hash Acl = new Hash();
            {
                Acl.Add("owner", CurrentUser.Id);
            }

            if (String.IsNullOrEmpty(PrinterName))
                return null;

            request.AddParameter("acl", Acl.Stringify());
            request.AddParameter("name", PrinterName);
            request.AddParameter("api_token", ApiToken);
            if (MetaJson != null)
                request.AddParameter("meta", MetaJson);

            return request;
        }
        public Printer Create(string PrinterName, object MetaJson)
        {
            RestRequest request = CreateRequest(PrinterName, MetaJson);
            if (request == null)
                return new Printer("printer cannot be created without acl or printer name");
            
            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Printer printer = JsonConvert.DeserializeObject<Printer>(httpResponse.Content, TSCloud.serializer_settings());
                printer.StatusCode = httpResponse.StatusCode;
                printer.Message = httpResponse.ErrorMessage;

                return printer;
            }
            catch (Exception ee)
            {
                return new Printer(ee.ToString());
            }
        }
        public void CreateAsync(string PrinterName, object MetaJson)
        {
            RestRequest request = CreateRequest(PrinterName, MetaJson);
            if (request == null)
                return;
            
            try
            {
                RestClient.ExecuteAsync(request, null);
            }
            catch
            {
            }
        }
        public void BatchUpdate(string dataJson)
        {
            if (RestClient == null)
                RestClient = new RestClient(Hostname);
            RestRequest request = new RestRequest(String.Format("{0}/printers/batch_update", ApiPath), Method.PUT);
            
            request.AddParameter("data", dataJson);
         //   request.AddParameter("api_token", ApiToken);
            
            try
            {
                RestClient.ExecuteAsync(request, null);
            }
            catch
            {
            }
        }
        #endregion

        #region private method
        //private string get_acl()
        //{
        //    if (CurrentUser == null)
        //        return "";
        //    Acl AclObject = new Acl(Int32.Parse(CurrentUser.Id.ToString()));
        //    return JsonConvert.SerializeObject(AclObject, Formatting.None);
        //}
        #endregion
    }


}
