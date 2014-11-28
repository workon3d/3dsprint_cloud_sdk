using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;

// External Library
using RestSharp;
using Newtonsoft.Json;

using TDSPRINT.Cloud.SDK.Datas;
using TDSPRINT.Cloud.SDK.Types;

namespace TDSPRINT.Cloud.SDK
{
    public class ModelClient : TSCloud
    {
        #region constructor
        public ModelClient()
        {
        }
        public ModelClient(TSCloud TcClient) : this()
        {
            TcHost = TcClient.TcHost;
            RestClient = new RestClient(TcHost);
            ApiToken = TcClient.ApiToken;
            CurrentUser = TcClient.CurrentUser;
        }
        #endregion

        #region public method
        public List<Model> All(Ftype type)
        {
            return index(0, type);
        }
        public List<Model> All()
        {
            return All(Ftype.All);
        }
        public Model Get(int FileId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, FileId.ToString()), Method.GET);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Model jsonResponse = JsonConvert.DeserializeObject<Model>(httpResponse.Content, serializer_settings());
                return jsonResponse;
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }

        public Model Create(string ModelName, int ParentId, string FilePath, object MetaJson, string Acl)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.POST);
            string strModelName;

            if (Acl == null)
                Acl = this.get_acl();

            if (ParentId != 0)
                request.AddParameter("parent_id", ParentId.ToString());

            if (!String.IsNullOrEmpty(ModelName))
                strModelName = ModelName;
            else
                strModelName = Path.GetFileName(FilePath);

            request.AddParameter("acl", Acl);
            request.AddParameter("name", strModelName);
            request.AddParameter("api_token", ApiToken);

            if (MetaJson != null)
                request.AddParameter("meta", MetaJson);

            if(!String.IsNullOrEmpty(FilePath))
                request.AddFile("file", FilePath);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content, serializer_settings());
                model.StatusCode = httpResponse.StatusCode;
                model.Message = httpResponse.ErrorMessage;

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
        public Model Create(string ModelName, int ParentId)
        {
            return Create(ModelName, ParentId, null, null,null);
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

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, ModelId.ToString()), Method.PUT);
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
                Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content, serializer_settings());
                model.StatusCode = httpResponse.StatusCode;
                model.Message = httpResponse.ErrorMessage;

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
            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/download", ApiPath, Convert.ToString(ModelId)), Method.GET);
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

        public string GetDownloadURL(int ModelId)
        {
            return String.Format("{0}/{1}/folders/{2}/download?api_token={3}", TcHost, ApiPath, Convert.ToString(ModelId), System.Uri.EscapeUriString(ApiToken));
        }

        public Model Delete(int ModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, Convert.ToString(ModelId)), Method.DELETE);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                return new Model(httpResponse.StatusCode);
            }
            catch(Exception ee)
            {
                return new Model(System.Net.HttpStatusCode.InternalServerError, ee.ToString());
            }
        }

        public List<Model> Find(string key, string value)
        {
            return search_by_meta(key, value);
        }
        public List<Model> Find(string query)
        {
            if (is_meta_search(query))
            {
                Meta Meta = get_meta(query);
                return search_by_meta(Meta.Key, Meta.Value);
            }
            else
            {
                return search_by_query(query);
            }
        }

        public HttpStatusCode Copy(int[] ModelIds, int TargetModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/copy", ApiPath), Method.PUT);
            request.AddParameter("ids", this.parse_model_ids(ModelIds));
            request.AddParameter("parent_id", Convert.ToString(TargetModelId));
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                return httpResponse.StatusCode;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
        public HttpStatusCode Copy(int ModelId, int TargetModelId)
        {
            int[] ModelIds = {ModelId};
            return Copy(ModelIds, TargetModelId);
        }
        
        public HttpStatusCode Move(int[] ModelIds, int TargetModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/move", ApiPath), Method.PUT);
            request.AddParameter("ids", this.parse_model_ids(ModelIds));
            request.AddParameter("parent_id", Convert.ToString(TargetModelId));
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                return httpResponse.StatusCode;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
        public HttpStatusCode Move(int ModelId, int TargetModelId)
        {
            int[] ModelIds = { ModelId };
            return Move(ModelIds, TargetModelId);
        }
        #endregion

        #region private method
        private string parse_model_ids(int[] model_ids)
        {
            int length = model_ids.Length;
            string strResult = null;

            if (length == 1)
                return Convert.ToString(model_ids[0]);

            try
            {
                StringBuilder buildString = new StringBuilder();

                for (int i = 0; i < model_ids.Length; i++)
                {
                    buildString = buildString.Append(Convert.ToString(model_ids[i]));
                    if (i != length)
                        buildString = buildString.Append(',');
                }

                strResult = buildString.ToString();
            }
            catch
            {
                throw;
            }

            return strResult;
        }
        private string get_acl()
        {
            Acl AclObject = new Acl(Int32.Parse(CurrentUser.id.ToString()));
            return JsonConvert.SerializeObject(AclObject, Formatting.None);
        }

        private bool is_meta_search(string query)
        {
            if (String.IsNullOrWhiteSpace(query))
                return false;

            try
            {
                if (query.Contains("="))
                    return true;
            }
            catch
            {
                throw;
            }

            return false;   
        }

        private Meta get_meta(string query)
        {
            if (!is_meta_search(query))
                return new Meta();

            try
            {
                string[] Split = query.Split('=');
                string key = Split[0].Trim();
                string value = Split[1].Trim();
                if (value[0] == '"' || value[0] == '\'')
                    value = value.Remove(0, 1);
                if (value[value.Length-1] == '"' || value[value.Length-1] == '\'')
                    value = value.Remove(value.Length-1, 1);

                return new Meta(key, value);
            }
            catch
            {
                throw;
            }
        }

        private List<Model> index(int Page, Ftype ftype)
        {
            return index(0, Page, ftype);
        }

        private List<Model> index(int ModelId, int Page, Ftype ftype)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.GET);
            if(ftype != Ftype.All)
                request.AddParameter("ftype", ftype.ToString());
            request.AddParameter("api_token", ApiToken);
            if(Page != 0)
                request.AddParameter("page", Page); 

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content);

                int num_pages = Int32.Parse(Convert.ToString(models.pagination.num_pages));
                if (Page == 0)
                {
                    List<Model> model_list = new List<Model>();

                    for (int i = 1; i <= num_pages; i++)
                    {
                        model_list.AddRange(index(ModelId, i, ftype));
                    }

                    return model_list;
                }

                List<Model> filtered_model_list = new List<Model>();

                if (ftype != Ftype.All)
                {
                    foreach (Model model in models.contents)
                    {
                        if (Convert.ToString(model.ftype) == ftype.ToString())
                            filtered_model_list.Add(model);
                    }

                    return filtered_model_list;
                }
                else
                {
                    return models.contents;
                }
            }
            catch
            {
                return new List<Model>();
            }
        }

        private List<Model> search_by_query(string query, int Page)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.GET);
            request.AddParameter("q", query);
            request.AddParameter("api_token", ApiToken);
            if (Page != 0)
                request.AddParameter("page", Page);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content);

                int num_pages = Int32.Parse(Convert.ToString(models.pagination.num_pages));
                if (Page == 0)
                {
                    List<Model> model_list = new List<Model>();

                    for (int i = 1; i <= num_pages; i++)
                    {
                        model_list.AddRange(search_by_query(query, i));
                    }

                    return model_list;
                }
                return models.contents;

            }
            catch
            {
                return new List<Model>();
            }
        }
        private List<Model> search_by_query(string query)
        {
            return search_by_query(query, 0);
        }

        private List<Model> search_by_meta(string key, string value, int Page)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.GET);
            request.AddParameter("meta", String.Format("{0}:{1}", key, value));
            request.AddParameter("api_token", ApiToken);
            if (Page != 0)
                request.AddParameter("page", Page);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content);

                int num_pages = Int32.Parse(Convert.ToString(models.pagination.num_pages));
                if (Page == 0)
                {
                    List<Model> model_list = new List<Model>();

                    for (int i = 1; i <= num_pages; i++)
                    {
                        model_list.AddRange(search_by_meta(key, value, i));
                    }

                    return model_list;
                }
                return models.contents;
            }
            catch
            {
                return new List<Model>();
            }
        }
        private List<Model> search_by_meta(string key, string value)
        {
            return search_by_meta(key, value, 0);
        }

        private JsonSerializerSettings serializer_settings()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.MissingMemberHandling = MissingMemberHandling.Ignore;

            return setting;
        }
        #endregion
    }
}
