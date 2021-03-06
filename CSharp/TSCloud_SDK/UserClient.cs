﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using TDSPRINT.Cloud.SDK.Datas;
using TDSPRINT.Cloud.SDK.Types;

namespace TDSPRINT.Cloud.SDK
{
    public class UserClient : ClientBase
    {
        #region Constructor
        public UserClient(TSCloud TSCloud)
            : base(TSCloud)
        {
        }
        #endregion

        #region Method
        //public Users All()
        //{
        //    RestRequest request = new RestRequest(String.Format("{0}/users", ApiPath), Method.GET);
        //    request.AddParameter("api_token", ApiToken);

        //    try
        //    {
        //        IRestResponse httpResponse = RestClient.Execute(request);
        //        List<User> user_list = JsonConvert.DeserializeObject<List<User>>(httpResponse.Content, TSCloud.serializer_settings());
        //        user_list.ForEach(x => x.SysInfo = GetSysInfo());

        //        return new Users(user_list);
        //    }
        //    catch
        //    {
        //        return new Users();
        //    }
        //}
        public User Create(User user)
        {
            if(String.IsNullOrEmpty(user.Email))
                throw new Exception("Email required");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/users", ApiPath), Method.POST);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("email", user.Email);
            request.AddParameter("password", user.Password);
            request.AddParameter("name", user.Name);
            request.AddParameter("phone", user.Phone);
            request.AddParameter("address", user.Address);
            request.AddParameter("company", user.Company);
            request.AddParameter("role", user.Role);
            request.AddParameter("meta", user.Meta.Stringify());

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                User created_user = JsonConvert.DeserializeObject<User>(httpResponse.Content, TSCloud.serializer_settings());
                created_user.SysInfo = GetSysInfo();

                return created_user;
            }
            catch(Exception ee)
            {
                return new User(Convert.ToString(ee));
            }
        }

        public User Register(string Email, string Name, string TeamName, string Password, string Phone, string Address, string Company)
        {
            RestRequest request = new RestRequest(String.Format("{0}/users/register", ApiPath), Method.POST);

            if (!String.IsNullOrEmpty(Name))
                request.AddParameter("name", Name);
            if (!String.IsNullOrEmpty(Email))
                request.AddParameter("email", Email);
            if (!String.IsNullOrEmpty(TeamName))
                request.AddParameter("team_name", TeamName);
            if (!String.IsNullOrEmpty(Password))
                request.AddParameter("password", Password);
            if (!String.IsNullOrEmpty(Phone))
                request.AddParameter("phone", Phone);
            if (!String.IsNullOrEmpty(Address))
                request.AddParameter("address", Address);
            if (!String.IsNullOrEmpty(Company))
                request.AddParameter("company", Company);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    User user_response = JsonConvert.DeserializeObject<User>(httpResponse.Content, TSCloud.serializer_settings());
                    user_response.StatusCode = httpResponse.StatusCode;
                    user_response.SysInfo = GetSysInfo();

                    return user_response;
                }
                else
                {
                    User user_response = new User();
                    user_response.StatusCode = httpResponse.StatusCode;
                    user_response.Message = httpResponse.ErrorMessage;

                    return user_response;
                }
            }
            catch (Exception ee)
            {
                return new User(Convert.ToString(ee));
            }
        }

        public User Update(User user)
        {
            if (user.Id == 0)
                throw new Exception("User ID Required");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/users/{1}", ApiPath, Convert.ToString(user.Id)), Method.PUT);
            request.AddParameter("api_token", ApiToken);

            if (user.Name != null)
                request.AddParameter("name", user.Name);
            if (user.Password != null)
                request.AddParameter("password", user.Password);
            if (user.Role != null)
                request.AddParameter("role", user.Role);
            if (user.Phone != null)
                request.AddParameter("phone", user.Phone);
            if (user.Address != null)
                request.AddParameter("address", user.Address);
            if (user.Company != null)
                request.AddParameter("company", user.Company);
            if (user.Meta != null)
                request.AddParameter("meta", user.Meta.Stringify());

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    User user_response = JsonConvert.DeserializeObject<User>(httpResponse.Content, TSCloud.serializer_settings());
                    user_response.StatusCode = httpResponse.StatusCode;
                    user_response.SysInfo = GetSysInfo();

                    return user_response;
                }
                else
                {
                    User user_response = new User();
                    user_response.StatusCode = httpResponse.StatusCode;
                    user_response.Message = httpResponse.ErrorMessage;

                    return user_response;
                }
            }
            catch (Exception ee)
            {
                return new User(ee.ToString());
            }
        }

        public List<User> FindAllByEmail(params String[] emails)
        {
            if (emails.Length < 1)
                throw new Exception("emails Required");
            if (!CheckExpiration())
                throw new Exception("token refresh fails");

            RestRequest request = new RestRequest(String.Format("{0}/users", ApiPath), Method.GET);
            request.AddParameter("api_token", ApiToken);
            request.AddParameter("emails", TSUtil.ConvertToStrings(emails));

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if(httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Users user_list = JsonConvert.DeserializeObject<Users>(httpResponse.Content, TSCloud.serializer_settings());
                    user_list.All.ForEach(x => x.SysInfo = GetSysInfo());
                    return user_list.All;
                }
                else
                {
                    return new List<User>();
                }
            }
            catch(Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }

        public User Get(int UserID)
        {
            if (!CheckExpiration())
                throw new Exception("token refresh fails");
            RestRequest request = new RestRequest(String.Format("{0}/users/{1}", ApiPath, Convert.ToString(UserID)), Method.GET);
            request.AddParameter("api_token", ApiToken);

            try
            {
                IRestResponse httpResponse = RestClient.Execute(request);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    User user = JsonConvert.DeserializeObject<User>(httpResponse.Content, TSCloud.serializer_settings());
                    user.SysInfo = GetSysInfo();
                    user.StatusCode = httpResponse.StatusCode;

                    return user;
                }
                else
                {
                    User user = new User();
                    user.StatusCode = httpResponse.StatusCode;
                    user.Message = httpResponse.ErrorMessage;

                    return user;
                }
            }
            catch(Exception ee)
            {
                return new User(Convert.ToString(ee));
            }
        }
        #endregion

        #region private method
        #endregion
    }
}
