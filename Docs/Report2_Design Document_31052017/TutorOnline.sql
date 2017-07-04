
USE master
GO
if exists (select * from sysdatabases where name='TutorOnline')
		drop database TutorOnline
go

use master
go
CREATE DATABASE [TutorOnline]
go
USE [TutorOnline]
go

CREATE TABLE [Role](
	[RoleId] [int] IdENTITY(1,1) PRIMARY KEY,
	[RoleName] [nvarchar](30) NOT NULL,
);

CREATE TABLE [Status](
	[StatusId] [int] IdENTITY(1,1) PRIMARY KEY,
	[Status] [nvarchar] (50) NOT NULL
);

create table [Parent](
	[ParentId] int Identity(1,1) primary key,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Balance] [money] NULL,
	[Photo] varchar(100) NULL,
	[Description] [text] NULL,	 
	[isDeleted] [bit] not null default 0
);

CREATE TABLE [Student](
	[StudentId] [int] IdENTITY(1,1) PRIMARY KEY,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[ParentId] [int] FOREIGN KEY REFERENCES [Parent](ParentId) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Balance] [money] NULL,
	[Photo] varchar(100) NULL,
	[Description] [text] NULL,	 
	[isDeleted] [bit] not null default 0
);

create table [BackendUser](
	[BackendUserId] [int] Identity(1,1) primary key,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](15) NULL,
	[Country] [nvarchar](15) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Photo] varchar(100) NULL,
	[Description] [text] NULL,	 
	[isDeleted] [bit] not null default 0
);

Create table [CV](
	[CVId] int identity(1,1) primary key,
	[CVLink] [nvarchar](200) null,
	[isRead] bit not null default 0,
	[isApproved] bit not null default 0,
	[isDeleted] bit not null default 0
);

Create table [Tutor](
	[TutorId] int Identity(1,1) primary key,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Balance] [money] NULL,
	[BankId] [nvarchar](24) NULL,
	[Salary] [money] NULL,
	[Photo] varchar(100) NULL,
	[Description] [text] NULL,	 
	[BankName] [nvarchar](200) null,
	[BMemName] [nvarchar](200) null,
	[isDeleted] [bit] not null default 0,
	[CVId] int foreign key references [CV](CVId) not null,
	[Status] int FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL
);

CREATE TABLE [Category](
	[CategoryId] [int] IDENTITY(1,1) PRIMARY KEY,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[isActived] bit not null default 1
);

CREATE TABLE [Subject](
	[SubjectId] [int] IDENTITY(1,1) PRIMARY KEY,
	[SubjectName] [nvarchar](255) NOT NULL,
	[CategoryId] [int] FOREIGN KEY REFERENCES [Category](CategoryId) NOT NULL,
	[Description] [text] NULL,
	[Purpose] [text] NULL,
	[Requirement] [text] NULL,
	[Photo] varchar(100) NULL,
	[isActived] bit not null default 1
);

CREATE TABLE [MaterialType] (
	[MaterialTypeId] [int] IdENTITY(1,1) PRIMARY KEY,
	[MaterialTypeName] [nvarchar](200) NOT NULL
);

CREATE TABLE [Lesson] (
	[LessonId] [int] IdENTITY(1,1) PRIMARY KEY,
	[LessonName] [nvarchar](500) NOT NULL,
	[SubjectId] [int] FOREIGN KEY REFERENCES [Subject](SubjectId) NOT NULL,
	[Content] [text] NULL,
	[isActived] bit not null default 1
);

CREATE TABLE [LearningMaterial] (
	[MaterialId] [int] IdENTITY(1,1) PRIMARY KEY,
	[MaterialUrl] [nvarchar](200) NOT NULL,
	[MaterialTypeId] [int] FOREIGN KEY REFERENCES [MaterialType](MaterialTypeId) NOT NULL,
	[Description] [text] NULL,
	[LessonId] int FOREIGN KEY REFERENCES [Lesson](LessonId) NOT NULL,
	[isDeleted] bit not null default 0
);

CREATE TABLE [Schedule] (
	[ScheduleId] [int] IdENTITY(1,1) PRIMARY KEY,
	[StudentId] [int] FOREIGN KEY REFERENCES [Student](StudentId) NOT NULL,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId) NOT NULL,
	[SlotOrder] [int] NOT NULL,
	[SlotDate] [datetime] NOT NULL,
	[Status] [int] FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL,
	[Type] [int] NOT NULL,
	[CanReason] [text] NULL,
	[Price] [money] NOT NULL
);

