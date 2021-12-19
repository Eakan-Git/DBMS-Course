create database DBMS_ThucHanh_Nhom15
go
use DBMS_ThucHanh_Nhom15
go
------------------------------------------------------------------
create table THONGTINCANHAN
(
	ID int identity(1,1) primary key,
	HoTen nvarchar(50),
	SoDienThoai varchar(15),
	DiaChi nvarchar(150),
	Email varchar(50)
)

create table NHANVIEN
(
	ID int foreign key references THONGTINCANHAN(ID),
	MaNV int identity(1,1) primary key 
)

create table KHACHHANG
(
	ID int foreign key references THONGTINCANHAN(ID),
	MaKH int identity(1,1) primary key 
)

create table TAIXE
(
	ID int foreign key references THONGTINCANHAN(ID),
	MaTX int identity(1,1) primary key,
	MaKV int,
	BienSoXe nvarchar(15),
	CMND nvarchar(15),
	SoTaiKhoan nvarchar(15),
	PhiThueChan bigint 
)

create table DONHANG
(
	MaDH int identity(1,1) primary key,
	MaKH int,
	NgayDat date,
	NgayGiao date,
	TinhTrang nvarchar(20),
	PhiVanChuyen bigint,
	TongTien bigint,
	MaKV int
)

create table THUNHAPTAIXE
(
	MaTX int,
	MaDH int
	constraint PK_THUNHAPTAIXE primary key (MaTX, MaDH)
)

create table KHUVUC
(
	MaKV int identity(1,1) primary key,
	TenKV nvarchar(50)
)

create table CHINHANH
(
	MaCN int identity(1,1) primary key,
	DiaChi nvarchar(150),
	MaHD int
)

create table SANPHAM
(
	MaSP int identity(1,1) primary key,
	TenSP nvarchar(150), 
	Gia bigint,
	MoTa nvarchar(200)
)

create table CUNGCAP_SP
(
	MaCN int,
	MaSP int,
	SLTonKho int
	constraint PK_CUNGCAPSP primary key (MaCN, MaSP)
)

create table CHITIETDONHANG
(
	MaDH int,
	MaSP int,
	SoLuong int,
	ThanhTien bigint
	constraint PK_CHITIETDONHANG primary key (MaDH, MaSP)
)

create table DOITAC
(
	MaDT int identity(1,1) primary key,
	TenDT nvarchar(150),
	NguoiDaiDien nvarchar(100),
	ThanhPho nvarchar(60),
	Quan nvarchar(60),
	SoChiNhanh int,
	SLDonHang int,
	LoaiHang nvarchar(50),
	DiaChiKinhDoanh nvarchar(150),
	SoDienThoai nvarchar(12)
)

create table HOPDONG
(
	MaHD int identity(1,1) primary key,
	MaDT int,
	MaSoThue nvarchar(20),
	NguoiDaiDien nvarchar(100),
	SoChiNhanhDK int,
	NgayBD date,
	NgayKT date,
	KichHoat int,
	PhiHoaHong bigint
)

alter table TAIXE add
	constraint FK_TAIXE_KHUVUC foreign key (MaKV) references KHUVUC (MaKV)

alter table DONHANG add
	constraint FK_DONHANG_KHACHHANG foreign key (MaKH) references KHACHHANG (MaKH),
	constraint FK_DONHANG_KHUVUC foreign key (MaKV) references KHUVUC (MaKV)

alter table THUNHAPTAIXE add
	constraint FK_THUNHAPTAIXE_TAIXE	foreign key (MaTX) references TAIXE (MaTX),
	constraint FK_THUNHAPTAIXE_DONHANG	foreign key (MaDH) references DONHANG (MaDH)

alter table CHITIETDONHANG add
	constraint FK_CHITIETDONHANG_DONHANG	foreign key (MaDH) references DONHANG (MaDH),
	constraint FK_CHITIETDONHANG_SANPHAM	foreign key (MaSP) references SANPHAM (MaSP)

alter table CUNGCAP_SP add
	constraint FK_CUNGCAPSP_CHINHANH	foreign key (MaCN) references CHINHANH (MaCN),
	constraint FK_CUNGCAPSP_SANPHAM		foreign key (MaSP) references SANPHAM (MaSP)

alter table HOPDONG add
	constraint FK_HOPDONG_DOITAC	foreign key (MaDT) references DOITAC (MaDT)

alter table CHINHANH add	
	constraint FK_CHINHANH_HOPDONG	foreign key (MaHD) references HOPDONG (MaHD)
