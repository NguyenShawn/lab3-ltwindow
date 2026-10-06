using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    /// <summary>
    /// Lớp quản lý danh sách sinh viên bằng List<SinhVien> và LINQ
    /// </summary>
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSachSinhVien;

        public QuanLySinhVien()
        {
            _danhSachSinhVien = new List<SinhVien>();
            KhoiTaoDuLieuMau();
        }

        /// <summary>
        /// Khởi tạo dữ liệu mẫu ban đầu để thuận tiện kiểm thử
        /// </summary>
        public void KhoiTaoDuLieuMau()
        {
            _danhSachSinhVien.Add(new SinhVien("SV001", "Nguyễn Văn An", new DateTime(2004, 5, 15), "CNTT01", 8.6));
            _danhSachSinhVien.Add(new SinhVien("SV002", "Trần Thị Bình", new DateTime(2004, 9, 20), "CNTT01", 7.2));
            _danhSachSinhVien.Add(new SinhVien("SV003", "Lê Hoàng Cường", new DateTime(2003, 11, 2), "CNTT02", 4.5));
            _danhSachSinhVien.Add(new SinhVien("SV004", "Phạm Thị Duyên", new DateTime(2004, 1, 10), "CNTT02", 9.1));
            _danhSachSinhVien.Add(new SinhVien("SV005", "Đỗ Văn Em", new DateTime(2004, 7, 28), "CNTT01", 6.0));
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên
        /// </summary>
        public List<SinhVien> LayDanhSach()
        {
            return _danhSachSinhVien;
        }

        /// <summary>
        /// Thêm sinh viên mới vào danh sách (Kiểm tra không trùng mã)
        /// </summary>
        public bool Them(SinhVien sv, out string thongBao)
        {
            if (sv == null)
            {
                thongBao = "Dữ liệu sinh viên không hợp lệ!";
                return false;
            }

            if (KiemTraTonTai(sv.MaSinhVien))
            {
                thongBao = $"Mã sinh viên '{sv.MaSinhVien}' đã tồn tại trong hệ thống!";
                return false;
            }

            _danhSachSinhVien.Add(sv);
            thongBao = "Thêm sinh viên thành công!";
            return true;
        }

        /// <summary>
        /// Kiểm tra sinh viên có tồn tại theo mã hay không
        /// </summary>
        public bool KiemTraTonTai(string maSV)
        {
            return _danhSachSinhVien.Any(sv => sv.MaSinhVien.Equals(maSV?.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo mã (chính xác)
        /// </summary>
        public SinhVien TimTheoMa(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;
            return _danhSachSinhVien.FirstOrDefault(sv => sv.MaSinhVien.Equals(maSV.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Tìm sinh viên theo họ tên (chứa từ khóa, dùng LINQ, hỗ trợ tìm có dấu hoặc không dấu)
        /// </summary>
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return new List<SinhVien>();

            string tuKhoaKhongDau = LoaiBoDauTiengViet(tuKhoa.Trim());
            return _danhSachSinhVien
                .Where(sv => sv.HoTen.IndexOf(tuKhoa.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                             LoaiBoDauTiengViet(sv.HoTen).IndexOf(tuKhoaKhongDau, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        private static string LoaiBoDauTiengViet(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;
            string normalized = str.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new System.Text.StringBuilder();
            foreach (char c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D');
        }

        /// <summary>
        /// Cập nhật điểm trung bình của sinh viên theo mã
        /// </summary>
        public bool SuaDiem(string maSV, double diemMoi, out string thongBao)
        {
            var sv = TimTheoMa(maSV);
            if (sv == null)
            {
                thongBao = $"Không tìm thấy sinh viên có mã '{maSV}'!";
                return false;
            }

            if (diemMoi < 0.0 || diemMoi > 10.0)
            {
                thongBao = "Điểm trung bình mới không hợp lệ (phải từ 0.0 đến 10.0)!";
                return false;
            }

            sv.DiemTrungBinh = diemMoi;
            thongBao = $"Cập nhật điểm cho sinh viên '{sv.HoTen}' thành công!";
            return true;
        }

        /// <summary>
        /// Xóa sinh viên khỏi danh sách theo mã
        /// </summary>
        public bool Xoa(string maSV, out string thongBao)
        {
            var sv = TimTheoMa(maSV);
            if (sv == null)
            {
                thongBao = $"Không tìm thấy sinh viên có mã '{maSV}' để xóa!";
                return false;
            }

            _danhSachSinhVien.Remove(sv);
            thongBao = $"Đã xóa sinh viên '{sv.HoTen}' (Mã: {sv.MaSinhVien}) thành công!";
            return true;
        }

        /// <summary>
        /// Sắp xếp sinh viên theo điểm trung bình giảm dần (dùng LINQ)
        /// </summary>
        public List<SinhVien> SapXepTheoDiemGiamDan()
        {
            return _danhSachSinhVien
                .OrderByDescending(sv => sv.DiemTrungBinh)
                .ThenBy(sv => sv.HoTen)
                .ToList();
        }

        /// <summary>
        /// Lọc danh sách sinh viên đạt (Điểm TB >= 5.0, dùng LINQ)
        /// </summary>
        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSachSinhVien
                .Where(sv => sv.DiemTrungBinh >= 5.0)
                .OrderByDescending(sv => sv.DiemTrungBinh)
                .ToList();
        }
    }
}
