using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVienOOP
{
    internal class Program
    {
        private static readonly QuanLySinhVien qlsv = new QuanLySinhVien();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool tiepTuc = true;
            while (tiepTuc)
            {
                HienThiMenu();
                int chon = NhapSoNguyen("=> Mời bạn chọn chức năng (0-8): ", 0, 8);
                Console.WriteLine();

                switch (chon)
                {
                    case 1:
                        ChucNangThemSinhVien();
                        break;
                    case 2:
                        ChucNangXuatDanhSach();
                        break;
                    case 3:
                        ChucNangTimTheoMa();
                        break;
                    case 4:
                        ChucNangTimTheoTen();
                        break;
                    case 5:
                        ChucNangSuaDiem();
                        break;
                    case 6:
                        ChucNangXoaSinhVien();
                        break;
                    case 7:
                        ChucNangSapXepTheoDiem();
                        break;
                    case 8:
                        ChucNangLocSinhVienDat();
                        break;
                    case 0:
                        Console.Write("Bạn có chắc chắn muốn thoát chương trình? (c/k): ");
                        string xacNhan = Console.ReadLine()?.Trim().ToLower();
                        if (xacNhan == "c" || xacNhan == "co" || xacNhan == "y" || xacNhan == "yes")
                        {
                            tiepTuc = false;
                            Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                        }
                        break;
                }

                if (tiepTuc)
                {
                    Console.WriteLine("\nNhấn Enter hoặc phím bất kỳ để tiếp tục...");
                    if (!Console.IsInputRedirected)
                    {
                        try { Console.ReadKey(); } catch { Console.ReadLine(); }
                    }
                    else
                    {
                        Console.ReadLine();
                    }
                }
            }
        }

        private static void HienThiMenu()
        {
            if (!Console.IsInputRedirected && !Console.IsOutputRedirected)
            {
                try { Console.Clear(); } catch { }
            }
            Console.WriteLine("===============================================================");
            Console.WriteLine("        HỌC PHẦN COMP1019 - LẬP TRÌNH TRÊN WINDOWS             ");
            Console.WriteLine("         BÀI THỰC HÀNH LAB 03 - QUẢN LÝ SINH VIÊN (OOP)        ");
            Console.WriteLine("===============================================================");
            Console.WriteLine("  1. Thêm sinh viên mới");
            Console.WriteLine("  2. Xuất danh sách sinh viên");
            Console.WriteLine("  3. Tìm sinh viên theo mã sinh viên");
            Console.WriteLine("  4. Tìm sinh viên theo họ tên (LINQ)");
            Console.WriteLine("  5. Sửa điểm trung bình theo mã sinh viên");
            Console.WriteLine("  6. Xóa sinh viên khỏi danh sách");
            Console.WriteLine("  7. Sắp xếp sinh viên theo điểm giảm dần (LINQ)");
            Console.WriteLine("  8. Lọc danh sách sinh viên đạt (Điểm >= 5.0) (LINQ)");
            Console.WriteLine("  0. Thoát chương trình");
            Console.WriteLine("===============================================================");
        }

        private static void ChucNangThemSinhVien()
        {
            Console.WriteLine("--- [CHỨC NĂNG 1: THÊM SINH VIÊN MỚI] ---");

            string maSV;
            while (true)
            {
                maSV = NhapChuoiKhongRong("Nhập mã sinh viên: ").ToUpper();
                if (qlsv.KiemTraTonTai(maSV))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Lỗi] Mã sinh viên '{maSV}' đã tồn tại! Vui lòng nhập mã khác.");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }

            string hoTen = NhapChuoiKhongRong("Nhập họ và tên: ");
            DateTime ngaySinh = NhapNgayThang("Nhập ngày sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoiKhongRong("Nhập mã lớp: ").ToUpper();
            double diemTB = NhapSoThuc("Nhập điểm trung bình (0.0 - 10.0): ", 0.0, 10.0);

            var sv = new SinhVien(maSV, hoTen, ngaySinh, maLop, diemTB);
            if (qlsv.Them(sv, out string msg))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Thành công] {msg}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Thất bại] {msg}");
            }
            Console.ResetColor();
        }

        private static void ChucNangXuatDanhSach()
        {
            Console.WriteLine("--- [CHỨC NĂNG 2: XUẤT DANH SÁCH SINH VIÊN] ---");
            InBangSinhVien(qlsv.LayDanhSach());
        }

        private static void ChucNangTimTheoMa()
        {
            Console.WriteLine("--- [CHỨC NĂNG 3: TÌM SINH VIÊN THEO MÃ] ---");
            string maSV = NhapChuoiKhongRong("Nhập mã sinh viên cần tìm: ");
            var sv = qlsv.TimTheoMa(maSV);

            if (sv != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[Tìm thấy] Thông tin sinh viên:");
                Console.ResetColor();
                InBangSinhVien(new List<SinhVien> { sv });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Thông báo] Không tìm thấy sinh viên có mã '{maSV}'!");
                Console.ResetColor();
            }
        }

        private static void ChucNangTimTheoTen()
        {
            Console.WriteLine("--- [CHỨC NĂNG 4: TÌM SINH VIÊN THEO HỌ TÊN (LINQ)] ---");
            string tuKhoa = NhapChuoiKhongRong("Nhập từ khóa họ tên cần tìm: ");
            var ketQua = qlsv.TimTheoTen(tuKhoa);

            if (ketQua.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Tìm thấy] Có {ketQua.Count} sinh viên khớp với từ khóa '{tuKhoa}':");
                Console.ResetColor();
                InBangSinhVien(ketQua);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Thông báo] Không có sinh viên nào có tên chứa từ khóa '{tuKhoa}'!");
                Console.ResetColor();
            }
        }

        private static void ChucNangSuaDiem()
        {
            Console.WriteLine("--- [CHỨC NĂNG 5: SỬA ĐIỂM TRUNG BÌNH] ---");
            string maSV = NhapChuoiKhongRong("Nhập mã sinh viên cần sửa điểm: ");
            var sv = qlsv.TimTheoMa(maSV);

            if (sv == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Thông báo] Không tìm thấy sinh viên có mã '{maSV}'!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"Sinh viên: {sv.HoTen} | Lớp: {sv.MaLop} | Điểm TB hiện tại: {sv.DiemTrungBinh:F1} ({sv.XepLoai()})");
            double diemMoi = NhapSoThuc("Nhập điểm trung bình mới (0.0 - 10.0): ", 0.0, 10.0);

            if (qlsv.SuaDiem(maSV, diemMoi, out string msg))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Thành công] {msg} -> Điểm mới: {diemMoi:F1} ({sv.XepLoai()})");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Thất bại] {msg}");
            }
            Console.ResetColor();
        }

        private static void ChucNangXoaSinhVien()
        {
            Console.WriteLine("--- [CHỨC NĂNG 6: XÓA SINH VIÊN KHỎI DANH SÁCH] ---");
            string maSV = NhapChuoiKhongRong("Nhập mã sinh viên cần xóa: ");
            var sv = qlsv.TimTheoMa(maSV);

            if (sv == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Thông báo] Không tìm thấy sinh viên có mã '{maSV}'!");
                Console.ResetColor();
                return;
            }

            Console.Write($"Bạn có chắc chắn muốn xóa sinh viên '{sv.HoTen}' (Mã: {sv.MaSinhVien})? (c/k): ");
            string confirm = Console.ReadLine()?.Trim().ToLower();
            if (confirm == "c" || confirm == "co" || confirm == "y" || confirm == "yes")
            {
                if (qlsv.Xoa(maSV, out string msg))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[Thành công] {msg}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Thất bại] {msg}");
                }
            }
            else
            {
                Console.WriteLine("Đã hủy thao tác xóa.");
            }
            Console.ResetColor();
        }

        private static void ChucNangSapXepTheoDiem()
        {
            Console.WriteLine("--- [CHỨC NĂNG 7: SẮP XẾP SINH VIÊN THEO ĐIỂM GIẢM DẦN (LINQ)] ---");
            var dsSapXep = qlsv.SapXepTheoDiemGiamDan();
            InBangSinhVien(dsSapXep);
        }

        private static void ChucNangLocSinhVienDat()
        {
            Console.WriteLine("--- [CHỨC NĂNG 8: LỌC SINH VIÊN ĐẠT (ĐIỂM >= 5.0) (LINQ)] ---");
            var dsDat = qlsv.LocSinhVienDat();
            InBangSinhVien(dsDat);
        }

        private static void InBangSinhVien(List<SinhVien> ds)
        {
            if (ds == null || ds.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Danh sách trống!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("+------------+----------------------+------------+----------+---------+------------+");
            Console.WriteLine("| Mã SV      | Họ và tên            | Ngày sinh  | Lớp      | Điểm TB | Xếp loại   |");
            Console.WriteLine("+------------+----------------------+------------+----------+---------+------------+");
            foreach (var sv in ds)
            {
                Console.WriteLine($"| {sv.MaSinhVien,-10} | {sv.HoTen,-20} | {sv.NgaySinh:dd/MM/yyyy} | {sv.MaLop,-8} | {sv.DiemTrungBinh,7:F1} | {sv.XepLoai(),-10} |");
            }
            Console.WriteLine("+------------+----------------------+------------+----------+---------+------------+");
            Console.WriteLine($"Tổng số sinh viên: {ds.Count}");
        }

        #region Helper Nhập liệu an toàn

        private static string NhapChuoiKhongRong(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Lỗi] Không được để trống dữ liệu! Vui lòng nhập lại.");
                Console.ResetColor();
            }
        }

        private static int NhapSoNguyen(string thongBao, int min, int max)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int giaTri) && giaTri >= min && giaTri <= max)
                {
                    return giaTri;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Lỗi] Vui lòng nhập số nguyên trong khoảng [{min} - {max}]!");
                Console.ResetColor();
            }
        }

        private static double NhapSoThuc(string thongBao, double min, double max)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine()?.Replace(',', '.');
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double giaTri) && giaTri >= min && giaTri <= max)
                {
                    return giaTri;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Lỗi] Giá trị phải là số thực từ {min:F1} đến {max:F1}!");
                Console.ResetColor();
            }
        }

        private static DateTime NhapNgayThang(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine()?.Trim();
                string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "yyyy-MM-dd" };
                if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay))
                {
                    if (ngay.Year >= 1900 && ngay <= DateTime.Now)
                    {
                        return ngay;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Lỗi] Năm sinh phải từ 1900 đến hiện tại!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Lỗi] Định dạng ngày tháng không hợp lệ (ví dụ: 15/05/2004)!");
                    Console.ResetColor();
                }
            }
        }

        #endregion
    }
}
