create database DBMS_ThucHanh_Nhom15
GO
USE DBMS_ThucHanh_Nhom15
GO	
------------------------------------------------------------------
create table THONGTINCANHAN
(
	ID int primary key,
	HoTen nvarchar(50),
	SoDienThoai nvarchar(12),
	DiaChi nvarchar(150),
	Email varchar(50)
)

create table NHANVIEN
(
	ID int foreign key references THONGTINCANHAN(ID),
	MaNV nvarchar(10)
	constraint PK_NHANVIEN primary key (MaNV)
)

create table KHACHHANG
(
	ID int foreign key references THONGTINCANHAN(ID),
	MaKH nvarchar(10)
	constraint PK_KHACHHANG primary key (MaKH)
)

create table TAIXE
(
	ID int foreign key references THONGTINCANHAN(ID),
	MaTX nvarchar(10),
	MaKV nvarchar(10),
	BienSoXe nvarchar(15),
	CMND nvarchar(15),
	SoTaiKhoan nvarchar(15),
	PhiThueChan bigint 
	constraint PK_TAIXE primary key (MaTX)
)

create table DONHANG
(
	MaDH nvarchar(10),
	MaKH nvarchar(10),
	NgayDat date,
	NgayGiao date,
	TinhTrang nvarchar(20),
	PhiVanChuyen bigint,
	TongTien bigint
	constraint PK_DONHANG primary key (MaDH)
)

create table THUNHAPTAIXE
(
	MaTX nvarchar(10),
	MaDH nvarchar(10)
	constraint PK_THUNHAPTAIXE primary key (MaTX, MaDH)
)

create table KHUVUC
(
	MaKV nvarchar(10),
	MaDH nvarchar(10),
	constraint PK_KHUVUC primary key (MaKV)
)

create table CHINHANH
(
	MaCN nvarchar(10),
	DiaChi nvarchar(150)
	constraint PK_CHINHANH primary key (MaCN)
)

create table SANPHAM
(
	MaSP nvarchar(10),
	MaCN nvarchar(10),
	TenSP nvarchar(150), 
	Gia bigint,
	MoTa nvarchar(200)
	constraint PK_SANPHAM primary key (MaSP)
)

create table CHITIETDONHANG
(
	MaDH nvarchar(10),
	MaSP nvarchar(10),
	SoLuong bigint,
	ThanhTien bigint
	constraint PK_CHITIETDONHANG primary key (MaDH, MaSP)
)

create table DOITAC
(
	MaDT nvarchar(10),
	TenDT nvarchar(150),
	NguoiDaiDien nvarchar(100),
	ThanhPho nvarchar(60),
	Quan nvarchar(60),
	SoChiNhanh int,
	SLDonHang int,
	LoaiHang nvarchar(50),
	DiaChiKinhDoanh nvarchar(150),
	SoDienThoai nvarchar(12),
	constraint PK_DOITAC primary key (MaDT)
)

create table HOPDONG
(
	MaHD nvarchar(10),
	MaDT nvarchar(10),
	MaCN nvarchar(10),
	MaSoThue nvarchar(20),
	NguoiDaiDien nvarchar(100),
	SoChiNhanhDK int,
	HieuLuc date,
	NgayBD date,
	NgayKT date,
	KichHoat int,
	PhiHoaHong bigint
	constraint PK_HOPDONG primary key (MaHD)
)

alter table TAIXE add
	constraint FK_TAIXE_KHUVUC foreign key (MaKV) references KHUVUC (MaKV)

alter table DONHANG add
	constraint FK_DONHANG_KHACHHANG foreign key (MaKH) references KHACHHANG (MaKH)

alter table THUNHAPTAIXE add
	constraint FK_THUNHAPTAIXE_TAIXE	foreign key (MaTX) references TAIXE (MaTX),
	constraint FK_THUNHAPTAIXE_DONHANG	foreign key (MaDH) references DONHANG (MaDH)

alter table KHUVUC add
	constraint FK_KHUVUC_DONHANG	foreign key (MaDH) references DONHANG (MaDH)

alter table SANPHAM add
	constraint FK_SANPHAM_CHINHANH foreign key (MaCN) references CHINHANH (MaCN)

alter table CHITIETDONHANG add
	constraint FK_CHITIETDONHANG_DONHANG	foreign key (MaDH) references DONHANG (MaDH),
	constraint FK_CHITIETDONHANG_SANPHAM	foreign key (MaSP) references SANPHAM (MaSP)

alter table HOPDONG add
	constraint FK_HOPDONG_DOITAC	foreign key (MaDT) references DOITAC (MaDT),
	constraint FK_HOPDONG_CHINHANH	foreign key (MaCN) references CHINHANH (MaCN)
