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
        public Group GetGroups()
        {
            throw new NotImplementedException();
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

        public bool Delete(int GroupID)
        {
            throw new NotImplementedException();
        }

        public Group Update(Group group)
        {
            throw new NotImplementedException();
        }

        public Group Get(int GroupID)
        {
            throw new NotImplementedException();
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
