using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using RestSharp;
using Newtonsoft.Json;

using TDSPRINT.Cloud.SDK.Datas;
using TDSPRINT.Cloud.SDK.Types;
using TDSPRINT.Cloud.SDK;


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
        public Models GetModels(Ftype ftype, params GetModelsOption[] Options)
        {
            return GetModels(0, ftype, 0, Options);
        }
        public Models GetModels(int FolderId, params GetModelsOption[] Options)
        {
            return GetModels(0, Ftype.All, FolderId, Options);
        }
        public Models GetModels(Ftype ftype, int FolderId, params GetModelsOption[] Options)
        {
            return GetModels(0, ftype, FolderId, Options);
        }
        public Models GetModels(int Page, Ftype ftype, params GetModelsOption[] Options)
        {
            return GetModels(Page, ftype, 0, Options);
        }
        public Models GetModels(int Page, Ftype ftype, int FolderId, params GetModelsOption[] GetModelsOptions)
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

            if (GetModelsOptions.Length > 0)
            {
                foreach (GetModelsOption Option in GetModelsOptions)
                {
                    switch(Option)
                    {
                        case GetModelsOption.OnlyChildren:
                            if(FolderId == 0)
                                request.AddParameter("root", "true");
                            request.AddParameter("descendants", "false");
                            break;
                        case GetModelsOption.AllDescendants:
                            request.AddParameter("descendants", "true");
                            break;
                    }
                }
            }

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content, TSCloud.serializer_settings());
                models.Contents.ForEach(x => x.SysInfo = GetSysInfo());

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
                owner_id = Convert.ToInt32(model.Acl["owner"]);
                model.SysInfo = GetSysInfo();

                return model;
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }

        public Model Create(Model model)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.POST);
            
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("name", !String.IsNullOrEmpty(model.Name) || String.IsNullOrEmpty(model.Filepath) ? model.Name : Path.GetFileName(model.Filepath));            
            if (model.ParentId != null && model.ParentId != 0)
                request.AddParameter("parent_id", Convert.ToString(model.ParentId));
            if (!String.IsNullOrEmpty(model.Filepath))
                request.AddFile("file", model.Filepath);
            else if(!String.IsNullOrEmpty(model.FileUrl))
                request.AddParameter("file_url", model.FileUrl);
            if (model.Meta != null)
                request.AddParameter("meta", model.Meta.Stringify());
            if (model.Acl != null)
                request.AddParameter("acl", model.Acl.Stringify());
            if (model.Ftype == Ftype.Page)
            {
                request.AddParameter("page[content]", model.Content);
                request.AddParameter("is_page", "true");
            }
            request.AddParameter("description", model.Description);
            
            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Model model_response = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());
                    model_response.StatusCode = httpResponse.StatusCode;
                    model_response.Message = httpResponse.ErrorMessage;
                    model_response.SysInfo = GetSysInfo();

                    return model_response;
                }
                else
                {
                    return new Model(httpResponse.StatusCode, httpResponse.Content);
                }
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }
        public Model Create(string ModelName, int ParentId, string FilePath, Hash Meta, Hash Acl)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.POST);
            string strModelName;

            if (Acl != null)
                request.AddParameter("acl", Acl.Stringify());

            if (ParentId != 0)
                request.AddParameter("parent_id", ParentId.ToString());

            if (!String.IsNullOrEmpty(ModelName))
                strModelName = ModelName;
            else
                strModelName = Path.GetFileName(FilePath);

            request.AddParameter("name", strModelName);
            request.AddParameter("api_token", ApiToken);

            if (Meta != null)
                request.AddParameter("meta", Meta.Stringify());

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
                    model.SysInfo = GetSysInfo();

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
        public Model Create(string ModelName, int ParentId, string FilePath, Hash Meta)
        {
            return Create(ModelName, 0, FilePath, Meta, null);
        }
        public Model Create(string ModelName, string FilePath)
        {
            return Create(ModelName, 0, FilePath, null, null);
        }
        //public Model Create(string FilePath, object MetaJson)
        //{
        //    return Create(null, 0, FilePath, MetaJson, null);
        //}
        public Model Create(string ModelName, int ParentId)
        {
            return Create(ModelName, ParentId, null, null, null);
        }
        public Model Create(string FilePath)
        {
            return Create(null, 0, FilePath, null, null);
        }

        public Model Update(Model model)
        {
            if (model.Id == 0)
                throw new Exception("Model ID Required");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, Convert.ToString(model.Id)), Method.PUT);

            request.AddParameter("api_token", ApiToken);
            request.AddParameter("name", !String.IsNullOrEmpty(model.Name) || String.IsNullOrEmpty(model.Filepath) ? model.Name : Path.GetFileName(model.Filepath));
            if (!String.IsNullOrEmpty(model.Filepath))
                request.AddFile("file", model.Filepath);
            if (model.Meta != null)
                request.AddParameter("meta", model.Meta.Stringify());
            if (model.Acl != null)
                request.AddParameter("acl", model.Acl.Stringify());
            if (model.Ftype == Ftype.Page)
                request.AddParameter("page", model.Content);
            request.AddParameter("description", model.Description);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Model model_response = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());
                    model_response.StatusCode = httpResponse.StatusCode;
                    model_response.Message = httpResponse.ErrorMessage;
                    model_response.SysInfo = GetSysInfo();

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
        public Model Update(int ModelId, string ModelName, string FilePath, Hash MetaJson)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, Convert.ToString(ModelId), Method.PUT));
            request.AddParameter("api_token", ApiToken);

            if(String.IsNullOrEmpty(ModelName) && !String.IsNullOrEmpty(FilePath))
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

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Model model = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());
                    model.StatusCode = httpResponse.StatusCode;
                    model.Message = httpResponse.ErrorMessage;
                    model.SysInfo = GetSysInfo();

                    return model;
                }
                else
                {
                    return new Model(httpResponse.StatusCode, httpResponse.Content);
                }
            }
            catch (Exception ee)
            {
                return new Model(ee.ToString());
            }
        }

        [Obsolete("This method will be deprecated", false)]
        public bool UpdateMeta(Model model, Hash Meta)
        {
            return UpdateMeta(model.Id, Meta);
        }
        [Obsolete("This method will be deprecated", false)]
        public bool UpdateMeta(int ModelId, Hash Meta)
        {
            if (ModelId == 0)
                throw new Exception("Model ID Required");

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/meta", ApiPath, Convert.ToString(ModelId)), Method.PUT);

            request.AddParameter("api_token", ApiToken);
            if (Meta != null)
                request.AddParameter("meta", Meta.Stringify());
            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                    //Model model_response = JsonConvert.DeserializeObject<Model>(httpResponse.Content, TSCloud.serializer_settings());
                    //model_response.StatusCode = httpResponse.StatusCode;
                    //model_response.Message = httpResponse.ErrorMessage;
                    //model_response.SysInfo = GetSysInfo();

                    //return model_response;
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

        [Obsolete("This method will be deprecated", false)]
        public bool RemoveMeta(Model model, List<String> KeyList)
        {
            return RemoveMeta(model.Id, KeyList);
        }
        [Obsolete("This method will be deprecated", false)]
        public bool RemoveMeta(int ModelId, List<String> KeyList)
        {
            if (ModelId == 0)
                throw new Exception("Model ID Required");

            if (KeyList.Count == 0)
                throw new Exception("Hash key to be removed required");
            string serialized = JsonConvert.SerializeObject(KeyList);

            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}/meta", ApiPath, Convert.ToString(ModelId)), Method.DELETE);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("keys", serialized);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
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

        [Obsolete("This method will be deprecated", false)]
        public string GetDownloadURL(int ModelId)
        {
            return String.Format("{0}/{1}/folders/{2}/download?api_token={3}", Hostname, ApiPath, Convert.ToString(ModelId), System.Uri.EscapeUriString(ApiToken));
        }

        [Obsolete("This method will be deprecated", false)]
        public Model Delete(int ModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/{1}", ApiPath, Convert.ToString(ModelId)), Method.DELETE);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return new Model(httpResponse.StatusCode);
                }
                else
                {
                    return new Model(httpResponse.StatusCode, httpResponse.ErrorMessage);
                }
            }
            catch(Exception ee)
            {
                return new Model(System.Net.HttpStatusCode.InternalServerError, ee.ToString());
            }
        }

        //public List<Model> Find(string key, string value)
        //{
        //    return search_by_meta(key, value);
        //}
        //public List<Model> Find(string query)
        //{
        //    if (is_meta_search(query))
        //    {
        //        Meta Meta = get_meta(query);
        //        return search_by_meta(Meta.Key, Meta.Value);
        //    }
        //    else
        //    {
        //        return search_by_query(query);
        //    }
        //}

        [Obsolete("This method will be deprecated", false)]
        public HttpStatusCode Copy(int[] ModelIds, int TargetModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/copy", ApiPath), Method.PUT);
            request.AddParameter("ids", TDSPRINT.Cloud.SDK.TSUtil.ConvertToIds(ModelIds));
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

        [Obsolete("This method will be deprecated", false)]
        public HttpStatusCode Copy(int ModelId, int TargetModelId)
        {
            int[] ModelIds = {ModelId};
            return Copy(ModelIds, TargetModelId);
        }

        [Obsolete("This method will be deprecated", false)]
        public HttpStatusCode Move(int[] ModelIds, int TargetModelId)
        {
            RestRequest request = new RestRequest(String.Format("{0}/folders/move", ApiPath), Method.PUT);
            request.AddParameter("ids", TSUtil.ConvertToIds(ModelIds));
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
        [Obsolete("This method will be deprecated", false)]
        public HttpStatusCode Move(int ModelId, int TargetModelId)
        {
            int[] ModelIds = { ModelId };
            return Move(ModelIds, TargetModelId);
        }
        #endregion

        #region private method
        private Hash GetSysInfo()
        {
            Hash SysInfo = new Hash();
            SysInfo["ApiToken"] = CurrentUser.ApiToken;
            SysInfo["ApiHost"] = this.Hostname;
            SysInfo["ApiPath"] = this.ApiPath;
            return SysInfo;
        }
        //private string parse_model_ids(int[] model_ids)
        //{
        //    int length = model_ids.Length;
        //    string strResult = null;

        //    if (length == 1)
        //        return Convert.ToString(model_ids[0]);

        //    try
        //    {
        //        StringBuilder buildString = new StringBuilder();

        //        for (int i = 0; i < model_ids.Length; i++)
        //        {
        //            buildString = buildString.Append(Convert.ToString(model_ids[i]));
        //            if (i != length)
        //                buildString = buildString.Append(',');
        //        }

        //        strResult = buildString.ToString();
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return strResult;
        //}
        //private string get_acl()
        //{
        //    Acl AclObject = new Acl(Int32.Parse(CurrentUser.Id.ToString()));
        //    return JsonConvert.SerializeObject(AclObject, Formatting.None);
        //}

        //private bool is_meta_search(string query)
        //{
        //    if (String.IsNullOrEmpty(query))
        //        return false;

        //    try
        //    {
        //        if (query.Contains("="))
        //            return true;
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return false;
        //}

        //private Meta get_meta(string query)
        //{
        //    if (!is_meta_search(query))
        //        return new Meta();

        //    try
        //    {
        //        string[] Split = query.Split('=');
        //        string key = Split[0].Trim();
        //        string value = Split[1].Trim();
        //        if (value[0] == '"' || value[0] == '\'')
        //            value = value.Remove(0, 1);
        //        if (value[value.Length - 1] == '"' || value[value.Length - 1] == '\'')
        //            value = value.Remove(value.Length - 1, 1);

        //        return new Meta(key, value);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

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

                    filtered_model_list.ForEach(x => x.SysInfo = GetSysInfo());
                    return filtered_model_list;
                }
                else
                {
                    models.Contents.ForEach(x => x.SysInfo = GetSysInfo());
                    return models.Contents;
                }
            }
            catch
            {
                return new List<Model>();
            }
        }

        //private List<Model> search_by_query(string query, int Page)
        //{
        //    RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.GET);
        //    request.AddParameter("q", query);
        //    request.AddParameter("api_token", ApiToken);
        //    if (Page != 0)
        //        request.AddParameter("page", Page);

        //    try
        //    {
        //        IRestResponse httpResponse = RestClient.Execute(request);
        //        Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content, TSCloud.serializer_settings());

        //        int num_pages = models.Pagination.NumPages;
        //        if (Page == 0)
        //        {
        //            List<Model> model_list = new List<Model>();

        //            for (int i = 1; i <= num_pages; i++)
        //            {
        //                model_list.AddRange(search_by_query(query, i));
        //            }

        //            return model_list;
        //        }
        //        return models.Contents;

        //    }
        //    catch
        //    {
        //        return new List<Model>();
        //    }
        //}
        //private List<Model> search_by_query(string query)
        //{
        //    return search_by_query(query, 0);
        //}

        //private List<Model> search_by_meta(string key, string value, int Page)
        //{
        //    RestRequest request = new RestRequest(String.Format("{0}/folders", ApiPath), Method.GET);
        //    request.AddParameter("meta", String.Format("{0}:{1}", key, value));
        //    request.AddParameter("api_token", ApiToken);
        //    if (Page != 0)
        //        request.AddParameter("page", Page);

        //    try
        //    {
        //        IRestResponse httpResponse = RestClient.Execute(request);
        //        Models models = JsonConvert.DeserializeObject<Models>(httpResponse.Content, TSCloud.serializer_settings());

        //        int num_pages = models.Pagination.NumPages;
        //        if (Page == 0)
        //        {
        //            List<Model> model_list = new List<Model>();

        //            for (int i = 1; i <= num_pages; i++)
        //            {
        //                model_list.AddRange(search_by_meta(key, value, i));
        //            }

        //            return model_list;
        //        }
        //        return models.Contents;
        //    }
        //    catch
        //    {
        //        return new List<Model>();
        //    }
        //}
        //private List<Model> search_by_meta(string key, string value)
        //{
        //    return search_by_meta(key, value, 0);
        //}

        #endregion
    }
}
