CREATE PROCEDURE CAPNHAT_DONHANG
	@MaDH NVARCHAR(10),
	@TinhTrang NVARCHAR(20)
AS
BEGIN
	BEGIN TRAN
		--Kiểm tra đơn hàng bằng id xem có tồn tại không
		IF NOT EXISTS (SELECT * FROM DONHANG WHERE DONHANG.MaDH = @MaDH)
		BEGIN 
			ROLLBACK
			raiserror(N'Đơn hàng không tồn tại', 16, 1)
        END
		DECLARE @TinhTrang NVARCHAR(20)
        --Kiểm tra xem đơn hàng đã giao chưa
		SET @TinhTrang = (SELECT DONHANG.TinhTrang FROM DONHANG WHERE DONHANG.MaDH = @MaDH)
		IF @TinhTrang = N'Đã giao hàng' --nếu đã giao thì không làm gì được nữa
		BEGIN
			ROLLBACK
			raiserror(N'Đơn hàng đã giao, không thể cập nhật.', 16, 1)
		END
		ELSE
		IF @TinhTrang = N'Đã hủy' --nếu đã hủy rồi thì không làm gì được nữa
		BEGIN
			ROLLBACK
			raiserror(N'Đơn hàng đã hủy, không thể cập nhật.', 16, 1)
		END
		UPDATE DONHANG
		SET DONHANG.TinhTrang = @TinhTrang
		WHERE DONHANG.MaDH = @MaDH
	COMMIT TRAN
END
GO

-- 3
CREATE PROCEDURE CAPNHAT_SLSP
	@MADH NVARCHAR(10),
	@MASP NVARCHAR(10),
	@SLSP BIGINT,
AS
BEGIN
	BEGIN TRAN
		IF NOT EXISTS (SELECT * FROM CHITIETDONHANG WHERE CHITIETDONHANG.MaSP = @MASP AND CHITIETDONHANG.MaDH=@MADH)
		BEGIN
			ROLLBACK
			RAISERROR(N'Chi tiết đơn hàng không tồn tại', 16, 1)
		END
    UPDATE CHITIETDONHANG 
	SET SoLuong=@SLSP
    WHERE MaSP = @MASP AND MADH = @MADH
	COMMIT TRAN
END
GO


CREATE PROCEDURE UPDATE_GIA
    @MASP NVARCHAR(10),
    @GIA BIGINT
AS
BEGIN
	BEGIN TRAN
		IF NOT EXISTS (SELECT * FROM QUANLYKHO WHERE QUANLYKHO.MASP = @MASP )
		BEGIN
			ROLLBACK
			RAISERROR(N'Sản phẩm không tồn tại ', 16, 1)
		END
    UPDATE SANPHAM
    SET GIA = @GIA
    WHERE MASP = @MASP
	COMMIT TRAN
END

--
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
    commit tran
end
go

execute THEMSANPHAM @MaSP = 'sp0000',  @MaCN = 'cn0000',  @TenSP = N'Chocopie', @MoTa = null, @Gia = 1000000   
go

create procedure KHACHHANG_XEMSANPHAM 
	@Gia bigint
as
begin
	begin tran
		if not exists (select * from SANPHAM where @Gia = 1000000)
		begin
			rollback tran
			raiserror (N'Sản phẩm không tồn tại', 10, 1)
		end

		SELECT * FROM SANPHAM WHERE Gia = 1000000
	commit tran
end
go
exec KHACHHANG_XEMSANPHAM @Gia = 1000000
