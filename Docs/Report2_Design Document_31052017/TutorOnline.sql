CREATE DATABASE [TutorOnline]
USE [TutorOnline]

CREATE TABLE [Roles](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[RoleName] [nvarchar](30) NOT NULL,
	[TuRevenPer] [float] NULL
);

CREATE TABLE [Users](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[RoleID] [int] FOREIGN KEY REFERENCES [Roles](Id) NOT NULL,
	[ParentID] [int] FOREIGN KEY REFERENCES [Users](Id) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeID] [nvarchar] (100) NOT NULL,
	[UserName] [varchar] (100) Unique NOT NULL,
	[Password] [varchar] (100) NOT NULL,
	[City] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[BankID] [nvarchar](24) NULL,
	[Salary] [money] NULL,
	[Wallet] [money] NULL,
	[Photo] [image] NULL,
	[Description] [text] NULL,
	[BankName] [nvarchar](200) null,
	[BMemName] [nvarchar](200) null,
	[isDeleted] [bit] not null default 0,
	[CV] [varchar](200) null,
	[isActived ] [bit] not null default 0
	
);

CREATE TABLE [TutorStatus](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[TutorID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[StatusID] [int] FOREIGN KEY REFERENCES [Status](Id) NOT NULL
)

CREATE TABLE [Status](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Status] [nvarchar] (20) NOT NULL
)	

CREATE TABLE [Transactions] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[UserID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[Content] [text] NOT NULL,
	[Amount] [money] NOT NULL,
	[TranDate] [datetime] NOT NULL
);


CREATE TABLE [Categories](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [varchar](255) NULL
);

CREATE TABLE [Subjects](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[SubjectName] [varchar](255) NOT NULL,
	[CategoryID] [int] FOREIGN KEY REFERENCES [Categories](Id) NOT NULL,
	[Description] [text] NULL,
	[Purpose] [text] NULL,
	[Requirement] [text] NULL,
	[Photo] [image] NULL,
	[Price] [money] NOT NULL
);

CREATE TABLE [Lessons] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[LessonName] [varchar](500) NOT NULL,
	[SubjectID] [int] FOREIGN KEY REFERENCES [Subjects](Id) NOT NULL,
	[Content] [text] NULL
);

CREATE TABLE [Slots] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[StudentID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[TutorID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[LessonID] [int] FOREIGN KEY REFERENCES [Lessons](Id) NOT NULL,
	[SlotOrder] [int] NOT NULL,
	[SlotDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[CanReason] [text] NULL,
	[Price] [money] NOT NULL
);

CREATE TABLE [Criterias] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[LanguageID] [int] NOT NULL,
	[CriteriaName] [varchar](255) NOT NULL,
	[SubjectID] [int] FOREIGN KEY REFERENCES [Subjects](Id) NOT NULL,
	[Type] [int] NOT NULL
);

CREATE TABLE [Feedbacks] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[AssessorID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[JudgedID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[SlotID] [int] FOREIGN KEY REFERENCES [Slots](Id)
);

CREATE TABLE [FeedbackDetails] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[FeedbackID] [int] FOREIGN KEY REFERENCES [Feedbacks](Id) NOT NULL,
	[CriteriaID] [int] FOREIGN KEY REFERENCES [Criterias](Id) NOT NULL,
	CONSTRAINT UC_FeedbackDe UNIQUE (FeedbackID, CriteriaID),
	[Score] [int] NOT NULL
);

CREATE TABLE [StudentSubjects] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[SubjectID] [int] FOREIGN KEY REFERENCES [Subjects](Id) NOT NULL,
	[StudentID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	CONSTRAINT UC_StudentSub UNIQUE (SubjectID, StudentID),
	[Status] [int] NOT NULL
);
CREATE TABLE [TutorSubjects] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[SubjectID] [int] FOREIGN KEY REFERENCES [Subjects](Id) NOT NULL,
	[TutorID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	CONSTRAINT UC_StudentSub UNIQUE (SubjectID, StudentID)
);

CREATE TABLE [TeachSchedules] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[TutorID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderSlot] [varchar](150) NULL
);

