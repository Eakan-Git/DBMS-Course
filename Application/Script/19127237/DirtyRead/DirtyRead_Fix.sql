use DBMS_ThucHanh_Nhom15
go
----------------------------------------------------------------
--Transaction 1: Nhân viên giảm giá phí vận chuyển cho các đơn hàng ở khu vực A
alter procedure PhiShipDH 
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
alter procedure XemDH_TaiKV
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