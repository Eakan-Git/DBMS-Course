use DBMS_ThucHanh_Nhom15
go
----------------------------------------------------------------------------------------
---- LOGIN
-- Create Login
exec sp_addLogin 'developer', 'developer'
exec sp_addLogin 'nhanvien', 'nhanvien'
exec sp_addLogin 'taixe', 'taixe'
exec sp_addLogin 'khachhang', 'khachhang'


---- USER
-- Create User
create user dev for login developer
create user nhanvien for login nhanvien
create user taixe for login taixe
create user khachhang for login khachhang


---- ROLE
-- Create Role
exec sp_addrole 'Developer'
exec sp_addrole 'Staff'
exec sp_addrole 'Driver'
exec sp_addrole 'Customer'

-- Add role member
exec sp_addrolemember 'Developer', 'dev'
exec sp_addrolemember 'Staff', 'nhanvien'
exec sp_addrolemember 'Driver', 'taixe'
exec sp_addrolemember 'Customer', 'khachhang'


---- GRANT
-- for Developer
alter role db_owner add MEMBER [dev]

-- for Staff
grant select, insert, update, delete on DOITAC		to Staff
grant select, insert, update, delete on CHINHANH	to Staff
grant select, insert, update, delete on SANPHAM		to Staff
grant select, insert, update, delete on DONHANG		to Staff
grant select, insert, update, delete on TAIXE		to Staff
grant select, insert, update, delete on KHUVUC		to Staff
grant select, insert, update, delete on KHACHHANG	to Staff
grant select, insert, update, delete on THONGTINCANHAN	to Staff	
grant select, insert, update on HOPDONG				to Staff

-- for Driver
grant select on KHUVUC			to Driver
grant select on THUNHAPTAIXE	to Driver
grant select on DONHANG			to Driver
grant select on CHITIETDONHANG	to Driver
grant select on SANPHAM			to Driver
grant select on CHINHANH		to Driver
grant select on TAIXE  to Driver
grant select on THONGTINCANHAN to Driver

-- for Customer
grant select, insert, update, delete on DONHANG			to Customer
grant select, insert, update, delete on CHITIETDONHANG	to Customer
grant select on SANPHAM		to Customer
grant select on CHINHANH	to Customer
grant select, insert, update on THONGTINCANHAN (Hoten, SoDienThoai, DiaChi, Email) to Customer


------------------------------------------------------------------------------
---- Test for khachhang
insert into DONHANG (MaDH) values (N'DH1000000')
delete from DONHANG where MaDH = N'DH1000000'
select * from DONHANG where MaDH = N'DH1000000'

insert into SANPHAM(MaSP) values (N'DH1000000')
delete from SANPHAM where MaSP = N'DH1000000'
select * from SANPHAM where MaSP = N'DH1000000'

-- Test for taixe
select * from TAIXE
select * from KHUVUC

insert into TAIXE (MaTX) values (N'TX1000000')
delete from TAIXE where MaTX = N'TX0007'

-- Test for nhanvien
insert into SANPHAM values (N'sp1001', N'CN662Y', N'Chocolate', 10000, N'Orange')
delete from SANPHAM where MaSP = N'sp1001'
select * from SANPHAM where MaSP = N'sp1001'

delete from HOPDONG where MaHD = N'HD0000'
