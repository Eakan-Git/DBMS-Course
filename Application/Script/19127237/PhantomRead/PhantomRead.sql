﻿use DBMS_ThucHanh_Nhom15
go
--------------------------------------------
--Tình huống 5 - Phantom Read
--Transaction 1: Khách hàng xem danh sách các sản phẩm giá lớn hơn 9tr đồng. (n sản phẩm)
create procedure Xem_SP_voigia
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

--Transaction 2: Nhân viên thêm một sản phẩm có giá lon hon 9tr đồng 
create procedure NV_themSP
    @TenSP nvarchar(150),
	@Gia bigint,
    @MoTa nvarchar(200),
	@GiaTieuChuan int
as
begin tran
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	insert SANPHAM (TenSP, MoTa, Gia) values (@TenSP, @MoTa, @Gia)

	if @Gia <= @GiaTieuChuan 
	begin 
		raiserror (N'Sản phẩm được thêm phải có giá lớn hơn giá tiêu chuẩn', 10, 1)
		rollback tran
	end
	else
		commit tran
go
