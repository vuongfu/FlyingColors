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
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](300) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Balance] [money] NOT NULL default 0,
	[Photo] varchar(100) NULL,
	[Description] [nvarchar](200) NULL,	 
	[isActived] [bit] not null default 1,
	[RegisterDate] datetime not null default getdate()
); 

CREATE TABLE [Student](
	[StudentId] [int] IdENTITY(1,1) PRIMARY KEY,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[ParentId] [int] FOREIGN KEY REFERENCES [Parent](ParentId) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](300) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Balance] [money] NOT NULL default 0,
	[Photo] varchar(100) NULL,
	[Description] [nvarchar](200) NULL,	 
	[isActived] [bit] not null default 1,
	[RegisterDate] datetime not null default getdate()
);

create table [BackendUser](
	[BackendUserId] [int] Identity(1,1) primary key,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](300) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Photo] varchar(100) NULL,
	[Description] [nvarchar](200) NULL,	 
	[isActived] [bit] not null default 1,
	[RegisterDate] datetime not null default GETDATE()
);

Create table [Tutor](
	[TutorId] int Identity(1,1) primary key,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [int] NOT NULL,
	[Address] [nvarchar](300) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Balance] [money] NOT NULL default 0,
	[BankId] [nvarchar](50) NULL,
	[Salary] [money] NULL,
	[Photo] varchar(100) NULL,
	[Description] [nvarchar](200) NULL,	 
	[BankName] [nvarchar](200) null,
	[BMemName] [nvarchar](200) null,
	[isActived] [bit] not null default 0,
	[RegisterDate] datetime not null default getdate()
);

CREATE TABLE [Category](
	[CategoryId] [int] IDENTITY(1,1) PRIMARY KEY,
	[CategoryName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[isActived] bit not null default 1
);

CREATE TABLE [Subject](
	[SubjectId] [int] IDENTITY(1,1) PRIMARY KEY,
	[SubjectName] [nvarchar](255) NOT NULL,
	[CategoryId] [int] FOREIGN KEY REFERENCES [Category](CategoryId) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Purpose] [nvarchar](1000) NULL,
	[Requirement] [nvarchar](1000) NULL,
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
	[Content] [nvarchar](1000) NULL,
	[isActived] bit not null default 1,
	[Order] int not null
);

CREATE TABLE [LearningMaterial] (
	[MaterialId] [int] IdENTITY(1,1) PRIMARY KEY,
	[MaterialUrl] [nvarchar](200) NOT NULL,
	[MaterialTypeId] [int] FOREIGN KEY REFERENCES [MaterialType](MaterialTypeId) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[LessonId] int FOREIGN KEY REFERENCES [Lesson](LessonId) NULL,
	[SubjectId] int FOREIGN KEY REFERENCES [Subject](SubjectId) NULL,
	[isActived] bit not null default 1
);


CREATE TABLE [Criteria] (
	[CriteriaId] [int] IdENTITY(1,1) PRIMARY KEY,
	[CriteriaName] [nvarchar](255) NOT NULL,  
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId) NOT NULL,
	[RoleId] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[isActived] bit not null default 1
);

CREATE TABLE [Schedule] (
	[ScheduleId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderSlot] int NOT NULL,
	[StudentId] [int] FOREIGN KEY REFERENCES [Student](StudentId) NULL,
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId) NULL,
	[Status] [int] FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL,
	[Type] [int] NULL,
	[CanReason] [nvarchar](1000) NULL,
	[Price] [money] NOT NULL,
	CONSTRAINT UC_DateSlot UNIQUE (TutorId,OrderDate,OrderSlot)
);

CREATE TABLE [StudentFeedback] (
	[StudentFeedbackId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	[StudentId] [int] FOREIGN KEY REFERENCES [Student](StudentId) NOT NULL,
	[ScheduleId] [int] FOREIGN KEY REFERENCES [Schedule](ScheduleId) NOT NULL,
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId),
	[FeedbackDate] datetime NOT NULL,
	[Rate] int NOT NULL,
	[Comment] nvarchar(1000) NULL
);

