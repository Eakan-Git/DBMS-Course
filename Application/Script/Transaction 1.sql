-- Tinh huong 1
exec NV_giamgia_SP @MaCN = 104620, @GiaGiam = 20000

select * 
from CUNGCAP_SP cc, SANPHAM sp 
where sp.MaSP = cc.MaSP and cc.MaCN = 104620

-- Tinh huong 2
exec PhiShipDH @MaKV = 59, @PhiShipGiamGia = 0

select * from DONHANG where MaKV = 59

-- Tinh huong 3
exec TX_danhan_DH @MaDH = 3, @TinhTrang = N'Đã nhận'

select * from DONHANG where MaDH = 3
update DONHANG set  TinhTrang = N'Đang xử lí' where MaDH = 3

-- Tinh huong 4
exec TX_xem_CTDH @MaDH = 8, @MaSP = 9

select * from CHITIETDONHANG where MaDH = 8 and MaSP = 9
update CHITIETDONHANG set SoLuong = 25 where MaDH = 8 and MaSP = 9

-- Tinh huong 5
exec Xem_SP_voigia @Gia = 9000000

-- Tinh huong 6
exec Xem_DanhSachHD @NgayBD = '2020-08-05'
