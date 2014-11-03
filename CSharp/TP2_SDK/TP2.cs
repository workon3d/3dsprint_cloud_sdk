using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

// External Library
using RestSharp;
using Newtonsoft.Json;

// TP2_SDK
using TeamPlatform.TP2_SDK.Datas;

namespace TeamPlatform.TP2_SDK
{
    public class TP2
    {
        #region member variables
        public RestClient RestClient;
        private readonly int m_SDK_VERSION = 1;
        private string m_Tp2Host = "http://tp2.dev:3000/";
        private readonly string m_ApiPath = "api/v1";
        private string m_ApiToken = null;
        private User m_CurrentUser;
        #endregion

        #region getter/setter
        public string Tp2Host
        {
            get { return m_Tp2Host; }
            set { m_Tp2Host = value; }
        }
        public string ApiPath
        {
            get { return m_ApiPath; }
        }
        public string ApiToken
        {
            get { return m_ApiToken; }
            set { m_ApiToken = value; }
        }
        public User CurrentUser
        {
            get { return m_CurrentUser; }
            set { m_CurrentUser = value; }
        }
        public int SDK_VERSION
        {
            get { return m_SDK_VERSION; }
        } 
        #endregion

        #region constructor
        public TP2()
        {
            m_ApiToken = null;
            m_CurrentUser = null;
        }
        #endregion

        #region public method
        public User authenticate(string Email, string Password)
        {
            RestClient = new RestClient(Tp2Host);
            var request = new RestRequest(ApiPath + "/authenticates", Method.POST);
            request.AddParameter("email", Email);
            request.AddParameter("password", Password);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                User CurrentUser = JsonConvert.DeserializeObject<Datas.User>(httpResponse.Content);
                this.CurrentUser = CurrentUser;
                m_ApiToken = CurrentUser.api_token;

                if (m_ApiToken != null)
                    return CurrentUser;
                else
                    return new User("api_token is null");
            }
            catch (Exception ee)
            {
                return new User(ee.ToString());
            }
        }

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

        #endregion
    }
}
