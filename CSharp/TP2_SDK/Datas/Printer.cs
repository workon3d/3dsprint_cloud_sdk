using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamPlatform.TP2_SDK.Datas
{
    public class Printers
    {
        public object parent;
        public List<Printer> contents;
        public Pagination pagination;
    }

    public class Printer : Model
    {
        #region constructor
        public Printer(HttpStatusCode status_code, string strMessage = null) : base(status_code, strMessage)
        {
        }
        public Printer(string strMessage) : base(strMessage)
        {
        }
        public Printer() : base()
        {
        }
        #endregion
    }
}
