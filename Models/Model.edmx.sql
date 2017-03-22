
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/11/2015 12:34:35
-- Generated from EDMX file: D:\Projects\Music Band\Symphony\Symphony\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ComposerTrack]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tracks] DROP CONSTRAINT [FK_ComposerTrack];
GO
IF OBJECT_ID(N'[dbo].[FK_GenusInstrument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Instruments] DROP CONSTRAINT [FK_GenusInstrument];
GO
IF OBJECT_ID(N'[dbo].[FK_InstrumentA1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A1] DROP CONSTRAINT [FK_InstrumentA1];
GO
IF OBJECT_ID(N'[dbo].[FK_StringerA1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A1] DROP CONSTRAINT [FK_StringerA1];
GO
IF OBJECT_ID(N'[dbo].[FK_A1A2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A2] DROP CONSTRAINT [FK_A1A2];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackA2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A2] DROP CONSTRAINT [FK_TrackA2];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackA3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A3] DROP CONSTRAINT [FK_TrackA3];
GO
IF OBJECT_ID(N'[dbo].[FK_ConcertA3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A3] DROP CONSTRAINT [FK_ConcertA3];
GO
IF OBJECT_ID(N'[dbo].[FK_ConcertA4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A4] DROP CONSTRAINT [FK_ConcertA4];
GO
IF OBJECT_ID(N'[dbo].[FK_LeaderA4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[A4] DROP CONSTRAINT [FK_LeaderA4];
GO
IF OBJECT_ID(N'[dbo].[FK_FolderFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_FolderFile];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Instruments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Instruments];
GO
IF OBJECT_ID(N'[dbo].[Admins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admins];
GO
IF OBJECT_ID(N'[dbo].[Concerts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Concerts];
GO
IF OBJECT_ID(N'[dbo].[Stringers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stringers];
GO
IF OBJECT_ID(N'[dbo].[Tracks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tracks];
GO
IF OBJECT_ID(N'[dbo].[Pics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pics];
GO
IF OBJECT_ID(N'[dbo].[Leaders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leaders];
GO
IF OBJECT_ID(N'[dbo].[A1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[A1];
GO
IF OBJECT_ID(N'[dbo].[A2]', 'U') IS NOT NULL
    DROP TABLE [dbo].[A2];
GO
IF OBJECT_ID(N'[dbo].[A3]', 'U') IS NOT NULL
    DROP TABLE [dbo].[A3];
GO
IF OBJECT_ID(N'[dbo].[A4]', 'U') IS NOT NULL
    DROP TABLE [dbo].[A4];
GO
IF OBJECT_ID(N'[dbo].[Composers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Composers];
GO
IF OBJECT_ID(N'[dbo].[Genus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genus];
GO
IF OBJECT_ID(N'[dbo].[Adverties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adverties];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Folders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Folders];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Instruments'
CREATE TABLE [dbo].[Instruments] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [Thumbnail] varbinary(max)  NULL,
    [GenusId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [Username] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Concerts'
CREATE TABLE [dbo].[Concerts] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Thumbnail] varbinary(max)  NULL
);
GO

-- Creating table 'Stringers'
CREATE TABLE [dbo].[Stringers] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [BirthYear] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [Thumbnail] varbinary(max)  NULL
);
GO

-- Creating table 'Tracks'
CREATE TABLE [dbo].[Tracks] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [Thumbnail] varbinary(max)  NULL,
    [ComposerId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Pics'
CREATE TABLE [dbo].[Pics] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Thumb] varbinary(max)  NULL,
    [Bytes] varbinary(max)  NULL,
    [ConcertId] uniqueidentifier  NULL,
    [StringerId] uniqueidentifier  NULL,
    [TrackId] uniqueidentifier  NULL,
    [LeaderId] uniqueidentifier  NULL,
    [InstrumentId] uniqueidentifier  NULL,
    [GenusId] uniqueidentifier  NULL,
    [ComposerId] uniqueidentifier  NULL,
    [NewsId] int  NULL
);
GO

-- Creating table 'Leaders'
CREATE TABLE [dbo].[Leaders] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [BirthYear] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [Thumbnail] varbinary(max)  NULL
);
GO

-- Creating table 'A1'
CREATE TABLE [dbo].[A1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InstrumentId] uniqueidentifier  NOT NULL,
    [StringerId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'A2'
CREATE TABLE [dbo].[A2] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [A1Id] int  NOT NULL,
    [TrackId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'A3'
CREATE TABLE [dbo].[A3] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TrackId] uniqueidentifier  NOT NULL,
    [ConcertId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'A4'
CREATE TABLE [dbo].[A4] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ConcertId] uniqueidentifier  NOT NULL,
    [LeaderId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Composers'
CREATE TABLE [dbo].[Composers] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Fullname] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [Thumbnail] varbinary(max)  NULL
);
GO

-- Creating table 'Genus'
CREATE TABLE [dbo].[Genus] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [Thumbnail] varbinary(max)  NULL
);
GO

-- Creating table 'Adverties'
CREATE TABLE [dbo].[Adverties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Image] varbinary(max)  NOT NULL,
    [Url] nvarchar(max)  NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Abstract] nvarchar(max)  NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Thumbnail] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Folders'
CREATE TABLE [dbo].[Folders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Parent] int  NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Name] uniqueidentifier  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Lenght] int  NOT NULL,
    [Bytes] varbinary(max)  NOT NULL,
    [FolderId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Instruments'
ALTER TABLE [dbo].[Instruments]
ADD CONSTRAINT [PK_Instruments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Username] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [Id] in table 'Concerts'
ALTER TABLE [dbo].[Concerts]
ADD CONSTRAINT [PK_Concerts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stringers'
ALTER TABLE [dbo].[Stringers]
ADD CONSTRAINT [PK_Stringers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tracks'
ALTER TABLE [dbo].[Tracks]
ADD CONSTRAINT [PK_Tracks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pics'
ALTER TABLE [dbo].[Pics]
ADD CONSTRAINT [PK_Pics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Leaders'
ALTER TABLE [dbo].[Leaders]
ADD CONSTRAINT [PK_Leaders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'A1'
ALTER TABLE [dbo].[A1]
ADD CONSTRAINT [PK_A1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'A2'
ALTER TABLE [dbo].[A2]
ADD CONSTRAINT [PK_A2]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'A3'
ALTER TABLE [dbo].[A3]
ADD CONSTRAINT [PK_A3]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'A4'
ALTER TABLE [dbo].[A4]
ADD CONSTRAINT [PK_A4]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Composers'
ALTER TABLE [dbo].[Composers]
ADD CONSTRAINT [PK_Composers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Genus'
ALTER TABLE [dbo].[Genus]
ADD CONSTRAINT [PK_Genus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Adverties'
ALTER TABLE [dbo].[Adverties]
ADD CONSTRAINT [PK_Adverties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Folders'
ALTER TABLE [dbo].[Folders]
ADD CONSTRAINT [PK_Folders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Name] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ComposerId] in table 'Tracks'
ALTER TABLE [dbo].[Tracks]
ADD CONSTRAINT [FK_ComposerTrack]
    FOREIGN KEY ([ComposerId])
    REFERENCES [dbo].[Composers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ComposerTrack'
CREATE INDEX [IX_FK_ComposerTrack]
ON [dbo].[Tracks]
    ([ComposerId]);
GO

-- Creating foreign key on [GenusId] in table 'Instruments'
ALTER TABLE [dbo].[Instruments]
ADD CONSTRAINT [FK_GenusInstrument]
    FOREIGN KEY ([GenusId])
    REFERENCES [dbo].[Genus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GenusInstrument'
CREATE INDEX [IX_FK_GenusInstrument]
ON [dbo].[Instruments]
    ([GenusId]);
GO

-- Creating foreign key on [InstrumentId] in table 'A1'
ALTER TABLE [dbo].[A1]
ADD CONSTRAINT [FK_InstrumentA1]
    FOREIGN KEY ([InstrumentId])
    REFERENCES [dbo].[Instruments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstrumentA1'
CREATE INDEX [IX_FK_InstrumentA1]
ON [dbo].[A1]
    ([InstrumentId]);
GO

-- Creating foreign key on [StringerId] in table 'A1'
ALTER TABLE [dbo].[A1]
ADD CONSTRAINT [FK_StringerA1]
    FOREIGN KEY ([StringerId])
    REFERENCES [dbo].[Stringers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StringerA1'
CREATE INDEX [IX_FK_StringerA1]
ON [dbo].[A1]
    ([StringerId]);
GO

-- Creating foreign key on [A1Id] in table 'A2'
ALTER TABLE [dbo].[A2]
ADD CONSTRAINT [FK_A1A2]
    FOREIGN KEY ([A1Id])
    REFERENCES [dbo].[A1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_A1A2'
CREATE INDEX [IX_FK_A1A2]
ON [dbo].[A2]
    ([A1Id]);
GO

-- Creating foreign key on [TrackId] in table 'A2'
ALTER TABLE [dbo].[A2]
ADD CONSTRAINT [FK_TrackA2]
    FOREIGN KEY ([TrackId])
    REFERENCES [dbo].[Tracks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackA2'
CREATE INDEX [IX_FK_TrackA2]
ON [dbo].[A2]
    ([TrackId]);
GO

-- Creating foreign key on [TrackId] in table 'A3'
ALTER TABLE [dbo].[A3]
ADD CONSTRAINT [FK_TrackA3]
    FOREIGN KEY ([TrackId])
    REFERENCES [dbo].[Tracks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackA3'
CREATE INDEX [IX_FK_TrackA3]
ON [dbo].[A3]
    ([TrackId]);
GO

-- Creating foreign key on [ConcertId] in table 'A3'
ALTER TABLE [dbo].[A3]
ADD CONSTRAINT [FK_ConcertA3]
    FOREIGN KEY ([ConcertId])
    REFERENCES [dbo].[Concerts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConcertA3'
CREATE INDEX [IX_FK_ConcertA3]
ON [dbo].[A3]
    ([ConcertId]);
GO

-- Creating foreign key on [ConcertId] in table 'A4'
ALTER TABLE [dbo].[A4]
ADD CONSTRAINT [FK_ConcertA4]
    FOREIGN KEY ([ConcertId])
    REFERENCES [dbo].[Concerts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConcertA4'
CREATE INDEX [IX_FK_ConcertA4]
ON [dbo].[A4]
    ([ConcertId]);
GO

-- Creating foreign key on [LeaderId] in table 'A4'
ALTER TABLE [dbo].[A4]
ADD CONSTRAINT [FK_LeaderA4]
    FOREIGN KEY ([LeaderId])
    REFERENCES [dbo].[Leaders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LeaderA4'
CREATE INDEX [IX_FK_LeaderA4]
ON [dbo].[A4]
    ([LeaderId]);
GO

-- Creating foreign key on [FolderId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_FolderFile]
    FOREIGN KEY ([FolderId])
    REFERENCES [dbo].[Folders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FolderFile'
CREATE INDEX [IX_FK_FolderFile]
ON [dbo].[Files]
    ([FolderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------