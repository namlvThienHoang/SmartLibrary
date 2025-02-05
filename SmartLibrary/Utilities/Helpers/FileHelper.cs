using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SmartLibrary.Utilities.Helpers
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

        // Phương thức upload file
        public static string UploadFile(HttpPostedFileBase file, string uploadFolderPath)
        {
            if (file == null || file.ContentLength == 0)
                throw new ArgumentException("File không hợp lệ.");

            // Kiểm tra định dạng file
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Chỉ chấp nhận file ảnh (jpg, jpeg, png, gif).");

            // Tạo tên file duy nhất
            string fileName = Guid.NewGuid().ToString() + fileExtension;
            string fullPath = Path.Combine(uploadFolderPath, fileName);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            // Lưu file
            file.SaveAs(fullPath);

            // Trả về đường dẫn tương đối
            return $"/Uploads/Books/{fileName}";
        }

        // Phương thức xóa file
        public static void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            // Lấy đường dẫn vật lý
            string physicalPath = HttpContext.Current.Server.MapPath(filePath);

            // Kiểm tra file tồn tại trước khi xóa
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
        }
    }
}