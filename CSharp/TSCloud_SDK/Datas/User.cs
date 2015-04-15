using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class User : CommonItem
    {
        #region variables
        private string m_email;
        private string m_api_token;
        //private string m_name = "Unknown";
        private string m_address;
        private string m_company;
        private object m_vprint;
        private Team m_team;
        private string m_role;
        private string m_avatar_url;
        private List<int> m_group_ids;
        #endregion

        #region getter/setter
        public string Password { get; set; }
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email
        {
            get { return m_email; }
            set { m_email = value; }
        }
        [JsonProperty("api_token")]
        public string ApiToken
        {
            get { return m_api_token; }
            set { m_api_token = value; }
        }
        [JsonProperty("address")]
        public string Address
        {
            get { return m_address; }
            set
            {
                try
                {
                    m_address = value;
                }
                catch
                {
                    m_address = null;
                }
            }
        }
        [JsonProperty("company")]
        public string Company
        {
            get { return m_company; }
            set
            {
                try
                {
                    m_company = value;
                }
                catch
                {
                    m_company = null;
                }
            }
        }
        [JsonProperty("vprint")]
        public object Vprint
        {
            get { return m_vprint; }
            set
            {
                try
                {
                    m_vprint = value;
                }
                catch
                {
                    m_vprint = null;
                }
            }
        }
        [JsonProperty("team")]
        public Team Team
        {
            get { return m_team; }
            set
            {
                try
                {
                    m_team = value;
                }
                catch
                {
                    m_team = new Team();
                }
            }
        }
        [JsonProperty("role")]
        public string Role
        {
            get { return m_role; }
            set { m_role = value; }
        }
        [JsonProperty("avatar_url")]
        public string AvatarUrl
        {
            get { return m_avatar_url; }
            set
            {
                try
                {
                    m_avatar_url = value;
                }
                catch
                {
                    m_avatar_url = null;
                }
            }
        }
        public List<int> GroupIds
        {
            get { return m_group_ids; }

        }
        [JsonProperty("groups")]
        public object _group_ids
        {
            set
            {
                try
                {
                    m_group_ids = JsonConvert.DeserializeObject<List<int>>(value.ToString());
                }
                catch
                {
                    m_group_ids = null;
                }
            }
        }
        #endregion

        #region constructor
        public User(HttpStatusCode status_code, string strMessage)
        {
            StatusCode = status_code;
            Message = strMessage;
        }
        public User(string strMessage)
        {
            Message = strMessage;
        }
        public User()
        {
        }
        #endregion

        #region static methods
        static public bool IsValid(User user)
        {
            if (user == null)
                return false;

            return user.IsValid();
        }
        public override bool IsValid()
        {
            if (!String.IsNullOrEmpty(ApiToken))
                return true;

            if (Id == 0 || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Name))
                return false;
            else
                return true;
        }
        public bool Follow(int ModelId)
        {
            if (!IsSysInfoDefined())
                throw new Exception("SysInfo is not defined");

            if (this.Id == 0)
               throw new Exception("User ID Required");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/follow", SysInfo["ApiPath"], ModelId), Method.PUT);
            request.AddParameter("api_token", SysInfo["ApiToken"]);
            request.AddParameter("u_id", this.Id);

            try
            {
                IRestResponse httpResponse = GetRestClient().Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
        public bool Unfollow(int ModelId)
        {
            if (!IsSysInfoDefined())
                throw new Exception("SysInfo is not defined");

            if (this.Id == 0)
               throw new Exception("User ID Required");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/unfollow", SysInfo["ApiPath"], ModelId), Method.PUT);
            request.AddParameter("api_token", SysInfo["ApiToken"]);
            request.AddParameter("u_id", this.Id);

            try
            {
                IRestResponse httpResponse = GetRestClient().Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
        #endregion

        #region Deprecated
        [Obsolete("Deprecated: Now, can access to Id property directly.", false)]
        public int GetId()
        {
            return Id;
        }
        #endregion

    }

    public class Users : CommonList
    {
        private List<User> m_users;

        public List<User> All
        {
            get { return m_users; }
        }

        public Users()
        {
        }

        public Users(List<User> users)
        {
            m_users = users;
        }

        public User FindById(int UserId)
        {
            User _user = m_users.Find(user => user.Id == UserId);
            if (User.IsValid(_user))
            {
                return _user;
            }
            else
            {
                return new User();
            }
        }
    }
}