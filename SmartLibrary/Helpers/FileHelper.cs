using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SmartLibrary.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Lưu file lên server.
        /// </summary>
        /// <param name="file">File được upload từ client.</param>
        /// <param name="folderPath">Đường dẫn thư mục lưu file.</param>
        /// <returns>Đường dẫn tương đối của file được lưu.</returns>
        public static string SaveFile(HttpPostedFileBase file, string folderPath)
        {
            if (file == null || file.ContentLength == 0)
            {
                return null; // Nếu không có file, trả về null
            }

            // Tạo tên file duy nhất để tránh trùng lặp
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

            // Đường dẫn đầy đủ để lưu file
            var serverPath = Path.Combine(HttpContext.Current.Server.MapPath(folderPath), fileName);

            // Đảm bảo thư mục lưu trữ tồn tại
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderPath));

            // Lưu file lên server
            file.SaveAs(serverPath);

            // Trả về đường dẫn tương đối
            return Path.Combine(folderPath, fileName).Replace("\\", "/");
        }
    }
}