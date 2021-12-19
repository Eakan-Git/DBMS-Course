-- Tinh huong 1
exec XemSP_CuaCN @MaCN = 104620

-- Tinh huong 2
exec XemDH_TaiKV @MaDH = 10

-- Tinh huong 3
exec KH_huyDH @MaDH = 3, @TinhTrang = N'Đã hủy'

-- Tinh huong 4
exec KH_capnhat_SL @MaDH = 8, @MaSP = 9, @SoLuong = 15

-- Tinh huong 5
exec NV_themSP @TenSP = N'Strawberry', @Gia = 9000002, @MoTa = NULL, @GiaTieuChuan = 9000000

delete from SANPHAM where MaSP = 2002
select * from SANPHAM where Gia > 9000000

-- Tinh huong 6
exec NV_themHD @MaDT = 33, @MaSoThue = 'LYN08', @NguoiDaiDien = 'Le Yen Nhi', @SoChiNhanhDK = 10, @NgayBD = '2020-08-06', @NgayKT = '2021-08-06', @KichHoat = 1, @PhiHoaHong = 500000, @NgayBDTieuChuan = '2020-08-05'

delete from HOPDONG where MaHD = 2002
select * from HOPDONG where NgayBD > '2020-08-05'