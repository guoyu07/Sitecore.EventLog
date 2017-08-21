/****** Object:  StoredProcedure [dbo].[GetSaveDetailItemById]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[GetSaveDetailItemById]
GO
/****** Object:  StoredProcedure [dbo].[GetPublishDetailItemById]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[GetPublishDetailItemById]
GO
/****** Object:  StoredProcedure [dbo].[GetPagedEvents]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[GetPagedEvents]
GO
/****** Object:  StoredProcedure [dbo].[GetEventsByItemId]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[GetEventsByItemId]
GO
/****** Object:  StoredProcedure [dbo].[GetEventItemById]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[GetEventItemById]
GO
/****** Object:  StoredProcedure [dbo].[CountEventsByItemId]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[CountEventsByItemId]
GO
/****** Object:  StoredProcedure [dbo].[CountEvents]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[CountEvents]
GO
/****** Object:  StoredProcedure [dbo].[AddSaveDetailItem]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[AddSaveDetailItem]
GO
/****** Object:  StoredProcedure [dbo].[AddPublishDetailItem]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[AddPublishDetailItem]
GO
/****** Object:  StoredProcedure [dbo].[AddEventItem]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP PROCEDURE [dbo].[AddEventItem]
GO
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_PublishDetail1]
GO
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_PublishDetail]
GO
/****** Object:  Index [IX_FK_Event_PublishDetail1]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP INDEX [IX_FK_Event_PublishDetail1] ON [dbo].[Event]
GO
/****** Object:  Index [IX_FK_Event_PublishDetail]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP INDEX [IX_FK_Event_PublishDetail] ON [dbo].[Event]
GO
/****** Object:  Table [dbo].[SaveDetail]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP TABLE [dbo].[SaveDetail]
GO
/****** Object:  Table [dbo].[PublishDetail]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP TABLE [dbo].[PublishDetail]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP TABLE [dbo].[Logs]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 3/6/2017 12:57:20 PM ******/
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[ItemPath] [nvarchar](max) NOT NULL,
	[EventType] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[SourceDatabase] [nvarchar](50) NOT NULL,
	[ItemVersion] [int] NULL,
	[PublishDetailId] [int] NULL,
	[SaveDetailId] [int] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[Logs]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](20) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL,
	[SCUser] [varchar](255) NULL,
	[SCAction] [varchar](255) NULL,
	[SCItemPath] [varchar](255) NULL,
	[SCLanguage] [varchar](100) NULL,
	[SCVersion] [varchar](100) NULL,
	[SCItemId] [varchar](38) NULL,
	[SiteName] [varchar](255) NULL,
	[SCMisc] [varchar](255) NULL
)

