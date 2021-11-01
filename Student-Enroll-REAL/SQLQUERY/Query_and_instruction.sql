-- the whole database query
-- database
drop database STUDENTENROL -- xóa database cũ, nếu chưa có database cũ, không thực thi query này
-- database creation
create database STUDENTENROL -- tạo database mới
-- database focusing
use STUDENTENROL
-- table --tạo bảng, bôi đen từ begin đến end sau đó bấm F5 để thực thi
begin
create table DOI_TUONG_DU_THI
(
ma_doi_tuong int PRIMARY KEY,
ten_doi_tuong	nvarchar(50)
)
create table NGANH
(
ma_nganh int PRIMARY KEY,
ten_nganh nvarchar(50),
diem_chuan float
)
create table TRUONG
(
ma_truong int primary key,
ten_truong nvarchar(50)
)
create table NGAY_THI
(
ngay_thi date primary key
)
create table TAI_KHOAN_DANG_NHAP
(
ten_tai_khoan varchar(10) primary key,
mat_khau varchar(20),
admin bit
)
create table MON_THI
(
ma_mon int primary key,
ten_mon nvarchar(10)
)
create table KHOI
(
ma_khoi varchar(3) primary key
)
create table CHI_TIET_KHOI_THI
(
ma_khoi varchar(3),
ma_mon int ,
PRIMARY KEY(ma_khoi,ma_mon)
)
create table PHONG_THI
(
ma_phong_thi int primary key,
so_phong varchar(4),
dia_diem nvarchar(50)
)
create table DIEM_THI
(
so_phach int NOT NULL IDENTITY(99,1) primary key,
diem float,
da_cham_diem bit,
so_bao_danh int,
ma_mon int,
)
create table PHIEU_DKDT
(
so_phieu int PRIMARY KEY,
ho_va_ten nvarchar(50),
ngay_sinh smalldatetime,
khu_vuc varchar(5),
nam_tot_nghiep_th int,
he_th varchar(3),
dia_chi_bao_tin nvarchar(50),
noi_sinh nvarchar(50),
ho_khau nvarchar(50),
dang_ky_thi varchar(3),
ma_khoi varchar(3),
ma_truong int,
ma_nganh int,
ma_doi_tuong int
)
create table  DU_LIEU_THI_SINH
(
so_bao_danh int NOT NULL IDENTITY(1000,1) PRIMARY KEY,
ho_va_ten nvarchar(50),
ngay_sinh date,
noi_sinh nvarchar(50),
dia_chi_bao_tin nvarchar(50),
le_phi_thi money,
tong_diem float,
trung_tuyen bit,
ma_phong_thi int,
ma_nganh int,
ma_khoi varchar(3)
)
create table TRANG_THAI_KI_THI
(
trang_thai int primary key
)
end
GO
-- constraint -- tương tự như tạo bảng
begin 
--	CHI TIET KHOI THI
ALTER TABLE CHI_TIET_KHOI_THI
ADD CONSTRAINT FK_CTKT_MA_MON
FOREIGN KEY (ma_mon)
REFERENCES MON_THI(ma_mon)
ALTER TABLE CHI_TIET_KHOI_THI
ADD CONSTRAINT FK_CTKT_MA_KHOI
FOREIGN KEY (ma_khoi)
REFERENCES KHOI(ma_khoi)
-- PHIEU_DKDT
ALTER TABLE PHIEU_DKDT
ADD CONSTRAINT FK_PHIEU_DKDT_MA_KHOI
FOREIGN KEY (ma_khoi)
REFERENCES KHOI(ma_khoi)
ALTER TABLE PHIEU_DKDT
ADD CONSTRAINT FK_PHIEU_DKDT_MA_TRUONG
FOREIGN KEY(ma_truong)
REFERENCES TRUONG(ma_truong)
ALTER TABLE PHIEU_DKDT
ADD CONSTRAINT FK_PHIEU_DKDT_MA_NGANH
FOREIGN KEY(ma_nganh)
REFERENCES NGANH(ma_nganh)
ALTER TABLE PHIEU_DKDT
ADD CONSTRAINT FK_PHIEU_DKDT_MA_DOI_TUONG
FOREIGN KEY(ma_doi_tuong)
REFERENCES DOI_TUONG_DU_THI(ma_doi_tuong)
ALTER TABLE PHIEU_DKDT
ADD CONSTRAINT CHECK_HE_TH_CB_KCB
CHECK (he_th IN ('CB','KCB'))
ALTER TABLE PHIEU_DKDT
ADD CONSTRAINT CHECK_DANG_KY_THI_CB_KCB
CHECK (dang_ky_thi IN ('CB','KCB'))
-- DU_LIEU_THI_SINH
ALTER TABLE DU_LIEU_THI_SINH
ADD CONSTRAINT FK_DU_LIEU_THI_SINH_PHONG_THI
FOREIGN KEY(ma_phong_thi)
REFERENCES PHONG_THI(ma_phong_thi)
ALTER TABLE DU_LIEU_THI_SINH
ADD CONSTRAINT FK_DU_LIEU_THI_SINH_NGANH
FOREIGN KEY(ma_nganh)
REFERENCES NGANH(ma_nganh)
ALTER TABLE DU_LIEU_THI_SINH
ADD CONSTRAINT FK_DU_LIEU_THI_SINH_KHOI
FOREIGN KEY(ma_khoi)
REFERENCES KHOI(ma_khoi)
-- DIEM_THI
ALTER TABLE DIEM_THI
ADD CONSTRAINT FK_DIEM_THI_SO_BAO_DANH
FOREIGN KEY(so_bao_danh)
REFERENCES DU_LIEU_THI_SINH(so_bao_danh)
ALTER TABLE DIEM_THI
ADD CONSTRAINT FK_DIEM_THI_MA_MON
FOREIGN KEY(ma_mon)
REFERENCES MON_THI(ma_mon)
end 
GO
-- trigger -- execute từng trigger một, bôi đen từng trigger một
begin
--TRIGGER 1: INSERT DU_LIEU_THI_SINH TONGDIEM = 0
--insert into DU_LIEU_THI_SINH values (3,'nguyen van b',null,'py','py',30000,20.5,null,null,null)
CREATE TRIGGER TONGDIEM_INSERT_DULIEUTHISINH
ON DU_LIEU_THI_SINH
FOR INSERT 
AS 
BEGIN
	DECLARE @so_bao_danh int
	SELECT @so_bao_danh = so_bao_danh FROM INSERTED 

	UPDATE DU_LIEU_THI_SINH SET tong_diem=0 WHERE so_bao_danh =@so_bao_danh
	PRINT ('Da cap nhat tong diem = 0 cho DULIEUTHISINH')
