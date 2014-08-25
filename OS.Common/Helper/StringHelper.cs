
namespace OS.Common.Helpers
{
   public static  class StringHelper
   {
       #region Filter
       /// <summary>
       /// 过滤 Sql 语句字符串中的注入脚本
       /// </summary>
       /// <param name="source">传入的字符串</param>
       /// <returns>过滤后的字符串</returns>
       public static string SqlFilter(string source)
       {
           source = source.Replace("\"", "");
           source = source.Replace("&", "&amp");
           source = source.Replace("<", "&lt");
           source = source.Replace(">", "&gt");
           source = source.Replace("delete", "");
           source = source.Replace("update", "");
           source = source.Replace("insert", "");
           source = source.Replace("'", "''");
           source = source.Replace(";", "；");
           source = source.Replace("(", "（");
           source = source.Replace(")", "）");
           source = source.Replace("Exec", "");
           source = source.Replace("Execute", "");
           source = source.Replace("xp_", "x p_");
           source = source.Replace("sp_", "s p_");
           source = source.Replace("0x", "0 x");
           return source;
       }
       #endregion
    }
}
