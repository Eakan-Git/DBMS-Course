use DBMS_ThucHanh
go
----------------------------------------------------------------------------------------
---- LOGIN
-- Create Login
exec sp_addLogin 'developer', 'developer'
exec sp_addLogin 'nhanvien', 'nhanvien'
exec sp_addLogin 'taixe', 'taixe'
exec sp_addLogin 'khachhang', 'khachhang'

-- Change password

-- Drop Login


---- USER
-- Create User
create user dev for login developer
create user nhanvien for login nhanvien
create user taixe for login taixe
create user khachhang for login khachhang

-- Drop user
--exec sp_dropuser 'Albert'

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

-- Drop Role 
--exec sp_droprole 'Sales';  

-- Drop role member
--exec sp_droprolemember 'Sales', 'Jonb'


---- GRANT
-- for Developer
grant create procedure, create function, create table to dev
grant select, insert, update, delete on NHANVIEN to Developer

-- for Staff
grant select, insert, update, delete on DOITAC		to Staff
grant select, insert, update, delete on CHINHANH	to Staff
grant select, insert, update, delete on SANPHAM		to Staff
grant select, insert, update, delete on DONHANG		to Staff
grant select, insert, update, delete on TAIXE		to Staff
grant select, insert, update, delete on KHUVUC		to Staff
grant select, insert, update on HOPDONG				to Staff
grant select, update, delete on THONGTINCANHAN		to Staff		-- insert?
-- KHACHHANG?

-- for Driver
grant select on KHUVUC			to Driver
grant select on THUNHAPTAIXE	to Driver
grant select on DONHANG			to Driver
grant select on CHITIETDONHANG	to Driver
grant select on SANPHAM			to Driver
grant select on CHINHANH		to Driver
grant select, update on THONGTINCANHAN (Hoten, SoDienThoai, DiaChi, Email) to Driver
grant select, update on TAIXE (MaKV) to Driver

-- for Customer
grant select, insert, update, delete on DONHANG			to Customer
grant select, insert, update, delete on CHITIETDONHANG	to Customer
grant select on SANPHAM		to Customer
grant select on CHINHANH	to Customer
grant select, update on THONGTINCANHAN (Hoten, SoDienThoai, DiaChi, Email) to Customer


---- DENY


---- REVOKE
