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
	[Address] [nvarchar](100) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Balance] [money] NULL,
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
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Balance] [money] NULL,
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
	[Address] [nvarchar](60) NULL,
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
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar] (50) NOT NULL,
	[SkypeId] [nvarchar] (100) NOT NULL,
	[UserName] [nvarchar] (100) Unique NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Balance] [money] NULL,
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
INSERT INTO [Role] (RoleName) VALUES ('System Admin');
INSERT INTO [Role] (RoleName) VALUES ('Supporter');
INSERT INTO [Role] (RoleName) VALUES ('Accountant');
INSERT INTO [Role] (RoleName) VALUES ('Manager');
INSERT INTO [Role] (RoleName) VALUES ('Parent');
INSERT INTO [Role] (RoleName) VALUES ('Student');
INSERT INTO [Role] (RoleName) VALUES ('Tutor');
INSERT INTO [Role] (RoleName) VALUES ('PreTutor');

--Insert data to Categories table 
INSERT INTO [Category] (CategoryName, [Description]) VALUES ('Tiếng Nhật', NULL);
INSERT INTO [Category] (CategoryName, [Description]) VALUES ('Tiếng Anh', NULL);


--Insert data to Subjects Table
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Bảng chữ cái Hiragana', 1, 'Khóa học gồm 8 buổi, được thiết kế dành cho học viên bắt đầu học tiếng Nhật', 'Học viên có thể viết, đọc, sử dụng thành thạo bảng chữ cái Hiragana', NULL, NULL);
INSERT INTO [Subject] (SubjectName, CategoryID, [Description], Purpose, Requirement, Photo) VALUES ('Bảng chữ cái Katakana', 1, 'Khóa học gồm 8 buổi, được thiết kế dành cho học viên bắt đầu học tiếng Nhật', 'Học viên có thể viết, đọc, sử dụng thành thạo bảng chữ cái Katakana', NULL, NULL);


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
INSERT INTO [Parent] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (5,'Nguyen','parent1',29-4-1975,1,'020, đường Kim Đồng','parent1@gmail.com','parent1','parent1','parent1','Hà Nội',null,'Việt Nam','0123456789',0,null,null);
INSERT INTO [Parent] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (5,'Tran','parent2',29-4-1970,2,'030, đường Kim Mã','parent2@gmail.com','parent2','parent2','parent2','Hà Nội',null,'Việt Nam','0123789456',0,null,null);

--Insert data to Student table
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,1,'Nguyen','Student1',3-3-2000,1,'020, đường Kim Đồng','student1@gmail.com','student1','student1','student1','Hà Nội',null,'Việt Nam','01632594938',0,null,null);
INSERT INTO [Student] (RoleId,ParentId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,Photo,[Description]) VALUES (6,2,'Tran','Student2',1-3-2002,1,'030, đường Kim Mã','student2@gmail.com','student2','student2','student2','Hà Nội',null,'Việt Nam','01632594938',0,null,null);


