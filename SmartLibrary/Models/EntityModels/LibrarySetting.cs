namespace SmartLibrary.Models.EntityModels
{
    public class LibrarySetting
    {
        public int LibrarySettingId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }

        // Thuộc tính mới: lưu kiểu dữ liệu của SettingValue
        // Ví dụ: "text", "number", "date", "image", ...
        public string DataType { get; set; }
    }

}