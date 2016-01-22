using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

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
        private string m_Apphost;
        //private string m_TcHost = "http://184.73.206.209";
        private string m_TcHost = "http://10.211.55.5:3000";

        private readonly string m_ApiPath = "api/v1";
        private string m_ApiToken = null;
        private User m_CurrentUser;
        private long m_Expiration;
        private string m_RefreshToken = null;
        //private Users m_users;
        #endregion

        #region getter/setter
        public string AppHost
        {
            get { return m_Apphost; }
            set { m_Apphost = value; }
        }
        public string ApiHost
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
        public long Expiration
        {
            get { return m_Expiration; }
            set { m_Expiration = value; }
        }
        public DateTime ExpirationDateTime
        {
            get { return ConvertTimeStamp(m_Expiration); }
        }

        public string RefreshToken
        {
            get { return m_RefreshToken; }
            set { m_RefreshToken = value; }
        }
        //public Users Users
        //{
        //    get { return m_users; }
        //    set { m_users = value; }
        //}
        #endregion

        #region constructor
        public TSCloud()
        {
            m_ApiToken = null;
            m_RefreshToken = null;
            m_CurrentUser = null;
        }
        public TSCloud(string AppHost) : this()
        {
            if (!IsValidHost(AppHost))
            {
                throw new Exception("AppHost URL is not valid");
            }
            
            this.AppHost = AppHost;
            this.ApiHost = GetApiHost();

            if (this.AppHost == this.ApiHost)
                this.AppHost = null;

            RestClient = new RestClient(this.ApiHost);
        }
        #endregion

        #region public method
        public bool IsOnline()
        {
            var request = new RestRequest("",Method.GET);
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
            return Authenticate(Email, Password, "");
        }

        public User Authenticate(string Email, string Password, string External)
        {
            var request = new RestRequest(ApiPath + "/authenticates", Method.POST);
            request.AddParameter("email", Email.ToLower());
            request.AddParameter("password", Password);
            if (!string.IsNullOrEmpty(External))
                request.AddParameter("external", External);
            request.RequestFormat = DataFormat.Json;

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    User CurrentUser = JsonConvert.DeserializeObject<Datas.User>(httpResponse.Content, TSCloud.serializer_settings());
                    ApiToken = CurrentUser.ApiToken;
                    RefreshToken = CurrentUser.RefreshToken;
                    Expiration = CurrentUser.TokenExpiration;
                    CurrentUser.SysInfo = GetSysInfo();
                    this.CurrentUser = CurrentUser;


                    //UserClient UserClient = new UserClient(this);
                    //m_users = UserClient.All();

                    if (ApiToken != null)
                    {
                        new Thread(new RefreshTokenWorker(this).Start).Start();
                        return CurrentUser;
                    }
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

        private User Authenticate(string api_token, string refresh_token, long token_expiration)
        {
            RestRequest request = new RestRequest(String.Format("{0}/profiles", ApiPath), Method.GET);
            request.AddParameter("api_token", api_token);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    return new User("Unauthorized");
                User CurrentUser = JsonConvert.DeserializeObject<Datas.User>(httpResponse.Content, TSCloud.serializer_settings());
                RefreshToken = refresh_token;
                Expiration = token_expiration;
                ApiToken = api_token;
                CurrentUser.SysInfo = GetSysInfo();
                CurrentUser.StatusCode = httpResponse.StatusCode;
                CurrentUser.ApiToken = api_token;
                this.CurrentUser = CurrentUser;
                
                //UserClient UserClient = new UserClient(this);
                //m_users = UserClient.All();

                new Thread(new RefreshTokenWorker(this).Start).Start();
                return CurrentUser;
            }
            catch (Exception ee)
            {
                return new User(ee.ToString());
            }
        }

        public User AuthenticateByApiToken(string email, string api_token, string refresh_token, long token_expiration)
        {
            if (string.IsNullOrEmpty(refresh_token) || token_expiration == 0)
                return new User("Unauthorized");
            User user = Authenticate(api_token, refresh_token, token_expiration);
            if (!user.IsValid() || user.Email != email)
                return new User("Unauthorized");
            return user;
        }

        public Hash GetDesktopSettings()
        {
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/profiles/desktop_settings", ApiPath), Method.GET);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Hash responseHash = JsonConvert.DeserializeObject<Hash>(httpResponse.Content, TSCloud.serializer_settings());

                return responseHash;
            }
            catch
            {
                return null;
            }
        }
        public Hash UpdateDesktopSettings(Hash settings)
        {
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/profiles/desktop_settings", ApiPath), Method.PUT);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("desktop_settings", settings.Stringify());

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Hash responseHash = JsonConvert.DeserializeObject<Hash>(httpResponse.Content, TSCloud.serializer_settings());

                return responseHash;
            }
            catch
            {
                return null;
            }
        }

        public Newtonsoft.Json.Linq.JObject AuthenticateCenterCode(string UserID, string Password)
        {
            var request = new RestRequest(ApiPath + "/authenticates", Method.POST);
            request.AddParameter("email", UserID);
            request.AddParameter("password", Password);
            request.AddParameter("external", "centercode");
            request.RequestFormat = DataFormat.Json;

            Newtonsoft.Json.Linq.JObject response = new Newtonsoft.Json.Linq.JObject();
            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(httpResponse.Content, TSCloud.serializer_settings());
                }
                else
                {
                    response.Add("error", (int)httpResponse.StatusCode);
                }
            }
            catch (Exception ee)
            {
                response.Add("error", ee.ToString());
            }

            return response;
        }
        public bool FeedbackCenterCode(string user_name, string user_email, string profiler, string error)
        {
            var request = new RestRequest(ApiPath + "/feedback", Method.POST);
            request.AddParameter("external", "centercode");
            request.AddParameter("user_name", user_name);
            request.AddParameter("user_email", user_email);
            request.AddParameter("profiler", profiler);
            request.AddParameter("error", error);
            request.RequestFormat = DataFormat.Json;

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public RefreshResponse CheckExpiration(DateTime expiration, string refresh_token)
        {
            if (expiration <= DateTime.Now && !string.IsNullOrEmpty(refresh_token))
            {
                try
                {
                    RestRequest request = new RestRequest(String.Format("{0}/authenticate/refresh", ApiPath), Method.PUT);
                    request.AddParameter("refresh_token", refresh_token);

                    IRestResponse httpResponse = RestClient.Execute(request);
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<RefreshResponse>(httpResponse.Content, TSCloud.serializer_settings());
                    }
                }
                catch (Exception e)
                {
                    return new RefreshResponse("token refresh fails - " + e.Message);
                }
                return new RefreshResponse("token refresh fails");
            }
            return null;
        }

        public RefreshResponse CheckExpiration(long expiration, string refresh_token)
        {
            return CheckExpiration(ConvertTimeStamp(expiration), refresh_token);
        }

        public bool CheckExpiration()
        {
            RefreshResponse refresh = CheckExpiration(ExpirationDateTime, RefreshToken);
            if (refresh != null)
            {
                if (refresh.IsValid()) {
                    ApiToken = CurrentUser.ApiToken = refresh.ApiToken;
                    RefreshToken = CurrentUser.RefreshToken = refresh.RefreshToken;
                    Expiration = CurrentUser.TokenExpiration = refresh.TokenExpiration;

                    new Thread(new RefreshTokenWorker(this).Start).Start();
                }
                else {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region private method
        virtual protected TSCloud GetSysInfo()
        {
            return this;
        }
        string GetApiHost()
        {
            RestClient = new RestClient(AppHost);
            RestRequest request = new RestRequest(String.Format("/api_host"), Method.GET);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    return AppHost;
                }

                Hash response = JsonConvert.DeserializeObject<Hash>(httpResponse.Content, TSCloud.serializer_settings());
    
                return Convert.ToString(response["api_host"]);
            }
            catch
            {
                return null;
            }
        }
        bool IsValidHost(string AppHost)
        {
            Uri uriResult;

            bool result = Uri.TryCreate(AppHost, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            return result;
        }
        DateTime ConvertTimeStamp(long timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(timestamp).ToLocalTime();
        }
        #endregion

        #region static method
        public static JsonSerializerSettings serializer_settings()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.MissingMemberHandling = MissingMemberHandling.Ignore;

            return setting;
        }
        #endregion
    }

    public class ClientBase : TSCloud
    {
        #region member variables
        private TSCloud m_TSCloud;
        #endregion

        #region constructor
        public ClientBase()
            : base()
        {
            m_TSCloud = this;
        }

        public ClientBase(string AppHost)
            : base(AppHost)
        {
            m_TSCloud = this;
        }

        public ClientBase(TSCloud _TSCloud)
        {
            m_TSCloud = _TSCloud;
            RestClient = new RestClient(ApiHost);
        }
        #endregion

        #region getter/setter
        new public string AppHost
        {
            get { return m_TSCloud.AppHost; }
            set { m_TSCloud.AppHost = value; }
        }
        new public string ApiHost
        {
            get { return m_TSCloud.ApiHost; }
            set { m_TSCloud.ApiHost = value; }
        }
        new public string ApiPath
        {
            get { return m_TSCloud.ApiPath; }
        }
        new public string ApiToken
        {
            get { return m_TSCloud.ApiToken; }
            set { m_TSCloud.ApiToken = value; }
        }
        new public string RefreshToken
        {
            get { return m_TSCloud.RefreshToken; }
            set { m_TSCloud.RefreshToken = value; }
        }
        new public long Expiration
        {
            get { return m_TSCloud.Expiration; }
            set { m_TSCloud.Expiration = value; }
        }
        new public DateTime ExpirationDateTime
        {
            get { return m_TSCloud.ExpirationDateTime; }
        }
        new public User CurrentUser
        {
            get { return m_TSCloud.CurrentUser; }
            set { m_TSCloud.CurrentUser = value; }
        }
        #endregion

        override protected TSCloud GetSysInfo()
        {
            return m_TSCloud;
        }
    }
}
