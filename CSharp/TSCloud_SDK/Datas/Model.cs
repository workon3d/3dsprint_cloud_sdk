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
    public class Models : CommonList
    {
        private List<Model> m_contents;
        [JsonProperty("contents")]
        public List<Model> Contents
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

    public class Model : CommonItem
    {
        #region member variable
        private bool m_readonly;
        //private string m_name;
        private int m_size;
        private string m_key;
        //private Hash m_meta;
        private string m_ancestry;
        private Ftype m_ftype;
        private string m_api_url;
        private Preview m_preview;
        private string m_description;
        private User m_owner;

        public User Owner
        {
            get { return m_owner; }
            set { m_owner = value; }
        }
        #endregion

        #region constructor
        public Model(HttpStatusCode status_code, string strMessage = null)
            : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public Model(string strMessage)
            : this()
        {
            Message = strMessage;
        }
        public Model()
        {
        }
        #endregion

        #region getter/setter


        [JsonProperty("size")]
        public object SetSize
        {
            set {
                try
                {
                    m_size = Convert.ToInt32(value);
                }
                catch
                {
                    m_size = 0;
                }
            }
        }
        public int Size
        {
            get { return m_size; }
        }
        [JsonProperty("key")]
        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }
        [JsonProperty("ancestry")]
        public string Ancestry
        {
            get { return m_ancestry; }
            set { m_ancestry = value; }
        }
        [JsonProperty("ftype")]
        public Ftype Ftype
        {
            get { return m_ftype; }
            set { m_ftype = value; }
        }

        [JsonProperty("api_url")]
        public string ApiUrl
        {
            get { return m_api_url; }
            set { m_api_url = value; }
        }
        [JsonProperty("preview")]
        public Preview Preview
        {
            get { return m_preview; }
            set { m_preview = value; }
        }
        [JsonProperty("description")]
        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }
        [JsonProperty("readonly")]
        public bool Readonly
        {
            get { return m_readonly; }
            set
            {
                try
                {
                    m_readonly = value;
                }
                catch
                {
                    m_readonly = false;
                }
            }
        }
        #endregion

        #region method
        public Model Update()
        {
            return new Model();
        }
        #endregion

        #region static method
        static public bool IsValid(Model model)
        {
            try
            {
                if (model.Id == 0 || String.IsNullOrEmpty(model.Key) || String.IsNullOrEmpty(model.Acl.ToString()))
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValid()
        {
            return Model.IsValid(this);
        }
        #endregion
    }

    public class Preview
    {
        #region variables
        private string m_state;
        private string m_url;
        private string m_vprint_save_url;
        private string m_big;
        private string m_small;
        #endregion

        #region getter/setter
        [JsonProperty("state")]
        public string State
        {
            get { return m_state; }
            set
            {
                try
                {
                    m_state = value;
                }
                catch
                {
                    m_state = null;
                }
            }
        }
        [JsonProperty("url")]
        public string Url
        {
            get { return m_url; }
            set
            {
                try
                {
                    m_url = value;
                }
                catch
                {
                    m_url = null;
                }
            }
        }
        [JsonProperty("vprint_save_url")]
        public string VprintSaveUrl
        {
            get { return m_vprint_save_url; }
            set
            {
                try
                {
                    m_vprint_save_url = value;
                }
                catch
                {
                    m_vprint_save_url = null;
                }
            }
        }
        [JsonProperty("big")]
        public string Big
        {
            get { return m_big; }
            set
            {
                try
                {
                    m_big = value;
                }
                catch
                {
                    m_big = null;
                }
            }
        }
        [JsonProperty("small")]
        public string Small
        {
            get { return m_small; }
            set
            {
                try
                {
                    m_small = value;
                }
                catch
                {
                    m_small = null;
                }
            }
        }
        #endregion

        #region methods
        public bool IsValidThumbnail()
        {
            if (!String.IsNullOrWhiteSpace(this.Big) && !String.IsNullOrWhiteSpace(this.Small))
                return true;
            return false;
        }
        public bool IsEmpty()
        {
            if (!String.IsNullOrEmpty(this.Big) || !String.IsNullOrEmpty(this.Small) || !String.IsNullOrEmpty(this.Url) || !String.IsNullOrEmpty(this.VprintSaveUrl))
                return false;

            if (String.IsNullOrEmpty(this.State))
                return true;
            else
                return false;
        }
        #endregion

    }

}
