using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class RefreshResponse
    {
        #region variables
        private string m_api_token;
        private string m_refresh_token;
        private long m_token_expiration;
        private string m_message;
        #endregion

        #region getter/setter
        [JsonProperty("api_token")]
        public string ApiToken
        {
            get { return m_api_token; }
            set { m_api_token = value; }
        }
        [JsonProperty("refresh_token")]
        public string RefreshToken
        {
            get { return m_refresh_token; }
            set { m_refresh_token = value; }
        }
        [JsonProperty("api_token_expires_at")]
        public long TokenExpiration
        {
            get { return m_token_expiration; }
            set { m_token_expiration = value; }
        }

        public string Message
        {
            get { return m_message; }
            set { Message = value; }
        }

        public DateTime ExpirationDateTime
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(m_token_expiration).ToLocalTime(); }
        }
        #endregion

        public RefreshResponse()
        {
            m_message = null;
        }

        public RefreshResponse(string message)
        {
            m_message = message;
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Message);
        }
    }
}
