using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace IRS.Common.Code
{
    public class ConnectionStringsCore
    {
        /// <summary>
        /// 获取连接节点值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionValue(string key)
        {
            if(AppConfigurataionServices.Configuration.GetConnectionString(key) != null)
            {
                return EncodeAndDecode.DecodeBase64(AppConfigurataionServices.Configuration.GetConnectionString(key));
                //return ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            return string.Empty;
        }

        public static void UpdateConnectionStringsConfig(string key,string conString)
        {
            if(AppConfigurataionServices.Configuration.GetConnectionString(key) != null)
            {
                //新建一个连接字符串实例
                conString = EncodeAndDecode.EncodeBase64(conString);
                AppConfigurataionServices.Configuration[key] = conString;
            }
        }

        public static string GetArgsValue(string key)
        {
            if(AppConfigurataionServices.Configuration[key] != null)
            {
                return AppConfigurataionServices.Configuration[key];
            }
            return string.Empty;
        }

        public static void UpdateArgsValue(string key ,string value)
        {
            if (AppConfigurataionServices.Configuration.GetConnectionString(key) != null)
            {
                AppConfigurataionServices.Configuration[key] = value;
            }
        }
    }
}
