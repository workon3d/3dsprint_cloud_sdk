using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Collections;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class CommonItem
    {
        #region member variables
        private int m_id;
        private string m_name;
        private Hash m_acl; // TODO: Replace this to Acl
        private string m_created_at;
        private string m_updated_at;
        private string m_message;
        private Hash m_meta;
        private HttpStatusCode m_StatusCode;
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
            set { m_name = value; }
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
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }
        [JsonProperty("acl")]
        protected object _SetAcl
        {
            set
            {
                try
                {
                    m_acl = Hash.Parse(value); 
                }
                catch
                {
                    m_acl = null;
                }
            }
        }
        public Hash Acl
        {
            get
            {
                return m_acl;
            }
            set
            {
                m_acl = value;
            }
        }
        public HttpStatusCode StatusCode
        {
            get { return m_StatusCode; }
            set { m_StatusCode = value; }
        }
        [JsonProperty("meta")]
        protected object _SetMeta
        {
            set
            {
                try
                {
                    m_meta = Hash.Parse(value);
                }
                catch
                {
                    m_meta = null;
                }
            }
        }
        public Hash Meta
        {
            set
            {
                m_meta = value;
            }
            get
            {
                return m_meta;
            }
        }
        #endregion

        #region constructor
        public CommonItem(HttpStatusCode status_code, string strMessage = null) : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public CommonItem(string strMessage) : this()
        {
            Message = strMessage;
        }
        public CommonItem()
        {
        }
        #endregion
    }
    public class CommonList
    {
        private int m_parent;
        private Pagination m_pagination;
        private string m_message;
        private HttpStatusCode m_StatusCode;

        public int Parent
        {
            get { return m_parent; }
        }
        [JsonProperty("parent")]
        public object _SetParent
        {
            set
            {
                try
                {
                    m_parent = Convert.ToInt32(value);
                }
                catch
                {
                    m_parent = 0;
                }
            }
        }
        [JsonProperty("pagination")]
        public Pagination Pagination
        {
            get { return m_pagination; }
            set { m_pagination = value; }
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
    }
    public class Pagination
    {
        #region variables
        private int m_total;
        private int m_per_page;
        private int m_num_pages;
        private int m_current_page;
        private int? m_prev_page;
        private int? m_next_page;
        #endregion

        #region constructor
        public Pagination()
        {
        }
        #endregion

        #region getter/setter
        [JsonProperty("total")]
        public int Total
        {
            get { return m_total; }
            set { m_total = value; }
        }

        [JsonProperty("per_page")]
        public int PerPage
        {
            get { return m_per_page; }
            set
            {
                try
                {
                    m_per_page = value;
                }
                catch
                {
                    m_per_page = 0;
                }
            }
        }

        [JsonProperty("num_pages")]
        public int NumPages
        {
            get { return m_num_pages; }
            set
            {
                try
                {
                    m_num_pages = value;
                }
                catch
                {
                    m_num_pages = 0;
                }
            }
        }

        [JsonProperty("current_page")]
        public int CurrentPage
        {
            get { return m_current_page; }
            set
            {
                try
                {
                    m_current_page = value;
                }
                catch
                {
                    m_current_page = 0;
                }
            }
        }

        [JsonProperty("prev_page")]
        public int? PrevPage
        {
            get { return m_prev_page; }
            set
            {
                m_prev_page = value;
            }
        }

        [JsonProperty("next_page")]
        public int? NextPage
        {
            get { return m_next_page; }
            set
            {
                m_next_page = value;
            }
        }
        #endregion
    }
    public class Meta
    {
        public string Key;
        public string Value;

        public Meta()
        {
        }

        public Meta(string strKey, string strValue)
        {
            Key = strKey;
            Value = strValue;
        }
    }
    public class Hash : Hashtable
    {
        public static Hash Parse(object objData)
        {
            string strData = null;
            try
            {
                strData = objData.ToString();
            }
            catch (Exception ee)
            {
                throw ee;
            }

            return Hash.Parse(strData);
        }
        public static Hash Parse(string strData)
        {
            try
            {
                Hash hash = JsonConvert.DeserializeObject<Hash>(strData, TSCloud.serializer_settings());
                return hash;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public string Stringify()
        {
            try
            {
                return JsonConvert.SerializeObject(this, TSCloud.serializer_settings());
            }
            catch
            {
                throw;
            }
        }
    }
}
