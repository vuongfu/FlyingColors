using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using TutorOnline.Business.Repository;

namespace TutorOnline.Web.Controllers
{
    public static class FileUpload
    {
        private static LearningMaterialRepository LMRes = new LearningMaterialRepository();

        public static char DirSeparator = System.IO.Path.DirectorySeparatorChar;
        public static string ImagePath = HttpContext.Current.Server.MapPath("~\\Content" + DirSeparator + "Uploads" + DirSeparator + "Images" + DirSeparator);
        public static string DocPath = HttpContext.Current.Server.MapPath("~\\Content" + DirSeparator + "Uploads" + DirSeparator + "Documents" + DirSeparator);
        public enum TypeUpload { image , document };

        public static string UploadFile(HttpPostedFileBase file, TypeUpload Type)
        {
            // Check if we have a file
            if (null == file) return "";
            // Make sure the file has content
            if (!(file.ContentLength > 0)) return "";

            string fileName = DateTime.Now.Millisecond + file.FileName;
            string fileExt = Path.GetExtension(file.FileName);

            // Make sure we were able to determine a proper extension
            if (null == fileExt) return "";
            string path;
            if (Type == TypeUpload.image)
            {
                // Check if the directory we are saving to exists
                if (!Directory.Exists(ImagePath))
                {
                    // If it doesn't exist, create the directory
                    Directory.CreateDirectory(ImagePath);                    
                }
                // Set our full path for saving
                path = ImagePath + DirSeparator + fileName;
            }
            else
            {
                // Check if the directory we are saving to exists
                if (!Directory.Exists(DocPath))
                {
                    // If it doesn't exist, create the directory
                    Directory.CreateDirectory(DocPath);
                }
                // Set our full path for saving
                path = DocPath + DirSeparator + fileName;
            }                     

            // Save our file
            file.SaveAs(Path.GetFullPath(path));

            if(Type == TypeUpload.image)
            {
                // Save our thumbnail as well
                ResizeImage(file, 70, 70);
            }
           
            // Return the filename
            return fileName;
        }

        //public static string GetFileName(HttpPostedFileBase file)
        //{
        //    if (null == file) return null;
        //    string filename = file.FileName;
        //    int count = filename.Length;
        //    while ((filename[count - 1] != '.') && count > 0)
        //        count--;
        //    //resultStr
        //    string resultStr = filename.Substring(0, filename.Length - (count + 1)).ToLower();
        //    return resultStr;
        //}

        public static string GetFileType(HttpPostedFileBase file)
        {
            if (null == file) return null;
            string filename = file.FileName;
            int count = filename.Length;
            while ((filename[count-1] != '.') && count > 0)
                count--;
            //resultStr
            string resultStr = filename.Substring(count).ToLower();
            return resultStr;
        }

        public static int GetFileTypeId(HttpPostedFileBase file)
        {
            string typeStr = GetFileType(file);
            var lstType = LMRes.GetAllMaType().ToList();
            //Check file type
            for (int i = 0; i < LMRes.GetAllMaType().Count(); i ++)
            {
                if (typeStr == lstType[i].MaterialTypeName.ToLower())
                {
                    int typeId = lstType[i].MaterialTypeId;
                    return typeId;
                }
            }

            return 0;
        }

        public static void DeleteFile(string fileName)
        {
            // Don't do anything if there is no name
            if (fileName.Length == 0) return;

            // Set our full path for deleting
            string path = ImagePath + DirSeparator + fileName;
            string thumbPath = ImagePath + DirSeparator + "Thumbnails" + DirSeparator + fileName;

            RemoveFile(path);
            RemoveFile(thumbPath);
        }

        private static void RemoveFile(string path)
        {
            // Check if our file exists
            if (File.Exists(Path.GetFullPath(path)))
            {
                // Delete our file
                File.Delete(Path.GetFullPath(path));
            }
        }

        public static void ResizeImage(HttpPostedFileBase file, int width, int height)
        {
            string thumbnailDirectory = String.Format(@"{0}{1}{2}", ImagePath, DirSeparator, "Thumbnails");

            // Check if the directory we are saving to exists
            if (!Directory.Exists(thumbnailDirectory))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(thumbnailDirectory);
            }

            // Final path we will save our thumbnail
            string imagePath = String.Format(@"{0}{1}{2}", thumbnailDirectory, DirSeparator, file.FileName);
            // Create a stream to save the file to when we're done resizing
            FileStream stream = new FileStream(Path.GetFullPath(imagePath), FileMode.OpenOrCreate);

            // Convert our uploaded file to an image
            Image OrigImage = Image.FromStream(file.InputStream);
            // Create a new bitmap with the size of our thumbnail
            Bitmap TempBitmap = new Bitmap(width, height);

            // Create a new image that contains are quality information
            Graphics NewImage = Graphics.FromImage(TempBitmap);
            NewImage.CompositingQuality = CompositingQuality.HighQuality;
            NewImage.SmoothingMode = SmoothingMode.HighQuality;
            NewImage.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Create a rectangle and draw the image
            Rectangle imageRectangle = new Rectangle(0, 0, width, height);
            NewImage.DrawImage(OrigImage, imageRectangle);

            // Save the final file
            TempBitmap.Save(stream, OrigImage.RawFormat);

            // Clean up the resources
            NewImage.Dispose();
            TempBitmap.Dispose();
            OrigImage.Dispose();
            stream.Close();
            stream.Dispose();
        }
    }
}