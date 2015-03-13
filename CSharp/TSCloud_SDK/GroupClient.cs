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

namespace TDSPRINT.Cloud.SDK
{
    public class GroupClient : TSCloud
    {
        #region member variables
        private Hash Configuration;
        #endregion

        #region constructor
        public GroupClient()
        {
            this.Configuration = null;
        }
        public GroupClient(TSCloud TSCloud) : this()
        {
            Hostname = TSCloud.Hostname;
            RestClient = new RestClient(Hostname);
            ApiToken = TSCloud.ApiToken;
            CurrentUser = TSCloud.CurrentUser;

            // Default Configuration
            Configuration = new Hash();
            Configuration["PerPage"] = 30;
        }
        public GroupClient(TSCloud TSCloud, Hash Configuration)
            : this(TSCloud)
        {
            this.Configuration = Configuration;
        }
        #endregion

        #region
        public Groups GetGroups(int Page)
        {
            RestRequest request = new RestRequest(String.Format("{0}/groups", ApiPath), Method.GET);
            request.AddParameter("api_token", ApiToken);
            if (Page != 0)
                request.AddParameter("page", Page);

            Groups groups = new Groups();

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    groups = JsonConvert.DeserializeObject<Groups>(httpResponse.Content, TSCloud.serializer_settings());
                    groups.StatusCode = httpResponse.StatusCode;

                    return groups;
                }
                else
                {
                    groups.StatusCode = httpResponse.StatusCode;
                    groups.Message = httpResponse.ErrorMessage;

                    return groups;
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public Group Create(Group group)
        {
            RestRequest request = new RestRequest(String.Format("{0}/groups", ApiPath), Method.POST);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("name", group.Name);
            request.AddParameter("description", group.Description);
            request.AddParameter("acl", group.Acl.Stringify());
            request.AddParameter("user_ids", group.GetUserIds());

            Group created_group = new Group();

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    created_group = JsonConvert.DeserializeObject<Group>(httpResponse.Content, TSCloud.serializer_settings());
                    created_group.StatusCode = httpResponse.StatusCode;

                    return created_group;
                }
                else
                {
                    created_group.StatusCode = httpResponse.StatusCode;
                    created_group.Message = httpResponse.ErrorMessage;

                    return created_group;
                }
            }
            catch (Exception ee)
            {
                return new Group(Convert.ToString(ee));
            }
        }

        public HttpStatusCode Delete(int GroupID)
        {
            RestRequest request = new RestRequest(String.Format("{0}/groups/{1}", ApiPath, Convert.ToString(GroupID)), Method.DELETE);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                return httpResponse.StatusCode;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public Group Update(Group group)
        {
            RestRequest request = new RestRequest(String.Format("{0}/groups/{1}", ApiPath, Convert.ToString(group.Id)), Method.PUT);
            request.AddParameter("api_token", ApiToken);

            if (!String.IsNullOrEmpty(group.Name))
                request.AddParameter("name", group.Name);
            if (!String.IsNullOrEmpty(group.Description))
                request.AddParameter("description", group.Description);
            if (group.Acl != null)
                request.AddParameter("acl", group.Acl.Stringify());
            if (group.Users.Count > 0)
                request.AddParameter("user_ids", group.GetUserIds());

            Group updated_group = new Group();

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    updated_group = JsonConvert.DeserializeObject<Group>(httpResponse.Content, TSCloud.serializer_settings());
                    updated_group.StatusCode = httpResponse.StatusCode;

                    return updated_group;
                }
                else
                {
                    updated_group.StatusCode = httpResponse.StatusCode;
                    updated_group.Message = httpResponse.ErrorMessage;

                    return updated_group;
                }
            }
            catch (Exception ee)
            {
                return new Group(Convert.ToString(ee));
            }
        }

        public Group Get(int GroupID)
        {
            RestRequest request = new RestRequest(String.Format("{0}/groups/{1}", ApiPath, Convert.ToString(GroupID)), Method.GET);
            request.AddParameter("api_token", ApiToken);

            Group group = new Group();
            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    group = JsonConvert.DeserializeObject<Group>(httpResponse.Content, TSCloud.serializer_settings());
                    group.StatusCode = httpResponse.StatusCode;

                    return group;
                }
                else
                {
                    group.StatusCode = httpResponse.StatusCode;
                    group.Message = httpResponse.ErrorMessage;

                    return group;
                }
            }
            catch (Exception ee)
            {
                return new Group(Convert.ToString(ee));
            }
        }

        public Group Copy(Group group)
        {
            throw new NotImplementedException();
        }

        public Group Move(Group group)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
