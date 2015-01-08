using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using TDSPRINT.Cloud.SDK.Types;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class Printers : CommonList
    {
        private List<Printer> m_contents;
        [JsonProperty("contents")]
        public List<Printer> Contents
        {
            get { return m_contents; }
            set { m_contents = value; }
        }

        #region constructor
        public Printers(HttpStatusCode status_code, string strMessage = null)
            : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
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
        #region constructor
        public Printer(HttpStatusCode status_code, string strMessage = null)
            : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public Printer(string strMessage)
            : this()
        {
            Message = strMessage;
        }
        public Printer()
        {
        }
        #endregion
    }

    public class Queues : CommonList
    {
        private List<Queue> m_contents;
        [JsonProperty("contents")]
        public List<Queue> Contents
        {
            get { return m_contents; }
            set { m_contents = value; }
        }

        #region constructor
        public Queues(HttpStatusCode status_code, string strMessage = null)
            : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public Queues()
        {
        }

        public Queues(string strMessage)
            : this()
        {
            Message = strMessage;
        }
        #endregion
    }
    public class Queue : CommonItem
    {
        #region member variables
        private QueueStatus m_status;
        #endregion

        #region getter/setter
        public TDSPRINT.Cloud.SDK.Types.QueueStatus Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        #endregion

        #region constructor
        public Queue(HttpStatusCode status_code, string strMessage = null)
            : this()
        {
            Message = strMessage;
            StatusCode = status_code;
        }
        public Queue(string strMessage)
            : this()
        {
            Message = strMessage;
        }
        public Queue()
        {
        }
        #endregion
    }
}
