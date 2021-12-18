use DBMS_ThucHanh_Nhom15
go
------------------------------------------------------------------------------
---- Tinh Huong 1
-- Transaction 1:  Nhân viên giảm giá cho các sản phẩm được cung cấp bởi chi nhánh A
alter procedure NV_giamgia_SP
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
create procedure XemSP_CuaCN
	@MaCN nvarchar(10)
as
begin tran
	SET TRAN ISOLATION LEVEL READ UNCOMMITTED		
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
-- Tinh Huong 2
--Transaction 1: Nhân viên giảm giá phí vận chuyển cho các đơn hàng ở khu vực A
create procedure PhiShipDH 
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
create procedure XemDH_TaiKV
	@MaDH nvarchar(10)
as
begin tran
	SET TRAN ISOLATION LEVEL READ UNCOMMITTED		
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
-- Tinh Huong 3
--Transaction 1: Tài xế cập nhật trạng thái đơn hàng “Đã nhận”
alter procedure TX_danhan_DH 
	@MaDH nvarchar(10),
	@TinhTrang nvarchar(20)
as
begin tran 
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
alter procedure KH_huyDH
	@MaDH nvarchar(10),
	@TinhTrang nvarchar(20)
as
begin tran
	SET TRAN ISOLATION LEVEL READ COMMITTED	
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

--// nhưng lúc này đơn hàng đã được xác nhận bởi shipper và thông tin hủy đơn hàng của khách hàng bị mất. 

-----------------------------------------
--Tình huống 4: Lỗi không đọc lại được dữ liệu (Unrepeatable Read)
--Lúc đầu số lượng của sản phẩm A trong chi tiết đơn hàng B thành 20
--Transaction 1: Tài xế xem lại chi tiết đơn hàng thì thấy số lượng sản phẩm là 10. 
alter procedure TX_xem_CTDH
	@MaDH nvarchar(10),
	@MaSP nvarchar(10)
as
begin tran 
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
alter procedure KH_capnhat_SL
	@MaDH nvarchar(10),
	@MaSP nvarchar(10),
	@SoLuong int
as
begin tran 
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
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

--Tình huống 5: 
--Transaction 1: Khách hàng xem danh sách các sản phẩm giá lớn hơn 9tr đồng. (n sản phẩm)
alter procedure Xem_SP_voigia
	@Gia int
as
begin tran 
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
alter procedure NV_themSP
	@MaSP nvarchar(10),
    @TenSP nvarchar(150),
	@Gia bigint,
    @MoTa nvarchar(200),
	@GiaTieuChuan int
as
begin tran
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
    if exists (select * from SANPHAM where @MaSP = MaSP)
    begin
        rollback tran
        raiserror(N'Sản phẩm đã tồn tại!', 10, 1)
    end

	else
	begin
		insert SANPHAM (MaSP, TenSP, MoTa, Gia) values (@MaSP, @TenSP, @MoTa, @Gia)
	
		if @Gia <= @GiaTieuChuan 
		begin 
			raiserror (N'Sản phẩm được thêm phải có giá lớn hơn giá tiêu chuẩn', 10, 1)
			rollback tran
		end
		else
			commit tran
	end
go

--// Transaction 1: Khách hàng xem danh sách các sản phẩm lại thì thấy có n + 1 sản phẩm thỏa điều kiện. 


--Tình huống 6:
--Transaction 1: Nhân viên A xem danh sách các hợp đồng có ngày bắt đầu trễ hơn 05/08/2020. (n hợp đồng)
create procedure Xem_DanhSachHD
	@NgayBD date
as
begin tran 
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
alter procedure NV_themHD
	@MaHD nvarchar(10),
    @MaDT nvarchar(10),
	@MaCN nvarchar(10),
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
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

	if exists (select * from HOPDONG where HOPDONG.MaHD = @MaHD)
	begin
		raiserror(N'Hợp đồng đã tồn tại.', 10, 1);
		rollback
	end
	else
	begin
		if not exists (select * from DOITAC where DOITAC.MaDT = @MaDT)
		begin
			raiserror(N'Đối tác không tồn tại.', 10, 1);
			rollback
		end
		else
		begin
			if not exists (select * from CHINHANH where CHINHANH.MaCN = @MaCN)
			begin
				raiserror(N'Chi nhánh không tồn tại.', 10, 1);
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
					INSERT HOPDONG(MaHD, MaDT, MaCN, MaSoThue, NguoiDaiDien, SoChiNhanhDK, NgayBD, NgayKT, KichHoat, PhiHoaHong) 
					VALUES (@MaHD, @MaDT, @MaCN, @MaSoThue, @NguoiDaiDien, @SoChiNhanhDK, @NgayBD, @NgayKT, @KichHoat, @PhiHoaHong)

					if @NgayBD <= @NgayBDTieuChuan
					begin 
						raiserror (N'Hợp đồng được thêm phải có ngày bắt đầu trễ hơn ngày bắt đầu tiêu chuẩn', 10, 1);
						rollback tran
					end
					else
						commit tran
				end
			end
		end
	end
go
--// Transaction 1: Nhân viên A xem danh sách các hợp đồng lại thì thấy có n + 1 hợp đồng thỏa điều kiện. 






