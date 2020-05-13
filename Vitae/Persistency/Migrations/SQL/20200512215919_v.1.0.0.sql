IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Country] (
    [CountryID] uniqueidentifier NOT NULL,
    [CountryCode] nvarchar(2) NULL,
    [Name] nvarchar(100) NULL,
    [Name_de] nvarchar(100) NULL,
    [Name_fr] nvarchar(100) NULL,
    [Name_it] nvarchar(100) NULL,
    [Name_es] nvarchar(100) NULL,
    [Iso3] nvarchar(3) NULL,
    [NumCode] int NULL,
    [PhoneCode] int NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([CountryID])
);

GO

CREATE TABLE [HierarchyLevel] (
    [HierarchyLevelID] uniqueidentifier NOT NULL,
    [HierarchyLevelCode] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Name_de] nvarchar(100) NULL,
    [Name_fr] nvarchar(100) NULL,
    [Name_it] nvarchar(100) NULL,
    [Name_es] nvarchar(100) NULL,
    CONSTRAINT [PK_HierarchyLevel] PRIMARY KEY ([HierarchyLevelID])
);

GO

CREATE TABLE [Industry] (
    [IndustryID] uniqueidentifier NOT NULL,
    [IndustryCode] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Name_de] nvarchar(100) NULL,
    [Name_fr] nvarchar(100) NULL,
    [Name_it] nvarchar(100) NULL,
    [Name_es] nvarchar(100) NULL,
    CONSTRAINT [PK_Industry] PRIMARY KEY ([IndustryID])
);

GO

CREATE TABLE [Language] (
    [LanguageID] uniqueidentifier NOT NULL,
    [LanguageCode] nvarchar(3) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Name_de] nvarchar(100) NOT NULL,
    [Name_fr] nvarchar(100) NOT NULL,
    [Name_it] nvarchar(100) NOT NULL,
    [Name_es] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY ([LanguageID])
);

GO

CREATE TABLE [Log] (
    [LogID] bigint NOT NULL IDENTITY,
    [CurriculumID] uniqueidentifier NOT NULL,
    [PublicationID] uniqueidentifier NULL,
    [LogLevel] int NOT NULL,
    [LogArea] int NOT NULL,
    [Link] nvarchar(2000) NULL,
    [IpAddress] nvarchar(50) NULL,
    [UserAgent] nvarchar(1000) NULL,
    [UserLanguage] nvarchar(2) NULL,
    [Message] nvarchar(200) NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY ([LogID])
);

GO

CREATE TABLE [MaritalStatus] (
    [MaritalStatusID] uniqueidentifier NOT NULL,
    [MaritalStatusCode] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Name_de] nvarchar(100) NULL,
    [Name_fr] nvarchar(100) NULL,
    [Name_it] nvarchar(100) NULL,
    [Name_es] nvarchar(100) NULL,
    CONSTRAINT [PK_MaritalStatus] PRIMARY KEY ([MaritalStatusID])
);

GO

CREATE TABLE [Month] (
    [MonthID] uniqueidentifier NOT NULL,
    [MonthCode] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Name_de] nvarchar(100) NULL,
    [Name_fr] nvarchar(100) NULL,
    [Name_it] nvarchar(100) NULL,
    [Name_es] nvarchar(100) NULL,
    CONSTRAINT [PK_Month] PRIMARY KEY ([MonthID])
);

GO

CREATE TABLE [Vfile] (
    [VfileID] uniqueidentifier NOT NULL,
    [Content] varbinary(max) NULL,
    [MimeType] nvarchar(100) NOT NULL,
    [FileName] nvarchar(255) NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_Vfile] PRIMARY KEY ([VfileID])
);

GO

