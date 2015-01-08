using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDSPRINT.Cloud.SDK.Types
{
    public enum QueueStatus
    {
        Saved = 0,
        InProgress = 1,
        Printed = 2,
        Paused = 3,
        Canceled = 4,
        MaterialWarning = 5,
        Failed = 6
    }
}
