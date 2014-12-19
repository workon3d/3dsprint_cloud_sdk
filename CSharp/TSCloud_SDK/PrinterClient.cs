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
        #endregion

        #region public method
        [Obsolete("Deprecated: This method is for only development")]
        public List<Printer> All()
        {
            return index(0);
        }

        public Printers GetPrinters(int Page)
        {
            RestRequest request = new RestRequest(String.Format("{0}/printers", ApiPath), Method.GET);
            request.AddParameter("api_token", ApiToken);
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
        public Printer GetQueue()
        {
            return new Printer();
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
            string Acl = this.get_acl();
            if (Acl.Length == 0)
                return null;

            if (String.IsNullOrEmpty(PrinterName))
                return null;

            request.AddParameter("acl", Acl);
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
        private string get_acl()
        {
            if (CurrentUser == null)
                return "";
            Acl AclObject = new Acl(Int32.Parse(CurrentUser.Id.ToString()));
            return JsonConvert.SerializeObject(AclObject, Formatting.None);
        }
        #endregion
    }


}
