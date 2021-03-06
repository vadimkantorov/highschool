IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'NeJudge')
	DROP DATABASE [NeJudge]
GO

CREATE DATABASE [NeJudge]
 COLLATE Cyrillic_General_CI_AS
GO

exec sp_dboption N'NeJudge', N'autoclose', N'true'
GO

exec sp_dboption N'NeJudge', N'bulkcopy', N'false'
GO

exec sp_dboption N'NeJudge', N'trunc. log', N'true'
GO

exec sp_dboption N'NeJudge', N'torn page detection', N'true'
GO

exec sp_dboption N'NeJudge', N'read only', N'false'
GO

exec sp_dboption N'NeJudge', N'dbo use', N'false'
GO

exec sp_dboption N'NeJudge', N'single', N'false'
GO

exec sp_dboption N'NeJudge', N'autoshrink', N'true'
GO

exec sp_dboption N'NeJudge', N'ANSI null default', N'false'
GO

exec sp_dboption N'NeJudge', N'recursive triggers', N'false'
GO

exec sp_dboption N'NeJudge', N'ANSI nulls', N'false'
GO

exec sp_dboption N'NeJudge', N'concat null yields null', N'false'
GO

exec sp_dboption N'NeJudge', N'cursor close on commit', N'false'
GO

exec sp_dboption N'NeJudge', N'default to local cursor', N'false'
GO

exec sp_dboption N'NeJudge', N'quoted identifier', N'false'
GO

exec sp_dboption N'NeJudge', N'ANSI warnings', N'false'
GO

exec sp_dboption N'NeJudge', N'auto create statistics', N'true'
GO

exec sp_dboption N'NeJudge', N'auto update statistics', N'true'
GO

if( ( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) ) or ( (@@microsoftversion / power(2, 24) = 7) and (@@microsoftversion & 0xffff >= 1082) ) )
	exec sp_dboption N'NeJudge', N'db chaining', N'false'
GO

use [NeJudge]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Questions_Contests]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Questions] DROP CONSTRAINT FK_Questions_Contests
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Submissions_Contests]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Submissions] DROP CONSTRAINT FK_Submissions_Contests
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Questions_Problems]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Questions] DROP CONSTRAINT FK_Questions_Problems
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Submissions_Problems]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Submissions] DROP CONSTRAINT FK_Submissions_Problems
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Questions_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Questions] DROP CONSTRAINT FK_Questions_Users
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Submissions_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Submissions] DROP CONSTRAINT FK_Submissions_Users
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Contests]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Contests]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Problems]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Problems]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Questions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Questions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Submissions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Submissions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users]
GO

if not exists (select * from master.dbo.syslogins where loginname = N'KANTOROV\ASPNET')
	exec sp_grantlogin N'KANTOROV\ASPNET'
	exec sp_defaultdb N'KANTOROV\ASPNET', N'NeJudge'
	exec sp_defaultlanguage N'KANTOROV\ASPNET', N'русский'
GO

if not exists (select * from dbo.sysusers where name = N'KANTOROV\ASPNET' and uid < 16382)
	EXEC sp_grantdbaccess N'KANTOROV\ASPNET', N'KANTOROV\ASPNET'
GO

exec sp_addrolemember N'db_owner', N'KANTOROV\ASPNET'
GO

CREATE TABLE [dbo].[Contests] (
	[TID] [int] IDENTITY (1, 1) NOT NULL ,
	[Beginning] [datetime] NOT NULL ,
	[Ending] [datetime] NOT NULL ,
	[Name] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Problems] (
	[PID] [int] IDENTITY (1, 1) NOT NULL ,
	[TID] [int] NOT NULL ,
	[Name] [nvarchar] (300) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Text] [ntext] COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[InputFormat] [ntext] COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[OutputFormat] [ntext] COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[InputSample] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[OutputSample] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[ShortName] [nvarchar] (30) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Author] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Questions] (
	[QID] [int] IDENTITY (1, 1) NOT NULL ,
	[PID] [int] NOT NULL ,
	[UID] [int] NOT NULL ,
	[TID] [int] NOT NULL ,
	[Question] [nvarchar] (300) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Answer] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Submissions] (
	[SID] [int] IDENTITY (1, 1) NOT NULL ,
	[PID] [int] NOT NULL ,
	[UID] [int] NOT NULL ,
	[TID] [int] NOT NULL ,
	[Time] [datetime] NOT NULL ,
	[Code] [nvarchar] (5) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Test] [int] NOT NULL ,
	[Language] [nvarchar] (7) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Info] [nvarchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Memory Used] [int] NOT NULL ,
	[Time Worked] [real] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users] (
	[UID] [int] IDENTITY (1, 1) NOT NULL ,
	[Username] [nvarchar] (14) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Password] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Fullname] [nvarchar] (60) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Mail] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[Role] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contests] ADD 
	CONSTRAINT [PK_Contests] PRIMARY KEY  CLUSTERED 
	(
		[TID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Problems] ADD 
	CONSTRAINT [PK_Problems] PRIMARY KEY  CLUSTERED 
	(
		[PID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Questions] ADD 
	CONSTRAINT [PK_Questions] PRIMARY KEY  CLUSTERED 
	(
		[QID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Submissions] ADD 
	CONSTRAINT [DF_Submissions_Test] DEFAULT (0) FOR [Test],
	CONSTRAINT [PK_Submissions] PRIMARY KEY  CLUSTERED 
	(
		[SID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users] ADD 
	CONSTRAINT [PK_Users] PRIMARY KEY  CLUSTERED 
	(
		[UID]
	)  ON [PRIMARY] 
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[Contests]  TO [KANTOROV\ASPNET]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[Problems]  TO [KANTOROV\ASPNET]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[Questions]  TO [KANTOROV\ASPNET]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[Submissions]  TO [KANTOROV\ASPNET]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[Users]  TO [KANTOROV\ASPNET]
GO

ALTER TABLE [dbo].[Questions] ADD 
	CONSTRAINT [FK_Questions_Contests] FOREIGN KEY 
	(
		[TID]
	) REFERENCES [dbo].[Contests] (
		[TID]
	) ON DELETE CASCADE  ON UPDATE CASCADE  NOT FOR REPLICATION ,
	CONSTRAINT [FK_Questions_Problems] FOREIGN KEY 
	(
		[PID]
	) REFERENCES [dbo].[Problems] (
		[PID]
	) ON DELETE CASCADE  ON UPDATE CASCADE  NOT FOR REPLICATION ,
	CONSTRAINT [FK_Questions_Users] FOREIGN KEY 
	(
		[UID]
	) REFERENCES [dbo].[Users] (
		[UID]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[Submissions] ADD 
	CONSTRAINT [FK_Submissions_Contests] FOREIGN KEY 
	(
		[TID]
	) REFERENCES [dbo].[Contests] (
		[TID]
	) ON DELETE CASCADE  ON UPDATE CASCADE  NOT FOR REPLICATION ,
	CONSTRAINT [FK_Submissions_Problems] FOREIGN KEY 
	(
		[PID]
	) REFERENCES [dbo].[Problems] (
		[PID]
	) ON DELETE CASCADE  ON UPDATE CASCADE  NOT FOR REPLICATION ,
	CONSTRAINT [FK_Submissions_Users] FOREIGN KEY 
	(
		[UID]
	) REFERENCES [dbo].[Users] (
		[UID]
	) ON DELETE CASCADE  ON UPDATE CASCADE  NOT FOR REPLICATION 
GO

