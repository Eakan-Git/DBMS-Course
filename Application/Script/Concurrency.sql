﻿select * from DONHANG where MaDH = N'DH0005'

create procedure TinhHuong1 (@MaDH nvarchar(10))
as
begin 
	update DONHANG
	set TinhTrang = N'Đã hủy'
	where MaDH = @MaDH

	WaitFor Delay '00:00:10'
update DONHANG
set TinhTrang = N'Đang giao hàng'
where MaDH = N'DH0005'