END
--TRIGGER 2: UPDATE DU_LIEU_THI_SINH TONGDIEM = DIEM 3 MON CONG LAI
--update DU_LIEU_THI_SINH SET tong_diem=8 WHERE so_bao_danh =2 

CREATE TRIGGER TONGDIEM_UPDATE_DULIEUTHISINH
ON DU_LIEU_THI_SINH
FOR UPDATE
AS
BEGIN
	DECLARE @so_bao_danh int, @tong_diem float, @ma_nganh int, @diem_chuan float
	SELECT @so_bao_danh = so_bao_danh FROM INSERTED 
	SELECT @ma_nganh = ma_nganh from inserted

	select @diem_chuan = diem_chuan from NGANH
	where @ma_nganh = ma_nganh

	SELECT @tong_diem=SUM(diem) FROM DIEM_THI
	WHERE so_bao_danh = @so_bao_danh

	UPDATE DU_LIEU_THI_SINH SET tong_diem=@tong_diem WHERE so_bao_danh =@so_bao_danh

	UPDATE DU_LIEU_THI_SINH SET trung_tuyen = 1 WHERE @ma_nganh = ma_nganh AND tong_diem >= @diem_chuan
	UPDATE DU_LIEU_THI_SINH SET trung_tuyen = 0 WHERE @ma_nganh = ma_nganh AND tong_diem < @diem_chuan
	PRINT ('Da cap nhat ket qua trung tuyen cho DULIEUTHISINH')

	PRINT ('Da cap nhat tong diem dung cho DULIEUTHISINH')
END

--TRIGGER 3: INSERT DIEM
--insert into DIEM_THI values(123,9,1,2,1)

CREATE TRIGGER TONGDIEM_INSERT_DIEMTHI
ON DIEM_THI
FOR INSERT
AS
BEGIN
	DECLARE @so_bao_danh int, @tong_diem float
	SELECT @so_bao_danh = so_bao_danh FROM INSERTED  
	SELECT @tong_diem=SUM(diem) FROM DIEM_THI
	WHERE so_bao_danh = @so_bao_danh

	UPDATE DU_LIEU_THI_SINH SET tong_diem=@tong_diem WHERE so_bao_danh =@so_bao_danh
	PRINT ('Da cap nhat tong diem moi cho DULIEUTHISINH')
