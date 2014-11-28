using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class User
    {
        public object id;
        public string email;
        public string api_token;
        public string Message;
        public HttpStatusCode StatusCode;

        public User(HttpStatusCode status_code, string strMessage)
        {
            StatusCode = status_code;
            Message = strMessage;
        }
        public User(string strMessage)
        {
            Message = strMessage;
        }
        public User()
        {
        }

        public int GetId()
        {
            return Convert.ToInt32(id);
        }

        static public bool IsValid(User user)
        {
            if (user.id == null || String.IsNullOrEmpty(user.email) || String.IsNullOrEmpty(user.api_token))
                return false;
            else
                return true;
        }
    }
}