CREATE TABLE [Criteria] (
	[CriteriaId] [int] IdENTITY(1,1) PRIMARY KEY,
	[CriteriaName] [nvarchar](255) NOT NULL,  
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId) NOT NULL,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL
);

CREATE TABLE [StudentFeedback] (
	[StudentFeedbackId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	[StudentId] [int] FOREIGN KEY REFERENCES [Student](StudentId) NOT NULL,
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId),
	[FeedbackDate] datetime NOT NULL,
	[Rate] int NOT NULL,
	[Comment] nvarchar(1000) NULL
);

CREATE TABLE [TutorFeedback] (
	[TutorFeedbackId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	[StudentId] [int] FOREIGN KEY REFERENCES [Student](StudentId) NOT NULL,
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId),
	[FeedbackDate] datetime NOT NULL,
	[Comment] nvarchar(1000) NULL
);

CREATE TABLE [TutorFeedbackDetail] (
	[TutorFeedbackId] [int] FOREIGN KEY REFERENCES [TutorFeedback](TutorFeedbackId) NOT NULL,
	[CriteriaId] [int] FOREIGN KEY REFERENCES [Criteria](CriteriaId) NOT NULL,
	PRIMARY KEY (TutorFeedbackId, CriteriaId),
	[CriteriaValue] [int] NOT NULL
);

CREATE TABLE [StudentSubject] (
	[StudentSubjectId] [int] IdENTITY(1,1) PRIMARY KEY,
	[SubjectId] [int] FOREIGN KEY REFERENCES [Subject](SubjectId) NOT NULL,
	[StudentId] [int] FOREIGN KEY REFERENCES [Student](StudentId) NOT NULL,
	CONSTRAINT UC_StudentSub UNIQUE (SubjectId, StudentId),
	[Status] [int] FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL
);

CREATE TABLE [TutorSubject] (
	[TutorSubjectId] [int] IdENTITY(1,1) PRIMARY KEY,
	[SubjectId] [int] FOREIGN KEY REFERENCES [Subject](SubjectId) NOT NULL,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	CONSTRAINT UC_TutorSub UNIQUE (SubjectId, TutorId),
	[Status] [int] FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL
);

CREATE TABLE [TeachSchedule] (
	[TeachScheduleId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderSlot] [nvarchar](150) NULL
);

CREATE TABLE [AuditLog] (
	[AuditId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TableName] nvarchar(100) NOT NULL,
	[ModifierId] [int] NOT NULL,
	[ModifierRole] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[Content] [text] NOT NULL
);

Create table [Question](
	[QuestionId] int Identity(1,1) primary key,
	[Content] nvarchar(1000) not null,
	[LessonId] int foreign key references [Lesson](LessonId) not null
);

create table [Answer](
	[AnswerId] int Identity(1,1) primary key,
	[Content] nvarchar(1000) not null,
	[QuestionId] int foreign key references [Question](QuestionId) not null,
	[isCorrect] bit not null
);

CREATE TABLE [Transaction] (
	[TransactionId] [int] IDENTITY(1,1) PRIMARY KEY,
	[Content] [text] NOT NULL,
	[Amount] [money] NOT NULL,
	[TranDate] [datetime] NOT NULL,
	[UserID] [int] NOT NULL,
	[UserType] int NOT NULL
);

---------------------------------------------------------Insert Data------------------------------------------------------------

--Insert data to Roles Table
INSERT INTO [Role] (RoleName) VALUES ('System Admin');
INSERT INTO [Role] (RoleName) VALUES ('Supporter');
INSERT INTO [Role] (RoleName) VALUES ('Accountant');
INSERT INTO [Role] (RoleName) VALUES ('Manager');
INSERT INTO [Role] (RoleName) VALUES ('Parent');
INSERT INTO [Role] (RoleName) VALUES ('Student');
INSERT INTO [Role] (RoleName) VALUES ('Tutor');
INSERT INTO [Role] (RoleName) VALUES ('PreTutor');

