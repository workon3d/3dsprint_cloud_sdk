using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamPlatform.TP2_SDK.Object
{
    public class User
    {
        public object id;
        public string email;
        public string api_token;
        public string message;
        public HttpStatusCode status_code;

        public User(string strMessage)
        {
            message = strMessage;
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