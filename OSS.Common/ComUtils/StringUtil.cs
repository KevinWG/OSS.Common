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

        private static readonly Random _rnd = new Random(DateTime.Now.Millisecond);

        private static readonly char[] _arrChar =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'd', 'c', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'p', 'r', 'q', 's', 't', 'u',
            'v', 'w', 'z', 'y', 'x',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'Q', 'P', 'R', 'T', 'S', 'V',
            'U', 'W', 'X', 'Y', 'Z'
        };

        /// <summary>
        /// 生成随机串
        /// </summary>
        /// <returns></returns>
        public static string RandomStr(int length = 8)
        {
            var num = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                num.Append(_arrChar[_rnd.Next(0, 59)]);
            }
            return num.ToString();
        }


        // 排除【0，O】I 4 这类
        private const string _arrCodeStr = "12356789ABCDEFGHJKLMNPQRSTUVWXYZ";

        /// <summary>
        /// 数字转化为短码
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToCode(this int num)
        {
            return ToCode(num, _arrCodeStr);
        }

        /// <summary>
        /// 数字转化为短码
        /// </summary>
        /// <param name="num"></param>
        /// <param name="arrStrs">转换时映射字母，32位，不可重复</param>
        /// <returns></returns>
        public static string ToCode(this long num, string arrStrs=null)
        {
            if (string.IsNullOrEmpty(arrStrs))
                arrStrs = _arrCodeStr;
            
            if (arrStrs.Length!=32)
                throw new ArgumentOutOfRangeException(nameof(arrStrs), "映射字符必须是32位，且不能重复！");

            const long codeTemp = 0x1F;
            var code = new StringBuilder(13);

            while (num > 0)
            {
                var index = num & codeTemp;
                code.Append(arrStrs[(int) index]);

                num >>= 5;
            }
            return code.ToString();
        }

        /// <summary>
        /// 根据短码反推数字
        /// </summary>
        /// <param name="code"></param>
        /// <param name="arrStrs">转换时映射字母，32位，不可重复</param>
        /// <returns></returns>
        public static long ToCodeNum(this string code, string arrStrs=null)
        {
            if (string.IsNullOrEmpty(arrStrs))
                arrStrs = _arrCodeStr;

            if ( arrStrs.Length != 32)
                throw new ArgumentOutOfRangeException(nameof(arrStrs), "映射字符必须是32位，且不能重复！");

            if (string.IsNullOrEmpty(code))
                return 0;
            var count = code.Length;
            if (count > 13)
                throw new ArgumentOutOfRangeException("code", "the code is not from [ToCode] method !");

            long value = 0;
            for (var i = count - 1; i >= 0; i--)
            {
                var num = arrStrs.IndexOf(code[i]);
                if (num < 0)
                    throw new ArgumentOutOfRangeException("code", "the code is not from [ToCode] method !");

                value = (value << 5) ^ num;
            }
            return value;
        }
    }


}