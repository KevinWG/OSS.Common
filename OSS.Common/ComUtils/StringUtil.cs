#region Copyright (C) 2017 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：字符串通用功能
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using System.Text;

namespace OSS.Common.ComUtils
{
    /// <summary>
    /// 字符串通用功能
    /// </summary>
    public static class StringUtil
    {

        private static readonly Random _rnd = new Random();
        private static readonly char[] _arrChar = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'd', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 'q', 's', 't', 'u', 'v',
            'w', 'z', 'y', 'x',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Q', 'P', 'R', 'T', 'S', 'V', 'U',
            'W', 'X', 'Y', 'Z'
        };

        /// <summary>
        /// 生成随机串
        /// </summary>
        /// <returns></returns>
        public static string RandomStr(int length=8)
        {
            var num = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                num.Append(_arrChar[_rnd.Next(0, 59)].ToString());
            }
            return num.ToString();
        }

        /// <summary>
        /// 随机数字
        /// </summary>
        /// <returns></returns>
        public static string RandomNum(int length=4)
        {
            var num = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                num.Append(_rnd.Next(0, 9));
            }
            return num.ToString();
        }
    }


}
