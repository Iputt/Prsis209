
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/09/2020 14:17:51
-- Generated from EDMX file: E:\MyProject\Prsis209\ps.module.Model\psDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [psdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ps_data_adjust]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ps_data_adjust];
GO
IF OBJECT_ID(N'[dbo].[ps_sys_user]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ps_sys_user];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ps_data_adjust'
CREATE TABLE [dbo].[ps_data_adjust] (
    [Id] uniqueidentifier  NOT NULL,
    [Created] datetime  NULL,
    [p_title] nvarchar(256)  NULL,
    [p_college] nvarchar(256)  NULL,
    [p_major] nvarchar(256)  NULL,
    [p_learnStyle] nvarchar(256)  NULL,
    [p_enrolment] int  NULL,
    [p_releaseTime] datetime  NULL,
    [p_contactMode] datetime  NULL,
    [p_content] nvarchar(256)  NULL,
    [p_spare] nvarchar(1024)  NULL,
    [IsDeleted] char(10)  NULL
);
GO

-- Creating table 'ps_sys_user'
CREATE TABLE [dbo].[ps_sys_user] (
    [Id] uniqueidentifier  NOT NULL,
    [avatarUrl] nvarchar(256)  NULL,
    [city] nvarchar(128)  NULL,
    [country] nvarchar(128)  NULL,
    [gender] char(10)  NULL,
    [lang] nvarchar(1)  NULL,
    [nickName] nvarchar(128)  NULL,
    [province] nvarchar(128)  NULL,
    [remark] nvarchar(128)  NULL,
    [Created] datetime  NULL,
    [IsDeleted] char(10)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ps_data_adjust'
ALTER TABLE [dbo].[ps_data_adjust]
ADD CONSTRAINT [PK_ps_data_adjust]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ps_sys_user'
ALTER TABLE [dbo].[ps_sys_user]
ADD CONSTRAINT [PK_ps_sys_user]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------