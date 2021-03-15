using OSS.Common.Extention;
using System;

namespace OSS.Common.Helpers
{
    public class TreeNumHelper
    {
        private const string _paddingNumStr = "00000000000000000000000";
        private const int _numLength = 16; // 2 ^ 53 16字符长度

        /// <summary>
        ///  生成编号
        /// </summary>
        /// <param name="parentNum"></param>
        /// <param name="maxPreNum"></param>
        /// <returns></returns>
        public static long GenerateNum(long parentNum, long maxPreNum)
        {
            string newNumStr;

            if (maxPreNum > 0)
            {
                var realNumStr = maxPreNum.ToString().TrimEnd('0');
                if (parentNum == 0 && realNumStr == "8")
                {
                    // 最高位特殊情况处理
                    newNumStr = "11"; // 排除 9,10   (浏览器最大不能超过2^53，最高位排除9，同时不能以0结尾，排除10）
                }
                else if (realNumStr.EndsWith("9"))
                {
                    // 涉及进位处理
                    var lastNodeIndex = realNumStr.LastIndexOf('0') + 1;
                    var lastNodeNum = realNumStr.Substring(lastNodeIndex).ToInt64() + 2;

                    // 999+2=1001 特殊情况处理
                    var lastNodeNumStr = (lastNodeNum % 100 == 1)
                        ? lastNodeNum.ToString().Replace('0', '1')
                        : lastNodeNum.ToString();

                    newNumStr = string.Concat(realNumStr.Substring(0, lastNodeIndex), lastNodeNumStr);
                }
                else
                {
                    newNumStr = (realNumStr.ToInt64() + 1).ToString();
                }
            }
            else if (maxPreNum <= 0 && parentNum > 0)
            {
                var parentRealNumStr = parentNum.ToString().TrimEnd('0');
                if (parentRealNumStr.Length + 2 > _numLength)
                {
                    throw new ArgumentOutOfRangeException("编号超出可设置的最大子集");
                }

                newNumStr = string.Concat(parentRealNumStr, "01");
            }
            else
            {
                newNumStr = "1";
            }

            if (newNumStr.Length > _numLength)
                throw new ArgumentOutOfRangeException("编号超出可设置的最大子集");

            if (newNumStr.Length < _numLength)
            {
                return string.Concat(newNumStr,
                        _paddingNumStr.Substring(0, _numLength - newNumStr.Length)
                        )
                    .ToInt64();
            }
            return newNumStr.ToInt64();

        }



        //public static long SubNumRange(long parentNum)
        //{
        //    var realnum
        //}


    }
}
