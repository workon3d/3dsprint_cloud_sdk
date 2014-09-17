using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamPlatform.TP2_SDK.TpResponse
{
    public class FileResponse
    {
        public int id;
        public string key;
        public string ftype;
        public string filename;
        public int filesize;
        public string kind;
        public string ancestry;
        public int version;
        public string state;
        public string small;
        public string big;
        public int comments_count;
        public int download_count;
        //public FileProperties properties;
        public List<Label> labels;
        public User owner;
        public string created_at;
        public string updated_at;
        public string deleted_at;
    }

    public class User
    {
        public int id;
        public string name;
        public string email;
        public string picture;
    }

    public class Label
    {
        public string name;
        public string style;
    }

    public class FileProperties
    {
        public string UnitMultToMM;
        public string Surafce;
        public string Volume;
        public string MinX;
    }
}
