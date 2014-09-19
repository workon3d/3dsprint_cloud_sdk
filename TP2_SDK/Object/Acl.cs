﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamPlatform.TP2_SDK.Object
{
    public class Acl
    {
        public int owner;

        public Acl()
        {
        }

        public Acl(int iOwner) : this()
        {
            owner = iOwner;
        }

        static public bool IsValid(Acl acl)
        {
            try
            {
                if (acl.owner == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }
    }
}
