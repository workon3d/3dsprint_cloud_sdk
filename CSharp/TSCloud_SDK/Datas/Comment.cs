using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TDSPRINT.Cloud.SDK.Types;
using Newtonsoft.Json;
using RestSharp;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class Comments : CommonList
    {
        private List<Comment> m_contents;
        [JsonProperty("contents")]
        public List<Comment> Contents
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

    public class Comment : CommonItem
    {
        #region Variables
        private int m_parent_id = 0;
        private int m_item_id;
        #endregion

        #region Constructor
        public Comment(HttpStatusCode status_code, string strMessage = null)
            : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public Comment(string strMessage)
            : this()
        {
            Message = strMessage;
        }
        public Comment()
        {
        }
        #endregion

        #region properties
        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        [JsonProperty("item_id")]
        protected object _ItemId
        {
            set
            {
                try
                {
                    m_item_id = Convert.ToInt32(value);
                }
                catch
                {
                    m_item_id = 0;
                }
            }
        }
        public int ItemId
        {
            get
            {
                return m_item_id;
            }
        }
        
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("vprint_meta")]
        public Hash VprintMeta { get; set; }

        [JsonProperty("owner")]
        public int? Owner { get; set; }

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
        public bool Remove()
        {
            if (!IsSysInfoDefined())
                throw new Exception("SysInfo is not defined");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");
            
            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/comments/{2}", SysInfo.ApiPath, Convert.ToString(this.ItemId), Convert.ToString(this.Id)), Method.DELETE);
            request.AddParameter("api_token", SysInfo.ApiToken);

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
                throw ee;
            }

        }
        public override bool IsValid()
        {
            try
            {
                if (Id == 0 || ItemId == 0 || Owner == null || CreatedAt == null)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
