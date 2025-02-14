namespace SmartLibrary.Models
{
    public class ModelCommons
    {
        public static class Roles 
        {
            public static string Admin = "Admin";
            public static string Manager = "Manager";
            public static string User = "User";
        }

        public static class UserStatus
        {
            public static string HoatDong = "Hoạt động";
            public static string KhongHoatDong = "Không hoạt động";
        }

        public static class BorrowBookStatus
        {
            public static string DaTra = "Đã trả";
            public static string DangMuon = "Đang mượn";
        }

        public static class ReservationStatus
        {
            public static string DangCho = "Đang chờ";
            public static string DaHuy = "Đã hủy";
            public static string HoanTat = "Hoàn tất";
        }

    }
}