
BEGIN TRY
    BEGIN TRANSACTION

DECLARE @ID uniqueidentifier = 'dd54d9cd-2437-4e9e-9451-34fb67ebfcfb'

-- IdentityContext
IF NOT EXISTS(SELECT 1 FROM [AspNetUsers] WHERE [Id] = @ID)
BEGIN
INSERT INTO [AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
     VALUES (@ID ,'petefrancis.mitchell@myvitae.ch' ,'PETEFRANCIS.MITCHELL@MYVITAE.CH' ,'petefrancis.mitchell@myvitae.ch' ,'PETEFRANCIS.MITCHELL@MYVITAE.CH' ,1 ,'AQAAAAEAACcQAAAAEM9xHq/I11jR504N4mFC+CcEdvencdlc8gfAmRwzeBLLnYuyB8QqWiDsQSyJ1XMiTw==','X7RKCA5QCJYUU3FS6G7UIG7PC2YRECYC','5fbfc0d8-2a6d-4875-8869-c332974e3cf3',null,0,0,null,1,0)

SET IDENTITY_INSERT [AspNetUserClaims] ON
INSERT INTO [AspNetUserClaims](Id, UserId, ClaimType, ClaimValue)
	 VALUES (8, @ID, 'CurriculumID', @ID)
SET IDENTITY_INSERT [AspNetUserClaims] OFF
END

DECLARE @UserRole uniqueidentifier = (SELECT [Id] FROM [AspNetRoles] WHERE [Name] = 'User')
IF NOT EXISTS(SELECT 1 FROM [AspNetUserRoles] WHERE [UserID] = @ID)
BEGIN
	INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
		VALUES (@ID, @UserRole)
END
-- VitaeContext
DECLARE @LanguageCode_en varchar(2) = 'en'
DECLARE @Email varchar(50) = 'petefrancis.mitchell@myvitae.ch'
DECLARE @CurriculumID uniqueidentifier = (SELECT [cla].[ClaimValue] FROM [AspNetUsers] [usr] INNER JOIN [AspNetUserClaims] [cla] ON [cla].[UserId] = [usr].[Id] WHERE [usr].[Email] = @Email)
DECLARE @UserID uniqueidentifier = (SELECT [cla].[UserId] FROM [AspNetUsers] [usr] INNER JOIN [AspNetUserClaims] [cla] ON [cla].[UserId] = [usr].[Id] WHERE [usr].[Email] = @Email)
DECLARE @LanguageID uniqueidentifier = (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_en)

IF NOT EXISTS(SELECT 1 FROM [Curriculum] WHERE [CurriculumID] = @CurriculumID)
BEGIN
INSERT INTO [Curriculum]
VALUES (@CurriculumID, @UserID, GETDATE(), GETDATE(), @LanguageID)

INSERT INTO [CurriculumLanguage] VALUES (@CurriculumID, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_en), 0, 0, GETDATE())
END

DECLARE @CurrLangId_en uniqueidentifier = (SELECT [LanguageID] FROM [CurriculumLanguage] WHERE [CurriculumID] = @CurriculumID AND [Order] = 0)

/* ABOUT */
INSERT INTO [About]
VALUES(NEWID(), 0, @CurrLangId_en, GETDATE(), 'Master of Science (M.Sc.)', '"The greatest glory in living lies not in never falling, but in rising every time we fall." -Nelson Mandela',  '', null, @CurriculumID)

/* PERSONAL DETAIL */
INSERT INTO [PersonalDetail]
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Pete Francis', 'Mitchell', '1976-06-23', 1, 'Friedensgasse', 12, 'Zürich', 'Zürich', '8003', 'petefrancis.mitchell@myvitae.ch', '+41', '791234567', 'Zürich ZH', (SELECT [MaritalStatusID] FROM [MaritalStatus] WHERE [MaritalStatusCode] = 3), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), @CurriculumID)
INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 0, GETDATE())
INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'us'), 1, GETDATE())
INSERT INTO [Child]
VALUES(NEWID(), 'Anna', '2009-02-14', 0, GETDATE(), (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail] WHERE [CurriculumID] = @CurriculumID))
INSERT INTO [Child]
VALUES(NEWID(), 'Pete', '2012-05-13', 0, GETDATE(), (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail] WHERE [CurriculumID] = @CurriculumID))

/* SOCIAL LINK */
INSERT INTO [SocialLink]
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 1, 'https://www.facebook.com/petefrancismitchell', @CurriculumID)
INSERT INTO [SocialLink]         
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 2, 'https://twitter.com/petefrancismitchell', @CurriculumID)
INSERT INTO [SocialLink]           
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 3, 'https://www.linkedin.com/in/petefrancismitchell', @CurriculumID)
INSERT INTO [SocialLink]         
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 4, 'https://github.com/petefrancismitchell', @CurriculumID)
INSERT INTO [SocialLink]          
VALUES (NEWID(), 4, @CurrLangId_en, GETDATE(), 5, 'https://www.xing.com/profile/petefrancis_mitchell/cv', @CurriculumID)
INSERT INTO [SocialLink]           
VALUES (NEWID(), 5, @CurrLangId_en, GETDATE(), 7, 'https://stackoverflow.com/users/1/petefrancismitchell', @CurriculumID)
        
