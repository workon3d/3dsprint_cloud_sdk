using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters;

// External Library
using RestSharp;
using Newtonsoft.Json;

// TP2_SDK
using TeamPlatform.TP2_SDK.Object;
using System.Net;

namespace TeamPlatform.TP2_SDK
{
    public class TpModelClient : TP2
    {
        #region constructor
        public TpModelClient()
        {
        }

        public TpModelClient(TP2 TpClient) : this()
        {
            RestClient = new RestClient(Tp2Host);
            ApiToken = TpClient.ApiToken;
            CurrentUser = TpClient.CurrentUser;
        }
        #endregion

        #region public method
        public List<Model> All()
        {
            RestRequest request = new RestRequest(String.Format("{0}/models", ApiPath), Method.GET);
            request.AddParameter("api_token",  ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content);

                return models.contents;
            }
            catch
            {
                return new List<Model>();
            }
        }
        public Model Get(int FileId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/models/{1}", ApiPath, FileId.ToString()), Method.GET);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Model jsonResponse = JsonConvert.DeserializeObject<Model>(httpResponse.Content);

                return jsonResponse;
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }

        public Model Create(string ModelName, int ParentId, string FilePath, object MetaJson, string Acl)
        {
            RestRequest request = new RestRequest(String.Format("{0}/models", ApiPath), Method.POST);
            string strModelName;

            if (Acl == null)
                Acl = this.GetAcl();

            if (ParentId != 0)
                request.AddParameter("parent_id", ParentId.ToString());

            if (!String.IsNullOrEmpty(ModelName))
                strModelName = ModelName;
            else
                strModelName = Path.GetFileName(FilePath);

            request.AddParameter("acl", Acl);
            request.AddParameter("name", strModelName);
            request.AddParameter("api_token", ApiToken);

            if(!String.IsNullOrEmpty(FilePath))
                request.AddFile("file", FilePath);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content);
                model.status_code = httpResponse.StatusCode;
                model.message = httpResponse.ErrorMessage;

                return model;
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }
        public Model Create(string ModelName, int ParentId, string FilePath, object MetaJson)
        {
            return Create(ModelName, 0, FilePath, null, null);
        }
        public Model Create(string ModelName, string FilePath)
        {
            return Create(ModelName, 0, FilePath, null, null);
        }
        public Model Create(string FilePath, object MetaJson)
        {
            return Create(null, 0, FilePath, MetaJson, null);
        }
        public Model Create(string FilePath)
        {
            return Create(null, 0, FilePath, null, null);
        }

        public Model Update(int ModelId, string ModelName, string FilePath, object MetaJson, string Acl)
        {
            #region JSON parameter checking
            try
            {
                if (MetaJson != null)
                {
                    JsonConvert.DeserializeObject(MetaJson.ToString());
                }
            }
            catch
            {
                return new Model("Invalid JSON meta");
            }

            try
            {
                JsonConvert.DeserializeObject(Acl);
            }
            catch
            {
                return new Model("Invalid JSON ACL");
            }
            #endregion

            RestRequest request = new RestRequest(String.Format("{0}/models/{1}", ApiPath, ModelId.ToString()), Method.PUT);
            request.AddParameter("api_token", ApiToken);

            if(String.IsNullOrEmpty(ModelName) && !String.IsNullOrWhiteSpace(FilePath))
                request.AddParameter("name", Path.GetFileName(FilePath));
            else 
                request.AddParameter("name", ModelName);

            if(!String.IsNullOrEmpty(FilePath))
                request.AddFile("file", FilePath);

            if(MetaJson != null)
                request.AddParameter("meta", MetaJson);
            
            if(!String.IsNullOrEmpty(Acl))
                request.AddParameter("acl", Acl);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content);
                model.status_code = httpResponse.StatusCode;
                model.message = httpResponse.ErrorMessage;

                return model;
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }
        public Model Update(int ModelId, string ModelName, string FilePath)
        {
            return Update(ModelId, ModelName, FilePath, null, null);
        }
        public Model Update(int ModelId, string ModelName)
        {
            return Update(ModelId, ModelName, null, null, null);
        }
        public Model Update(int ModelId, object MetaJson)
        {
            return Update(ModelId, null, null, MetaJson, null);
        }

        public byte[] Download(int ModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/models/{1}/download", ApiPath, Convert.ToString(ModelId)), Method.GET);
            request.AddParameter("api_token", ApiToken);
            
            try
            {
                return RestClient.DownloadData(request);
            }
            catch
            {
                throw;
            }
        }

        public Model Delete(int ModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/models/{1}", ApiPath, Convert.ToString(ModelId)), Method.DELETE);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                return new Model(httpResponse.StatusCode);
            }
            catch(Exception ee)
            {
                return new Model(ee.ToString(), System.Net.HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region private method
        private string GetAcl()
        {
            Acl AclObject = new Acl(Int32.Parse(CurrentUser.id.ToString()));
            return JsonConvert.SerializeObject(AclObject, Formatting.None);
        }
        #endregion
    }
}
