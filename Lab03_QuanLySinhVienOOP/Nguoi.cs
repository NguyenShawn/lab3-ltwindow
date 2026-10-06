using System;

namespace Lab03_QuanLySinhVienOOP
{
    /// <summary>
    /// Lớp cơ sở đại diện cho một con người
    /// </summary>
    public class Nguoi
    {
        // Thuộc tính họ tên
        public string HoTen { get; set; }

        // Thuộc tính ngày sinh
        public DateTime NgaySinh { get; set; }

        // Constructor không tham số
        public Nguoi()
        {
            HoTen = string.Empty;
            NgaySinh = DateTime.MinValue;
        }

        // Constructor có tham số
        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        // Phương thức lấy thông tin cơ bản (virtual để lớp con có thể override)
        public virtual string LayThongTin()
        {
            return $"Họ tên: {HoTen} | Ngày sinh: {NgaySinh:dd/MM/yyyy}";
        }
    }
}
