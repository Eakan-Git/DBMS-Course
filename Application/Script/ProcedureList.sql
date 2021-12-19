use DBMS_ThucHanh_Nhom15 
go
-----------------------------------------------------------------------------------------
-- Thêm nhân viên mới
create procedure insertNV
	@HoTen nvarchar(50),
	@SoDT varchar(15),
	@DiaChi nvarchar(150),
	@Email nvarchar(50)
as
begin tran
	if exists (select * from THONGTINCANHAN where SoDienThoai = @SoDT) 
	begin
		raiserror (N'Số điện thoại đã tồn tại', 10, 1)
		rollback tran
	end

	else
	begin
		insert into THONGTINCANHAN (HoTen, SoDienThoai, DiaChi, Email) values (@HoTen, @SoDT, @DiaChi, @Email)
		insert into NHANVIEN (ID) select tt.ID from THONGTINCANHAN tt where tt.SoDienThoai = @SoDT
		commit tran
	end
go

--exec insertNV @HoTen = 'LYN', @SoDT = '0918319976', @DiaChi = 'Le Loi', @Email = Null
--go

-- update ttcn cua nv
UPDATE THONGTINCANHAN set HoTen=@HoTen, SoDienThoai=@SDT, DiaChi=@DiaChi, Email=@Email where ID=(select NV.ID from NHANVIEN NV where NV.MaNV=@MaNV

-- tìm nv vs ID cho trc 
select NV.MANV, TT.HOTEN,TT.DIACHI,TT.EMAIL from NHANVIEN NV, THONGTINCANHAN TT where NV.MANV = @MANV and NV.ID = TT.ID

-- Xóa 1 nv vs mã NV cho trc
Delete NHANVIEN where MANV=@MANV
Delete THONGTINCANHAN where SoDienThoai = @SDT

-- xem ds NV với n dòng 
select nv.MaNV,tt.HoTen,tt.SoDienThoai,tt.DiaChi,tt.Email 
from NHANVIEN nv, THONGTINCANHAN tt 
where nv.ID=tt.ID 
Order by nv.MaNV,tt.HoTen,tt.SoDienThoai,tt.DiaChi,tt.Email offset @offset rows fetch next @rows rows only

-- xem ds sp với n dòng 
select * from SANPHAM 
order by MaSP offset @offset rows fetch next @rows rows only

-- xem sp vs mã cho trc
select * from SANPHAM where MaSp = @MaSP

-- xóa sp vs mã cho trc 
delete from SANPHAM where MaSP = @MaSP

-- update sp
update SANPHAM set TenSP = @TenSP, Gia = @Gia, MoTa = @MoTa where MaSP = @MaSP

-- them sp 
insert into SANPHAM values (@MaSP, @TenSP, @Gia, @MoTa)

