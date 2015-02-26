using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TDSPRINT.Cloud.SDK.Types
{
    public enum Ftype {
        Unknown = 0,
        All = 1,
        File = 2,
        Folder = 3,
        Page = 4,
        vPrint = 5,
    }

    public enum GetModelsOption
    {
        OnlyChildren = 0,
        AllDescendants = 1,
    }

    public class FtypeEnumConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Ftype ftype = (Ftype) value;
            switch (ftype)
            {
                case Ftype.File:
                    writer.WriteValue("file");
                    break;
                case Ftype.Folder:
                    writer.WriteValue("folder");
                    break;
                case Ftype.vPrint:
                    writer.WriteValue("vprint");
                    break;
                case Ftype.Page:
                    writer.WriteValue("page");
                    break;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string) reader.Value;
            Ftype? ftype = null;

            switch (enumString)
            {
                case "file":
                    ftype = Ftype.File;
                    break;
                case "folder":
                    ftype = Ftype.Folder;
                    break;
                case "vprint":
                    ftype = Ftype.vPrint;
                    break;
                case "page":
                    ftype = Ftype.Page;
                    break;
            }

            return ftype;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
