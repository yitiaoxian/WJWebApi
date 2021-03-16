using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WJWebApi
{
    /// <summary>
    /// JSON 格式化字符串与对象之间相互转换的工具类
    /// </summary>
    public static class JsonUtil
    {
        /// <summary>
        /// 将指定的对象序列化成json数据
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            try
            {
                return null == obj ? null : JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return null;
            }
        }

        public static T ToObject<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { NullValueHandling=NullValueHandling.Ignore});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
