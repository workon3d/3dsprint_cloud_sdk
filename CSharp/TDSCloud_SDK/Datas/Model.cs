using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TDSPRINT.Cloud.SDK.Types;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class Models
    {
        public object parent;
        public List<Model> contents;
        public Pagination pagination;
    }

    public class Model
    {
        #region member variable
        public int id;
        public string name;
        public string key;
        public object meta;
        public object ancestry;
        public Ftype ftype;
        public object acl;
        public string created_at;
        public string updated_at;
        public string Message;
        public HttpStatusCode StatusCode;
        #endregion

        #region constructor
        public Model(HttpStatusCode status_code, string strMessage = null)
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public Model(string strMessage)
        {
            Message = strMessage;
        }
        public Model()
        {
        }
        #endregion

        #region static method
        static public bool IsValid(Model model)
        {
            try
            {
                if (model.id == 0 || String.IsNullOrEmpty(model.key) || String.IsNullOrEmpty(model.acl.ToString()))
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValid()
        {
            return Model.IsValid(this);
        }
        #endregion
    }

    public class Meta
    {
        public string Key;
        public string Value;

        public Meta()
        {
        }

        public Meta(string strKey, string strValue)
        {
            Key = strKey;
            Value = strValue;
        }
    }
    
    public class Pagination
    {
        public int total;
        public object per_page;
        public object num_pages;
        public object current_page;
        public object prev_page;
        public object next_page;
    }
}
