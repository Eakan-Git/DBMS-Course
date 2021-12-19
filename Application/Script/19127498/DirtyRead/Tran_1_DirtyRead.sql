exec NV_giamgia_SP @MaCN = 104620, @GiaGiam = 20000

select * 
from CUNGCAP_SP cc, SANPHAM sp 
where sp.MaSP = cc.MaSP and cc.MaCN = 104620