END
--TRIGGER 4: UPDATE DIEM
--update DIEM_THI set diem=10 where so_phach=123 and so_bao_danh = 2
CREATE TRIGGER TONGDIEM_UPDATE_DIEMTHI
ON DIEM_THI
FOR UPDATE
AS 
BEGIN
	DECLARE @so_bao_danh int, @tong_diem float
	SELECT @so_bao_danh = so_bao_danh FROM INSERTED  
	SELECT @tong_diem=SUM(diem) FROM DIEM_THI
	WHERE so_bao_danh = @so_bao_danh

	UPDATE DU_LIEU_THI_SINH SET tong_diem=@tong_diem WHERE so_bao_danh =@so_bao_danh
	PRINT ('Da cap nhat tong diem moi cho DULIEUTHISINH')
END

--TRIGGER 5: INSERT diem_chuan trong table NGANH, SET diem_chuan = 0
CREATE TRIGGER DIEMCHUAN_INSERT_NGANH
ON NGANH
FOR INSERT 
AS 
BEGIN
	DECLARE @ma_nganh int
	SELECT @ma_nganh = ma_nganh FROM INSERTED 

	UPDATE NGANH SET diem_chuan=0 WHERE @ma_nganh = ma_nganh
	PRINT ('Da cap nhat diem chuan = 0 cho NGANH')
END
--TRIGGER 6: UPDATE DIEMCHUAN VÀ CẬP NHẬT KẾT QUẢ ĐẬU/RỚT
CREATE TRIGGER TRUNGTUYEN_UPDATE_DIEMCHUAN
ON NGANH
FOR UPDATE
AS
BEGIN
	DECLARE @ma_nganh int, @diem_chuan float
	SELECT @ma_nganh = ma_nganh FROM INSERTED  
	SELECT @diem_chuan = diem_chuan FROM INSERTED 

	UPDATE DU_LIEU_THI_SINH SET trung_tuyen = 1 WHERE @ma_nganh = ma_nganh AND tong_diem >= @diem_chuan
	UPDATE DU_LIEU_THI_SINH SET trung_tuyen = 0 WHERE @ma_nganh = ma_nganh AND tong_diem < @diem_chuan
	PRINT ('Da cap nhat ket qua trung tuyen cho DULIEUTHISINH')
END
--UPDATE NGANH SET diem_chuan = 28 WHERE ma_nganh=2
select * from DIEM_THI

--drop trigger DULIEUTHISINH_INSERT_DIEMTHI
CREATE TRIGGER DULIEUTHISINH_INSERT_DIEMTHI
ON DU_LIEU_THI_SINH
FOR INSERT
AS
BEGIN
	declare @so_bao_danh int,@ma_khoi varchar(3),@ma_mon int
	select @so_bao_danh = so_bao_danh from INSERTED
	select @ma_khoi = ma_khoi from INSERTED

	DECLARE CURSOR_INSERTER CURSOR FOR
	SELECT ma_mon from CHI_TIET_KHOI_THI where ma_khoi = @ma_khoi
	OPEN CURSOR_INSERTER
	FETCH NEXT FROM CURSOR_INSERTER INTO @ma_mon
	while @@FETCH_STATUS = 0
	BEGIN
		insert into DIEM_THI values(0,0,@so_bao_danh,@ma_mon)
		FETCH NEXT FROM CURSOR_INSERTER INTO @ma_mon
	END
	CLOSE CURSOR_INSERTER
	DEALLOCATE CURSOR_INSERTER
END

--select * from DU_LIEU_THI_SINH
--select * from DIEM_THI
--insert into DU_LIEU_THI_SINH values('Hoàng Ái Ly','2001-09-09','Thái Bình','Thái Bình',30000,0,0,1,1,'A00')
--delete from DU_LIEU_THI_SINH
--truncate table DIEM_THI
end 
GO
-- initial data values  -- bôi đen từ begin đến end để bổ sung dữ liệu
begin
insert into DOI_TUONG_DU_THI values(1,N'THÍ SINH THPT')
insert into DOI_TUONG_DU_THI values(2,N'THÍ SINH TỰ DO')
select* from DOI_TUONG_DU_THI

