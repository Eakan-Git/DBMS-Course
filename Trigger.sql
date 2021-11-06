use DBMS_ThucHanh
go
----------------------------------------------------------------------------------------
---- 1) ThanhTien = (SoLuong * Gia) 
-- update SoLuong
create trigger trigger_ThanhTien_updateSL
on CHITIETDONHANG
for insert, update, delete as
if update (SoLuong) 
begin
	if exists (select ctdh.SoLuong from CHITIETDONHANG ctdh where ctdh.SoLuong <= 0)
	begin
		 raiserror(N'Số lượng sản phẩm không hợp lệ', 10, 1)
		 rollback transaction
	end
	
	update CHITIETDONHANG
	set ThanhTien = (ctdh.SoLuong * sp.Gia)
	from CHITIETDONHANG ctdh, inserted i, SANPHAM sp
	where sp.MaSP = i.MaSP and
			ctdh.MaSP = sp.MaSP 
end

-- update Gia
create trigger trigger_ThanhTien_updateSP
on SANPHAM
for insert, update, delete as
if update (Gia)
begin
	if exists (select ctdh.SoLuong from CHITIETDONHANG ctdh where ctdh.SoLuong <= 0)
	begin
		 raiserror(N'Số lượng sản phẩm không hợp lệ', 10, 1)
		 rollback transaction
	end
	
	update CHITIETDONHANG
	set ThanhTien = ctdh.SoLuong * sp.Gia
	from CHITIETDONHANG ctdh, inserted i, SANPHAM sp
	where sp.MaSP = i.MaSP and
			ctdh.MaSP = sp.MaSP 
end


---- 2) Tổng tiền = PhiVanChuyen + sum(CHITIETHOADON.ThanhTien)
create trigger trigger_TongTien
on CHITIETDONHANG
for insert, update, delete as
begin
	update DONHANG
	set TongTien = PhiVanChuyen + (select sum(ctdh.ThanhTien)
									from CHITIETDONHANG ctdh
									where ctdh.MaDH = DONHANG.MaDH)
	where 
		exists (select * from inserted i where i.MaDH = DONHANG.MaDH) or
		exists (select * from deleted d where d.MaDH = DONHANG.MaDH) 
end

------------------------------------------------------------------------
-- Test for trigger 
insert into SANPHAM values (N'sp1001', N'CN662Y', N'Chocolate', 10000, N'Orange')
insert into CHITIETDONHANG (MaDH, MaSP, SoLuong) values (N'DH0005', N'sp1001', 10)

insert into SANPHAM values (N'sp1002', N'CN662Y', N'Hershey kisses', 13000, N'Pink')
insert into CHITIETDONHANG (MaDH, MaSP, SoLuong) values (N'DH0005', N'sp1002', 15)

delete from CHITIETDONHANG where MaDH = N'DH0005' and MaSP = N'sp1001'
delete from SANPHAM where MaSP = N'sp1001'

delete from CHITIETDONHANG where MaDH = N'DH0005' and MaSP = N'sp1002'
delete from SANPHAM where MaSP = N'sp1002'

update CHITIETDONHANG set SoLuong = 7 where MaDH = N'DH0005' and MaSP = N'sp1001'
update CHITIETDONHANG set SoLuong = 3 where MaDH = N'DH0005' and MaSP = N'sp1002'

select * from SANPHAM where MaSP = N'sp1002' 
select * from CHITIETDONHANG where MaDH = N'DH0005'
select * from DONHANG


-- Cập nhật ThanhTien và TongTien trước khi test Trigger
update CHITIETDONHANG
set ThanhTien = ctdh.SoLuong * sp.Gia
from CHITIETDONHANG ctdh, SANPHAM sp
where ctdh.MaSP = sp.MaSP 

update DONHANG
set TongTien = PhiVanChuyen + (select sum(ctdh.ThanhTien)
								from CHITIETDONHANG ctdh
								where ctdh.MaDH = DONHANG.MaDH)

