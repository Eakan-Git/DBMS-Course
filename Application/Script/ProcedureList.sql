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
create procedure updateNV
@MaNV int,
@HoTen nvarchar(50),
@SoDienThoai varchar(15),
@DiaChi nvarchar(150),
@Email varchar(50)
as
begin tran
	UPDATE THONGTINCANHAN set HoTen=@HoTen, SoDienThoai=@SoDienThoai, DiaChi=@DiaChi, Email=@Email 
	where ID=(select NV.ID from NHANVIEN NV where NV.MaNV=@MaNV)
	commit tran
go

--select * from THONGTINCANHAN tt, NHANVIEN nv where nv.id = tt.id and nv.MaNV = 278974
--exec updateNV 278974, N'Quang Trường', '0854693777', 'Test', 'aaa'
go
-- tìm nv vs SDT cho trc 
create procedure lookupNV_SDT
@SDT varchar(15)
as
begin tran
	select NV.MANV, TT.HOTEN, TT.SoDienThoai,TT.DIACHI,TT.EMAIL from NHANVIEN NV, THONGTINCANHAN TT 
	where TT.SoDienThoai = @SDT and NV.ID = TT.ID
	commit tran
go

--exec lookupNV_SDT N'07624466063'
--go
-- Xóa 1 nv vs mã NV và SĐT cho trc
create procedure deleteNV
@MaNV int,
@SoDienThoai nvarchar(50)
as
begin tran
	Delete NHANVIEN where MANV=@MaNV
	Delete THONGTINCANHAN where SoDienThoai = @SoDienThoai
commit tran
go

exec deleteNV 278974, '0854693777'
go
-- xem ds NV với n dòng 
create proc ViewStaffListWith_n_Rows
@offset int,
@rows int
As
begin tran
	begin
		select nv.MaNV,tt.HoTen,tt.SoDienThoai,tt.DiaChi,tt.Email 
		from NHANVIEN nv, THONGTINCANHAN tt 
		where nv.ID=tt.ID 
		Order by nv.MaNV,tt.HoTen,tt.SoDienThoai,tt.DiaChi,tt.Email offset @offset rows fetch next @rows rows only
		commit tran
	end

-- xem ds sp với n dòng 
select * from SANPHAM 
order by MaSP offset @offset rows fetch next @rows rows only

-- xem sp vs mã cho trc
select * from SANPHAM where MaSp = @MaSP

-- xóa sp vs mã cho trc 
delete from SANPHAM where MaSP = @MaSP

-- update sp
update SANPHAM set TenSP = @TenSP, Gia = @Gia, MoTa = @MoTa where MaSP = @MaSP
go
-- them sp 
--create procedure addSP
--@TenSP nvarchar(150),
--@Gia bigint,
--@MoTa nvarchar(200)
--as
--begin tran
--	insert into SANPHAM values (@TenSP, @Gia, @MoTa)
--	commit tran
--go

