using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDSPRINT.Cloud.SDK.Datas;

namespace TDSPRINT.Cloud.SDK
{
    public class TSUtil
    {
        #region Converter
        public static string ConvertToIds(List<int> ids)
        {
            return String.Join(",", ids.Select(x => Convert.ToString(x)).ToArray());
        }
        public static string ConvertToIds(List<CommonItem> items)
        {
            return String.Join(",", items.Select(x => Convert.ToString(x.Id)).ToArray());
        }
        public static string ConvertToIds(int[] ids)
        {
            return String.Join(",", ids.Select(x => Convert.ToString(x)).ToArray());
        }
        public static string ConvertToStrings(String[] strings)
        {
            return String.Join(",", strings.Select(x => Convert.ToString(x)).ToArray());
        }
        #endregion
    }
}
