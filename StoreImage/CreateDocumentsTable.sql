IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Document]') AND type in (N'U'))
DROP TABLE [dbo].[Document]
GO

CREATE TABLE [dbo].[Document](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Size] [bigint] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

