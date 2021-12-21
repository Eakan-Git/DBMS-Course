use DBMS_ThucHanh_Nhom15
go
------------------------------------------------------------------------------
---- Tinh Huong 1 - Dirty Read
-- Transaction 1:  Nhân viên giảm giá cho các sản phẩm được cung cấp bởi chi nhánh A
create or alter procedure NV_giamgia_SP2
	@MaCN nvarchar(10),
	@GiaGiam int
as
begin tran 
    if not exists (select * from CHINHANH where @MaCN = MaCN)
    begin
        raiserror (N'Chi nhánh không tồn tại', 10, 1);
		rollback tran
    end

	else
	begin
		update SANPHAM
		set Gia = Gia - @GiaGiam
		from SANPHAM sp, CUNGCAP_SP cc
		where sp.MaSP = cc.MaSP and cc.MaCN = @MaCN 
		waitfor delay '00:00:10'
	end

	if @GiaGiam >= 20000
	begin
		raiserror (N'Giá giảm vượt quá 20000', 10, 1);
		rollback tran
	end

	else 
	begin
		if @GiaGiam < 0
		begin
			raiserror (N'Giá giảm không hợp lệ', 10, 1);
			rollback tran
		end
		else 
			commit tran
	end
go

-- Transaction 2: Khách hàng xem các sản phẩm được cung cấp bởi chi nhánh A 
create or alter procedure XemSP_CuaCN2
	@MaCN nvarchar(10)
as
begin tran
	SET TRAN ISOLATION LEVEL READ COMMITTED		-- sửa ở đây
	if not exists (select * from CHINHANH where @MaCN = MaCN)
	begin
		raiserror (N'Chi nhánh không tồn tại', 10, 1);
		rollback tran
	end
	
	else
	begin
		select * 
		from CUNGCAP_SP cc, SANPHAM sp 
		where sp.MaSP = cc.MaSP and cc.MaCN = @MaCN 
		commit tran
	end
go

-----------------------------------------
-- Tinh Huong 2 - Dirty Read 
--Transaction 1: Nhân viên giảm giá phí vận chuyển cho các đơn hàng ở khu vực A
create or alter procedure PhiShipDH2
	@MaKV nvarchar(10),
	@PhiShipGiamGia int
as
begin tran 
    if not exists (select * from KHUVUC where @MaKV = MaKV)
    begin
        raiserror (N'Khu vực không tồn tại', 10, 1);
		rollback tran
    end

	else
	begin
		update DONHANG
		set PhiVanChuyen = @PhiShipGiamGia
		from DONHANG dh
		where dh.MaKV = @MaKV 
		waitfor delay '00:00:10'
	end

	if exists (select * from DONHANG where MaKV = @MaKV and PhiVanChuyen = @PhiShipGiamGia)
	begin
		raiserror (N'Phí vận chuyển ở khu vực này không được free', 10, 1);
		rollback tran
	end
	else 
		commit tran
go

--Transaction 2: Khách hàng có đơn hàng ở khu vực A xem phí vận chuyển
create or alter procedure XemDH_TaiKV2
	@MaDH nvarchar(10)
as
begin tran
	SET TRAN ISOLATION LEVEL READ COMMITTED		-- sửa ở đây
	if not exists (select * from DONHANG where @MaDH = MaDH)
	begin
		raiserror (N'Đơn hàng không tồn tại', 10, 1);
		rollback tran
	end
	
	else
	begin
		select * 
		from DONHANG dh
		where MaDH = @MaDH
		commit tran
	end
go

-----------------------------------------
-- Tinh Huong 3 - Lost update
--Transaction 1: Tài xế cập nhật trạng thái đơn hàng “Đã nhận”
create or alter procedure TX_danhan_DH2
	@MaDH nvarchar(10),
	@TinhTrang nvarchar(20)
