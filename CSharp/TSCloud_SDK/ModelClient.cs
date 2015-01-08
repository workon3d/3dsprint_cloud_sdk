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
using System.Collections;

namespace TDSPRINT.Cloud.SDK
{
    public class ModelClient : TSCloud
    {
        #region member variables
        private Hash Configuration;
        #endregion

        #region constructor
        public ModelClient()
        {
            this.Configuration = null;
        }
        public ModelClient(TSCloud TSCloud) : this()
        {
            Hostname = TSCloud.Hostname;
            RestClient = new RestClient(Hostname);
            ApiToken = TSCloud.ApiToken;
            CurrentUser = TSCloud.CurrentUser;
            Users = TSCloud.Users;

            // Default Configuration
            Configuration = new Hash();
            Configuration["PerPage"] = 30;
        }
        public ModelClient(TSCloud TSCloud, Hash Configuration) : this(TSCloud)
        {
            this.Configuration = Configuration;
        }
        #endregion

        #region deligate
        public delegate void onProgress(float fPercent);
        #endregion

        #region public method
        public Models GetModels(Ftype ftype)
        {
            return GetModels(0, ftype);
        }
        public Models GetModels(int FolderId)
        {
            return GetModels(0, Ftype.All, FolderId);
        }
        public Models GetModels(Ftype ftype, int FolderId)
        {
            return GetModels(0, ftype, FolderId);
        }
        public Models GetModels(int Page, Ftype ftype)
        {
            return GetModels(Page, ftype, 0);
        }
        public Models GetModels(int Page, Ftype ftype, int FolderId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.GET);
            if (ftype == Ftype.Folder)
                request.AddParameter("folder", "true");
            else if(ftype == Ftype.File)
                request.AddParameter("ftype", "file");

            request.AddParameter("api_token", ApiToken);
            if (Page != 0)
                request.AddParameter("page", Page);
            if (FolderId != 0)
                request.AddParameter("parent_id", FolderId);
            if (Configuration["PerPage"] != null)
                request.AddParameter("per_page", Configuration["PerPage"]);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content, TSCloud.serializer_settings());

