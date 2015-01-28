using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDSPRINT.Cloud.SDK.Types
{
    public enum Ftype { 
        All,
        File,
        Folder,
        vPrint,
    }

    public enum GetModelsOption
    {
        OnlyChildren = 0,
        AllDescendants = 1,
    }
}
