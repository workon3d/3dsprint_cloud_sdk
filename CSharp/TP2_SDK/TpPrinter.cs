using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

// External Library
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// TP2_SDK
using TeamPlatform.TP2_SDK.Datas;

namespace TeamPlatform.TP2_SDK
{
    public class TpPrinter : TP2
    {
        #region constructor
        public TpPrinter()
        {
        }
        public TpPrinter(TP2 TpClient)
            : this()
        {
            RestClient = new RestClient(Tp2Host);
            ApiToken = TpClient.ApiToken;
            CurrentUser = TpClient.CurrentUser;
        }
        #endregion

        #region public method
        public User authenticate(string api_token)
        {
            if (RestClient == null)
                RestClient = new RestClient(Tp2Host);

            RestRequest request = new RestRequest(String.Format("{0}/profiles", ApiPath), Method.GET);
            request.AddParameter("api_token", api_token);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    return new User("Unauthorized"); ;
                User CurrentUser = JsonConvert.DeserializeObject<Datas.User>(httpResponse.Content);
                CurrentUser.StatusCode = httpResponse.StatusCode;
                CurrentUser.api_token = api_token;
                this.CurrentUser = CurrentUser;
                this.ApiToken = api_token;

                return CurrentUser;
            }
            catch (Exception ee)
            {
                return new User(ee.ToString());
            }
        }

        public Printer Create(string PrinterName, object MetaJson)
        {
            RestRequest request = new RestRequest(String.Format("{0}/printers", ApiPath), Method.POST);
            string Acl = this.get_acl();
            if (Acl.Length == 0)
                return new Printer("printer cannot be created without acl");

            if (String.IsNullOrEmpty(PrinterName))
                return new Printer("printer cannot be created without name");
            
            request.AddParameter("acl", Acl);
            request.AddParameter("name", PrinterName);
            request.AddParameter("api_token", ApiToken);
            if (MetaJson != null)
                request.AddParameter("meta", MetaJson);

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
            RestRequest request = new RestRequest(String.Format("{0}/printers", ApiPath), Method.POST);
            string Acl = this.get_acl();
            if (Acl.Length == 0)
                return;

            if (String.IsNullOrEmpty(PrinterName))
                return;

            request.AddParameter("acl", Acl);
            request.AddParameter("name", PrinterName);
            request.AddParameter("api_token", ApiToken);
            if (MetaJson != null)
                request.AddParameter("meta", MetaJson);

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
                RestClient = new RestClient(Tp2Host);
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
            Acl AclObject = new Acl(Int32.Parse(CurrentUser.id.ToString()));
            return JsonConvert.SerializeObject(AclObject, Formatting.None);
        }
        #endregion
    }


}
