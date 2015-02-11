using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections;

namespace OS.Common.Utils
{

    //public static class ImageHelper
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="jpeg_image_upload"></param>
    //    /// <param name="target_height"></param>
    //    /// <param name="fixedType"></param>
    //    /// <param name="path"></param>
    //    /// <returns>返回是否保存成功</returns>
    //    public static bool SaveFileImageFixedByHight(HttpPostedFileBase jpeg_image_upload, int target_height, string path)
    //    {
    //        return SaveFileImage(jpeg_image_upload, 0, target_height, FixedRateType.FixedHeight, path);
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="jpeg_image_upload"></param>
    //    /// <param name="target_width"></param>
    //    /// <param name="fixedType"></param>
    //    /// <param name="path"></param>
    //    /// <returns>返回是否保存成功</returns>
    //    public static bool SaveFileImageFixedByWidth(HttpPostedFileBase jpeg_image_upload, int target_width, string path)
    //    {
    //       return SaveFileImage(jpeg_image_upload, target_width, 0, FixedRateType.FixedWidth, path);
    //    }


    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="jpeg_image_upload"></param>
    //    /// <param name="target_width"></param>
    //    /// <param name="target_height"></param>
    //    /// <param name="fixedType"></param>
    //    /// <param name="path">绝对路径</param>
    //    /// <returns>返回是否保存成功</returns>
    //    public static bool SaveFileImage(HttpPostedFileBase jpeg_image_upload, int target_width, int target_height, FixedRateType fixedType, string path)
    //    {
    //        bool isOkay = false;
    //        Image original_image = null;

    //        try
    //        {
    //            if (jpeg_image_upload != null&& !string.IsNullOrEmpty(path))
    //            {
    //                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
    //                string dirPath = path.Substring(0, path.LastIndexOfAny(new char[] { '\\', '/' }));
    //                if (!Directory.Exists(dirPath))
    //                {
    //                    Directory.CreateDirectory(dirPath);
    //                }
    //                isOkay = SaveImage(original_image, target_width, target_height, fixedType,  path);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            LogUtil.Error("生成图片过程出错，详情："+ex.Message);
    //        }
    //        finally
    //        {
    //            if (original_image != null) original_image.Dispose();
    //        }
    //        return isOkay;
    //    }
    //    /// <summary>
    //    /// 保存图片
    //    /// </summary>
    //    /// <param name="original_image">图片数据</param>
    //    /// <param name="target_width">画布宽度（图像宽度）</param>
    //    /// <param name="target_height">画布高度（图像高度）</param>
    //    /// <param name="fixedType">固定比率类型</param>
    //    /// <param name="path">保存路径</param>
    //    /// <returns>返回是否保存成功，失败可能原因：路径不正确  </returns>
    //    public static bool SaveImage(Image original_image, int target_width, int target_height, FixedRateType fixedType, string path)
    //    {
    //        bool isOkay = true;
    //        Image new_image = null;
    //        int width = original_image.Width;
    //        int height = original_image.Height;
    //        int new_width, new_height;
    //        #region 计算比例
    //        float target_ratio = (float)target_width / (float)target_height;
    //        float image_ratio = (float)width / (float)height;
    //        switch (fixedType)
    //        {
    //            case FixedRateType.Fixed:
    //                new_height = target_ratio > image_ratio ? target_height : (int)Math.Floor((float)target_width / image_ratio);
    //                new_width = target_ratio > image_ratio ? (int)Math.Floor(image_ratio * (float)target_height) : target_width;
    //                break;
    //            case FixedRateType.FixedWidth:
    //                new_height = (int)Math.Floor((float)target_width / image_ratio);
    //                new_width = target_width;
    //                target_height = new_height;
    //                break;
    //            case FixedRateType.FixedHeight:
    //                new_height = target_height;
    //                new_width = (int)Math.Floor(image_ratio * (float)target_height);
    //                target_width = new_width;
    //                break;
    //            default:
    //                new_width = target_width;
    //                new_height = target_height;
    //                break;
    //        }
    //        #endregion

    //        Graphics graphics = null;
    //        try
    //        {
    //            new_image = new Bitmap(target_width > new_width ? target_width : new_width,
    //                target_height > new_height ? target_height : new_height);
    //            graphics = Graphics.FromImage(new_image);
    //            if (fixedType == FixedRateType.Fixed)
    //            {   //  判断是否填充
    //                graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, target_width, target_height));
    //            }

    //            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
    //            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
    //            int paste_x = (target_width - new_width) / 2;
    //            int paste_y = (target_height - new_height) / 2;
    //            graphics.DrawImage(original_image, paste_x, paste_y, new_width, new_height);
    //            new_image.Save(path);
    //        }
    //        catch
    //        {
    //            isOkay = false;
    //        }
    //        finally
    //        {
    //            if (graphics != null) graphics.Dispose();
    //            if (new_image != null) new_image.Dispose();
    //        }
    //        return isOkay;
    //    }

    //}
    //public enum FixedRateType
    //{
    //    /// <summary>
    //    /// 不固定比率
    //    /// </summary>
    //    None = 0,
    //    /// <summary>
    //    /// 固定比率,根据传进的画布大小，不足部分自动补白
    //    /// </summary>
    //    Fixed = 1,
    //    /// <summary>
    //    ///固定比率, 以宽度为主（不会出现填充），高度自适应
    //    /// </summary>
    //    FixedWidth = 2,
    //    /// <summary>
    //    /// 固定比率,以高度为主（不会出现填充），宽度自适应
    //    /// </summary>
    //    FixedHeight = 3
    //}
}
