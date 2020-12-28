
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/17/2019 22:51:40
-- Generated from EDMX file: D:\Рабоччий стол 02-06\BarterMarket\VirtualGarage.Logic\DataModel\BarterMarket.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BarterMarket];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CashOperations_CashOperationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashOperations] DROP CONSTRAINT [FK_CashOperations_CashOperationType];
GO
IF OBJECT_ID(N'[dbo].[FK_CashOperations_Certificates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashOperations] DROP CONSTRAINT [FK_CashOperations_Certificates];
GO
IF OBJECT_ID(N'[dbo].[FK_CashOperations_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashOperations] DROP CONSTRAINT [FK_CashOperations_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Certificates_Packet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Certificates] DROP CONSTRAINT [FK_Certificates_Packet];
GO
IF OBJECT_ID(N'[dbo].[FK_Certificates_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Certificates] DROP CONSTRAINT [FK_Certificates_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Offers_OfferCategories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK_Offers_OfferCategories];
GO
IF OBJECT_ID(N'[dbo].[FK_Offers_OfferStatuses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK_Offers_OfferStatuses];
GO
IF OBJECT_ID(N'[dbo].[FK_Offers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK_Offers_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Packet_Offers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packet] DROP CONSTRAINT [FK_Packet_Offers];
GO
IF OBJECT_ID(N'[dbo].[FK_Packet_PacketStatuses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packet] DROP CONSTRAINT [FK_Packet_PacketStatuses];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_UserRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_UserRoles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CashOperations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashOperations];
GO
IF OBJECT_ID(N'[dbo].[CashOperationType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashOperationType];
GO
IF OBJECT_ID(N'[dbo].[Certificates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Certificates];
GO
IF OBJECT_ID(N'[dbo].[OfferCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OfferCategories];
GO
IF OBJECT_ID(N'[dbo].[OfferRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OfferRequests];
GO
IF OBJECT_ID(N'[dbo].[Offers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offers];
GO
IF OBJECT_ID(N'[dbo].[OfferStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OfferStatuses];
GO
IF OBJECT_ID(N'[dbo].[Packet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Packet];
GO
IF OBJECT_ID(N'[dbo].[PacketStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PacketStatuses];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[UserQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserQuestions];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [UserRoleID] int IDENTITY(1,1) NOT NULL,
    [UserRoleName] varchar(100)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserNick] varchar(50)  NULL,
    [UserEmail] varchar(50)  NULL,
    [UserPassword] varchar(50)  NULL,
    [UserRoleID] int  NOT NULL,
    [UserCompanyName] varchar(100)  NULL,
    [UserCompanyDetails] varchar(1000)  NULL,
    [UserRegisrationDate] datetime  NOT NULL,
    [UserPiarsCount] int  NOT NULL,
    [UserPhone] varchar(20)  NULL,
    [Network] varchar(50)  NULL,
    [Uid] varchar(50)  NULL
);
GO

-- Creating table 'OfferCategories'
CREATE TABLE [dbo].[OfferCategories] (
    [OfferCategoryID] int IDENTITY(1,1) NOT NULL,
    [OfferCategoryName] varchar(100)  NOT NULL
);
GO

-- Creating table 'Offers'
CREATE TABLE [dbo].[Offers] (
    [OfferID] int IDENTITY(1,1) NOT NULL,
    [OfferName] varchar(100)  NULL,
    [OfferCategoryID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [OfferCompanyAddress] varchar(500)  NULL,
    [OfferManagerName] varchar(100)  NOT NULL,
    [OfferManagerPhone] varchar(50)  NOT NULL,
    [OfferManagerEmail] varchar(100)  NULL,
    [OfferDetails] varchar(1000)  NOT NULL,
    [OfferInstagramLink] varchar(100)  NULL,
    [OfferVKLink] varchar(100)  NULL,
    [OfferOdnoklassnikiLink] varchar(100)  NULL,
    [OfferFacebookLink] varchar(100)  NULL,
    [OfferTwitterLink] varchar(100)  NULL,
    [OfferWhatsappLink] varchar(100)  NULL,
    [OfferTelegramLink] varchar(100)  NULL,
    [OfferViberLink] varchar(100)  NULL,
    [OfferPhoto] varbinary(max)  NULL,
    [OfferDate] datetime  NOT NULL,
    [ImageType] char(10)  NULL,
    [OfferStatusID] int  NOT NULL
);
GO

-- Creating table 'Packets'
CREATE TABLE [dbo].[Packets] (
    [PacketID] int IDENTITY(1,1) NOT NULL,
    [OfferID] int  NOT NULL,
    [PacketCost] int  NOT NULL,
    [PacketsCount] int  NOT NULL,
    [PacketDate] datetime  NOT NULL,
    [PacketName] varchar(50)  NOT NULL,
    [PacketStatusID] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'CashOperations'
CREATE TABLE [dbo].[CashOperations] (
    [CashOperationID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [CashOperationTypeID] int  NOT NULL,
    [CashAmount] int  NOT NULL,
    [CertificateID] int  NULL,
    [CertificateCount] int  NULL,
    [OperationDate] datetime  NOT NULL
);
GO

-- Creating table 'CashOperationTypes'
CREATE TABLE [dbo].[CashOperationTypes] (
    [CashOperationTypeID] int IDENTITY(1,1) NOT NULL,
    [CashOperationTypeName] varchar(100)  NOT NULL,
    [IsIncrementOperation] bit  NOT NULL
);
GO

-- Creating table 'Certificates'
CREATE TABLE [dbo].[Certificates] (
    [CertificateID] int IDENTITY(1,1) NOT NULL,
    [PacketID] int  NOT NULL,
    [CustomerID] int  NULL,
    [CertificateCodeValue] nchar(9)  NOT NULL,
    [CertificateCreateDate] datetime  NOT NULL,
    [CertificateImplementDate] datetime  NULL,
    [CertificateIsImplement] bit  NOT NULL
);
GO

-- Creating table 'OfferRequests'
CREATE TABLE [dbo].[OfferRequests] (
    [OfferRequestID] int IDENTITY(1,1) NOT NULL,
    [CompanyName] varchar(200)  NOT NULL,
    [CompanyAddress] varchar(200)  NOT NULL,
    [CompanyRegistrationRegion] varchar(100)  NOT NULL,
    [CompanyRegions] varchar(500)  NOT NULL,
    [CompanySite] varchar(200)  NOT NULL,
    [CompanyDetails] varchar(3000)  NOT NULL,
    [UserName] varchar(100)  NOT NULL,
    [TelephoneNumber] varchar(100)  NOT NULL,
    [SourceAboutUs] varchar(300)  NOT NULL,
    [AdvertisingTarget] varchar(300)  NOT NULL
);
GO

-- Creating table 'UserQuestions'
CREATE TABLE [dbo].[UserQuestions] (
    [UserQuestionID] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(100)  NOT NULL,
    [UserEmail] varchar(100)  NOT NULL,
    [UserMessage] varchar(3000)  NOT NULL
);
GO

-- Creating table 'OfferStatuses'
CREATE TABLE [dbo].[OfferStatuses] (
    [OfferStatusID] int IDENTITY(1,1) NOT NULL,
    [OfferStatusName] varchar(50)  NOT NULL
);
GO

-- Creating table 'PacketStatuses'
CREATE TABLE [dbo].[PacketStatuses] (
    [PacketStatusID] int IDENTITY(1,1) NOT NULL,
    [PacketStatusName] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserRoleID] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([UserRoleID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [OfferCategoryID] in table 'OfferCategories'
ALTER TABLE [dbo].[OfferCategories]
ADD CONSTRAINT [PK_OfferCategories]
    PRIMARY KEY CLUSTERED ([OfferCategoryID] ASC);
GO

-- Creating primary key on [OfferID] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [PK_Offers]
    PRIMARY KEY CLUSTERED ([OfferID] ASC);
GO

-- Creating primary key on [PacketID] in table 'Packets'
ALTER TABLE [dbo].[Packets]
ADD CONSTRAINT [PK_Packets]
    PRIMARY KEY CLUSTERED ([PacketID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [CashOperationID] in table 'CashOperations'
ALTER TABLE [dbo].[CashOperations]
ADD CONSTRAINT [PK_CashOperations]
    PRIMARY KEY CLUSTERED ([CashOperationID] ASC);
GO

-- Creating primary key on [CashOperationTypeID] in table 'CashOperationTypes'
ALTER TABLE [dbo].[CashOperationTypes]
ADD CONSTRAINT [PK_CashOperationTypes]
    PRIMARY KEY CLUSTERED ([CashOperationTypeID] ASC);
GO

-- Creating primary key on [CertificateID] in table 'Certificates'
ALTER TABLE [dbo].[Certificates]
ADD CONSTRAINT [PK_Certificates]
    PRIMARY KEY CLUSTERED ([CertificateID] ASC);
GO

-- Creating primary key on [OfferRequestID] in table 'OfferRequests'
ALTER TABLE [dbo].[OfferRequests]
ADD CONSTRAINT [PK_OfferRequests]
    PRIMARY KEY CLUSTERED ([OfferRequestID] ASC);
GO

-- Creating primary key on [UserQuestionID] in table 'UserQuestions'
ALTER TABLE [dbo].[UserQuestions]
ADD CONSTRAINT [PK_UserQuestions]
    PRIMARY KEY CLUSTERED ([UserQuestionID] ASC);
GO

-- Creating primary key on [OfferStatusID] in table 'OfferStatuses'
ALTER TABLE [dbo].[OfferStatuses]
ADD CONSTRAINT [PK_OfferStatuses]
    PRIMARY KEY CLUSTERED ([OfferStatusID] ASC);
GO

-- Creating primary key on [PacketStatusID] in table 'PacketStatuses'
ALTER TABLE [dbo].[PacketStatuses]
ADD CONSTRAINT [PK_PacketStatuses]
    PRIMARY KEY CLUSTERED ([PacketStatusID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserRoleID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_UserRoles]
    FOREIGN KEY ([UserRoleID])
    REFERENCES [dbo].[UserRoles]
        ([UserRoleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_UserRoles'
CREATE INDEX [IX_FK_Users_UserRoles]
ON [dbo].[Users]
    ([UserRoleID]);
GO

-- Creating foreign key on [OfferCategoryID] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [FK_Offers_OfferCategories]
    FOREIGN KEY ([OfferCategoryID])
    REFERENCES [dbo].[OfferCategories]
        ([OfferCategoryID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Offers_OfferCategories'
CREATE INDEX [IX_FK_Offers_OfferCategories]
ON [dbo].[Offers]
    ([OfferCategoryID]);
GO

-- Creating foreign key on [UserID] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [FK_Offers_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Offers_Users'
CREATE INDEX [IX_FK_Offers_Users]
ON [dbo].[Offers]
    ([UserID]);
GO

-- Creating foreign key on [OfferID] in table 'Packets'
ALTER TABLE [dbo].[Packets]
ADD CONSTRAINT [FK_Packet_Offers]
    FOREIGN KEY ([OfferID])
    REFERENCES [dbo].[Offers]
        ([OfferID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Packet_Offers'
CREATE INDEX [IX_FK_Packet_Offers]
ON [dbo].[Packets]
    ([OfferID]);
GO

-- Creating foreign key on [CashOperationTypeID] in table 'CashOperations'
ALTER TABLE [dbo].[CashOperations]
ADD CONSTRAINT [FK_CashOperations_CashOperationType]
    FOREIGN KEY ([CashOperationTypeID])
    REFERENCES [dbo].[CashOperationTypes]
        ([CashOperationTypeID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CashOperations_CashOperationType'
CREATE INDEX [IX_FK_CashOperations_CashOperationType]
ON [dbo].[CashOperations]
    ([CashOperationTypeID]);
GO

-- Creating foreign key on [CertificateID] in table 'CashOperations'
ALTER TABLE [dbo].[CashOperations]
ADD CONSTRAINT [FK_CashOperations_Certificates]
    FOREIGN KEY ([CertificateID])
    REFERENCES [dbo].[Certificates]
        ([CertificateID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CashOperations_Certificates'
CREATE INDEX [IX_FK_CashOperations_Certificates]
ON [dbo].[CashOperations]
    ([CertificateID]);
GO

-- Creating foreign key on [UserID] in table 'CashOperations'
ALTER TABLE [dbo].[CashOperations]
ADD CONSTRAINT [FK_CashOperations_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CashOperations_Users'
CREATE INDEX [IX_FK_CashOperations_Users]
ON [dbo].[CashOperations]
    ([UserID]);
GO

-- Creating foreign key on [PacketID] in table 'Certificates'
ALTER TABLE [dbo].[Certificates]
ADD CONSTRAINT [FK_Certificates_Packet]
    FOREIGN KEY ([PacketID])
    REFERENCES [dbo].[Packets]
        ([PacketID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Certificates_Packet'
CREATE INDEX [IX_FK_Certificates_Packet]
ON [dbo].[Certificates]
    ([PacketID]);
GO

-- Creating foreign key on [CustomerID] in table 'Certificates'
ALTER TABLE [dbo].[Certificates]
ADD CONSTRAINT [FK_Certificates_Users]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Certificates_Users'
CREATE INDEX [IX_FK_Certificates_Users]
ON [dbo].[Certificates]
    ([CustomerID]);
GO

-- Creating foreign key on [OfferStatusID] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [FK_Offers_OfferStatuses]
    FOREIGN KEY ([OfferStatusID])
    REFERENCES [dbo].[OfferStatuses]
        ([OfferStatusID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Offers_OfferStatuses'
CREATE INDEX [IX_FK_Offers_OfferStatuses]
ON [dbo].[Offers]
    ([OfferStatusID]);
GO

-- Creating foreign key on [PacketStatusID] in table 'Packets'
ALTER TABLE [dbo].[Packets]
ADD CONSTRAINT [FK_Packet_PacketStatuses]
    FOREIGN KEY ([PacketStatusID])
    REFERENCES [dbo].[PacketStatuses]
        ([PacketStatusID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Packet_PacketStatuses'
CREATE INDEX [IX_FK_Packet_PacketStatuses]
ON [dbo].[Packets]
    ([PacketStatusID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------