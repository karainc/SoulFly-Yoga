CREATE TABLE [dbo].[Users] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [displayName] NVARCHAR (255) NOT NULL,
    [email]       NVARCHAR (255) NOT NULL,
    [password]    NVARCHAR (255) NOT NULL,
    [birthday]    NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

CREATE TABLE [dbo].[Poses] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (255) NOT NULL,
    [description] TEXT           NOT NULL,
    [image]       NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

GO

CREATE TABLE [dbo].[Routine] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [userId]       INT            NOT NULL,
    [intention]    NVARCHAR (255) NOT NULL,
    [poseId]       INT            NOT NULL,
    [cycles]       INT            NOT NULL,
    [creationDate] DATETIME       NOT NULL,
    [reflection]   TEXT           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Routine_Users] FOREIGN KEY ([userId]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_Routine_Poses] FOREIGN KEY ([poseId]) REFERENCES [dbo].[Poses] ([id])
);
GO

CREATE TABLE [dbo].[RoutinePoses] (
    [id]        INT IDENTITY (1, 1) NOT NULL,
    [routineId] INT NOT NULL,
    [poseId]    INT NOT NULL,
    [commentId] INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_RoutinePoses_Comment] FOREIGN KEY ([commentId]) REFERENCES [dbo].[Comment] ([id]),
    CONSTRAINT [FK_RoutinePoses_Routine] FOREIGN KEY ([routineId]) REFERENCES [dbo].[Routine] ([id]),
    CONSTRAINT [FK_RoutinePoses_Poses] FOREIGN KEY ([poseId]) REFERENCES [dbo].[Poses] ([id])
);

GO

CREATE TABLE [dbo].[Comment] (
    [id]        INT            IDENTITY (1, 1) NOT NULL,
    [userId]    INT            NOT NULL,
    [routineId] INT            NOT NULL,
    [text]      NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Comment_Routine] FOREIGN KEY ([routineId]) REFERENCES [dbo].[Routine] ([id]),
    CONSTRAINT [FK_Comment_Users] FOREIGN KEY ([userId]) REFERENCES [dbo].[Users] ([id])
);

GO

ALTER TABLE [Routine] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([routineId]) REFERENCES [Routine] ([id])
GO

ALTER TABLE [RoutinePoses] ADD FOREIGN KEY ([poseId]) REFERENCES [Poses] ([id])
GO

ALTER TABLE [RoutinePoses] ADD FOREIGN KEY ([routineId]) REFERENCES [Routine] ([id])
GO

ALTER TABLE [Routine] ADD FOREIGN KEY ([poseId]) REFERENCES [Poses] ([id])
GO

ALTER TABLE [RoutinePoses] ADD FOREIGN KEY ([commentId]) REFERENCES [Comment] ([id])
GO