CREATE TABLE [TutorFeedback] (
	[TutorFeedbackId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	[StudentId] [int] FOREIGN KEY REFERENCES [Student](StudentId) NOT NULL,
	[ScheduleId] [int] FOREIGN KEY REFERENCES [Schedule](ScheduleId) NOT NULL,
	[LessonId] [int] FOREIGN KEY REFERENCES [Lesson](LessonId),
	[FeedbackDate] datetime NOT NULL,
	[Comment] nvarchar(1000) NULL,
	[TestResult] int NULL 
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
	[Status] [int] FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL,
	[StudiedLesson] int NOT NULL default 0
);

CREATE TABLE [TutorSubject] (
	[TutorSubjectId] [int] IdENTITY(1,1) PRIMARY KEY,
	[SubjectId] [int] FOREIGN KEY REFERENCES [Subject](SubjectId) NOT NULL,
	[TutorId] [int] FOREIGN KEY REFERENCES [Tutor](TutorId) NOT NULL,
	CONSTRAINT UC_TutorSub UNIQUE (SubjectId, TutorId),
	[Status] [int] FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL,
	[Experience] [nvarchar](4000) not null
);

CREATE TABLE [AuditLog] (
	[AuditId] [int] IdENTITY(1,1) PRIMARY KEY,
	[TableName] nvarchar(100) NOT NULL,
	[ModifierId] [int] NOT NULL,
	[ModifierRole] [int] FOREIGN KEY REFERENCES [Role](RoleId) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[Content] [nvarchar](500) NOT NULL
);

Create table [Question](
	[QuestionId] int Identity(1,1) primary key,
	[Photo] varchar(100) NULL,
	[Content] nvarchar(500) not null,
	[LessonId] int foreign key references [Lesson](LessonId),
	[SubjectId] [int] FOREIGN KEY REFERENCES [Subject](SubjectId),
	[isActived] bit not null default 1
);

create table [Answer](
	[AnswerId] int Identity(1,1) primary key,
	[Content] nvarchar(500) not null,
	[QuestionId] int foreign key references [Question](QuestionId) not null,
	[isCorrect] bit not null,
	[isActived] bit not null default 1
);

CREATE TABLE [Transaction] (
	[TransactionId] [int] IDENTITY(1,1) PRIMARY KEY,
	[Content] [nvarchar](1000) NOT NULL,
	[Amount] [money] NOT NULL,
	[TranDate] [datetime] NOT NULL,
	[UserID] [int] NOT NULL,
	[UserType] int NOT NULL
);

---------------------------------------------------------Insert Data------------------------------------------------------------

--Insert data to Roles Table
INSERT INTO [Role] (RoleName) VALUES (N'Quản lý hệ thống');
INSERT INTO [Role] (RoleName) VALUES (N'Hỗ trợ');
INSERT INTO [Role] (RoleName) VALUES (N'Kế toán');
INSERT INTO [Role] (RoleName) VALUES (N'Quản lý');
INSERT INTO [Role] (RoleName) VALUES (N'Phụ huynh');
INSERT INTO [Role] (RoleName) VALUES (N'Học sinh');
INSERT INTO [Role] (RoleName) VALUES (N'Gia sư');
INSERT INTO [Role] (RoleName) VALUES (N'Ứng viên gia sư');

--Insert data to Categories table 
INSERT INTO [Category] (CategoryName, [Description]) VALUES (N'Tiếng Nhật', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES (N'Tiếng Anh', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES (N'Tiếng Trung', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES (N'Tiếng Pháp', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES (N'Tiếng Nga', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES (N'Tiếng Hàn', NULL);

--Insert data to Subjects Table
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES (N'Bảng chữ cái Hiragana', 1, N'Khóa học gồm 8 buổi, được thiết kế dành cho học viên bắt đầu học tiếng Nhật', N'Học viên có thể viết, đọc, sử dụng thành thạo bảng chữ cái Hiragana', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES (N'Bảng chữ cái Katakana', 1, N'Khóa học gồm 8 buổi, được thiết kế dành cho học viên bắt đầu học tiếng Nhật', N'Học viên có thể viết, đọc, sử dụng thành thạo bảng chữ cái Katakana', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES (N'Tiếng anh sơ cấp', 2, N'Tiếng anh dành cho người mới bắt đầu', N'Thành thạo chào hỏi cơ bản trong cuộc sống hằng ngày', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES (N'Tiếng anh trung cấp', 2,N'Tiếng anh giao tiếp', N'Giao tiếp tốt trong cuộc sống', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES (N'Tiếng anh cao cấp', 2,N'Tiếng anh dành cho doanh nhân', N'Giao tiếp tốt trong môi trường doanh nghiệp', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES (N'Tiếng anh chuyên ngành', 2,N'Tiếng anh chuyên ngành kỹ thuật phần mềm', N'Hiểu được các thuật ngữ tiếng anh của ngành kỹ thuật phần mềm', NULL, NULL);


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
INSERT INTO [Status] (Status) VALUES ('schedule_available');

--Insert data to Parent table
INSERT INTO [Parent] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (5,N'Nguyễn Văn',N'Lang',1975-9-24,1,N'số 20 đường Kim Đồng','parent1@gmail.com','parent1','parent1','parent1',N'Hà Nội',null,N'Việt Nam','0123456789',0,null,null);
INSERT INTO [Parent] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (5,N'Trần Thị',N'Linh',1970-8-23,2,N'số 30 đường Kim Mã','parent2@gmail.com','parent2','parent2','parent2',N'Hà Nội',null,N'Việt Nam','0123789456',0,null,null);

--Insert data to Student table
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,1,N'Nguyễn Đức',N'Hùng',2000-3-3,1,N'số 20 đường Kim Đồng','student1@gmail.com','student1','student1','student1',N'Hà Nội',null,N'Việt Nam','01364294938',1000000,null,null);
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,2,N'Trần Thị',N'Duyên',2001-3-1,2,N'số 30 đường Kim Mã','student2@gmail.com','student2','student2','student2',N'Hà Nội',null,N'Việt Nam','01212594938',500000,null,null);
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,null,N'Trần Đức',N'Nam',1999-7-23,1,N'số 151 đường Giải Phóng','student3@gmail.com','student3','student3','student3',N'Hà Nội',null,N'Việt Nam','01632544938',0,null,null);
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,null,N'Trần Thị',N'Lan',2000-6-15,2,N'số 421 đường Trường Chinh','student4@gmail.com','student4','student4','student4',N'Hà Nội',null,N'Việt Nam','01472594938',0,null,null);
--Insert data to Tutor table
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (7,N'Nguyễn Đức',N'Trung',1983-3-3,1,N'số 3 Trần Duy Hưng','tutor1@gmail.com','tutor1','tutor1','tutor1',N'Hà Nội',null,N'Việt Nam','01635464938',120000,'9876543210',120000,null,null,N'ngân hàng Tiên Phong','tutor1',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (7,N'Trần Thị',N'Châu',1986-9-3,2,N'số 124 Hoàng Quốc Việt','tutor2@gmail.com','tutor2','tutor2','tutor2',N'Hà Nội',null,N'Việt Nam','01635594998',0,'9876598210',150000,null,null,N'ngân hàng Vietcombank','tutor2',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (7,N'Nguyễn Ngọc',N'Anh',1980-9-1,2,N'số 87 Lê Lai','tutor3@gmail.com','tutor3','tutor3','tutor3',N'Hải Phòng',null,N'Việt Nam','01635594998',0,'9877498210',100000,null,null,N'ngân hàng Techcombank','tutor3',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (7,N'Đỗ Mạnh',N'Cường',1983-7-25,1,N'số 54 Trường Chinh','tutor4@gmail.com','tutor4','tutor4','tutor4',N'Hà Nội',null,N'Việt Nam','01632594978',0,'9146543210',200000,null,null,N'ngân hàng Quân đội','tutor4',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (7,N'Lý Thị',N'Linh',1985-1-23,2,N'số 98 Lê Văn Lương','tutor5@gmail.com','tutor5','tutor5','tutor5',N'Hà Nội',null,N'Việt Nam','01636594998',0,'9876592560',200000,null,null,N'ngân hàng Nông nghiệp','tutor5',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (7,N'Nguyễn Minh',N'Quang',1986-4-25,1,N'số 8 Trần Duy Hưng','tutor6@gmail.com','tutor6','tutor6','tutor6',N'Hà Nội',null,N'Việt Nam','01632231938',0,'9716543210',150000,null,null,N'ngân hàng Tiên Phong','tutor6',1);

INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (8,N'Nguyễn Ngọc',N'Ninh',1986-2-28,1,N'số 17 Quán Thánh','tutor7@gmail.com','tutor7','tutor7','tutor7',N'Hà Nội',null,N'Việt Nam','01675394938',0,'9876253210',0,null,null,N'ngân hàng Tiên Phong','tutor7',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (8,N'Nguyễn Đức',N'Giang',1979-5-30,1,N'số 45 Phạm Văn Đồng','tutor8@gmail.com','tutor8','tutor8','tutor8',N'Hải Phòng',null,N'Việt Nam','01789694938',0,'9276543210',0,null,null,N'ngân hàng Nông nghiệp','tutor8',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (8,N'Nguyễn Thị',N'Hà',1984-10-30,2,N'số 475 Nguyễn Văn Linh','tutor9@gmail.com','tutor9','tutor9','tutor9',N'Hà Nội',null,N'Việt Nam','01732494938',0,'9276543232',0,null,null,N'ngân hàng Nông nghiệp','tutor9',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (8,N'Nguyễn Thị',N'Hằng',1983-11-12,2,N'số 35 Nguyễn Trãi','tutor10@gmail.com','tutor10','tutor10','tutor10',N'Hà Nội',null,N'Việt Nam','01714494938',0,'9276585232',0,null,null,N'ngân hàng Vietcombank','tutor10',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (8,N'Nguyễn Thị',N'Huyền',1982-1-12,2,N'số 75 Nguyễn Trãi','tutor11@gmail.com','tutor11','tutor11','tutor11',N'Hà Nội',null,N'Việt Nam','01717794938',0,'9276775232',0,null,null,N'ngân hàng Vietcombank','tutor11',1);
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName,isActived) VALUES (8,N'Nguyễn Minh',N'Hoàng',1980-7-1,1,N'số 44 Nguyễn Văn Linh','tutor12@gmail.com','tutor12','tutor12','tutor12',N'Hà Nội',null,N'Việt Nam','01717794978',0,'9276775252',0,null,null,N'ngân hàng Vietcombank','tutor12',1);

																																																								
--Insert data to BackendUser table
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (1,N'Nguyễn Huy',N'Phát',1995-1-23,1,N'số 85 đại lộ Tôn Đức Thắng','systemadmin1@gmail.com','systemadmin1','systemadmin1',N'Hải Phòng',N'Việt Nam','0947003988',null,N'Pine cool ngầu');
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (1,N'Nguyễn Bảo',N'Long',1995-4-29,1,N'số 11 ga Hà Đông','systemadmin2@gmail.com','systemadmin2','systemadmin2',N'Hà Nội',N'Việt Nam','01632594938',null,N'Long lạnh lùng');
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,N'Nông Thị Hoài',N'Thương',1995-9-1,2,N'số 6 đồi số 6','supporter1@gmail.com','supporter1','supporter1',N'Cao Bằng','Việt Nam','0123888888',null,N'Thương xinh đẹp');
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,N'Nguyễn Thị Khánh',N'Huyền',1994-9-2,2,N'số 18 làng Quan Họ','supporter2@gmail.com','supporter2','supporter2',N'Bắc Ninh',N'Việt Nam','0123666666',null,N'Huyền xinh đẹp');
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (3,N'Trần Viết',N'Vương',1995-8-31,1,N'số 8 đường Hoàng Hoa Thám','accountant1@gmai.com','accountant1','accountant1',N'Quảng Nam',N'Việt Nam','01666432971',null,N'Vương đẹp trai');
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (3,N'Võ Thế',N'Lâm',1995-7-5,1,N'số 126 Lê Lợi','accountant2@gmail.com','accountant2','accountant2',N'Quảng Bình',N'Việt Nam','01659849505',null,N'Lâm đẹp trai');
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (4,N'Nguyễn Ngọc',N'Anh',1994-9-22,2,N'182 Chùa Hàng','manager1@gmail.com','manager1','manager1',N'Hải Phòng',N'Việt Nam','01653917952',null,N'Ngọc Anh xinh đẹp');		
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (4,N'Bạch San',N'San',2001-7-4,2,N'190 Nguyễn Thị Minh Khai','manager2@gmail.com','manager2','manager2',N'Buôn Mê Thuột',N'Việt Nam','01636118187',null,N'San San xinh đẹp');	
	
--Insert data to MaterialumentType table
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('pdf');
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('docx');
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('mp3');

--Insert data to Lesson table
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (1,N'Bài 1', 1, N'Giới thiệu khái quát về chương trình,học Hiragana: 10 chữ cái đầu tiên, chào hỏi cơ bản 1')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (2,N'Bài 2', 1, N'Luyện tập: Hiragana và Chào hỏi cơ bản 1, Học Hiragana: 10 chữ tiếp theo, chào hỏi cơ bản 2')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (3,N'Bài 3', 1, N'Luyện tập: Hiragana và Chào hỏi cơ bản 2, Học Hiragana: 10 chữ tiếp theo, chào hỏi cơ bản 3')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (4,N'Bài 4', 1, N'Luyện tập: Hiragana và Chào hỏi cơ bản 3, Học Hiragana: 8 chữ tiếp theo, chào hỏi cơ bản 4')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (5,N'Bài 5', 1, N'Luyện tập: Hiragana và Chào hỏi cơ bản 4, Học Hiragana: 8 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (6,N'Bài 6', 1, N'Luyện tập: Hiragana và Chào hỏi cơ bản, Học Hiragana: Âm đục + Âm bán đục ')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (7,N'Bài 7', 1, N'Luyện tập: Hiragana và Chào hỏi cơ bản, Học Hiragana: Âm ghép')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (8,N'Bài 8', 1, N'Luyện tập: Hiragana và Chào hỏi cơ bản, Học Hiragana: Âm dài + Âm ngắt')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (1,N'Bài 1', 2, N'Giới thiệu khái quát về chương trình,học Katakana: Âm dài + âm ngắt + 10 chữ cái đầu tiên')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (2,N'Bài 2', 2, N'Luyện tập: Katakana, Học Katakana: 10 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (3,N'Bài 3', 2, N'Luyện tập: Katakana, Học Katakana: 10 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (4,N'Bài 4', 2, N'Luyện tập: Katakana, Học Katakana: 8 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (5,N'Bài 5', 2, N'Luyện tập: Katakana, Học Katakana: 8 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (6,N'Bài 6', 2, N'Luyện tập: Katakana, Học Katakana: Âm ghép')
							 
--Insert data to Material table 



-- Insert data to Schedule table


--Insert data to Criteria table ?????
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',1,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',1,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',1,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',2,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',2,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',2,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',3,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',3,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',3,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',4,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',4,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',4,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',5,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',5,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',5,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',6,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',6,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',6,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',7,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',7,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',7,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',8,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',8,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',8,6);

INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',9,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',9,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',9,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',10,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',10,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',10,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',11,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',11,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',11,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',12,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',12,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',12,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',13,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',13,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',13,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng đọc',14,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng phát âm',14,6);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES (N'Khả năng tiếp thu',14,6);

--Insert data to StudentFeedback


--Insert data to TutorFeedback table


--Insert data to TutorFeedback detail table


--Insert data to StudentSubject table


--Insert data to TutorSubject table
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,1,6,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,1,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (3,1,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (3,2,6,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,2,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,2,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,3,6,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,3,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (3,3,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (3,4,6,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,4,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,4,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,5,6,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (3,5,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (4,5,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (4,6,6,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (3,6,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,6,7,N'1 năm');

INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,7,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,8,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (4,9,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (4,10,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,11,7,N'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,12,7,N'1 năm');
--Insert data to TeachSchedule table


--Insert data to Question table
INSERT INTO Question (Content,LessonId) VALUES (N'ue',1);
INSERT INTO Question (Content,LessonId) VALUES (N'kaki',1);
INSERT INTO Question (Content,LessonId) VALUES (N'koe',1);
INSERT INTO Question (Content,LessonId) VALUES (N'えき',1);
INSERT INTO Question (Content,LessonId) VALUES (N'いけ',1);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'keisatsu',2);
INSERT INTO Question (Content,LessonId) VALUES (N'tokei',2);
INSERT INTO Question (Content,LessonId) VALUES (N'asa',2);
INSERT INTO Question (Content,LessonId) VALUES (N'くつ',2);
INSERT INTO Question (Content,LessonId) VALUES (N'せき',2);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'ano sakana',3);
INSERT INTO Question (Content,LessonId) VALUES (N'nani',3);
INSERT INTO Question (Content,LessonId) VALUES (N'niku',3);
INSERT INTO Question (Content,LessonId) VALUES (N'たいせつ',3);
INSERT INTO Question (Content,LessonId) VALUES (N'ちかてつ',3);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'yasumi',4);
INSERT INTO Question (Content,LessonId) VALUES (N'moshi moshi',4);
INSERT INTO Question (Content,LessonId) VALUES (N'yakiniku',4);
INSERT INTO Question (Content,LessonId) VALUES (N'ともだち',4);
INSERT INTO Question (Content,LessonId) VALUES (N'さむい',4);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'yakitori',5);
INSERT INTO Question (Content,LessonId) VALUES (N'sumimasen',5);
INSERT INTO Question (Content,LessonId) VALUES (N'kuruma',5);
INSERT INTO Question (Content,LessonId) VALUES (N'せんたく',5);
INSERT INTO Question (Content,LessonId) VALUES (N'わたし',5);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'hajime mashite  ',6);
INSERT INTO Question (Content,LessonId) VALUES (N'shinbun',6);
INSERT INTO Question (Content,LessonId) VALUES (N'denwa',6);
INSERT INTO Question (Content,LessonId) VALUES (N'すごいですね',6);
INSERT INTO Question (Content,LessonId) VALUES (N'ぎんざ',6);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'okyakusan',7);
INSERT INTO Question (Content,LessonId) VALUES (N'kanojo',7);
INSERT INTO Question (Content,LessonId) VALUES (N'ocha',7);
INSERT INTO Question (Content,LessonId) VALUES (N'しゅくだい',7);
INSERT INTO Question (Content,LessonId) VALUES (N'かいしゃ',7);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'roppyakuen',8);
INSERT INTO Question (Content,LessonId) VALUES (N'yūmei',8);
INSERT INTO Question (Content,LessonId) VALUES (N'chūgoku',8);
INSERT INTO Question (Content,LessonId) VALUES (N'がっこう',8);
INSERT INTO Question (Content,LessonId) VALUES (N'いっかい',8);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'kukkī',9);
INSERT INTO Question (Content,LessonId) VALUES (N'kokekokkō',9);
INSERT INTO Question (Content,LessonId) VALUES (N'kēki',9);
INSERT INTO Question (Content,LessonId) VALUES (N'コーク',9);
INSERT INTO Question (Content,LessonId) VALUES (N'キック',9);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'テキスト',10);
INSERT INTO Question (Content,LessonId) VALUES (N'デザート',10);
INSERT INTO Question (Content,LessonId) VALUES (N'kēsu',10);
INSERT INTO Question (Content,LessonId) VALUES (N'sōsēji',10);
INSERT INTO Question (Content,LessonId) VALUES (N'uisukī',10);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'supīdo',11);
INSERT INTO Question (Content,LessonId) VALUES (N'hītā',11);
INSERT INTO Question (Content,LessonId) VALUES (N'nōto',11);
INSERT INTO Question (Content,LessonId) VALUES (N'ネクタイ',11);
INSERT INTO Question (Content,LessonId) VALUES (N'パスタ',11);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'roppyakuen',12);
INSERT INTO Question (Content,LessonId) VALUES (N'gamu',12);
INSERT INTO Question (Content,LessonId) VALUES (N'mōtā',12);
INSERT INTO Question (Content,LessonId) VALUES (N'ハム',12);
INSERT INTO Question (Content,LessonId) VALUES (N'ヨット',12);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'supōtsukurabu',13);
INSERT INTO Question (Content,LessonId) VALUES (N'emēru',13);
INSERT INTO Question (Content,LessonId) VALUES (N'hoteru',13);
INSERT INTO Question (Content,LessonId) VALUES (N'レストラン',13);
INSERT INTO Question (Content,LessonId) VALUES (N'カラオケ',13);
                                                
INSERT INTO Question (Content,LessonId) VALUES (N'kyanseru',14);
INSERT INTO Question (Content,LessonId) VALUES (N'fairu',14);
INSERT INTO Question (Content,LessonId) VALUES (N'waishatsu',14);
INSERT INTO Question (Content,LessonId) VALUES (N'ピョンヤン',14);
INSERT INTO Question (Content,LessonId) VALUES (N'フィリピン',14);


--Insert data to Answer table
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'うえ',1,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'うけ',1,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'いか',1,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'いえ',1,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'かこ',2,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'こけ',2,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'かき',2,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'きか',2,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'えこ',3,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'こえ',3,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'けお',3,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おけ',3,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'eki',4,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ei',4,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ike',4,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ie',4,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'eki',5,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ei',5,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ike',5,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ie',5,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'せいさつ',6,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'けいさく',6,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'けいかつ',6,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'けいさつ',6,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'とけえ',7,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'とけい',7,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おてい',7,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おけい',7,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'あさ',8,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おそ',8,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'えせ',8,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'いき',8,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kutsu',9,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kusu',9,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tsusu',9,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tsuku',9,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'keki',10,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'seshi',10,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kechi',10,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'seki',10,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'あにさかな',11,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'あのさかの',11,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'あのさかな',11,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'あにさきの',11,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'なに',12,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'にく',12,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'あの',12,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'あに',12,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'にき',13,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'にく',13,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'くに',13,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'にす',13,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'taisetsu',14,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tasetsu',14,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'taseitsu',14,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'taisesu',14,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'chikaketsu',15,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'chikasetsu',15,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'chikatetsu',15,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'chikaesu',15,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やすみ',16,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やすむ',16,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ゆすま',16,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ゆすむ',16,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'もさもさ',17,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'もしもし',17,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'みそみそ',17,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'もかもか',17,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やきぬく',18,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やきにか',18,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やきにく',18,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ゆきにく',18,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tomodachi',19,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tamagochi',19,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tamadachi',19,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tomakuchi',19,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'samuu',20,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'samui',20,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'samiu',20,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'samii',20,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ゆきとり',21,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やきとり',21,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やきにく',21,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'やきぬり',21,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'しみません',22,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'すみません',22,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'すまみせん',22,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'しみまけん',22,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'くるま',23,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'くれめ',23,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'くるみ',23,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'かるま',23,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'sentaki',24,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'sentaku',24,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kentaku',24,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kentaki',24,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'watashi',25,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'wasashi',25,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'wataki',25,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'wasaki',25,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'はじままして',26,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'はじめまして',26,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'はじめみして',26,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ばじめまじて',26,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'しんぷん',27,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'しんぶん',27,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'じんぷん',27,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'じんぶん',27,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'でんが',28,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'てんわ',28,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'でんわ',28,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'てんが',28,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'sugoidesune',29,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'sugiodesune',29,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'zugoidesune',29,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'zugoitesune',29,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ginza',30,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kinza',30,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kinsa',30,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kinka',30,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おきゃくさん',31,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おきゅくかん',31,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おきゃくさん',31,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おきゅくさん',31,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'かのしょ',32,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'かのじょ',32,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'かのじょう',32,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'かのしょう',32,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おちゃ',33,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'こちゃ',33,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'おちょ',33,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'こちょう',33,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'shokudai',34,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'shokkudai',34,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'shukkudai',34,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'shukudai',34,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'saisha',35,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kaisha',35,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kasha',35,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kashu',35,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'るっぴゃくえん',36,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ろっぴゃくえん',36,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ろぴゃくえん',36,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'るぴゃくえん',36,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ゆうめい',37,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ゆいめい',37,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ようめい',37,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ようめ',37,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ちゅごく',38,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ちゅこく',38,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ちゅうごく',38,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ちゅうこく',38,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kakkou',39,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kakou',39,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'gakou',39,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'gakkou',39,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ikai',40,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ikkai',40,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'iikai',40,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'iikkai',40,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'クッキー',41,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'クキー',41,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キクー',41,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キックー',41,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'コケココ',42,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'コケココー',42,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'コケコッコー',42,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'コケッココー',42,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ケキ',43,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ケーキ',43,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ケキー',43,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ケーキー',43,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kōku',44,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kōki',44,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kōke',44,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kōka',44,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'gakku',45,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kikku',45,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'gikku',45,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kiggu',45,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tekisuto',46,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tegisuto',46,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tekizuto',46,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'tokisuso',46,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'dezāso',47,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'dezāto',47,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'detāto',47,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'desāto',47,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ケース',48,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キース',48,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ケーズ',48,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キーズ',48,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ソーセジ',49,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ソケージ',49,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ソーケージ',49,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ソーセージ',49,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ウイスキ',50,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'イエスキー',50,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ウオスキー',50,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ウイスキー',50,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スピドー',51,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スピード',51,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スビドー',51,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スビード',51,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ヒーター',52,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ヘーター',52,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ヒートー',52,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ヘートー',52,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ニーチ',53,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ノート',53,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ネーテ',53,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ナータ',53,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'nekuta',54,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'nekitai',54,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'nekutai',54,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'nekutaii',54,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'basuta',55,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'hashita',55,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'pasuta',55,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'pashita',55,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ミッセージ',56,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'メッセージ',56,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'マッサージ',56,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'メッサージ',56,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'カマ',57,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ガマ',57,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'カム',57,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ガム',57,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'モーテー',58,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'モートー',58,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'モーター',58,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'モッター',58,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'hamu',59,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kamu',59,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'bamu',59,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'samu',59,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'yotto',60,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'yōtto',60,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'yōto',60,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'yottō',60,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スポースクラブ',61,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スポースケラブ',61,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スポースクワブ',61,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'スポースケワブ',61,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'イマール',62,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'イマール',62,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'エマール',62,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'エメール',62,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ホテル',63,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ホチル',63,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ホチス',63,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ホテス',63,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'resutoran',64,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'resutoran',64,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'esutore',64,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'resutoso',64,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kawaoku',65,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'karaoku',65,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'karaoke',65,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'kawaoke',65,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キャソセル',66,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キャンセル',66,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キュンセル',66,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'キュソセル',66,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ファイル',67,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ファル',67,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ウァイル',67,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ウァル',67,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ワイシャシ',68,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ワイシャツ',68,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ワイツャツ',68,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'ワイツャシ',68,0);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'pyosoyan',69,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'pyosoyaso',69,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'pyonyaso',69,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'pyonyan',69,1);
                                                          
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'piripin',70,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'firipin',70,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'philipin',70,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES (N'phiripin',70,0);


--Insert data to Transaction table
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType ) VALUES (N'student1 nạp 700000 VNĐ',700000,'2017/08/05 16:05:00',1,1);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType ) VALUES (N'student1 nạp 300000 VNĐ',300000,'2017/08/06 13:00:00',1,1);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType ) VALUES (N'student2 nạp 500000 VNĐ',500000,'2017/08/01 5:00:00',2,1);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType ) VALUES (N'Trả lương cho tutor1',1000000,'2017/08/07 08:00:00',1,2);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType ) VALUES (N'Trả tiền dạy tiết 1 ngày 08/08/2017 cho tutor1',120000,'2017/08/08 09:00:00',1,2);
INSERT INTO [Transaction] (Content,Amount,TranDate,UserID,UserType ) VALUES (N'Trả lương cho tutor2',2500000,'2017/08/08 08:05:00',2,2);                                       