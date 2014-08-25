using System;

namespace OS.Common.Volidate
{
     [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public abstract class BaseValidateAttribute : Attribute
    {
        protected string errorMessage;
        internal abstract bool Validate(string propertyName, object propertyValue);

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
        }
    }
    /// <summary>
    /// 必填验证属性
    /// </summary>
    public class RequireAttribute : BaseValidateAttribute
    {
        public RequireAttribute(string _errorMessage=null)
        {
            errorMessage = _errorMessage;
        }
        internal override bool Validate(string propertyName, object propertyValue)
        {
            bool result=propertyValue != null && propertyValue.ToString() != string.Empty;
            if (!result)
            {
                errorMessage = errorMessage ??string.Format("请输入 {0} 信息", propertyName);
            }
            return result;
        }
    }
    /// <summary>
    /// 数字验证属性
    /// </summary>
    public class NumberAttribute : BaseValidateAttribute
    {
        private long min, max;
        public NumberAttribute(long _min=long.MinValue, long _max=long.MaxValue, string _errorMessage=null)
        {
            min          = _min;
            max          = _max;
            errorMessage = _errorMessage;
        }
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        internal override bool Validate(string propertyName, object propertyValue)
        {
            errorMessage = errorMessage ??string.Format("{0} 的值 必须介于 {1}~{2} 的数值", propertyName, min, max);
            if (propertyValue==null)
            {
                return false;
            }
            long longValue = 0;
            if (long.TryParse(propertyValue.ToString(), out longValue))
            {
                return min <= longValue&& longValue <= max;
            }
            return false;
        }
    }


}
