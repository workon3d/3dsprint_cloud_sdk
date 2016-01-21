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
        #region Member Variable
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

        #region Constructor
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

        #region Getter/Setter
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

        #region Static Method
        static public bool IsValid(Model model) 
        {
            return model.IsValid();
        }
        #endregion

        #region Method
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

        public Model Update()
        {
            if (!IsSysInfoDefined())
                throw new Exception("SysInfo is not defined");
            if (this.Id == 0)
                throw new Exception("Model ID Required");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", SysInfo.ApiPath, Convert.ToString(this.Id)), Method.PUT);
            request.AddParameter("api_token",SysInfo.ApiToken);
            request.AddParameter("name", this.Name);
            if (this.Meta != null)
                request.AddParameter("meta", this.Meta.Stringify());
            if (this.Acl != null)
                request.AddParameter("acl", this.Acl.Stringify());
            if (this.Ftype == Ftype.Page)
                request.AddParameter("page", this.Content);
            request.AddParameter("description", this.Description);

            try
            {
                IRestResponse httpResponse = GetRestClient().Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Model model_response = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());
                    model_response.StatusCode = httpResponse.StatusCode;
                    model_response.Message = httpResponse.ErrorMessage;
                    model_response.SysInfo = this.SysInfo;

                    return model_response;
                }
                else
                {
                    return new Model(httpResponse.Content);
                }
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }

        public bool UpdateMeta(Hash Meta)
        {
            if (!this.IsValid())
                throw new Exception("Model is not valid");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/meta", SysInfo.ApiPath, Convert.ToString(this.Id)), Method.PUT);

            request.AddParameter("api_token",SysInfo.ApiToken);

            if (Meta != null)
                request.AddParameter("meta", Meta.Stringify());
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

        public bool RemoveMeta(List<String> KeyList)
        {
            if (!this.IsValid())
                throw new Exception("Model is not valid");

            if (KeyList.Count == 0)
                throw new Exception("Hash key to be removed required");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            string serialized = JsonConvert.SerializeObject(KeyList);

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/meta", SysInfo.ApiPath, Convert.ToString(this.Id)), Method.DELETE);
            request.AddParameter("api_token",SysInfo.ApiToken);
            request.AddParameter("keys", serialized);

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

        public string GetLogs(string From = null, string To = null)
        {
            if (!this.IsValid())
                throw new Exception("Model is not valid");

            if (!IsSysInfoDefined())
                throw new Exception("SysInfo is not defined");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/logs", SysInfo.ApiPath, Convert.ToString(this.Id)), Method.GET);
            request.AddParameter("api_token",SysInfo.ApiToken);
            if (From != null)
                request.AddParameter("from", From);
            if (To != null)
                request.AddParameter("to", To);

            try
            {
                IRestResponse httpResponse = GetRestClient().Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Convert.ToString(httpResponse.Content);
                }
                else
                {
                    throw new Exception(String.Format("{0} : {1}", httpResponse.StatusCode, httpResponse.ErrorMessage));
                }
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
        
        public List<Comment> GetComments()
        {
            if (!IsSysInfoDefined())
                throw new Exception("SysInfo is not defined");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/comments", SysInfo.ApiPath, Convert.ToString(this.Id)), Method.GET);
            request.AddParameter("api_token",SysInfo.ApiToken);

            try
            {
                IRestResponse httpResponse = GetRestClient().Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Comments comment_list = JsonConvert.DeserializeObject<Comments>(httpResponse.Content, TSCloud.serializer_settings());
                    comment_list.StatusCode = httpResponse.StatusCode;
                    comment_list.Message = httpResponse.ErrorMessage;
                    comment_list.Contents.ForEach(x => x.SysInfo = this.SysInfo);

                    return comment_list.Contents;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public Comment CreateComment(string Content)
        {
            if (!IsSysInfoDefined())
                throw new Exception("SysInfo is not defined");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/comments", SysInfo.ApiPath, Convert.ToString(this.Id)), Method.POST);
            request.AddParameter("api_token",SysInfo.ApiToken);
            request.AddParameter("content", Content);

            try
            {
                IRestResponse httpResponse = GetRestClient().Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Comment response = JsonConvert.DeserializeObject<Comment>(httpResponse.Content, TSCloud.serializer_settings());
                    response.StatusCode = httpResponse.StatusCode;
                    response.Message = httpResponse.ErrorMessage;
                    response.SysInfo = this.SysInfo;

                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public Model CopyTo(int TargetFolderId)
        {
            throw new NotImplementedException();
        }

        public Model MoveTo(int TargetFolderId)
        {
            throw new NotImplementedException();
        }

        public Model Remove()
        {
            throw new NotImplementedException();
        }

        public string GetDownloadURL()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