insert into NGANH values(1,N'TRÍ TUỆ NHÂN TẠO',NULL)
insert into NGANH values(2,N'CÔNG NGHỆ THÔNG TIN',NULL)
insert into NGANH values(3,N'KỸ THUẬT PHẦN MỀM',NULL)
insert into NGANH values(4,N'QUẢN TRỊ KINH DOANH',NULL)
insert into NGANH values(5,N'KINH TẾ ĐỐI NGOẠI',NULL)
insert into NGANH values(6,N'BÁO CHÍ',NULL)
insert into NGANH values(7,N'TRUYỀN THÔNG ĐA PHƯƠNG TIỆN',NULL)
insert into NGANH values(8,N'TÂM LÝ HỌC',NULL)
select* from NGANH

insert into TRUONG values(1,N'TRƯỜNG ĐẠI HỌC CÔNG NGHỆ THÔNG TIN')
insert into TRUONG values(2,N'TRƯỜNG ĐẠI HỌC KHOA HỌC TỰ NHIÊN')
insert into TRUONG values(3,N'TRƯỜNG ĐẠI HỌC KINH TẾ QUỐC DÂN')
insert into TRUONG values(4,N'TRƯỜNG ĐẠI HỌC FPT')
insert into TRUONG values(5,N'TRƯỜNG ĐẠI HỌC KHOA HỌC XÃ HỘI VÀ NHÂN VĂN')
select* from TRUONG

insert into NGAY_THI values('2021-06-20')
select* from NGAY_THI

insert into TAI_KHOAN_DANG_NHAP values('ngotvjpr0','benhuhatvung',0)
insert into TAI_KHOAN_DANG_NHAP values('tuthegod','meokhongbietkhoc',1)
insert into TAI_KHOAN_DANG_NHAP values('yasuo','0150',0)
-- commit ngày 7 tháng 2 năm 2021 vào lúc 11 giờ 8 phút sáng
insert into TAI_KHOAN_DANG_NHAP values('phungthanhtu','123456',0)
insert into TAI_KHOAN_DANG_NHAP values('nguyenthanhnga','123456',0)
insert into TAI_KHOAN_DANG_NHAP values('nguyenvandat','123456',0)
insert into TAI_KHOAN_DANG_NHAP values('nguyentrunghieu','123456',0)
insert into TAI_KHOAN_DANG_NHAP values('admin','admin',1)
select* from TAI_KHOAN_DANG_NHAP

insert into MON_THI values(1,N'Toán')
insert into MON_THI values(2,N'Văn')
insert into MON_THI values(3,N'Anh')
insert into MON_THI values(4,N'Lý')
insert into MON_THI values(5,N'Hóa')
insert into MON_THI values(6,N'Sinh')
insert into MON_THI values(7,N'Sử')
insert into MON_THI values(8,N'Địa')
insert into MON_THI values(9,N'GDCD')
select* from MON_THI

insert into KHOI values('A00')
insert into KHOI values('A01')
insert into KHOI values('B00')
insert into KHOI values('B01')
insert into KHOI values('C00')
insert into KHOI values('C01')
insert into KHOI values('D01')
select* from KHOI

insert into CHI_TIET_KHOI_THI values('A00',1)
insert into CHI_TIET_KHOI_THI values('A00',2)
insert into CHI_TIET_KHOI_THI values('A00',4)
insert into CHI_TIET_KHOI_THI values('A01',1)
insert into CHI_TIET_KHOI_THI values('A01',3)
insert into CHI_TIET_KHOI_THI values('A01',4)
insert into CHI_TIET_KHOI_THI values('B00',1)
insert into CHI_TIET_KHOI_THI values('B00',5)
insert into CHI_TIET_KHOI_THI values('B00',6)
insert into CHI_TIET_KHOI_THI values('C00',2)
insert into CHI_TIET_KHOI_THI values('C00',7)
insert into CHI_TIET_KHOI_THI values('C00',8)
insert into CHI_TIET_KHOI_THI values('C01',1)
insert into CHI_TIET_KHOI_THI values('C01',2)
insert into CHI_TIET_KHOI_THI values('C01',4)
insert into CHI_TIET_KHOI_THI values('D01',1)
insert into CHI_TIET_KHOI_THI values('D01',2)
insert into CHI_TIET_KHOI_THI values('D01',3)
select* from CHI_TIET_KHOI_THI

