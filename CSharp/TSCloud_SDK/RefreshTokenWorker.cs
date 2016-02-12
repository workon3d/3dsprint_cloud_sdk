using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TDSPRINT.Cloud.SDK
{
    class RefreshTokenWorker
    {
        private TSCloud m_TSCloud;
        private DateTime m_expiration;

        public RefreshTokenWorker(TSCloud client)
        {
            m_TSCloud = client;
            m_expiration = client.ExpirationDateTime;
        }

        public void Start()
        {
            while (m_expiration > DateTime.Now)
            {
                 Thread.Sleep(TimeSpan.FromDays(1));
            }
            m_TSCloud.CheckExpiration();
        }
    }
}