CREATE TABLE [Curriculum] (
    [CurriculumID] uniqueidentifier NOT NULL,
    [UserID] uniqueidentifier NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastUpdated] datetime2 NOT NULL,
    [LanguageID] uniqueidentifier NULL,
    CONSTRAINT [PK_Curriculum] PRIMARY KEY ([CurriculumID]),
    CONSTRAINT [FK_Curriculum_Language_LanguageID] FOREIGN KEY ([LanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [About] (
    [AboutID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [AcademicTitle] nvarchar(100) NULL,
    [Slogan] nvarchar(4000) NULL,
    [Photo] varchar(max) NOT NULL,
    [VfileID] uniqueidentifier NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_About] PRIMARY KEY ([AboutID]),
    CONSTRAINT [FK_About_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_About_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_About_Vfile_VfileID] FOREIGN KEY ([VfileID]) REFERENCES [Vfile] ([VfileID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Abroad] (
    [AbroadID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CountryID] uniqueidentifier NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Start] datetime2 NOT NULL,
    [End] datetime2 NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Abroad] PRIMARY KEY ([AbroadID]),
    CONSTRAINT [FK_Abroad_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [Country] ([CountryID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Abroad_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Abroad_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Award] (
    [AwardID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [AwardedFrom] nvarchar(100) NOT NULL,
    [Link] nvarchar(255) NULL,
    [AwardedOn] datetime2 NOT NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Award] PRIMARY KEY ([AwardID]),
    CONSTRAINT [FK_Award_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Award_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Certificate] (
    [CertificateID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Issuer] nvarchar(100) NOT NULL,
    [Link] nvarchar(255) NULL,
    [IssuedOn] datetime2 NOT NULL,
    [ExpiresOn] datetime2 NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Certificate] PRIMARY KEY ([CertificateID]),
    CONSTRAINT [FK_Certificate_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Certificate_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Course] (
    [CourseID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [SchoolName] nvarchar(100) NOT NULL,
    [Link] nvarchar(255) NULL,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [CountryID] uniqueidentifier NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [Start] datetime2 NOT NULL,
    [End] datetime2 NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY ([CourseID]),
    CONSTRAINT [FK_Course_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [Country] ([CountryID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Course_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Course_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CurriculumLanguage] (
    [CurriculumID] uniqueidentifier NOT NULL,
    [LanguageID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [IsSelected] bit NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_CurriculumLanguage] PRIMARY KEY ([CurriculumID], [LanguageID]),
    CONSTRAINT [FK_CurriculumLanguage_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE CASCADE,
    CONSTRAINT [FK_CurriculumLanguage_Language_LanguageID] FOREIGN KEY ([LanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE CASCADE
);

GO

CREATE TABLE [Education] (
    [EducationID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [SchoolName] nvarchar(100) NOT NULL,
    [Link] nvarchar(255) NULL,
    [CountryID] uniqueidentifier NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Subject] nvarchar(100) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Grade] real NULL,
    [Start] datetime2 NOT NULL,
    [End] datetime2 NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Education] PRIMARY KEY ([EducationID]),
    CONSTRAINT [FK_Education_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [Country] ([CountryID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Education_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Education_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Experience] (
    [ExperienceID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [JobTitle] nvarchar(100) NOT NULL,
    [CompanyName] nvarchar(100) NOT NULL,
    [CompanyDescription] nvarchar(1000) NULL,
    [Link] nvarchar(255) NULL,
    [HierarchyLevelID] uniqueidentifier NOT NULL,
    [IndustryID] uniqueidentifier NOT NULL,
    [CountryID] uniqueidentifier NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Start] datetime2 NOT NULL,
    [End] datetime2 NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Experience] PRIMARY KEY ([ExperienceID]),
    CONSTRAINT [FK_Experience_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [Country] ([CountryID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Experience_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Experience_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Experience_HierarchyLevel_HierarchyLevelID] FOREIGN KEY ([HierarchyLevelID]) REFERENCES [HierarchyLevel] ([HierarchyLevelID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Experience_Industry_IndustryID] FOREIGN KEY ([IndustryID]) REFERENCES [Industry] ([IndustryID]) ON DELETE CASCADE
);

GO

CREATE TABLE [Interest] (
    [InterestID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [InterestName] nvarchar(100) NOT NULL,
    [Association] nvarchar(100) NULL,
    [Description] nvarchar(1000) NULL,
    [Link] nvarchar(255) NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Interest] PRIMARY KEY ([InterestID]),
    CONSTRAINT [FK_Interest_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Interest_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LanguageSkill] (
    [LanguageSkillID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [Rate] real NOT NULL,
    [SpokenLanguageID] uniqueidentifier NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_LanguageSkill] PRIMARY KEY ([LanguageSkillID]),
    CONSTRAINT [FK_LanguageSkill_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LanguageSkill_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LanguageSkill_Language_SpokenLanguageID] FOREIGN KEY ([SpokenLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [PersonalDetail] (
    [PersonalDetailID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [Firstname] nvarchar(100) NOT NULL,
    [Lastname] nvarchar(100) NOT NULL,
    [Birthday] datetime2 NOT NULL,
    [Gender] bit NOT NULL,
    [Street] nvarchar(100) NULL,
    [StreetNo] nvarchar(10) NULL,
    [State] nvarchar(100) NULL,
    [City] nvarchar(100) NULL,
    [ZipCode] nvarchar(10) NULL,
    [Email] nvarchar(100) NOT NULL,
    [PhonePrefix] nvarchar(6) NOT NULL,
    [MobileNumber] nvarchar(16) NOT NULL,
    [Citizenship] nvarchar(100) NOT NULL,
    [MaritalStatusID] uniqueidentifier NULL,
    [CountryID] uniqueidentifier NULL,
    [SpokenLanguageID] uniqueidentifier NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_PersonalDetail] PRIMARY KEY ([PersonalDetailID]),
    CONSTRAINT [FK_PersonalDetail_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [Country] ([CountryID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonalDetail_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonalDetail_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonalDetail_MaritalStatus_MaritalStatusID] FOREIGN KEY ([MaritalStatusID]) REFERENCES [MaritalStatus] ([MaritalStatusID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonalDetail_Language_SpokenLanguageID] FOREIGN KEY ([SpokenLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Publication] (
    [PublicationID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [PublicationIdentifier] uniqueidentifier NOT NULL,
    [Anonymize] bit NOT NULL,
    [Password] nvarchar(250) NULL,
    [CurriculumID] uniqueidentifier NULL,
    [Notes] nvarchar(1000) NULL,
    CONSTRAINT [PK_Publication] PRIMARY KEY ([PublicationID]),
    CONSTRAINT [FK_Publication_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Publication_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Reference] (
    [ReferenceID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [Firstname] nvarchar(100) NOT NULL,
    [Lastname] nvarchar(100) NOT NULL,
    [Gender] bit NOT NULL,
    [CompanyName] nvarchar(100) NULL,
    [Link] nvarchar(255) NULL,
    [Description] nvarchar(1000) NULL,
    [Email] nvarchar(100) NULL,
    [PhonePrefix] nvarchar(6) NOT NULL,
    [PhoneNumber] nvarchar(16) NOT NULL,
    [CountryID] uniqueidentifier NULL,
    [Hide] bit NOT NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Reference] PRIMARY KEY ([ReferenceID]),
    CONSTRAINT [FK_Reference_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [Country] ([CountryID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reference_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reference_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Skill] (
    [SkillID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [Category] nvarchar(100) NOT NULL,
    [Skillset] nvarchar(1000) NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_Skill] PRIMARY KEY ([SkillID]),
    CONSTRAINT [FK_Skill_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Skill_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [SocialLink] (
    [SocialLinkID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CurriculumLanguageLanguageID] uniqueidentifier NULL,
    [CreatedOn] datetime2 NOT NULL,
    [SocialPlatform] int NOT NULL,
    [Link] nvarchar(255) NOT NULL,
    [CurriculumID] uniqueidentifier NULL,
    CONSTRAINT [PK_SocialLink] PRIMARY KEY ([SocialLinkID]),
    CONSTRAINT [FK_SocialLink_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [Curriculum] ([CurriculumID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SocialLink_Language_CurriculumLanguageLanguageID] FOREIGN KEY ([CurriculumLanguageLanguageID]) REFERENCES [Language] ([LanguageID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Child] (
    [ChildID] uniqueidentifier NOT NULL,
    [Firstname] nvarchar(100) NOT NULL,
    [Birthday] datetime2 NOT NULL,
    [Order] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [PersonalDetailID] uniqueidentifier NULL,
    CONSTRAINT [PK_Child] PRIMARY KEY ([ChildID]),
    CONSTRAINT [FK_Child_PersonalDetail_PersonalDetailID] FOREIGN KEY ([PersonalDetailID]) REFERENCES [PersonalDetail] ([PersonalDetailID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [PersonCountry] (
    [PersonalDetailID] uniqueidentifier NOT NULL,
    [CountryID] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_PersonCountry] PRIMARY KEY ([PersonalDetailID], [CountryID]),
    CONSTRAINT [FK_PersonCountry_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [Country] ([CountryID]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonCountry_PersonalDetail_PersonalDetailID] FOREIGN KEY ([PersonalDetailID]) REFERENCES [PersonalDetail] ([PersonalDetailID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_About_CurriculumID] ON [About] ([CurriculumID]);

GO

CREATE INDEX [IX_About_CurriculumLanguageLanguageID] ON [About] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_About_VfileID] ON [About] ([VfileID]);

GO

CREATE INDEX [IX_Abroad_CountryID] ON [Abroad] ([CountryID]);

GO

CREATE INDEX [IX_Abroad_CurriculumID] ON [Abroad] ([CurriculumID]);

GO

CREATE INDEX [IX_Abroad_CurriculumLanguageLanguageID] ON [Abroad] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_Award_CurriculumID] ON [Award] ([CurriculumID]);

GO

CREATE INDEX [IX_Award_CurriculumLanguageLanguageID] ON [Award] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_Certificate_CurriculumID] ON [Certificate] ([CurriculumID]);

GO

CREATE INDEX [IX_Certificate_CurriculumLanguageLanguageID] ON [Certificate] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_Child_PersonalDetailID] ON [Child] ([PersonalDetailID]);

GO

CREATE UNIQUE INDEX [IX_Country_CountryCode] ON [Country] ([CountryCode]) WHERE [CountryCode] IS NOT NULL;

GO

CREATE INDEX [IX_Course_CountryID] ON [Course] ([CountryID]);

GO

CREATE INDEX [IX_Course_CurriculumID] ON [Course] ([CurriculumID]);

GO

CREATE INDEX [IX_Course_CurriculumLanguageLanguageID] ON [Course] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_Curriculum_LanguageID] ON [Curriculum] ([LanguageID]);

GO

CREATE INDEX [IX_CurriculumLanguage_CurriculumID] ON [CurriculumLanguage] ([CurriculumID]);

GO

CREATE INDEX [IX_CurriculumLanguage_LanguageID] ON [CurriculumLanguage] ([LanguageID]);

GO

CREATE INDEX [IX_Education_CountryID] ON [Education] ([CountryID]);

GO

CREATE INDEX [IX_Education_CurriculumID] ON [Education] ([CurriculumID]);

GO

CREATE INDEX [IX_Education_CurriculumLanguageLanguageID] ON [Education] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_Experience_CountryID] ON [Experience] ([CountryID]);

GO

CREATE INDEX [IX_Experience_CurriculumID] ON [Experience] ([CurriculumID]);

GO

CREATE INDEX [IX_Experience_CurriculumLanguageLanguageID] ON [Experience] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_Experience_HierarchyLevelID] ON [Experience] ([HierarchyLevelID]);

GO

CREATE INDEX [IX_Experience_IndustryID] ON [Experience] ([IndustryID]);

GO

CREATE UNIQUE INDEX [IX_HierarchyLevel_HierarchyLevelCode] ON [HierarchyLevel] ([HierarchyLevelCode]);

GO

CREATE UNIQUE INDEX [IX_Industry_IndustryCode] ON [Industry] ([IndustryCode]);

GO

CREATE INDEX [IX_Interest_CurriculumID] ON [Interest] ([CurriculumID]);

GO

CREATE INDEX [IX_Interest_CurriculumLanguageLanguageID] ON [Interest] ([CurriculumLanguageLanguageID]);

GO

CREATE UNIQUE INDEX [IX_Language_LanguageCode] ON [Language] ([LanguageCode]);

GO

CREATE INDEX [IX_LanguageSkill_CurriculumID] ON [LanguageSkill] ([CurriculumID]);

GO

CREATE INDEX [IX_LanguageSkill_CurriculumLanguageLanguageID] ON [LanguageSkill] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_LanguageSkill_SpokenLanguageID] ON [LanguageSkill] ([SpokenLanguageID]);

GO

CREATE INDEX [IX_Log_CurriculumID] ON [Log] ([CurriculumID]);

GO

CREATE INDEX [IX_Log_PublicationID] ON [Log] ([PublicationID]);

GO

CREATE UNIQUE INDEX [IX_MaritalStatus_MaritalStatusCode] ON [MaritalStatus] ([MaritalStatusCode]);

GO

CREATE UNIQUE INDEX [IX_Month_MonthCode] ON [Month] ([MonthCode]);

GO

CREATE INDEX [IX_PersonalDetail_CountryID] ON [PersonalDetail] ([CountryID]);

GO

CREATE INDEX [IX_PersonalDetail_CurriculumID] ON [PersonalDetail] ([CurriculumID]);

GO

CREATE INDEX [IX_PersonalDetail_CurriculumLanguageLanguageID] ON [PersonalDetail] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_PersonalDetail_MaritalStatusID] ON [PersonalDetail] ([MaritalStatusID]);

GO

CREATE INDEX [IX_PersonalDetail_SpokenLanguageID] ON [PersonalDetail] ([SpokenLanguageID]);

GO

CREATE INDEX [IX_PersonCountry_CountryID] ON [PersonCountry] ([CountryID]);

GO

CREATE INDEX [IX_Publication_CurriculumID] ON [Publication] ([CurriculumID]);

GO

CREATE INDEX [IX_Publication_CurriculumLanguageLanguageID] ON [Publication] ([CurriculumLanguageLanguageID]);

GO

CREATE UNIQUE INDEX [IX_Publication_PublicationIdentifier] ON [Publication] ([PublicationIdentifier]);

GO

CREATE INDEX [IX_Reference_CountryID] ON [Reference] ([CountryID]);

GO

CREATE INDEX [IX_Reference_CurriculumID] ON [Reference] ([CurriculumID]);

GO

CREATE INDEX [IX_Reference_CurriculumLanguageLanguageID] ON [Reference] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_Skill_CurriculumID] ON [Skill] ([CurriculumID]);

GO

CREATE INDEX [IX_Skill_CurriculumLanguageLanguageID] ON [Skill] ([CurriculumLanguageLanguageID]);

GO

CREATE INDEX [IX_SocialLink_CurriculumID] ON [SocialLink] ([CurriculumID]);

GO

CREATE INDEX [IX_SocialLink_CurriculumLanguageLanguageID] ON [SocialLink] ([CurriculumLanguageLanguageID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200512215919_v.1.0.0', N'3.1.3');

GO

