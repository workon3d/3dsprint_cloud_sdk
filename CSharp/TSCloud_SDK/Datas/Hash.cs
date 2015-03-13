using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Collections;

namespace TDSPRINT.Cloud.SDK.Datas
{
    public class Hash : Hashtable
    {
        public static Hash Parse(object objData)
        {
            string strData = null;
            try
            {
                strData = objData.ToString();
            }
            catch (Exception ee)
            {
                throw ee;
            }

            return Hash.Parse(strData);
        }
        public static Hash Parse(string strData)
        {
            try
            {
                Hash hash = JsonConvert.DeserializeObject<Hash>(strData, TSCloud.serializer_settings());
                return hash;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public string Stringify()
        {
            try
            {
                return JsonConvert.SerializeObject(this, TSCloud.serializer_settings());
            }
            catch
            {
                throw;
            }
        }
    }
}
