using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// External Library
using RestSharp;
using Newtonsoft.Json;

// TP2_SDK
using TeamPlatform.TP2_SDK.Object;

namespace TeamPlatform.TP2_SDK
{
    public class TP2
    {
        #region member variables
        public RestClient RestClient;
        private readonly string m_Tp2Host = "http://tp2.dev:3000/";
        private readonly string m_ApiPath = "api/v1";
        private string m_ApiToken = null;
        private User m_CurrentUser;
        #endregion

        #region getter/setter
        public string Tp2Host
        {
            get { return m_Tp2Host; }
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
                User CurrentUser = JsonConvert.DeserializeObject<Object.User>(httpResponse.Content);
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
        #endregion
    }
}
