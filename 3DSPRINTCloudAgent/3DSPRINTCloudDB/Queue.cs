using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json.Linq;

namespace _3DSPRINTCloudDB
{
    class Queue : JsonBase
    {
        private string _printer_id;
        public Queue(string printer_id)
            : base(false, false)
        {
            _printer_id = printer_id;
        }
        
        public Queue(string printer_id, bool read = false, bool write = false)
            : this(printer_id)
        {
            _read = read;
            _write = write;
            Load();
            if (json["queue"] == null)
            {
                _json["queue"] = new JArray();
            }
        }

        protected override string GetFileName()
        {
            if (string.IsNullOrEmpty(_printer_id))
                return null;
            string sanitizedString = Regex.Replace(_printer_id, "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", "_");
            return string.Format("queue_{0}.dat", sanitizedString);
        }

        public JArray List
        {
            get
            {
                return (JArray)json["queue"];
            }

            set
            {
                _json["queue"] = value;
            }
        }
    }
}