--Insert data to Categories table
INSERT INTO [Category] (CategoryName, [Description]) VALUES ('Tieng Nhat', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES ('Tieng Anh', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES ('Toan', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES ('Vat Ly', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES ('Hoa Hoc', NULL);

--Insert data to Subjects Table
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Tieng nhat so cap', 1, 'Tieng nhat danh cho nguoi moi bat dau', 'Thuoc 2 bang chu cai, buoc dau lam quen voi tieng nhat', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Tieng nhat trung cap', 1, 'Khoa tieng nhat trinh do N4', 'Hoan thanh 30 bai dau giao trinh minanonihongo, trinh do tuong duong n4', 'Da hoan thanh khoa tieng nhat so cap', NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Tieng anh so cap', 2, 'Tieng anh danh cho nguoi moi bat dau', 'Nam ro duoc 12 thi co ban trong tieng anh', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Tieng anh doanh nghiep', 2, 'Tieng anh danh cho cong viec', 'Lam quen va thanh thao cac mau cau giao tiep trong doanh nghiep', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Toan lop 5', 3, 'Toan danh cho hoc sinh lop 5', 'On tap va luyen cac dang bai tap de thi qua cap 1', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Toan lop 7', 3, 'Toan danh cho hoc sinh lop 7', 'Tim hieu va luyen cac dang bai tap nang cao toan lop 7', 'Co nen tang toan tot, can on luyen kien thuc de chuan bi cho cac ki thi cap huyen hoac tinh', NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Vat Ly lop 9', 4, 'Vat Ly danh cho hoc sinh lop 9', 'On tap va luyen cac dang bai tap vat ly lop 9', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Vat Ly lop 11', 4, 'Vat Ly danh cho hoc sinh lop 11', 'Tim hieu va luyen cac dang bai tap nang cao vat ly lop 11', 'Co nen tang vat ly tot, can on luyen kien thuc de chuan bi cho cac ki thi cap huyen hoac tinh', NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Hoa Hoc lop 9', 5, 'Hoa Hoc danh cho hoc sinh lop 9', 'On tap va luyen cac dang bai tap hoa hoc lop 9', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Hoa Hoc lop 11', 5, 'Hoa Hoc danh cho hoc sinh lop 11', 'Tim hieu va luyen cac dang bai tap nang cao hoa hoc lop 11', 'Co nen tang hoa hoc tot, can on luyen kien thuc de chuan bi cho cac ki thi cap huyen hoac tinh', NULL);

--Insert data to Status table
INSERT INTO [Status] (Status) VALUES ('user_active');
INSERT INTO [Status] (Status) VALUES ('user_inactive');
INSERT INTO [Status] (Status) VALUES ('schedule_finish');
INSERT INTO [Status] (Status) VALUES ('schedule_booked');
INSERT INTO [Status] (Status) VALUES ('schedule_cancel');
INSERT INTO [Status] (Status) VALUES ('tusubject_active');
INSERT INTO [Status] (Status) VALUES ('tusubject_inactive');
INSERT INTO [Status] (Status) VALUES ('stusubject_processing');
INSERT INTO [Status] (Status) VALUES ('stusubject_cancel');
INSERT INTO [Status] (Status) VALUES ('stusubject_finish');

--Insert data to Parent table
INSERT INTO [Parent] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (5,'Nguyen','Long',29-4-1995,1,'Phường Phú La','longnbse03804@fpt.edu.vn','longnguyen','longnb','1','Ha Noi',null,'Viet Nam','01632594938',1000000,null,'nhà giàu');
INSERT INTO [Parent] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (5,'Vo','Lam',1-1-1991,1,'Ngách 2 Ngõ 5','lamvt@fpt.edu.vn','lamvo','lamvt','2','Quang Binh',null,'Viet Nam','0198765432',0,null,'nhà nghèo');
INSERT INTO [Parent] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (5,'Nông','Thương',2-2-1992,2,'Đồi số 1','thuongnt@fpt.edu.vn','NTHThuong','thuongnth','3','Cao Bằng',null,'Viet Nam','0123456789',1000000,null,null);

--Insert data to Student table
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,1,'Tran','Vuong',3-3-2003,1,'Phường Phú La','vuong123@gmail.com','TranVietVuong','vuongtv','1','Ha Noi',null,'Viet Nam','01632594938',1000000,null,null);
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,2,'Nguyen','An',4-4-2004,1,null,'An123@gmail.com','An333','An1','2','Ha Noi',null,'Viet Nam','12312341351',0,null,null);
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,3,'Nguyen','Hieu',5-5-2005,1,null,'Hieu123@gmail.com','Hieu123','Hieu1','3','Ha Noi',null,'Viet Nam','124351251234',2000000,null,'Ham học hỏi');

--Insert data to CV table
INSERT INTO [CV] (CVLink) values('CV1.pdf');
INSERT INTO [CV] (CVLink) values('CV2.pdf');
INSERT INTO [CV] (CVLink) values('CV3.pdf');
INSERT INTO [CV] (CVLink) values('CV4.pdf');
INSERT INTO [CV] (CVLink) values('CV5.pdf');
INSERT INTO [CV] (CVLink) values('CV6.pdf');

