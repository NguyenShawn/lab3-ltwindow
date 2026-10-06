using System;

namespace Lab03_QuanLySinhVienOOP
{
    /// <summary>
    /// Lớp SinhVien kế thừa từ Nguoi
    /// </summary>
    public class SinhVien : Nguoi
    {
        private double _diemTrungBinh;

        // Mã sinh viên
        public string MaSinhVien { get; set; }

        // Mã lớp
        public string MaLop { get; set; }

        // Điểm trung bình có kiểm tra dữ liệu từ 0.0 đến 10.0
        public double DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                if (value < 0.0 || value > 10.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Điểm trung bình phải nằm trong khoảng từ 0.0 đến 10.0!");
                }
                _diemTrungBinh = value;
            }
        }

        // Constructor không tham số
        public SinhVien() : base()
        {
            MaSinhVien = string.Empty;
            MaLop = string.Empty;
            _diemTrungBinh = 0.0;
        }

        // Constructor đầy đủ tham số
        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh; // Kích hoạt kiểm tra validation của setter
        }

        /// <summary>
        /// Phương thức xếp loại học lực dựa trên điểm trung bình
        /// </summary>
        public string XepLoai()
        {
            if (DiemTrungBinh >= 9.0) return "Xuất sắc";
            if (DiemTrungBinh >= 8.0) return "Giỏi";
            if (DiemTrungBinh >= 6.5) return "Khá";
            if (DiemTrungBinh >= 5.0) return "Trung bình";
            return "Yếu";
        }

        /// <summary>
        /// Override phương thức LayThongTin của lớp Nguoi
        /// </summary>
        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien,-10} | Họ tên: {HoTen,-20} | Ngày sinh: {NgaySinh:dd/MM/yyyy} | Lớp: {MaLop,-8} | Điểm TB: {DiemTrungBinh,4:F1} | Xếp loại: {XepLoai()}";
        }
    }
}
