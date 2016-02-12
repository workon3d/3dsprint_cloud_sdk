using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDSPRINT.Cloud.SDK;
using TDSPRINT.Cloud.SDK.Datas;

namespace TSCloud_SDK_NET40_Test
{
    public class TestBase
    {
        protected TSCloud _TSCloud = null;
        protected ModelClient _ModelClient = null;
        protected GroupClient _GroupClient = null;
        protected UserClient _UserClient = null;
        protected Model _Model = null;
        protected User _CurrentUser = null;
        protected int _TargetModelId = 14940;

        public virtual void Initialize()
        {
            _TSCloud = new TSCloud("http://tp2staging.herokuapp.com");
            SetCurrentUser();
        }

        private void SetCurrentUser()
        {
            User current_user = new User();
            current_user.ApiToken = "inska";
            current_user.Id = 22;
            current_user.Name = "Inseok Lee";
            current_user.Email = "inseok.lee@3dsystems.com";
            current_user.Role = "admin";
            current_user.Company = "test";

            current_user.SysInfo = _TSCloud;

            _TSCloud.CurrentUser = current_user;
            _TSCloud.ApiToken = "inska";
        }
    }
}
