create procedure THEMSANPHAM
    @MaSP nvarchar(10),
    @MaCN nvarchar(10),
    @TenSP nvarchar(150),
	@Gia bigint,
    @MoTa nvarchar(200)
as
begin
    begin tran
        if exists (select * from SANPHAM where @MaSP = MaSP)
        begin
            rollback tran
            raiserror(N'Sản phẩm đã tồn tại!', 10, 1)
        end

        if not exists (select * from CHINHANH where @MaCN = MaCN)
        begin
            rollback tran
            raiserror (N'Chi nhánh không tồn tại', 10, 1)
        end

        insert SANPHAM (MaSP, MaCN, TenSP, MoTa, Gia) values (@MaSP, @MaCN, @TenSP, @MoTa, @Gia)
		if @Gia != 1000000 
		begin 
			rollback tran
            raiserror (N'Sản phẩm được thêm phải có giá 1000000', 10, 1)
		end

    commit tran
end
go

execute THEMSANPHAM @MaSP = 'sp0001',  @MaCN = 'CN024X',  @TenSP = N'Pocky', @MoTa = null, @Gia = 1000000  
go
