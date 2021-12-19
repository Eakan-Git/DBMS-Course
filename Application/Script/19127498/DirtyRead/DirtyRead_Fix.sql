use DBMS_ThucHanh_Nhom15
go
----------------------------------------------
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
alter procedure XemSP_CuaCN
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