as
begin tran 
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
    if not exists (select * from DONHANG where @MaDH = MaDH)
    begin
        raiserror (N'Đơn hàng không tồn tại', 10, 1);
		rollback tran
    end

	else
	begin
		declare @TinhTrang2 nvarchar(20)
		--Kiểm tra xem đơn hàng đã giao chưa
		set @TinhTrang2 = (select dh.TinhTrang FROM DONHANG dh where dh.MaDH = @MaDH)
		if @TinhTrang2 = N'Đã giao hàng' --nếu đã giao thì không làm gì được nữa
		begin
			raiserror(N'Đơn hàng đã giao, không thể cập nhật.', 10, 1);
			rollback tran
		end 

		else
		begin
			if @TinhTrang2 = N'Đã hủy' --nếu đã hủy rồi thì không làm gì được nữa
			begin
				raiserror(N'Đơn hàng đã hủy, không thể cập nhật.', 10, 1);
				rollback tran
			end

			else
			begin
				waitfor delay '00:00:10'
				update DONHANG
				set TinhTrang = @TinhTrang
				where DONHANG.MaDH = @MaDH
				commit tran
			end
		end
	end
go

--Transaction 2: Khách hủy đơn hàng đó
create or alter procedure KH_huyDH2
	@MaDH nvarchar(10),
	@TinhTrang nvarchar(20)
as
begin tran
	-- SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	if not exists (select * from DONHANG where @MaDH = MaDH)
    begin
        raiserror (N'Đơn hàng không tồn tại', 10, 1);
		rollback tran
    end

	else
	begin
		declare @TinhTrang2 nvarchar(20)
		--Kiểm tra xem đơn hàng đã giao chưa
		set @TinhTrang2 = (select dh.TinhTrang FROM DONHANG dh where dh.MaDH = @MaDH)
		if @TinhTrang2 = N'Đã giao hàng' --nếu đã giao thì không làm gì được nữa
		begin
			raiserror(N'Đơn hàng đã giao, không thể cập nhật.', 10, 1);
			rollback tran
		end 

		else
		begin
			if @TinhTrang2 = N'Đã hủy' --nếu đã hủy rồi thì không làm gì được nữa
			begin
				raiserror(N'Đơn hàng đã hủy, không thể cập nhật.', 10, 1);
				rollback tran
			end

			else
			begin
				update DONHANG
				set TinhTrang = @TinhTrang
				where DONHANG.MaDH = @MaDH
				waitfor delay '00:00:11'
				commit tran
			end
		end
	end
go

-----------------------------------------
--Tình huống 4 - Unrepeatable Read
--Lúc đầu số lượng của sản phẩm A trong chi tiết đơn hàng B thành 20
--Transaction 1: Tài xế xem lại chi tiết đơn hàng thì thấy số lượng sản phẩm là 10. 
create or alter procedure TX_xem_CTDH2
	@MaDH nvarchar(10),
	@MaSP nvarchar(10)
as
begin tran 
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
    if not exists (select * from CHITIETDONHANG where @MaDH = MaDH)
    begin
        raiserror (N'Đơn hàng không tồn tại', 10, 1);
		rollback tran
    end

	else
	begin
		if not exists (select * from CHITIETDONHANG where @MaSP = MaSP)
		begin
			raiserror (N'Sản phẩm không tồn tại', 10, 1);
			rollback tran
		end

		else
		begin
			select * from CHITIETDONHANG where MaDH = @MaDH and MaSP = @MaSP
			waitfor delay '00:00:10'
			select * from CHITIETDONHANG where MaDH = @MaDH and MaSP = @MaSP
			commit tran
		end
	end
go

--Transaction 2: Khách hàng sửa số lượng của sản phẩm A trong chi tiết đơn hàng B thành 10
create or alter procedure KH_capnhat_SL2
	@MaDH nvarchar(10),
	@MaSP nvarchar(10),
	@SoLuong int
as
begin tran 
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	if not exists (select * from CHITIETDONHANG where @MaDH = MaDH)
    begin
        raiserror (N'Đơn hàng không tồn tại', 10, 1);
		rollback tran
    end

	else
	begin
		if not exists (select * from CHITIETDONHANG where @MaSP = MaSP)
		begin
			raiserror (N'Sản phẩm không tồn tại', 10, 1);
			rollback tran
		end

		else
		begin
			update CHITIETDONHANG
			set SoLuong = @SoLuong
			where MaDH = @MaDH and MaSP = @MaSP
			commit tran
		end
	end
