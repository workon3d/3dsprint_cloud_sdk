using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSPRINTCloudDB
{
    class Env : JsonBase
    {
        public Env(bool read = false, bool write = false)
            : base(read, write)
        {
        }

        protected override string GetFileName()
        {
            return "env.dat";
        }
    }
}