insert into PHONG_THI values(1,'A121',N'THPT CHUYÊN LÊ HỒNG PHONG TPHCM')
insert into PHONG_THI values(2,'A120',N'THPT CHUYÊN LÊ HỒNG PHONG TPHCM')
insert into PHONG_THI values(3,'A024',N'THPT CHUYÊN LÊ HỒNG PHONG TPHCM')
insert into PHONG_THI values(4,'D101',N'THPT CHUYÊN LÊ HỒNG PHONG TPHCM')
insert into PHONG_THI values(5,'D201',N'THPT CHUYÊN LÊ HỒNG PHONG TPHCM')
insert into PHONG_THI values(6,'D207',N'THPT CHUYÊN LÊ HỒNG PHONG TPHCM')
select* from PHONG_THI

truncate table TRANG_THAI_KI_THI
insert into TRANG_THAI_KI_THI values(0)
-- 0 : initial
-- 1 : registering finished
-- 2 : scoring finished
end 
GO

drop proc collectGarbage
drop proc insertThiSinh

--DELETE cursor and FLAg -- tạo proc cho db
begin

--DELETE cursor and FLAg -- tạo proc cho db
CREATE PROC collectGarbage
AS
BEGIN
	CLOSE curDKDT
	CLOSE curPhongThi
	DEALLOCATE curDKDT
	DEALLOCATE curPhongThi
END
----------------------------------

--Click finish register
CREATE PROC insertThiSinh
AS
--Cursor -- code will be run when finish registering
BEGIN
	--Cursor PhongThi
	DECLARE @ma_phong_thi int
	DECLARE curPhongThi CURSOR
	FOR SELECT ma_phong_thi FROM dbo.PHONG_THI
	OPEN curPhongThi
	FETCH NEXT FROM curPhongThi INTO @ma_phong_thi
	--DEALLOCATE curPhongThi
-------------------------------------------------------------------
	--Cursor DKDT
	DECLARE @soPhieu int,
			@ho_va_ten nvarchar(50),
			@ngay_sinh date,
			@noi_sinh nvarchar(50),
			@dia_chi_bao_tin nvarchar(50),
			@ma_khoi varchar(3),
			@ma_nganh int
	DECLARE @full int
	SET @full = 0
	DECLARE curDKDT CURSOR
	FOR SELECT so_phieu, ho_va_ten, ngay_sinh, noi_sinh, dia_chi_bao_tin, ma_nganh, ma_khoi FROM dbo.PHIEU_DKDT
	--DEALLOCATE curDKDT
	OPEN curDKDT --Open curosr DKDT
	FETCH NEXT FROM curDKDT INTO @soPhieu, @ho_va_ten, @ngay_sinh, @noi_sinh, @dia_chi_bao_tin, @ma_nganh, @ma_khoi
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
	--Cursor loop phòng thi--
		IF (@full = 20)
			BEGIN
				OPEN curPhongThi
				SET @full = 0
				FETCH NEXT FROM curPhongThi INTO @ma_phong_thi
			END
		ELSE
			BEGIN
				SET @full = @full + 1;
			END
	--Get Insert Data---------------------------------------------------------------------
		INSERT INTO DU_LIEU_THI_SINH (ho_va_ten, ngay_sinh, noi_sinh, dia_chi_bao_tin, le_phi_thi, ma_phong_thi, ma_nganh, ma_khoi)
								VALUES(@ho_va_ten, @ngay_sinh, @noi_sinh, @dia_chi_bao_tin, 30000, @ma_phong_thi, @ma_nganh, @ma_khoi)
	--curDKDT.moveNext---------------------------------------------------------------------
		FETCH NEXT FROM curDKDT INTO @soPhieu, @ho_va_ten, @ngay_sinh, @noi_sinh, @dia_chi_bao_tin, @ma_nganh, @ma_khoi
	END

	EXEC collectGarbage
END
----------------------------------


end go


--step 1: execute all
--step 2: EXEC insertThiSinh

------ TỪ DÒNG NÀY TRỞ ĐI, VUI LÒNG KHÔNG EXECUTE
EXEC insertThiSinh
update TRANG_THAI_KI_THI set trang_thai = 1

-- confirm diem
select trang_thai from TRANG_THAI_KI_THI

select* from DIEM_THI
SELECT* from DU_LIEU_THI_SINH
-- reset database to zero
delete from DIEM_THI
delete from DU_LIEU_THI_SINH
update TRANG_THAI_KI_THI set trang_thai = 0

select * from KHOI
select * from CHI_TIET_KHOI_THI
end 