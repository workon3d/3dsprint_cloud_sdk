using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class User
    {
        #region variables
        private int m_id;
        private string m_email;
        private string m_api_token;
        private string m_message;
        private HttpStatusCode m_StatusCode;
        #endregion

        #region getter/setter
        [JsonProperty("id")]
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
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
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }
        public HttpStatusCode StatusCode
        {
            get { return m_StatusCode; }
            set { m_StatusCode = value; }
        }
        #endregion

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

        [Obsolete("Deprecated: Now, can access to Id property directly.", false)]
        public int GetId()
        {
            return Convert.ToInt32(m_id);
        }

        static public bool IsValid(User user)
        {
            if (user.Id == 0 || String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.ApiToken))
                return false;
            else
                return true;
        }
    }
}