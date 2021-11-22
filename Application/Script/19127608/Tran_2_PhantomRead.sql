
CREATE PROCEDURE NHANVIEN_THEM_HOPDONG
	@MaHD NVARCHAR(10),
	@MaDT NVARCHAR(10),
	@MaCN NVARCHAR(10),
	@MaSoThue NVARCHAR(20),
	@NguoiDaiDien NVARCHAR(100),
	@SoChiNhanhDK INT,
	@HieuLuc DATE,
	@NgayBD DATE,
	@NgayKT DATE,
	@KichHoat INT,
	@PhiHoaHong BIGINT
AS
BEGIN
	BEGIN TRAN
		--Kiểm tra xem hợp đồng có tồn tại không
		IF EXISTS (SELECT * FROM HOPDONG WHERE HOPDONG.MaHD = @MaHD)
		BEGIN
			ROLLBACK
			raiserror(N'Hợp đồng đã tồn tại.', 16, 1)
		END
		--Kiểm tra đối tác có tồn tại không
		IF NOT EXISTS (SELECT * FROM DOITAC WHERE DOITAC.MaDT = @MaDT)
		BEGIN
			ROLLBACK
			raiserror(N'Đối tác không tồn tại.', 16, 1)
		END
		--Kiểm tra chi nhánh có tồn tại không
		IF NOT EXISTS (SELECT * FROM CHINHANH WHERE CHINHANH.MaCN = @MaCN)
		BEGIN
			ROLLBACK
			raiserror(N'Chi nhánh không tồn tại.', 16, 1)
		END
		--Kiểm tra xem ngày bắt đầu có nhỏ hơn ngày kết thúc không
		IF @NgayBD > @NgayKT
		BEGIN
			ROLLBACK
			raiserror(N'Ngày bắt đầu phải bé hơn ngày kết thúc.', 16, 1)
		END
		INSERT HOPDONG(MaHD, MaDT, MaCN, MaSoThue, NguoiDaiDien, SoChiNhanhDK, HieuLuc, NgayBD, NgayKT, KichHoat, PhiHoaHong) 
		VALUES (@MaHD, @MaDT, @MaCN, @MaSoThue, @NguoiDaiDien, @SoChiNhanhDK, @HieuLuc, @NgayBD, NgayKT, @KichHoat, @PhiHoaHong)
	COMMIT TRAN
END
GO

EXEC NHANVIEN_THEM_HOPDONG @MaHD = N'HD0004', @MaDT = N'DT7897', @MaCN = N'CN000Y', @MaSoThue = N'IFDA6', @NguoiDaiDien = N'Blais', @SoChiNhanhDK = 2,
								@HieuLuc = CAST(N'2019-08-12' AS Date), @NgayBD = CAST(N'2019-02-16' AS Date), NgayKT = CAST(N'2021-08-27' AS Date),
								@KickHoat = 1, @PhiHoaHong = 1000000
GO
