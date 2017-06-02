CREATE DATABASE [TutorOnline]
USE [TutorOnline]

CREATE TABLE [Roles](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[RoleName] [nvarchar](30) NOT NULL,
	[TuRevenPer] [float] NULL
);

CREATE TABLE [Users](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[RoleID] [int] FOREIGN KEY REFERENCES [Roles](Id),
	[ParentID] [int] NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeID] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Country] [nvarchar](15) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[BankID] [nvarchar](24) NULL,
	[Salary] [money] NULL,
	[Wallet] [money] NULL,
	[Photo] [image] NULL,
	[Description] [text] NULL
);

CREATE TABLE [Transactions] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[UserID] [int] FOREIGN KEY REFERENCES [Users](Id),
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
	[CategoryID] [int] FOREIGN KEY REFERENCES [Categories](Id),
	[Description] [text] NULL,
	[Duration] [datetime] NOT NULL,
	[Purpose] [text] NULL,
	[Requirement] [text] NULL,
	[Photo] [image] NULL,
	[Price] [money] NOT NULL
);

CREATE TABLE [Lessons] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[LessonName] [varchar](500) NOT NULL,
	[SubjectID] [int] FOREIGN KEY REFERENCES [Subjects](Id),
	[Content] [text] NULL
);

CREATE TABLE [Slots] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[StudentID] [int] FOREIGN KEY REFERENCES [Users](Id),
	[TutorID] [int] FOREIGN KEY REFERENCES [Users](Id),
	[LessonID] [int] FOREIGN KEY REFERENCES [Lessons](Id),
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
	[SubjectID] [int] FOREIGN KEY REFERENCES [Subjects](Id),
	[Type] [int] NOT NULL
);

CREATE TABLE [Feedbacks] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[AssessorID] [int] FOREIGN KEY REFERENCES [Users](Id),
	[JudgedID] [int] FOREIGN KEY REFERENCES [Users](Id),
	[SlotID] [int] FOREIGN KEY REFERENCES [Slots](Id)
);

CREATE TABLE [FeedbackDetails] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[FeedbackID] [int] FOREIGN KEY REFERENCES [Feedbacks](Id),
	[CriteriaID] [int] FOREIGN KEY REFERENCES [Criterias](Id),
	CONSTRAINT UC_FeedbackDe UNIQUE (FeedbackID, CriteriaID),
	[Score] [int] NOT NULL
);

CREATE TABLE [StudentSubjects] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[SubjectID] [int] FOREIGN KEY REFERENCES [Subjects](Id),
	[StudentID] [int] FOREIGN KEY REFERENCES [Users](Id),
	CONSTRAINT UC_StudentSub UNIQUE (SubjectID, StudentID),
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL
);

CREATE TABLE [TeachSchedules] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[TutorID] [int] FOREIGN KEY REFERENCES [Users](Id),
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
	[DocType] [int] FOREIGN KEY REFERENCES [DocumentType](Id),
	[Description] [text] NULL
);

CREATE TABLE [AuditLog] (
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[DocumentID] [int] FOREIGN KEY REFERENCES [Documents](Id),
	[ModifierID] [int] FOREIGN KEY REFERENCES [Users](Id),
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
INSERT INTO [Roles] (RoleName) VALUES ('Guest');
INSERT INTO [Roles] (RoleName) VALUES ('FreeTutor');

--Insert data to Users Table
INSERT INTO [Users] (RoleID, LastName, FirstName, BirthDate, [Address], City, PostalCode, Country, PhoneNumber, Photo) 
			VALUES ('1', 'Viet Vuong', 'Tran', '19950831', 'Binh Minh, Thang Binh', 'Quang Nam', '', 'Viet Nam', '01666432971', '');
INSERT INTO [Users] (RoleID, LastName, FirstName, BirthDate, [Address], City, PostalCode, Country, PhoneNumber, Photo) 
			VALUES ('2', 'The Lam', 'Vo', '19950505', 'Dong Hoi', 'Quang Binh', '', 'Viet Nam', '0986709041', '');
INSERT INTO [Users] (RoleID, LastName, FirstName, BirthDate, [Address], City, PostalCode, Country, PhoneNumber, Photo) 
			VALUES ('3', 'Bao Long', 'Nguyen', '19950606', 'Ha Dong', 'Ha Noi', '', 'Viet Nam', '0905678964', '');
INSERT INTO [Users] (RoleID, LastName, FirstName, BirthDate, [Address], City, PostalCode, Country, PhoneNumber, Photo) 
			VALUES ('4', 'Huy Phat', 'Nguyen', '19950707', 'Dong Da', 'Hai Phong', '', 'Viet Nam', '0162987654', '');
INSERT INTO [Users] (RoleID, LastName, FirstName, BirthDate, [Address], City, PostalCode, Country, PhoneNumber, Photo) 
			VALUES ('5', 'Thi Hoai Thuong', 'Nong', '19950902', 'Yen Tu', 'Cao Bang', '', 'Viet Nam', '0167543222', '');
INSERT INTO [Users] (RoleID, LastName, FirstName, BirthDate, [Address], City, PostalCode, Country, PhoneNumber, Photo) 
			VALUES ('6', 'Thi Khanh Huyen', 'Nguyen', '19950903', 'Quan Ho', 'Bac Ninh', '', 'Viet Nam', '0987654344', '');
INSERT INTO [Users] (RoleID, LastName, FirstName, BirthDate, [Address], City, PostalCode, Country, PhoneNumber, Photo) 
			VALUES ('7', 'Truong Lam', 'Phan', '19650808', 'Dong Anh', 'Ha Noi', '', 'Viet Nam', '01666555666', '');

--Insert data to Subjects Table
INSERT INTO [Subjects] (SubjectName) VALUES ('English');
INSERT INTO [Subjects] (SubjectName) VALUES ('Japanese');
INSERT INTO [Subjects] (SubjectName) VALUES ('Math');