GO
/****** Object:  Table [dbo].[PublishDetail]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublishDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cultures] [nvarchar](max) NOT NULL,
	[TargetDatabases] [nvarchar](max) NOT NULL,
	[IsSitePublish] [bit] NOT NULL,
	[WithSubItems] [bit] NOT NULL,
	[WithRelatedItems] [bit] NOT NULL,
	[RootItemId] [uniqueidentifier] NULL,
	[RootItemPath] [nvarchar](max) NOT NULL,
	[PublishType] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PublishDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[SaveDetail]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaveDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Language] [nvarchar](max) NULL,
	[Changes] [nvarchar](max) NULL,
 CONSTRAINT [PK_SaveDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Index [IX_FK_Event_PublishDetail]    Script Date: 3/6/2017 12:57:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Event_PublishDetail] ON [dbo].[Event]
(
	[SaveDetailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_FK_Event_PublishDetail1]    Script Date: 3/6/2017 12:57:20 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Event_PublishDetail1] ON [dbo].[Event]
(
	[PublishDetailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_PublishDetail] FOREIGN KEY([SaveDetailId])
REFERENCES [dbo].[SaveDetail] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_PublishDetail]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_PublishDetail1] FOREIGN KEY([PublishDetailId])
REFERENCES [dbo].[PublishDetail] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_PublishDetail1]
GO
/****** Object:  StoredProcedure [dbo].[AddEventItem]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddEventItem]  
@ItemId uniqueidentifier,
@ItemPath nvarchar(max),
@EventType int,
@Date datetime,
@UserName nvarchar(max),
@SourceDatabase nvarchar(50),
@ItemVersion int = null,
@PublishDetailId int = null,
@SaveDetailId int = null
AS
BEGIN
	INSERT INTO [dbo].[Event]
           ([ItemId]
           ,[ItemPath]
           ,[EventType]
           ,[Date]
           ,[UserName]
           ,[SourceDatabase]
           ,[ItemVersion]
           ,[PublishDetailId]
           ,[SaveDetailId])
     VALUES
           (@ItemId,@ItemPath,@EventType,@Date,@UserName,@SourceDatabase,@ItemVersion,@PublishDetailId,@SaveDetailId)

	 SELECT * FROM [dbo].[Event] WHERE Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[AddPublishDetailItem]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPublishDetailItem]  
@Cultures nvarchar(max),
@TargetDatabases nvarchar(max),
@IsSitePublish bit,
@WithSubItems bit,
@WithRelatedItems bit,
@RootItemId uniqueidentifier,
@RootItemPath nvarchar(max),
@PublishType nvarchar(max)
AS
BEGIN
	INSERT INTO [dbo].[PublishDetail]
           ([Cultures]
           ,[TargetDatabases]
           ,[IsSitePublish]
           ,[WithSubItems]
           ,[WithRelatedItems]
           ,[RootItemId]
           ,[RootItemPath]
           ,[PublishType])
     VALUES
           (@Cultures,@TargetDatabases,@IsSitePublish,@WithSubItems,@WithRelatedItems,@RootItemId,@RootItemPath,@PublishType)

	 SELECT * FROM [dbo].[PublishDetail] WHERE Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[AddSaveDetailItem]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSaveDetailItem]  
@Language nvarchar(max), 
@Changes nvarchar(max) 
AS
BEGIN
	INSERT INTO [dbo].[SaveDetail]
           ([Language],[Changes])
     VALUES
           (@Language,@Changes)

	 SELECT * FROM [dbo].[SaveDetail] WHERE Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[CountEvents]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[CountEvents]  
AS
BEGIN
	SELECT count(*) FROM dbo.[Event] 
END
GO
/****** Object:  StoredProcedure [dbo].[CountEventsByItemId]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[CountEventsByItemId]  
@ItemId uniqueidentifier
AS
BEGIN
	SELECT count(*) FROM dbo.[Event]
	WHERE ItemId = @ItemId 
END
GO
/****** Object:  StoredProcedure [dbo].[GetEventItemById]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetEventItemById]  
@Id int
AS
BEGIN
	SELECT * FROM dbo.[Event]
	WHERE Id = @Id 
END
GO
/****** Object:  StoredProcedure [dbo].[GetEventsByItemId]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEventsByItemId]
	@ItemId uniqueidentifier,
	@MaximumRows int,
	@StartRows int
AS
BEGIN
	SELECT * FROM dbo.[Event] 
	WHERE ItemId = @ItemId 
	ORDER BY [Date] DESC
	offset @StartRows*@MaximumRows rows
	FETCH NEXT @MaximumRows rows only
END

GO
/****** Object:  StoredProcedure [dbo].[GetPagedEvents]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetPagedEvents] 
	@MaximumRows int,
	@StartRows int
AS
BEGIN
	SELECT * FROM dbo.[Event]  
	ORDER BY [Date] DESC
	offset @StartRows rows
	FETCH NEXT @MaximumRows rows only
END
GO
/****** Object:  StoredProcedure [dbo].[GetPublishDetailItemById]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPublishDetailItemById]  
@Id int
AS
BEGIN
	SELECT * FROM dbo.[PublishDetail]
	WHERE Id = @Id 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSaveDetailItemById]    Script Date: 3/6/2017 12:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSaveDetailItemById]  
@Id int
AS
BEGIN
	SELECT * FROM dbo.[SaveDetail]
	WHERE Id = @Id 
END
GO
