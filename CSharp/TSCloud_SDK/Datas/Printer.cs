using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class Printers : CommonList
    {
        #region constructor
        public Printers()
        {
        }

        public Printers(string strMessage)
            : this()
        {
            Message = strMessage;
        }
        #endregion
    }

    public class Printer : CommonItem
    {
    }
}
