#region Copyright (C) 2021 (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局辅助类 - 树形编码生成辅助
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion
using OSS.Common.Extension;
using System;

namespace OSS.Common.Helpers
{
    /// <summary>
    ///  树形编码生成辅助类
    /// </summary>
    public class TreeNumHelper
    {
        private const string _paddingNumStr = "0000000000000000";
        private const int    _numLength     = 16; // 2 ^ 53 16字符长度

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
                    var lastNodeNum   = realNumStr.Substring(lastNodeIndex).ToInt64() + 2;

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
                return string.Concat(newNumStr
                        , _paddingNumStr.Substring(0, _numLength - newNumStr.Length))
                    .ToInt64();
            }

            return newNumStr.ToInt64();

        }

        private const string _minSubPaddingNumStr = "1111111111111111";
        private const string _maxSubPaddingNumStr = "9999999999999999";

        /// <summary>
        ///  获取子节点编号区域范围
        /// </summary>
        /// <param name="parentNum"></param>
        /// <returns></returns>
        public static (long minSubNum, long maxSubNum) FormatSubNumRange(long parentNum)
        {
            var realParentNum = parentNum.ToString().TrimEnd('0');

            var paddingLength = _numLength - realParentNum.Length - 1;
            if (paddingLength <= 0)
                return (parentNum, parentNum);

            var minNum = string.Concat(realParentNum, "0", _minSubPaddingNumStr.Substring(0, paddingLength)).ToInt64();
            var maxNum = string.Concat(realParentNum, "0", _maxSubPaddingNumStr.Substring(0, paddingLength)).ToInt64();

            return (minNum, maxNum);
        }


        /// <summary>
        ///  获取子节点编号区域范围
        /// </summary>
        /// <param name="treeNum"></param>
        /// <returns></returns>
        public static long[] FormatParents(long treeNum)
        {
            var treeNumStr = treeNum.ToString();
            var realParentNum = treeNumStr.TrimEnd('0');

            var realParentNumSplits = realParentNum.Split(new []{ '0' },StringSplitOptions.RemoveEmptyEntries);
            if (realParentNumSplits.Length<=1)
            {
                return new []{ 0L };
            }

            var tempStr = string.Empty;
            var strRes=new long[realParentNumSplits.Length-1];
            
            for (var i = 0; i < realParentNumSplits.Length-1; i++)
            {
                tempStr =string.Concat(tempStr,"0", realParentNumSplits[i]);
                strRes[i] = string.Concat(tempStr, _paddingNumStr.Substring(0,_numLength - tempStr.Length)).ToInt64();
            }

            return strRes;
        }



    }
}
