create database demo

USE [demo]
GO

/****** Object:  Table [dbo].[Document]    Script Date: 8/26/2015 10:39:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Document](
	[DocumentId] [int] NOT NULL identity(1,1),
	[Name] [varchar](50) NOT NULL,
	[Size] [bigint] NOT NULL,
	[type] [varchar](50) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] ,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


