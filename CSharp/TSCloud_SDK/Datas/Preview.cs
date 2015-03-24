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
            if (!String.IsNullOrEmpty(this.Big) && !String.IsNullOrEmpty(this.Small))
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
