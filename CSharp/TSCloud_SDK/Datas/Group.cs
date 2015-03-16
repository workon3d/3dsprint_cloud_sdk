using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TDSPRINT.Cloud.SDK.Types;
using Newtonsoft.Json;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class Groups : CommonList
    {
        private List<Group> m_contents;
        [JsonProperty("contents")]
        public List<Group> Contents
        {
            get
            {
                return m_contents;
            }
            set
            {
                m_contents = value;
            }
        }
    }

    public class Group : CommonItem
    {
        #region constructor
        public Group(HttpStatusCode status_code, string strMessage = null)
            : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public Group(string strMessage)
            : this()
        {
            Message = strMessage;
        }
        public Group()
        {
        }
        #endregion

        #region properties
        [JsonProperty("users")]
        public List<User> Users { get; set; }
        
        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("parent_id")]
        public int ParentId { get; set; }
        #endregion

        #region method
        public String GetUserIds()
        {
            var user_ids = Users.Select( user => user.Id ).ToArray();
            return String.Join(",", user_ids.Select( x => x.ToString()).ToArray());
        }
        #endregion
    }
}
