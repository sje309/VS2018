/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Redis
*文件名称：Config
*创建人：  shuyizhi
*创建时间：2018/4/13 13:51:01
*文件描述: 提供配置文件读取辅助功能
************************************************************************/

using System.Collections.Generic;
using System.Configuration;

namespace Redis
{
    public class Config
    {
        /// <summary>
        /// 获取配置字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigValue( string key )
        {
            return ConfigurationManager.AppSettings[key] + string.Empty;
        }

        /// <summary>
        /// 获取配置整数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt( string key )
        {
            return int.Parse(GetConfigValue(key));
        }

        /// <summary>
        /// 获取配置开关
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool( string key )
        {
            string value = GetConfigValue(key).ToLower();
            if (value == "1" || value == "true" || value == "y")
                return true;
            return false;
        }

        /// <summary>
        /// 获取配置字符串数组
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> GetConfigList( string key, char split )
        {
            string str = GetConfigValue(key);
            string[] ary = str.Split(split);
            return new List<string>(ary);
        }
    }
}