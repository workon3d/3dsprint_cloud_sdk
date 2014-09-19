using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TeamPlatform.TP2_SDK.Types;

namespace TeamPlatform.TP2_SDK.Object
{
    public class Models
    {
        public object parent;
        public List<Model> contents;
        public Pagination pagination;
    }

    public class Model
    {
        public int id;
        public string name;
        public string key;
        public object meta;
        public object ancestry;
        public Ftype ftype;
        public object acl;
        public string created_at;
        public string updated_at;
        public string message;

        public Model(string strMessage)
        {
            message = strMessage;
        }

        public Model()
        {
            // TODO: Complete member initialization
        }

        static public bool IsValid(Model model)
        {
            try
            {
                if (model.id == 0 || String.IsNullOrEmpty(model.key) || String.IsNullOrEmpty(model.acl.ToString()))
                    return false;
                else
                    return true;
            }
            catch(Exception ee)
            {
                return false;
            }
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
