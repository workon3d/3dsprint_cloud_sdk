using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamPlatform.TP2_SDK.TpResponse
{
    public class AuthenticateResponse
    {
        public Current_user current_user;
    }

    public class Current_user
    {
        public string email;
        public string api_token;
    }
}