CREATE TABLE [DocumentType] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[TypeName] [varchar](200) NOT NULL
);

CREATE TABLE [Documents] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[DocUrl] [varchar](200) NOT NULL,
	[DocType] [int] FOREIGN KEY REFERENCES [DocumentType](Id) NOT NULL,
	[Description] [text] NULL
);

CREATE TABLE [AuditLog] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[DocumentID] [int] FOREIGN KEY REFERENCES [Documents](Id) NOT NULL,
	[ModifierID] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[Content] [text] NOT NULL
);

---------------------------------------------------------Insert Data------------------------------------------------------------

--Insert data to Roles Table
INSERT INTO [Roles] (RoleName) VALUES ('System Admin');
INSERT INTO [Roles] (RoleName) VALUES ('Supporter');
INSERT INTO [Roles] (RoleName) VALUES ('Accountant');
INSERT INTO [Roles] (RoleName) VALUES ('Manager');
INSERT INTO [Roles] (RoleName) VALUES ('Parent');
INSERT INTO [Roles] (RoleName) VALUES ('Student');
INSERT INTO [Roles] (RoleName, TuRevenPer) VALUES ('Tutor', 35);
INSERT INTO [Roles] (RoleName) VALUES ('PreTutor');

--Insert data to Users Table
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password], City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (1, '', 'Viet Vuong', 'Tran', '19950831', 1, 'Binh Minh, Thang Binh', 'vuongtv@gmail.com','vuongfu', 'vuongtv' ,'123456' , 'Quang Nam', '', 'Viet Nam', '01666432971', '', '', '', '', 'I am a System Admin');
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password], City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (2, '', 'Huy Phat', 'Nguyen', '19951022', 1, '28 Truong Chinh', 'phatnh@gmail.com', 'phatfu','phatnh' , '123456' , 'Hai Phong', '', 'Viet Nam', '0169555432', '', '', '', '', 'I am a Supporter');
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password], City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (3, '', 'Thi Hoai Thuong', 'Nong', '19950901', 2, 'Tra Linh', 'thuongnth@gmail.com','thuongfu' , 'thuongnth' , '123456',  'Cao Bang', '', 'Viet Nam', '0905231400', '', '', '', '', 'I am an Accountant');
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password], City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (4, '', 'Bao Long', 'Nguyen', '19951003', 1, 'Ha Dong', 'longnb@gmail.com','longfu' , 'longnb' , '123456' ,  'Ha Noi', '', 'Viet Nam', '01222423200', '', '', '', '', 'I am a Manager');
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password], City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (5, '', 'Truong Lam', 'Phan', '1970', 1, 'Thanh Xuan', 'lampt@gmail.com','lampt', 'lampt' , '123456' ,  'Ha Noi', '', 'Viet Nam', '0169322455', '', '', '', '', 'I am a Parent');
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password], City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (6, 5, 'The Lam', 'Vo', '19950723', 1, 'Thanh Xuan', 'lamvt@gmail.com','lamfu','lamvt' , '123456' ,  'Ha Noi', '', 'Viet Nam', '0169444620', '', '', 5000000, '', 'I am a Student');
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password], City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (7, '', 'Thi Khanh Huyen', 'Nguyen', '19940707', 2, 'Quan Ho', 'huyenntk@gmail.com','huyenntk' ,'huyenntk', '123456' ,  'Bac Ninh', '', 'Viet Nam', '0986709041', '00125419005', '300000', '6000000', '', 'I am a Tutor');
INSERT INTO [Users] (RoleID, ParentID, LastName, FirstName, BirthDate, [Gender], [Address], [Email], SkypeID, UserName,[Password],City, PostalCode, Country, PhoneNumber, BankID, Salary, Wallet, Photo, [Description]) 
			VALUES (8, '', 'Huu Vu', 'Hoang', '19950907', 1, 'Binh Minh, Thang Binh', 'vuhh@gmail.com','vuhh' ,'vuhh', '123456' ,  'Quang Nam', '', 'Viet Nam', '0923666777', '', '', '', '', 'I am a free Tutor');