                return models;
            }
            catch(Exception ee)
            {
                throw ee;
            }
        }

        [Obsolete("This All(Ftype) method is able to causing overload to API Sever, so please use only development")]
        public List<Model> All(Ftype type)
        {
            return index(0, type);
        }
        [Obsolete("This All() method is able to causing overload to API Sever, so please use only for development")]
        public List<Model> All()
        {
            return All(Ftype.All);
        }
        public Model Get(int ModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, ModelId.ToString()), Method.GET);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());

                int owner_id = 0;
                Hash hash = JsonConvert.DeserializeObject<Hash>(model.Acl.ToString());
                owner_id = Convert.ToInt32(hash["owner"]);
                model.Owner = Users.FindById(owner_id);
                return model;
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
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());
                    model.StatusCode = httpResponse.StatusCode;
                    model.Message = httpResponse.ErrorMessage;

                    return model;
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

        public Model Update(int ModelId, string ModelName, string FilePath, string strMetaJson)
        {
            try
            {
                Hash hashed_meta = JsonConvert.DeserializeObject<Hash>(strMetaJson, TSCloud.serializer_settings());
                return Update(ModelId, ModelName, FilePath, hashed_meta);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        public Model Update(int ModelId, string ModelName, string FilePath, Hash MetaJson)
        {
            //#region JSON parameter checking
            //try
            //{
            //    JsonConvert.DeserializeObject(Acl);
            //}
            //catch
            //{
            //    return new Model("Invalid JSON ACL");
            //}
            //#endregion

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, ModelId.ToString()), Method.PUT);
            request.AddParameter("api_token", ApiToken);

            if(String.IsNullOrEmpty(ModelName) && !String.IsNullOrWhiteSpace(FilePath))
                request.AddParameter("name", Path.GetFileName(FilePath));
            else 
                request.AddParameter("name", ModelName);

            if(!String.IsNullOrEmpty(FilePath))
                request.AddFile("file", FilePath);

            if (MetaJson != null)
            {
                string strMeta = JsonConvert.SerializeObject(MetaJson, TSCloud.serializer_settings());
                request.AddParameter("meta", strMeta);
            }
            
            //if(!String.IsNullOrEmpty(Acl))
            //    request.AddParameter("acl", Acl);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());
                    model.StatusCode = httpResponse.StatusCode;
                    model.Message = httpResponse.ErrorMessage;

                    return model;
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
        //public Model Update(int ModelId, string ModelName, string FilePath)
        //{
        //    return Update(ModelId, ModelName, FilePath, null, null);
        //}
        //public Model Update(int ModelId, string ModelName)
        //{   
        //    return Update(ModelId, ModelName, null, null, null);
        //}
        //public Model Update(int ModelId, string sMetaJson)
        //{
        //    return Update(ModelId, null, null, MetaJson, null);
        //}

        public HttpStatusCode Download(int ModelId, string strDownloadPath, onProgress _onProgress)
        {
            if (!Directory.Exists(Path.GetDirectoryName(strDownloadPath)))
                return HttpStatusCode.BadRequest;

            Model file = this.Get(ModelId);
            int filesize = Convert.ToInt32(file.Size);
            string download_url = this.GetDownloadURL(ModelId);
            try
            {
                HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(download_url);
                Request.Method = "GET";
                Request.AllowAutoRedirect = true;

                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                using (Stream ResponseStream = Response.GetResponseStream())
                {
                    using (FileStream fs = new FileStream(strDownloadPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        int current = 0;
                        float percentage = 0;
                        int chunkSize = 1024 * 1024;
                        byte[] buffer = new byte[chunkSize];
                        int BytesRead;
                        while ((BytesRead = ResponseStream.Read(buffer, 0, chunkSize)) > 0)
                        {
                            fs.Write(buffer, 0, BytesRead);
                            current += BytesRead;

                            percentage = (float)(Math.Round((double)current / (double)filesize, 4) * 100);
                            if (_onProgress != null)
                                _onProgress(percentage);
                        }
                    }
                }

                return Response.StatusCode;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        
        [Obsolete("This method will be deprecated, so use Download(int, string)", false)]
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
            return String.Format("{0}/{1}/folders/{2}/download?api_token={3}", Hostname, ApiPath, Convert.ToString(ModelId), System.Uri.EscapeUriString(ApiToken));
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
            Acl AclObject = new Acl(Int32.Parse(CurrentUser.Id.ToString()));
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
            if (Configuration["PerPage"] != null)
                request.AddParameter("per_page", Configuration["PerPage"]);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content, TSCloud.serializer_settings());

                int num_pages = models.Pagination.NumPages;
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
                    foreach (Model model in models.Contents)
                    {
                        if (Convert.ToString(model.Ftype) == ftype.ToString())
                            filtered_model_list.Add(model);
                    }

                    return filtered_model_list;
                }
                else
                {
                    return models.Contents;
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
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content, TSCloud.serializer_settings());

                int num_pages = models.Pagination.NumPages;
                if (Page == 0)
                {
                    List<Model> model_list = new List<Model>();

                    for (int i = 1; i <= num_pages; i++)
                    {
                        model_list.AddRange(search_by_query(query, i));
                    }

                    return model_list;
                }
                return models.Contents;

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
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content, TSCloud.serializer_settings());

                int num_pages = models.Pagination.NumPages;
                if (Page == 0)
                {
                    List<Model> model_list = new List<Model>();

                    for (int i = 1; i <= num_pages; i++)
                    {
                        model_list.AddRange(search_by_meta(key, value, i));
                    }

                    return model_list;
                }
                return models.Contents;
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

        #endregion
    }
}
