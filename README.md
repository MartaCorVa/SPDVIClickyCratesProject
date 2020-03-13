CREATE TABLE [dbo].[Game] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [IdUser]     NVARCHAR (128) NOT NULL,
    [DateStart]  DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [DateStop]   DATETIME2 (7)  NULL,
    [Difficulty] INT            NULL,
    [Score]      INT            NULL,
    [Duration]   INT            NULL
);

CREATE TABLE [dbo].[Online] (
    [Id]         NVARCHAR (128) NOT NULL,
    [State]      NVARCHAR (50)  NULL,
    [NickName]   NVARCHAR (128) NULL,
    [Level]      NVARCHAR (128) NULL,
    [LevelBadge] NVARCHAR (128) NULL,
    [Image]      NVARCHAR (128) NULL
);

CREATE TABLE [dbo].[Player] (
    [Id]          NVARCHAR (128) NOT NULL,
    [FirstName]   NVARCHAR (50)  NOT NULL,
    [LastName]    NVARCHAR (50)  NOT NULL,
    [NickName]    NVARCHAR (50)  NOT NULL,
    [Email]       NVARCHAR (50)  NOT NULL,
    [DateJoined]  DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [DateOfBirth] DATETIME2 (7)  NOT NULL,
    [City]        NVARCHAR (50)  NOT NULL,
    [BlobUri]     NVARCHAR (512) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_User_ToAspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

