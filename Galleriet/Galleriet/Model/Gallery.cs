using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Galleriet.Model
{
    public class Gallery
    {
        // Fields
        private static readonly Regex ApprovedExenstions;

        private static string PhysicalUploadImagePath;

        private static readonly Regex SantizePath;

        // Constructor
        static Gallery()
        {
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            Regex approvedExReg = new Regex("^.*.(gif|jpg|png)$");
            Regex santizePathReg = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));

            ApprovedExenstions = approvedExReg;
            SantizePath = santizePathReg;
            PhysicalUploadImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");

            SantizePath.Replace(invalidChars, "");
        }

        // Methods
        public IEnumerable<string> GetImagesNames()
        {
            IEnumerable<string> images = new List<string>();
            string thumbPath = Path.Combine(PhysicalUploadImagePath, "Thumbs");

            images = Directory.EnumerateFiles(thumbPath, "*", SearchOption.AllDirectories)
                .Select(Path.GetFileName);

            return images;
        }

        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadImagePath, name));
        }

        private bool IsValidImage(Image image)
        {
            return image.RawFormat.Guid == ImageFormat.Gif.Guid || 
                image.RawFormat.Guid == ImageFormat.Jpeg.Guid || 
                image.RawFormat.Guid == ImageFormat.Png.Guid;
        }

        public string SaveImage(Stream stream, string fileName)
        {
            using (var image = Image.FromStream(stream))
            {
                if (!IsValidImage(image))
                {
                    throw new ArgumentException("Invalid image format");
                }

                fileName = GetAvailableFilename(fileName);
                var imagePath = Path.Combine(PhysicalUploadImagePath, fileName);
                var thumbnailPath = Path.Combine(PhysicalUploadImagePath, "Thumbs", fileName);

                image.Save(imagePath);

                using (var thumbnail = image.GetThumbnailImage(60, 45, null, IntPtr.Zero))
                {
                    thumbnail.Save(thumbnailPath);
                }
            }

            return fileName;
        }

        public string GetAvailableFilename(string fileName)
        {
            var count = 1;
            var imagePath = Path.Combine(PhysicalUploadImagePath, fileName);
            var thumbnailPath = Path.Combine(PhysicalUploadImagePath, "Thumbs", fileName);

            var extension = Path.GetExtension(fileName);
            var filenameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            while (File.Exists(thumbnailPath) || File.Exists(imagePath))
            {
                fileName = string.Format("{0}({1}){2}", filenameWithoutExtension, count++, extension);

                imagePath = Path.Combine(PhysicalUploadImagePath, fileName);
                thumbnailPath = Path.Combine(PhysicalUploadImagePath, "Thumbs", fileName);
            }

            return fileName;
        }
    }
}