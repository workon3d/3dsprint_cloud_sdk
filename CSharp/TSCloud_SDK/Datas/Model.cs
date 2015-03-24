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
        private int m_size;
        private string m_key;
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
        [JsonProperty("content")]
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public string Filepath { get; set; }
        public string FileUrl { get; set; }
        [JsonProperty("size")]
        protected object _SetSize
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
        [JsonConverter(typeof(FtypeEnumConverter))]
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

        #region static method
        static public bool IsValid(Model model) 
        {
            return model.IsValid();
        }
        public override bool IsValid()
        {
            try
            {
                if (Id == 0 || String.IsNullOrEmpty(Key) || Meta == null)
                    return false;
                else
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
