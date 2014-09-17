using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// External Library
using RestSharp;
using Newtonsoft.Json;

// TP2_SDK
using TeamPlatform.TP2_SDK.TpResponse;

namespace TeamPlatform.TP2_SDK
{
    public class TP2
    {
        private readonly string m_Tp2Host = "http://tp2.dev:3000/";
        public string Tp2Host
        {
            get { return m_Tp2Host; }
        }
        private readonly string m_ApiPath = "api/v1";
        public string ApiPath
        {
            get { return m_ApiPath; }
        }
        private string m_ApiToken = null;
        public string ApiToken
        {
            get { return m_ApiToken; }
            set { m_ApiToken = value; }
        }

        public TP2()
        {
            m_ApiToken = null;
        }
        public TP2(string strApiToken)
        {
            m_ApiToken = strApiToken;
        }

        public bool authenticate(string Email, string Password)
        {
            var client = new RestClient(Tp2Host);
            var request = new RestRequest(ApiPath + "/authenticates", Method.POST);
            request.AddParameter("email", Email);
            request.AddParameter("password", Password);

            try
            {
                IRestResponse httpResponse = client.Execute(request);
                AuthenticateResponse jsonResponse = JsonConvert.DeserializeObject<TpResponse.AuthenticateResponse>(httpResponse.Content);
                m_ApiToken = jsonResponse.current_user.api_token;

                if (m_ApiToken != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ee)
            {
                return false;
            }
        }
    }
}