/* EXPERIENCE */                       
INSERT INTO [Experience]            
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Lecturer', 'Vocational trainers', 'Provider of higher vocational training in the field of economics and computer science', 'https://www.vocationaltrainers.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 14), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Part-time lecturer, based on mandates.', '2017-09-01', null, @CurriculumID)
INSERT INTO [Experience]           
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'Senior Software Engineer', 'Arando Technologies', 'Software service provider', 'https://arando.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 10), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Architecture and full-stack development of new beta application, realized with various web technologies. Project owner of some experimental Arando labs projects. Technical project management for internal and external projects. Regular training and coaching of employees.', '2009-05-01', null, @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 'Software Engineer', 'Acme International', 'Security service provider', 'https://acme.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 10), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Bern', 'Architecture, specification, design, implementation, test and documentation of software components and multimedia applications. Project management: Requirement specification, implementation, engineering and test with direct contact with customers.', '2007-11-01', '2009-04-01', @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'Freelancer', 'Johndoe and sons', 'Security service provider', 'https://jondueandsons.at', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 1), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'at'), 'Salzburg', 'Development of tools and diagnostic services.', '2006-11-01', '2007-10-01', @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 4, @CurrLangId_en, GETDATE(), 'Trainee', 'Sonacare', 'Healthcare', 'http://sonacare.de', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 15), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'de'), 'Stuttgart', 'Development of system tools for HL7 application.', '2004-04-01', '2005-09-01', @CurriculumID)
                                  
/* EDUCATION */                    
                                  
INSERT INTO [Education]            
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'ETH Zurich', 'https://eth.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zurich', 'MTEC', 'Master Management, Technology, and Economics',  'Post-graduate study with focus on: Business Administration, Software Engineering, Project Management and Coaching.', '5.4', '2008-11-01', '2010-3-01', @CurriculumID)
INSERT INTO [Education]            
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'ZHW Zürcher Hochschule Winterthur', 'https://zhw.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Bachelor of Science', 'Information Technology', 'Diploma study with focus on: Softwarearchitecture / modeling and software development. Diploma thesis: "A new way to communicate in the web".', '5.3', '2002-08-01', '2006-10-01', @CurriculumID)
INSERT INTO [Education]           
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'Kantonsschule Rämibühl', 'https://www.rgzh.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Matura', 'Natural Science (Profile N)', 'Profile N (scientific and mathematical) with focus on astrophysics. Matura thesis: "A new way to beat a chess computer".', '5', '1995-08-01', '2000-07-01', @CurriculumID)


/* LANGUAGE SKILL */
INSERT INTO [LanguageSkill]                                                                                                       
VALUES(NEWID(), 0, @CurrLangId_en , GETDATE(), 4, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                   
VALUES(NEWID(), 1, @CurrLangId_en , GETDATE(), 4, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'en'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                      
VALUES(NEWID(), 3, @CurrLangId_en , GETDATE(), 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'fr'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                      
VALUES(NEWID(), 3, @CurrLangId_en , GETDATE(), 2, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'ru'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                      
VALUES(NEWID(), 3, @CurrLangId_en , GETDATE(), 1, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'zh'), @CurriculumID)


/* AWARD */                        
INSERT INTO [Award]             
VALUES(NEWID(), 0, @CurrLangId_en, GETDATE(), 'MTEC Award', 'Best grade in MTEC program', 'ETH Zurich', 'https://www.zhaw.ch', '2010-03-01', @CurriculumID)
INSERT INTO [Award]              
VALUES(NEWID(), 1, @CurrLangId_en, GETDATE(), 'ZHW Alumni Award', 'Best Master Thesis', 'ZHW Zürcher Hochschule Winterthur', 'https://www.zhw.ch', '2006-10-01', @CurriculumID)
                                
/* INTEREST */                               
INSERT INTO [Interest]          
VALUES(NEWID(), 0, @CurrLangId_en, GETDATE(), 'Sports', null, 'Swimming, squash and bowling', null, @CurriculumID)
INSERT INTO [Interest]          
VALUES(NEWID(), 1, @CurrLangId_en, GETDATE(), 'Cooking', null, 'Cooking can be a test of creativity', null, @CurriculumID)
INSERT INTO [Interest]         
VALUES(NEWID(), 2, @CurrLangId_en, GETDATE(), 'Travelling', null, 'Travel, new countries and cultures have always fascinated me', null, @CurriculumID)
INSERT INTO [Interest]         
VALUES(NEWID(), 4, @CurrLangId_en, GETDATE(), 'Watersports', null, 'Kitesurfing is my favorite', null, @CurriculumID)

/* SKILL */
INSERT INTO [Skill]               
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Programming languages', 'C#, Java, Python, Rust, C++', @CurriculumID)
INSERT INTO [Skill]              
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'Frameworks', 'Java SE Development Kit 13, .NET Framework (v4.8), .NET Core (v3.1), Entity Framework (v6.4), Entity Framework Core (v3.1)', @CurriculumID)
INSERT INTO [Skill]               
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 'Web technologies', 'ASP.NET Core, ASP.NET Web API, ASP.NET MVC, ASP.NET WebForms, TypeScript, HTML, XML, SASS', @CurriculumID)
INSERT INTO [Skill]               
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'Database technlogies', 'NoSQL, Microsoft SQL Server, Oracle, T-SQL, PL/SQL', @CurriculumID)
INSERT INTO [Skill]           
VALUES (NEWID(), 4, @CurrLangId_en, GETDATE(), 'IDE/Tools', 'Visual Studio, Eclipse, Azure DevOps, Git, Subversion (SVN)', @CurriculumID)
INSERT INTO [Skill]           
VALUES (NEWID(), 5, @CurrLangId_en, GETDATE(), 'Project methods', 'Agile, Scrum, RUP', @CurriculumID)
INSERT INTO [Skill]               
VALUES (NEWID(), 6, @CurrLangId_en, GETDATE(), 'Development methods', 'Domain driven Development, Prototyping', @CurriculumID)