--Insert data to Transactions table
INSERT INTO [Transactions] (UserID, Content, Amount, TranDate) VALUES (5, 'Nop 5000000vnd cho hoc vien Vo The Lam', 5000000, '20170603');
INSERT INTO [Transactions] (UserID, Content, Amount, TranDate) VALUES (6, 'Mua khoa hoc tieng nhat trung cap', 3000000, '20170615');
INSERT INTO [Transactions] (UserID, Content, Amount, TranDate) VALUES (7, 'Nhan luong thang 5 nam 2017', 6000000, '20170601');

--Insert data to Categories table
INSERT INTO [Categories] (CategoryName, [Description]) VALUES ('Tieng Nhat', '');
INSERT INTO [Categories] (CategoryName, [Description]) VALUES ('Tieng Anh', '');
INSERT INTO [Categories] (CategoryName, [Description]) VALUES ('Toan', '');
INSERT INTO [Categories] (CategoryName, [Description]) VALUES ('Vat Ly', '');
INSERT INTO [Categories] (CategoryName, [Description]) VALUES ('Hoa Hoc', '');

--Insert data to Subjects Table
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Tieng nhat so cap', 1, 'Tieng nhat danh cho nguoi moi bat dau', 'Thuoc 2 bang chu cai, buoc dau lam quen voi tieng nhat', '', '', 1000000, 2);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Tieng nhat trung cap', 1, 'Khoa tieng nhat trinh do N4', 'Hoan thanh 30 bai dau giao trinh minanonihongo, trinh do tuong duong n4', 'Da hoan thanh khoa tieng nhat so cap', '', 2000000, 4);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Tieng anh so cap', 2, 'Tieng anh danh cho nguoi moi bat dau', 'Nam ro duoc 12 thi co ban trong tieng anh', '', '', 3000000, 5);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Tieng anh doanh nghiep', 2, 'Tieng anh danh cho cong viec', 'Lam quen va thanh thao cac mau cau giao tiep trong doanh nghiep', '', '', 500000, 1);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Toan lop 5', 3, 'Toan danh cho hoc sinh lop 5', 'On tap va luyen cac dang bai tap de thi qua cap 1', '', '', 3000000, 6);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Toan lop 7', 3, 'Toan danh cho hoc sinh lop 7', 'Tim hieu va luyen cac dang bai tap nang cao toan lop 7', 'Co nen tang toan tot, can on luyen kien thuc de chuan bi cho cac ki thi cap huyen hoac tinh', '', 3000000, 8);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Vat Ly lop 9', 4, 'Vat Ly danh cho hoc sinh lop 9', 'On tap va luyen cac dang bai tap vat ly lop 9', '', '', 1000000, 1.5);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Vat Ly lop 11', 4, 'Vat Ly danh cho hoc sinh lop 11', 'Tim hieu va luyen cac dang bai tap nang cao vat ly lop 11', 'Co nen tang vat ly tot, can on luyen kien thuc de chuan bi cho cac ki thi cap huyen hoac tinh', '', 2000000, 3);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Hoa Hoc lop 9', 5, 'Hoa Hoc danh cho hoc sinh lop 9', 'On tap va luyen cac dang bai tap hoa hoc lop 9', '', '', 3000000, 5);
INSERT INTO [Subjects] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo, Price, Duration) VALUES ('Hoa Hoc lop 11', 5, 'Hoa Hoc danh cho hoc sinh lop 11', 'Tim hieu va luyen cac dang bai tap nang cao hoa hoc lop 11', 'Co nen tang hoa hoc tot, can on luyen kien thuc de chuan bi cho cac ki thi cap huyen hoac tinh', '', 2500000, 3.5);

--Insert data into ... table
