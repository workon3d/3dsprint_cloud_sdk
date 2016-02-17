using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace _3DSPRINTCloudDB
{
    class Printer : JsonBase
    {
        public Printer(bool read = false, bool write = false)
            : base(read, write)
        {
            if (json["printers"] == null)
            {
                _json["printers"] = new JArray();
            }
        }

        protected override string GetFileName()
        {
            return "printer.dat";
        }

        public JArray List
        {
            get
            {
                return (JArray)json["printers"];
            }
        }
    }
}