--Insert data to Tutor table
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,CVId,[Status]) VALUES (7,'Nguyễn','Huyền',3-3-1983,2,'Quận 1','huyenNTK@fpt.edu.vn','Nana','Huyenntk','1','Hồ Chí Minh',null,'Viet Nam','01632594938',1000000,'12345667',100000,null,null,'Tien Phong','HuyenNtk',1,1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,CVId,[Status]) VALUES (7,'Lý','Trọng',4-4-1984,1,null,'Trong123@gmail.com','LyTrong','LyTrong','2','Ha Noi',null,'Viet Nam','12312341351',0,'203498572',50000,null,null,'VPBank','Ly Trong',2,1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,CVId,[Status]) VALUES (7,'Thúy','Kiều',5-5-1985,2,null,'Kieu123@gmail.com','ThuyKieu','ThuyKieu','3','Ha Noi',null,'Viet Nam','124351251234',2000000,'9012367019',100000,null,null,'Vietcombank','Thuy Kieu',3,1);
																																																								
--Insert data to BackendUser table
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (1,'Dương','Quá',25-3-1985,1,null,'Ngoisaoden9@gmail.com','DuongQua','1','Ha Noi','Viet Nam','09777777',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,'Từ','Hải',25-3-1985,1,null,'TuHai@gmail.com','TuHai','1','Ha Noi','Viet Nam','123512132',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (3,'Bá','Kiến',22-2-1989,1,null,'BaKien@gmail.com','BaKien','2','Cao Bằng','Viet Nam','123514154',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (4,'Chí','Phèo',21-4-1987,1,null,'ChiPheo@gmail.com','ChiPheo','3','Đà Nẵng','Viet Nam','12341235',null,null);		

--Insert data to MaterialumentType table
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('PDF');
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('Materialx');
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('Audio file');

--Insert data to Lesson table
INSERT INTO [Lesson] (LessonName,SubjectId,Content) VALUES ('Bài 1', 1, 'Sơ lược về tiếng nhật và 25 chữ hira đầu tiên')
INSERT INTO [Lesson] (LessonName,SubjectId,Content) VALUES ('Bài 2', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản 1, Học Hiragana: 20 chữ tiếp theo; Âm đục + Âm bán đục')
INSERT INTO [Lesson] (LessonName,SubjectId,Content) VALUES ('Bài 3', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản 2, Học Hiragana: Âm dài')


--Insert data to Material table
INSERT INTO [LearningMaterial] (MaterialUrl, MaterialTypeId, [Description], [LessonId]) VALUES ('C:\Users\MSSQLSERVER\Documents\1', 1, 'Tài liệu học cho bài 1',1);
INSERT INTO [LearningMaterial] (MaterialUrl, MaterialTypeId, [Description], [LessonId]) VALUES ('C:\Users\MSSQLSERVER\Music', 3, 'Tài liệu nghe cho bài 2',2);
INSERT INTO [LearningMaterial] (MaterialUrl, MaterialTypeId, [Description], [LessonId]) VALUES ('C:\Users\MSSQLSERVER\Documents\word', 2, 'Tài liệu nghe cho bài 3',3);


-- Insert data to Schedule table
INSERT INTO [Schedule] (StudentId,TutorId,LessonId,SlotOrder,SlotDate,Status,Type,CanReason,Price) VALUES (1, 1, 1, 5, 29-6-2017, 5 ,1, null, 150000);
INSERT INTO [Schedule] (StudentId,TutorId,LessonId,SlotOrder,SlotDate,Status,Type,CanReason,Price) VALUES (2, 1, 1, 8, 29-6-2017, 5 ,1, null, 150000);
INSERT INTO [Schedule] (StudentId,TutorId,LessonId,SlotOrder,SlotDate,Status,Type,CanReason,Price) VALUES (1, 2, 2, 6, 30-6-2017, 6 ,1, 'Trục trặc máy móc', 150000);

--Insert data to Criteria table
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('khả năng nhận biết hiragana',1,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('ý thức của học sinh',1,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('khả năng giao tiếp thu bài mới',1,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('sự đúng giờ của tutor',1,2);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng sư phạm của tutor',1,2);

--Insert data to StudentFeedback
INSERT INTO [StudentFeedback] (TutorId,StudentId,Rate,LessonId,FeedbackDate,Comment) VALUES (1,1,3,1,29-6-2017, null);
INSERT INTO [StudentFeedback] (TutorId,StudentId,Rate,LessonId,FeedbackDate,Comment) VALUES (1,2,4,1,29-6-2017, null);
INSERT INTO [StudentFeedback] (TutorId,StudentId,Rate,LessonId,FeedbackDate,Comment) VALUES (2,1,5,2,30-6-2017, null);

--Insert data to TutorFeedback table
INSERT INTO [TutorFeedback] (TutorId,StudentId,LessonId,FeedbackDate,Comment) VALUES (1,1,1,1, null);
INSERT INTO [TutorFeedback] (TutorId,StudentId,LessonId,FeedbackDate,Comment) VALUES (1,2,1,1, null);
INSERT INTO [TutorFeedback] (TutorId,StudentId,LessonId,FeedbackDate,Comment) VALUES (2,1,1,1, null);
INSERT INTO [TutorFeedback] (TutorId,StudentId,LessonId,FeedbackDate,Comment) VALUES (1,1,1,1, null);
INSERT INTO [TutorFeedback] (TutorId,StudentId,LessonId,FeedbackDate,Comment) VALUES (1,1,1,1, null);

--Insert data to TutorFeedback detail table
INSERT INTO TutorFeedbackDetail (TutorFeedbackId,CriteriaId,CriteriaValue) VALUES (1,1,5);
INSERT INTO TutorFeedbackDetail (TutorFeedbackId,CriteriaId,CriteriaValue) VALUES (1,2,4);
INSERT INTO TutorFeedbackDetail (TutorFeedbackId,CriteriaId,CriteriaValue) VALUES (1,3,3);
INSERT INTO TutorFeedbackDetail (TutorFeedbackId,CriteriaId,CriteriaValue) VALUES (1,4,4);
INSERT INTO TutorFeedbackDetail (TutorFeedbackId,CriteriaId,CriteriaValue) VALUES (1,5,5);

--Insert data to StudentSubject table
INSERT INTO StudentSubject (SubjectId,StudentId,[Status]) VALUES (1,1,8);
INSERT INTO StudentSubject (SubjectId,StudentId,[Status]) VALUES (2,1,8);
INSERT INTO StudentSubject (SubjectId,StudentId,[Status]) VALUES (3,2,9);
INSERT INTO StudentSubject (SubjectId,StudentId,[Status]) VALUES (4,2,8);
INSERT INTO StudentSubject (SubjectId,StudentId,[Status]) VALUES (1,3,10);

--Insert data to TutorSubject table
INSERT INTO TutorSubject (SubjectId,TutorId,[Status]) VALUES (1,1,6);
INSERT INTO TutorSubject (SubjectId,TutorId,[Status]) VALUES (1,2,6);
INSERT INTO TutorSubject (SubjectId,TutorId,[Status]) VALUES (3,2,6);
INSERT INTO TutorSubject (SubjectId,TutorId,[Status]) VALUES (4,2,6);
INSERT INTO TutorSubject (SubjectId,TutorId,[Status]) VALUES (5,2,7);

--Insert data to TeachSchedule table
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,30-6-2017,3);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,30-6-2017,4);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,30-6-2017,5);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,30-6-2017,6);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,30-6-2017,7);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,1-7-2017,3);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,1-7-2017,4);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,1-7-2017,5);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,1-7-2017,6);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (1,1-7-2017,7);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (2,30-6-2017,5);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (2,30-6-2017,6);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (2,30-6-2017,7);
INSERT INTO TeachSchedule (TutorId,OrderDate,OrderSlot) VALUES (2,30-6-2017,8);

--Insert data to Question table
INSERT INTO Question (Content,LessonId) VALUES ('1+1=?',1);
INSERT INTO Question (Content,LessonId) VALUES ('Thủ đô của nước Nhật là?',1);
INSERT INTO Question (Content,LessonId) VALUES ('Aka trong tiếng Việt nghĩa là gì?',1);

--Insert data to Answer table
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('2',1,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('3',1,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('4',1,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Tokyo',2,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Hà Nội',2,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Cao Bằng',2,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Sơn Tây',2,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Đỏ',3,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Đen',3,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Xanh',3,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('Tím',3,0);

--Insert data to Transaction table
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType) VALUES ('Chuyen tien vao tai khoan vuongtv',150000,1-1-2017,1,1);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType) VALUES ('Trả lương cho tài khoản Huyenntk',-1000000,2-2-2017,1,2);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType) VALUES ('Chuyen tien vao tai khoan An333',120000,3-3-2017,1,1);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType) VALUES ('Tài khoản Hieu123 thực hiện mua buổi học',-100000,1-1-2017,3,1);
