use DBMS_ThucHanh_Nhom15
go
-------------------------------------------------------------
--Transaction 1: Nhân viên A xem danh sách các hợp đồng có ngày bắt đầu trễ hơn 05/08/2020. (n hợp đồng)
alter procedure Xem_DanhSachHD
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

--Transaction 2: Nhân viên B thêm một hợp đồng có ngày bắt đầu là 06/08/2021
alter procedure NV_themHD
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
