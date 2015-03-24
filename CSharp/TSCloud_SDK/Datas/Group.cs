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
        #region Variables
        private int m_parent_id = 0;
        #endregion

        #region Constructor
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
        public List<int> Users { get; set; }
        
        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("parent_id")]
        protected object _SetParent
        {
            set
            {
                try
                {
                    m_parent_id = Convert.ToInt32(value);
                }
                catch
                {
                    m_parent_id = 0;
                }
            }
        }
        public int ParentId
        {
            get { return m_parent_id; }
            set { m_parent_id = value; }
        }
        #endregion

        #region method
        public String GetUserIds()
        {
            return TSUtil.ConvertToIds(Users);
        }
        #endregion
    }
}
