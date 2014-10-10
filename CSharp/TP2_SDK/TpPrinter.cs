﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// External Library
using RestSharp;
using Newtonsoft.Json;

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
        public Printer Create(string PrinterName, object MetaJson)
        {
            RestRequest request = new RestRequest(String.Format("{0}/printers", ApiPath), Method.POST);
            string Acl = this.get_acl();

            if (!String.IsNullOrEmpty(PrinterName))
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
        #endregion

        #region private method
        private string get_acl()
        {
            Acl AclObject = new Acl(Int32.Parse(CurrentUser.id.ToString()));
            return JsonConvert.SerializeObject(AclObject, Formatting.None);
        }
        #endregion
    }


}
