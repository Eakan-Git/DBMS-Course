select * from DONHANG where MaDH = N'DH0005'

create procedure TinhHuong1 (@MaDH nvarchar(10))
as
begin 
	update DONHANG
	set TinhTrang = N'Đã hủy'
	where MaDH = @MaDH

	WaitFor Delay '00:00:10'

	if N'Đã hủy' != (select TinhTrang
						from DONHANG
						where MaDH = @MaDH) 
	begin
		raiserror (N'Đơn hàng không hủy được!', 10, 1)
	end
end

EXECUTE TinhHuong1 @MaDH = N'DH0005'


update DONHANG
set TinhTrang = N'Đang giao hàng'
where MaDH = N'DH0005'
