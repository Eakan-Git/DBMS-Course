create procedure KHACHHANG_XEMSANPHAM 
	@Gia bigint
as
begin
	begin tran
		if not exists (select * from SANPHAM where @Gia = Gia)
		begin
			rollback tran
			raiserror (N'Sản phẩm không tồn tại', 10, 1)
		end

		select * from SANPHAM where @Gia = Gia

		waitfor delay '00:00:10'
		
		select * from SANPHAM where @Gia = Gia

	commit tran
end
go

exec KHACHHANG_XEMSANPHAM @Gia = 1000000 
