
#region Copyright (C) 2019 Kevin (OSS开源系列) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：树形数字位生成
*
*　　	创建人： KK
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using OSS.Common.Extention;

namespace OSS.Common.ComUtils
{
    /// <summary>
    ///   树形数字位
    /// </summary>
    public static class TreeNumUtil
    {
        //  2位位置索引+ 14位数字位    16位是js接收的最大长度（2的53次方，已排除以9开头的部分最大数）
        private const int _positionMod = 100;
        private const int _numLength = 14;

        private const string _paddingNumStr = "00000000000000000000000";

        public static long GenerateNextNum(long previousNum)
        {
            return GenerateNum(previousNum, TreeNumType.Next);
        }

        public static long GenerateFirstSubNum(long parentId)
        {
            return GenerateNum(parentId, TreeNumType.Sub);
        }

        private static long GenerateNum(long treeNum, TreeNumType generateType)
        {
            var position = GetPositionIndex(treeNum);

            treeNum = treeNum / _positionMod;
            var preNumStr = treeNum.ToString().TrimEnd('0');

            #region 计算节点数值

            long curNodeNum = 0;

            if (treeNum > 0)
            {
                var preNodeNumStr = preNumStr.Substring(position);

                if (generateType == TreeNumType.Sub)
                {
                    position += preNodeNumStr.Length;
                    curNodeNum++;
                }
                else if (generateType == TreeNumType.Next)
                {
                    var preNodeNum = preNodeNumStr.ToInt32();
                    curNodeNum = preNodeNum + 1;
                }

            }
            else
                curNodeNum++;


            if (position == 0 && curNodeNum == 9)
            {
                //  首位不能以9开头，否则对应子节点数量太少
                curNodeNum = 11;
            }

            if (curNodeNum % 10 == 0)
            {
                //  0为补位标识，不能出现在节点数的尾部
                curNodeNum++;
            }


            #endregion

            #region 拼装节点值形成树形全局值


            var curNodeNumStr = curNodeNum.ToString();

            var paddingLength = _numLength - position - curNodeNumStr.Length;
            if (paddingLength < 0)
            {
                // 超过位数
                return -1;
            }

            if (position < 10)
            {
                paddingLength++;
            }

            var paddingStr = paddingLength > 0 ? _paddingNumStr.Substring(0, paddingLength) : string.Empty;

            var curNumStr = string.Concat(
                position > 0 ? preNumStr.Substring(0, position) : string.Empty,
                curNodeNum,
                paddingStr,
                position );


            #endregion

            return curNumStr.ToInt64();
        }

        public static int GetPositionIndex(long treeNum)
        {
            return (int)(treeNum % _positionMod);
        }


        public enum TreeNumType
        {
            Next,
            Sub
        }


    }
}