go

-----------------------------------------
--Tình huống 5 - Phantom Read
--Transaction 1: Khách hàng xem danh sách các sản phẩm giá lớn hơn 9tr đồng. (n sản phẩm)
create or alter procedure Xem_SP_voigia2
	@Gia int
as
begin tran 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
    if not exists (select * from SANPHAM where Gia > @Gia)
    begin
        raiserror (N' Không tồn tại sản phẩm có giá lớn hơn giá này.', 10, 1);
		rollback tran
    end

	else
	begin
		select count(*) as SoSPLonHonTieuChuan from SANPHAM sp where sp.Gia > @Gia

		waitfor delay '00:00:10'

		select * from SANPHAM sp where sp.Gia > @Gia

		commit tran
	end
go

--Transaction 2: Nhân viên thêm một sản phẩm có giá 1tr đồng 
create or alter procedure NV_themSP2
    @TenSP nvarchar(150),
	@Gia bigint,
    @MoTa nvarchar(200),
	@GiaTieuChuan int
as
begin tran
	--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	insert SANPHAM (TenSP, MoTa, Gia) values (@TenSP, @MoTa, @Gia)
	
	if @Gia <= @GiaTieuChuan 
	begin 
		raiserror (N'Sản phẩm được thêm phải có giá lớn hơn giá tiêu chuẩn', 10, 1)
		rollback tran
	end
	else
		commit tran
go

-----------------------------------------
--Tình huống 6 - Phantom Read
--Transaction 1: Nhân viên A xem danh sách các hợp đồng có ngày bắt đầu trễ hơn 05/08/2020. (n hợp đồng)
create or alter procedure Xem_DanhSachHD2
	@NgayBD date
as
begin tran 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
    if not exists (select * from HOPDONG where NgayBD > @NgayBD)
    begin
        raiserror (N' Không tồn tại hợp đồng có ngày bắt đầu trễ hơn ngày này.', 10, 1);
		rollback tran
    end

	else
	begin
		select count(*) as NgayBDLonHonTieuChuan from HOPDONG hd where hd.NgayBD > @NgayBD

		waitfor delay '00:00:10'

		select * from HOPDONG hd where hd.NgayBD > @NgayBD

		commit tran
	end
go

--Transaction 2: Nhân viên B thêm một hợp đồng có ngày bắt đầu là 12/04/2021
create or alter procedure NV_themHD2
    @MaDT nvarchar(10),
	@MaSoThue nvarchar(20),
	@NguoiDaiDien nvarchar(100),
	@SoChiNhanhDK int,
	@NgayBD date,
	@NgayKT date,
	@KichHoat int,
	@PhiHoaHong bigint,
	@NgayBDTieuChuan date
as
begin tran
	-- SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

	if not exists (select * from DOITAC where DOITAC.MaDT = @MaDT)
	begin
		raiserror(N'Đối tác không tồn tại.', 10, 1);
		rollback
	end
	else
	begin
		if @NgayBD > @NgayKT
		begin
			raiserror(N'Ngày bắt đầu phải bé hơn ngày kết thúc.', 10, 1);
			rollback
		end
		else
		begin
			INSERT HOPDONG(MaDT, MaSoThue, NguoiDaiDien, SoChiNhanhDK, NgayBD, NgayKT, KichHoat, PhiHoaHong) 
			VALUES (@MaDT, @MaSoThue, @NguoiDaiDien, @SoChiNhanhDK, @NgayBD, @NgayKT, @KichHoat, @PhiHoaHong)

			if @NgayBD <= @NgayBDTieuChuan
			begin 
				raiserror (N'Hợp đồng được thêm phải có ngày bắt đầu trễ hơn ngày bắt đầu tiêu chuẩn', 10, 1);
				rollback tran
			end
			else
				commit tran
		end
	end
go

