﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

// External Library
using RestSharp;
using Newtonsoft.Json;

using TDSPRINT.Cloud.SDK.Datas;

namespace TDSPRINT.Cloud.SDK
{
    public class TSCloud
    {
        #region member variables
        public RestClient RestClient;
        private string m_TcHost = "http://184.73.206.209";
        private readonly string m_ApiPath = "api/v1";
        private string m_ApiToken = null;
        private User m_CurrentUser;
        private Users m_users;
        #endregion

        #region getter/setter
        public string Hostname
        {
            get { return m_TcHost; }
            set { m_TcHost = value; }
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
        public Users Users
        {
            get { return m_users; }
            set { m_users = value; }
        }
        #endregion

        #region constructor
        public TSCloud()
        {
            m_ApiToken = null;
            m_CurrentUser = null;
            RestClient = new RestClient(Hostname);
        }
        public TSCloud(string Hostname) : this()
        {
            this.Hostname = Hostname;
            RestClient = new RestClient(Hostname);
        }
        #endregion

        #region public method
        public bool IsOnline()
        {
            var request = new RestRequest("", Method.GET);
            IRestResponse httpResponse = RestClient.Execute(request);

            if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                return false;

            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized || httpResponse.StatusCode == HttpStatusCode.OK)
                return true;

            return false;
        }

        [Obsolete("Please use Authenticate(Email, Password)")]
        public User authenticate(string Email, string Password)
        {
            return Authenticate(Email, Password);
        }
        public User Authenticate(string Email, string Password)
        {
            var request = new RestRequest(ApiPath + "/authenticates", Method.POST);
            request.AddParameter("email", Email.ToLower());
            request.AddParameter("password", Password);
            request.RequestFormat = DataFormat.Json;

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    User CurrentUser = JsonConvert.DeserializeObject<Datas.User>(httpResponse.Content, TSCloud.serializer_settings());
                    this.CurrentUser = CurrentUser;
                    m_ApiToken = CurrentUser.ApiToken;

                    UserClient UserClient = new UserClient(this);
                    m_users = UserClient.All();

                    if (m_ApiToken != null)
                        return CurrentUser;
                    else
                        return new User("api_token is null");
                }
                else
                {
                    return new User(httpResponse.Content);
                }

                
            }
            catch (Exception ee)
            {
                return new User(ee.ToString());
            }
        }
        [Obsolete("Please use Authenticate(Email, Password)")]
        public User authenticate(string api_token)
        {
            return Authenticate(api_token);
        }
        public User Authenticate(string api_token)
        {
            RestRequest request = new RestRequest(String.Format("{0}/profiles", ApiPath), Method.GET);
            request.AddParameter("api_token", api_token);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    return new User("Unauthorized");
                User CurrentUser = JsonConvert.DeserializeObject<Datas.User>(httpResponse.Content, TSCloud.serializer_settings());
                CurrentUser.StatusCode = httpResponse.StatusCode;
                CurrentUser.ApiToken = api_token;
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

        #region methods
        public static JsonSerializerSettings serializer_settings()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.MissingMemberHandling = MissingMemberHandling.Ignore;

            return setting;
        }
        #endregion
    }
}