/* ABROAD */
INSERT INTO [Abroad]
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'gb'), 'Bath', 'Two years living abroad with host family accommodation.', '2000-08-01', '2002-07-01', @CurriculumID)

/* REFERENCE */
INSERT INTO [Reference]
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'John', 'Goose', 1, 'Acme International', 'https://abb.ch', 'Former supervisor at Acme International', 'john.goose@acme.ch', '+41', '44 000 00 00', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'gb'), 0, @CurriculumID)

/* CERTIFICATE */
INSERT INTO [Certificate]        
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Cambridge English Level 5 Certificate in ESOL International', 'Certificate of Proficiency in English', 'Cambridge Assessment English', 'https://www.cambridgeenglish.org', '2014-03-01', null, @CurriculumID)
INSERT INTO [Certificate]        
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'Microsoft Certified Professional (MCP)', 'Microsoft Specialist: Programming in C#', 'Microsoft', 'https://www.microsoft.com', '2015-02-13', null, @CurriculumID)
INSERT INTO [Certificate]         
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 'Professional Scrum Master I (PSM I)', 'Microsoft Specialist: Programming in C#', 'Scrum.org', 'https://www.scrum.org/certificates', '2016-10-04', null, @CurriculumID)
INSERT INTO [Certificate]         
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'SVEB instructor level 1', 'Conduct learning events with adults / AdA FA-M 1', 'Klubschule Migros', 'https://alice.ch/de/ausbilden-als-beruf/ada-abschluesse/sveb-zertifikat-kursleiterin', '2017-02-16', null, @CurriculumID)

/* COURSE */
INSERT INTO [Course]            
VALUES(NEWID(), 1, @CurrLangId_en, GETDATE(), 'Trainingcenter Switzerland', 'https://www.traningcenterswitzerland.ch', 'What''s new in .NET 4.5 and Visual Studio 2013', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Luzern', '2011-11-03', '2011-11-06', @CurriculumID)
INSERT INTO [Course]           
VALUES(NEWID(), 2, @CurrLangId_en, GETDATE(), 'Trainingcenter Switzerland', 'https://www.traningcenterswitzerland.ch', 'Web application development with MVC 5', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'de'), 'München', '2014-10-12', '2014-10-20', @CurriculumID)
INSERT INTO [Course]             
VALUES(NEWID(), 3, @CurrLangId_en, GETDATE(), 'Trainingcenter Switzerland', 'https://www.traningcenterswitzerland.ch', 'Data Access with Java Spring Framework', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Bern', '2016-02-24', '2016-02-26', @CurriculumID)
INSERT INTO [Course]              
VALUES(NEWID(), 4, @CurrLangId_en, GETDATE(), 'Trainingcenter Switzerland', 'https://www.traningcenterswitzerland.ch', 'Application Lifecycle Management Basis', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'it'), 'Milano', '2018-10-31', null, @CurriculumID)
INSERT INTO [Course]               
VALUES(NEWID(), 5, @CurrLangId_en, GETDATE(), 'Trainingcenter Switzerland', 'https://www.traningcenterswitzerland.ch', 'Develop secure websites', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-01-17', null, @CurriculumID)
INSERT INTO [Course]              
VALUES(NEWID(), 6, @CurrLangId_en, GETDATE(), 'Trainingcenter Switzerland', 'https://www.traningcenterswitzerland.ch', 'Web application development with ASP.NET Core 3.0', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-11-07', '2019-11-08', @CurriculumID)

/* PUBLICATION */
INSERT INTO [Publication]
VALUES(NEWID(), 1, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_en), GETDATE(), NEWID(), 0, 1, null, @CurriculumID, '')

--------------------------------

COMMIT TRANSACTION -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
		SELECT ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage
        ROLLBACK TRAN --RollBack in case of Error
END CATCH