--Insert data to Tutor table
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName) VALUES (7,'Lý','tutor1',3-3-1983,2,'Trần Duy Hưng','tutor1@gmail.com','tutor1','tutor1','tutor1','Hà Nội',null,'Việt Nam','01632594938',0,'9876543210',0,null,null,'ngân hàng Tiên Phong','tutor1');
INSERT INTO [Tutor] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,SkypeId,UserName,[Password],City,PostalCode,Country,PhoneNumber,Balance,BankId,Salary,Photo,[Description],BankName,BMemName) VALUES (7,'Nguyễn','tutor2',3-9-1986,2,'Hoàng Quốc Việt','tutor2@gmail.com','tutor2','tutor2','tutor2','Hà Nội',null,'Việt Nam','01635594998',0,'9876598210',0,null,null,'ngân hàng Tiên Phong','tutor2');

																																																								
--Insert data to BackendUser table
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (1,'Dương','SystemAdmin',25-3-1985,1,null,'systemadmin@gmail.com','systemadmin','systemadmin','Hà Nội','Việt Nam','01697404180',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,'Trần','supporter1',25-3-1985,1,null,'supporter1@gmail.com','supporter1','supporter1','Hà Nội','Việt Nam','012351213265',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,'Nguyễn','supporter2',2-3-1980,1,null,'supporter2@gmail.com','supporter2','supporter2','Hà Nội','Việt Nam','012354213233',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (3,'Trịnh','accountant1',22-12-1985,2,null,'accountant1@gmail.com','accountant1','accountant1','Cao Bằng','Việt Nam','012351415466',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (3,'Bế','accountant2',20-12-1982,1,null,'accountant2@gmail.com','accountant2','accountant2','Đà Nẵng','Việt Nam','012351498466',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,'Nguyễn','supporter3',2-10-1981,2,null,'supporter3@gmail.com','supporter3','supporter3','Thái Nguyên','Việt Nam','019352415466',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,'Bế','supporter4',21-12-1985,1,null,'supporter4@gmail.com','supporter4','supporter4','Đà Nẵng','Việt Nam','012451498466',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (2,'Nguyễn','supporter5',2-11-19810,1,null,'supporter5@gmail.com','supporter5','supporter5','Thái Nguyên','Việt Nam','010352415466',null,null);
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (4,'Phạm','manager1',29-4-1985,1,null,'manager1@gmail.com','manager1','manager1','Đà Nẵng','Viet Nam','01289123566',null,null);		
INSERT INTO [BackendUser] (RoleId,LastName,FirstName,BirthDate,Gender,[Address],Email,UserName,[Password],City,Country,PhoneNumber,Photo,[Description]) VALUES (4,'Phạm','manager2',22-4-1981,1,null,'manager2@gmail.com','manager2','manager2','Hải Phòng','Viet Nam','01989123566',null,null);	
	
--Insert data to MaterialumentType table
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('pdf');
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('docx');
INSERT INTO [MaterialType] (MaterialTypeName) VALUES ('mp3');

--Insert data to Lesson table
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (1,'Bài 1', 1, 'Giới thiệu khái quát về chương trình,học Hiragana: 10 chữ cái đầu tiên, chào hỏi cơ bản 1')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (2,'Bài 2', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản 1, Học Hiragana: 10 chữ tiếp theo, chào hỏi cơ bản 2')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (3,'Bài 3', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản 2, Học Hiragana: 10 chữ tiếp theo, chào hỏi cơ bản 3')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (4,'Bài 4', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản 3, Học Hiragana: 8 chữ tiếp theo, chào hỏi cơ bản 4')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (5,'Bài 5', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản 4, Học Hiragana: 8 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (6,'Bài 6', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản, Học Hiragana: Âm đục + Âm bán đục ')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (7,'Bài 7', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản, Học Hiragana: Âm ghép')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (8,'Bài 8', 1, 'Luyện tập: Hiragana và Chào hỏi cơ bản, Học Hiragana: Âm dài + Âm ngắt')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (1,'Bài 1', 2, 'Giới thiệu khái quát về chương trình,học Katakana: Âm dài + âm ngắt + 10 chữ cái đầu tiên')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (2,'Bài 2', 2, 'Luyện tập: Katakana, Học Katakana: 10 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (3,'Bài 3', 2, 'Luyện tập: Katakana, Học Katakana: 10 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (4,'Bài 4', 2, 'Luyện tập: Katakana, Học Katakana: 8 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (5,'Bài 5', 2, 'Luyện tập: Katakana, Học Katakana: 8 chữ tiếp theo')
INSERT INTO [Lesson] ([Order],LessonName,SubjectId,Content) VALUES (6,'Bài 6', 2, 'Luyện tập: Katakana, Học Katakana: Âm ghép')
							 
--Insert data to Material table 



-- Insert data to Schedule table


--Insert data to Criteria table ?????
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',1,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',1,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',1,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',2,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',2,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',2,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',3,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',3,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',3,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',4,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',4,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',4,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',5,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',5,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',5,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',6,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',6,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',6,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',7,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',7,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',7,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',8,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',8,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',8,1);

INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',9,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',9,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',9,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',10,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',10,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',10,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',11,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',11,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',11,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',12,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',12,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',12,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',13,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',13,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',13,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng đọc',14,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng phát âm',14,1);
INSERT INTO [Criteria] (CriteriaName,LessonId,RoleId) VALUES ('Khả năng tiếp thu',14,1);

--Insert data to StudentFeedback


--Insert data to TutorFeedback table


--Insert data to TutorFeedback detail table


--Insert data to StudentSubject table


--Insert data to TutorSubject table
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,1,6,'1 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (1,2,6,'2 năm');
INSERT INTO TutorSubject (SubjectId,TutorId,Status,Experience) VALUES (2,1,6,'1 năm');
--Insert data to TeachSchedule table


--Insert data to Question table
INSERT INTO Question (Content,LessonId) VALUES ('ue',1);
INSERT INTO Question (Content,LessonId) VALUES ('kaki',1);
INSERT INTO Question (Content,LessonId) VALUES ('koe',1);
INSERT INTO Question (Content,LessonId) VALUES ('えき',1);
INSERT INTO Question (Content,LessonId) VALUES ('いけ',1);

INSERT INTO Question (Content,LessonId) VALUES ('keisatsu',2);
INSERT INTO Question (Content,LessonId) VALUES ('tokei',2);
INSERT INTO Question (Content,LessonId) VALUES ('asa',2);
INSERT INTO Question (Content,LessonId) VALUES ('くつ',2);
INSERT INTO Question (Content,LessonId) VALUES ('せき',2);

INSERT INTO Question (Content,LessonId) VALUES ('ano sakana',3);
INSERT INTO Question (Content,LessonId) VALUES ('nani',3);
INSERT INTO Question (Content,LessonId) VALUES ('niku',3);
INSERT INTO Question (Content,LessonId) VALUES ('たいせつ',3);
INSERT INTO Question (Content,LessonId) VALUES ('ちかてつ',3);

INSERT INTO Question (Content,LessonId) VALUES ('yasumi',4);
INSERT INTO Question (Content,LessonId) VALUES ('moshi moshi',4);
INSERT INTO Question (Content,LessonId) VALUES ('yakiniku',4);
INSERT INTO Question (Content,LessonId) VALUES ('ともだち',4);
INSERT INTO Question (Content,LessonId) VALUES ('さむい',4);

INSERT INTO Question (Content,LessonId) VALUES ('yakitori',5);
INSERT INTO Question (Content,LessonId) VALUES ('sumimasen',5);
INSERT INTO Question (Content,LessonId) VALUES ('kuruma',5);
INSERT INTO Question (Content,LessonId) VALUES ('せんたく',5);
INSERT INTO Question (Content,LessonId) VALUES ('わたし',5);

INSERT INTO Question (Content,LessonId) VALUES ('hajime mashite  ',6);
INSERT INTO Question (Content,LessonId) VALUES ('shinbun',6);
INSERT INTO Question (Content,LessonId) VALUES ('denwa',6);
INSERT INTO Question (Content,LessonId) VALUES ('すごいですね',6);
INSERT INTO Question (Content,LessonId) VALUES ('ぎんざ',6);

INSERT INTO Question (Content,LessonId) VALUES ('okyakusan',7);
INSERT INTO Question (Content,LessonId) VALUES ('kanojo',7);
INSERT INTO Question (Content,LessonId) VALUES ('ocha',7);
INSERT INTO Question (Content,LessonId) VALUES ('しゅくだい',7);
INSERT INTO Question (Content,LessonId) VALUES ('かいしゃ',7);

INSERT INTO Question (Content,LessonId) VALUES ('roppyakuen',8);
INSERT INTO Question (Content,LessonId) VALUES ('yūmei',8);
INSERT INTO Question (Content,LessonId) VALUES ('chūgoku',8);
INSERT INTO Question (Content,LessonId) VALUES ('がっこう',8);
INSERT INTO Question (Content,LessonId) VALUES ('いっかい',8);

INSERT INTO Question (Content,LessonId) VALUES ('kukkī',9);
INSERT INTO Question (Content,LessonId) VALUES ('kokekokkō',9);
INSERT INTO Question (Content,LessonId) VALUES ('kēki',9);
INSERT INTO Question (Content,LessonId) VALUES ('コーク',9);
INSERT INTO Question (Content,LessonId) VALUES ('キック',9);

INSERT INTO Question (Content,LessonId) VALUES ('テキスト',10);
INSERT INTO Question (Content,LessonId) VALUES ('デザート',10);
INSERT INTO Question (Content,LessonId) VALUES ('kēsu',10);
INSERT INTO Question (Content,LessonId) VALUES ('sōsēji',10);
INSERT INTO Question (Content,LessonId) VALUES ('uisukī',10);

INSERT INTO Question (Content,LessonId) VALUES ('supīdo',11);
INSERT INTO Question (Content,LessonId) VALUES ('hītā',11);
INSERT INTO Question (Content,LessonId) VALUES ('nōto',11);
INSERT INTO Question (Content,LessonId) VALUES ('ネクタイ',11);
INSERT INTO Question (Content,LessonId) VALUES ('パスタ',11);

INSERT INTO Question (Content,LessonId) VALUES ('roppyakuen',12);
INSERT INTO Question (Content,LessonId) VALUES ('gamu',12);
INSERT INTO Question (Content,LessonId) VALUES ('mōtā',12);
INSERT INTO Question (Content,LessonId) VALUES ('ハム',12);
INSERT INTO Question (Content,LessonId) VALUES ('ヨット',12);

INSERT INTO Question (Content,LessonId) VALUES ('supōtsukurabu',13);
INSERT INTO Question (Content,LessonId) VALUES ('emēru',13);
INSERT INTO Question (Content,LessonId) VALUES ('hoteru',13);
INSERT INTO Question (Content,LessonId) VALUES ('レストラン',13);
INSERT INTO Question (Content,LessonId) VALUES ('カラオケ',13);

INSERT INTO Question (Content,LessonId) VALUES ('kyanseru',14);
INSERT INTO Question (Content,LessonId) VALUES ('fairu',14);
INSERT INTO Question (Content,LessonId) VALUES ('waishatsu',14);
INSERT INTO Question (Content,LessonId) VALUES ('ピョンヤン',14);
INSERT INTO Question (Content,LessonId) VALUES ('フィリピン',14);


--Insert data to Answer table
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('うえ',1,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('うけ',1,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('いか',1,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('いえ',1,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('かこ',2,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('こけ',2,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('かき',2,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('きか',2,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('えこ',3,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('こえ',3,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('けお',3,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おけ',3,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('eki',4,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ei',4,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ike',4,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ie',4,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('eki',5,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ei',5,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ike',5,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ie',5,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('せいさつ',6,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('けいさく',6,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('けいかつ',6,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('けいさつ',6,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('とけえ',7,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('とけい',7,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おてい',7,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おけい',7,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('あさ',8,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おそ',8,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('えせ',8,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('いき',8,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kutsu',9,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kusu',9,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tsusu',9,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tsuku',9,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('keki',10,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('seshi',10,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kechi',10,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('seki',10,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('あにさかな',11,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('あのさかの',11,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('あのさかな',11,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('あにさきの',11,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('なに',12,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('にく',12,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('あの',12,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('あに',12,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('にき',13,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('にく',13,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('くに',13,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('にす',13,0);



INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('taisetsu',14,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tasetsu',14,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('taseitsu',14,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('taisesu',14,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('chikaketsu',15,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('chikasetsu',15,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('chikatetsu',15,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('chikaesu',15,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やすみ',16,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やすむ',16,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ゆすま',16,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ゆすむ',16,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('もさもさ',17,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('もしもし',17,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('みそみそ',17,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('もかもか',17,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やきぬく',18,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やきにか',18,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やきにく',18,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ゆきにく',18,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tomodachi',19,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tamagochi',19,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tamadachi',19,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tomakuchi',19,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('samuu',20,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('samui',20,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('samiu',20,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('samii',20,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ゆきとり',21,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やきとり',21,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やきにく',21,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('やきぬり',21,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('しみません',22,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('すみません',22,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('すまみせん',22,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('しみまけん',22,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('くるま',23,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('くれめ',23,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('くるみ',23,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('かるま',23,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('sentaki',24,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('sentaku',24,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kentaku',24,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kentaki',24,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('watashi',25,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('wasashi',25,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('wataki',25,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('wasaki',25,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('はじままして',26,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('はじめまして',26,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('はじめみして',26,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ばじめまじて',26,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('しんぷん',27,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('しんぶん',27,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('じんぷん',27,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('じんぶん',27,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('でんが',28,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('てんわ',28,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('でんわ',28,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('てんが',28,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('sugoidesune',29,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('sugiodesune',29,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('zugoidesune',29,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('zugoitesune',29,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ginza',30,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kinza',30,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kinsa',30,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kinka',30,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おきゃくさん',31,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おきゅくかん',31,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おきゃくさん',31,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おきゅくさん',31,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('かのしょ',32,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('かのじょ',32,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('かのじょう',32,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('かのしょう',32,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おちゃ',33,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('こちゃ',33,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('おちょ',33,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('こちょう',33,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('shokudai',34,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('shokkudai',34,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('shukkudai',34,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('shukudai',34,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('saisha',35,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kaisha',35,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kasha',35,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kashu',35,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('るっぴゃくえん',36,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ろっぴゃくえん',36,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ろぴゃくえん',36,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('るぴゃくえん',36,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ゆうめい',37,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ゆいめい',37,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ようめい',37,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ようめ',37,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ちゅごく',38,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ちゅこく',38,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ちゅうごく',38,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ちゅうこく',38,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kakkou',39,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kakou',39,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('gakou',39,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('gakkou',39,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ikai',40,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ikkai',40,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('iikai',40,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('iikkai',40,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('クッキー',41,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('クキー',41,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キクー',41,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キックー',41,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('コケココ',42,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('コケココー',42,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('コケコッコー',42,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('コケッココー',42,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ケキ',43,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ケーキ',43,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ケキー',43,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ケーキー',43,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kōku',44,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kōki',44,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kōke',44,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kōka',44,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('gakku',45,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kikku',45,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('gikku',45,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kiggu',45,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tekisuto',46,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tegisuto',46,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tekizuto',46,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('tokisuso',46,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('dezāso',47,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('dezāto',47,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('detāto',47,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('desāto',47,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ケース',48,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キース',48,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ケーズ',48,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キーズ',48,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ソーセジ',49,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ソケージ',49,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ソーケージ',49,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ソーセージ',49,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ウイスキ',50,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('イエスキー',50,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ウオスキー',50,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ウイスキー',50,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スピドー',51,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スピード',51,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スビドー',51,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スビード',51,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ヒーター',52,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ヘーター',52,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ヒートー',52,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ヘートー',52,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ニーチ',53,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ノート',53,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ネーテ',53,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ナータ',53,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('nekuta',54,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('nekitai',54,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('nekutai',54,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('nekutaii',54,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('basuta',55,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('hashita',55,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('pasuta',55,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('pashita',55,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ミッセージ',56,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('メッセージ',56,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('マッサージ',56,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('メッサージ',56,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('カマ',57,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ガマ',57,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('カム',57,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ガム',57,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('モーテー',58,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('モートー',58,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('モーター',58,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('モッター',58,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('hamu',59,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kamu',59,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('bamu',59,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('samu',59,0);


INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('yotto',60,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('yōtto',60,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('yōto',60,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('yottō',60,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スポースクラブ',61,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スポースケラブ',61,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スポースクワブ',61,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('スポースケワブ',61,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('イマール',62,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('イマール',62,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('エマール',62,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('エメール',62,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ホテル',63,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ホチル',63,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ホチス',63,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ホテス',63,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('resutoran',64,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('resutoran',64,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('esutore',64,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('resutoso',64,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kawaoku',65,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('karaoku',65,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('karaoke',65,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('kawaoke',65,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キャソセル',66,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キャンセル',66,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キュンセル',66,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('キュソセル',66,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ファイル',67,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ファル',67,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ウァイル',67,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ウァル',67,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ワイシャシ',68,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ワイシャツ',68,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ワイツャツ',68,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('ワイツャシ',68,0);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('pyosoyan',69,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('pyosoyaso',69,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('pyonyaso',69,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('pyonyan',69,1);

INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('piripin',70,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('firipin',70,1);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('philipin',70,0);
INSERT INTO Answer (Content,QuestionId,isCorrect) VALUES ('phiripin',70,0);


--Insert data to Transaction table


