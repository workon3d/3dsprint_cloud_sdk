using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

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

    public class Team
    {
        #region variables
        private int m_id;
        private string m_name;
        private string m_created_at;
        private string m_updated_at;
        #endregion

        #region getter/setter
        [JsonProperty("id")]
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        [JsonProperty("name")]
        public string Name
        {
            get { return m_name; }
            set
            {
                try
                {
                    m_name = value;
                }
                catch
                {
                    m_name = null;
                }
            }
        }
        [JsonProperty("created_at")]
        public string CreatedAt
        {
            get { return m_created_at; }
            set { m_created_at = value; }
        }
        [JsonProperty("updated_at")]
        public string UpdatedAt
        {
            get { return m_updated_at; }
            set { m_updated_at = value; }
        }
        #endregion

        #region constructor
        public Team()
        {
        }
        #endregion
    }
}