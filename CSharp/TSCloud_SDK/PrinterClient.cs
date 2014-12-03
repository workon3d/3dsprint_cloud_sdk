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
                Printer printer = JsonConvert.DeserializeObject<Printer>(httpResponse.Content);
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
