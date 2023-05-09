﻿CREATE TABLE [dbo].[UserAccount]
(
	[UserAccountId] UNIQUEIDENTIFIER NOT NULL,
	[Name] VARCHAR (100) NOT NULL,
	[Email] VARCHAR (150) NOT NULL
)
GO

ALTER TABLE [dbo].[UserAccount] ADD CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED  ([UserAccountId])
GO