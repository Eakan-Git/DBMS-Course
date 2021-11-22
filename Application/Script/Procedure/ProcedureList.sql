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
