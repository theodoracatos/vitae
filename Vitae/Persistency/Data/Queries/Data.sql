USE Vitae
GO

BEGIN TRY
    BEGIN TRANSACTION

-- IdentityContext
IF NOT EXISTS(SELECT 1 FROM [dbo].[AspNetUsers] WHERE [Id] = 'a7e161f0-0ff5-45f8-851f-9b041f565abb')
BEGIN
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
     VALUES ('a7e161f0-0ff5-45f8-851f-9b041f565abb' ,'theodoracatos@gmail.com' ,'THEODORACATOS@GMAIL.COM' ,'theodoracatos@gmail.com' ,'THEODORACATOS@GMAIL.COM' ,1 ,'AQAAAAEAACcQAAAAEM9xHq/I11jR504N4mFC+CcEdvencdlc8gfAmRwzeBLLnYuyB8QqWiDsQSyJ1XMiTw==','X7RKCA5QCJYUU3FS6G7UIG7PC2YRECYC','5fbfc0d8-2a6d-4875-8869-c332974e3cf3',null,0,0,null,1,0)

SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON
INSERT INTO [dbo].[AspNetUserClaims](Id, UserId, ClaimType, ClaimValue)
	 VALUES (8, 'a7e161f0-0ff5-45f8-851f-9b041f565abb', 'CurriculumID', 'a7e161f0-0ff5-45f8-851f-9b041f565abb')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
END

DECLARE @UserRole uniqueidentifier = (SELECT [Id] FROM [AspNetRoles] WHERE [Name] = 'User')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
	VALUES ('a7e161f0-0ff5-45f8-851f-9b041f565abb', @UserRole)

-- VitaeContext
DECLARE @LanguageCode_de varchar(2) = 'de'
DECLARE @LanguageCode_en varchar(2) = 'en'
DECLARE @Email varchar(50) = 'theodoracatos@gmail.com'
DECLARE @CurriculumID uniqueidentifier = (SELECT [cla].[ClaimValue] FROM [AspNetUsers] [usr] INNER JOIN [AspNetUserClaims] [cla] ON [cla].[UserId] = [usr].[Id] WHERE [usr].[Email] = @Email)
DECLARE @UserID uniqueidentifier = (SELECT [cla].[UserId] FROM [AspNetUsers] [usr] INNER JOIN [AspNetUserClaims] [cla] ON [cla].[UserId] = [usr].[Id] WHERE [usr].[Email] = @Email)
DECLARE @LanguageID uniqueidentifier = (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_de)

IF NOT EXISTS(SELECT 1 FROM [Curriculum] WHERE [CurriculumID] = @CurriculumID)
BEGIN
INSERT INTO [Curriculum]
VALUES (@CurriculumID, @UserID, GETDATE(), GETDATE(), @LanguageID)

INSERT INTO [CurriculumLanguage] VALUES (@CurriculumID, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_de), 0, 0, GETDATE())
INSERT INTO [CurriculumLanguage] VALUES (@CurriculumID, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_en), 1, 0, GETDATE())
END

DECLARE @CurrLangId_de uniqueidentifier = (SELECT [LanguageID] FROM [CurriculumLanguage] WHERE [CurriculumID] = @CurriculumID AND [Order] = 0)
DECLARE @CurrLangId_en uniqueidentifier = (SELECT [LanguageID] FROM [CurriculumLanguage] WHERE [CurriculumID] = @CurriculumID AND [Order] = 1)

/* ABOUT */
INSERT INTO [About]
VALUES(NEWID(), 0, @CurrLangId_de, GETDATE(), 'Dipl.-Ing. FH | MAS ZFH', '"Das Ganze ist mehr als die Summe seiner Teile." Aristoteles',  '', null, @CurriculumID)

INSERT INTO [About]
VALUES(NEWID(), 0, @CurrLangId_en, GETDATE(), 'Bachelor of Science (B.Sc.) | MAS ZFH', '"The whole is more than the sum of its parts." Aristotle',  '', null, @CurriculumID)

/* PERSONAL DETAIL */
INSERT INTO [PersonalDetail]
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), 'Alexandros', 'Theodoracatos', '1983-06-23', 1, 'Zwischenbächen', 143, 'Zürich', 'Zürich', '8048', 'theodoracatos@gmail.com', '+41', '787044438', 'Zürich ZH, Eschenbach SG', (SELECT [MaritalStatusID] FROM [MaritalStatus] WHERE [MaritalStatusCode] = 3), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), @CurriculumID)
INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 0, GETDATE())
INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'gr'), 1, GETDATE())
INSERT INTO [Child]
VALUES(NEWID(), 'Marilena', '2014-08-14', 0, GETDATE(), (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail] WHERE [CurriculumID] = @CurriculumID))
INSERT INTO [Child]
VALUES(NEWID(), 'Aris', '2017-08-13', 0, GETDATE(), (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail] WHERE [CurriculumID] = @CurriculumID))

/* SOCIAL LINK */
INSERT INTO [SocialLink]
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), 1, 'https://www.facebook.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]         
VALUES (NEWID(), 1, @CurrLangId_de, GETDATE(), 2, 'https://twitter.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]           
VALUES (NEWID(), 2, @CurrLangId_de, GETDATE(), 3, 'https://www.linkedin.com/in/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]         
VALUES (NEWID(), 3, @CurrLangId_de, GETDATE(), 4, 'https://github.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]          
VALUES (NEWID(), 4, @CurrLangId_de, GETDATE(), 5, 'https://www.xing.com/profile/Alexandros_Theodoracatos/cv', @CurriculumID)
INSERT INTO [SocialLink]           
VALUES (NEWID(), 5, @CurrLangId_de, GETDATE(), 7, 'https://stackoverflow.com/users/4369275/grecool', @CurriculumID)
        
INSERT INTO [SocialLink]                             
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 1, 'https://www.facebook.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]    
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 2, 'https://twitter.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]      
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 3, 'https://www.linkedin.com/in/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]        
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 4, 'https://github.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]      
VALUES (NEWID(), 4, @CurrLangId_en, GETDATE(), 5, 'https://www.xing.com/profile/Alexandros_Theodoracatos/cv', @CurriculumID)
INSERT INTO [SocialLink]       
VALUES (NEWID(), 5, @CurrLangId_en, GETDATE(), 7, 'https://stackoverflow.com/users/4369275/grecool', @CurriculumID)

/* EXPERIENCE */
INSERT INTO [Experience]
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), 'Dozent', 'IFA Weiterbildung AG', 'Anbieterin höherer Berufsbildung in den Bereich Wirtschaft und Informatik', 'https://www.ifa.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 14), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Teilzeit-Dozent auf Mandatsbasis.', '2019-10-01', null, @CurriculumID)
INSERT INTO [Experience]           
VALUES (NEWID(), 1, @CurrLangId_de, GETDATE(), 'Senior Softwareingenieur', 'Quilvest (Switzerland) Ltd.', 'Multi-Family Office (Private Banking)', 'http://quilvest.com', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 10), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Architektur und Fullstack-Entwicklung elektronischer Businessprozesse, realisiert mit neuesten .NET Web-Technologien. Technischer Lead und Project Owner eigenentwickelter .NET Applikationssysteme. Technische Projektleitung für interne und externe Projekte. Regelmässige Schulung und Coaching von Mitarbeitern (auf Deutsch und Englisch).', '2012-02-01', null, @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 2, @CurrLangId_de, GETDATE(), 'Software Ingenieur', 'Ruf Telematik AG', 'Anbieter von Fahrgastinformationssystemen', 'http://ruf.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 9), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Architektur, Spezifikation, Design, Implementation, Test und Dokumentation von Softwarekomponenten und Multimediaapplikationen. Projektarbeit: Anforderungsspezifikation, Umsetzung, Engineering, Test und Projektleitung mit direktem Kundenkontakt.', '2008-11-01', '2012-01-31', @CurriculumID)
INSERT INTO [Experience]           
VALUES (NEWID(), 3, @CurrLangId_de, GETDATE(), 'Freelancer', 'Ruf Telematik AG', 'Anbieter von Fahrgastinformationssystemen', 'http://ruf.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 9), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Entwicklung von Tools und Diagnoseprogrammen für Embedded-Geräte.', '2005-09-01', '2008-10-01', @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 4, @CurrLangId_de, GETDATE(), 'Trainee', 'ABB Schweiz AG', 'Lösungen für Schutz und Kontrolle in elektronischen Schaltanlagen', 'http://abb.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 3), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Entwicklung von Systemtools für elektrische Schaltanlagen (IEC 61850). Entwicklung von diversen Multimediaapplikationen für Firmenpräsentationen (Demos).', '2003-08-01', '2005-08-01', @CurriculumID)
                                   
INSERT INTO [Experience]            
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Lecturer', 'IFA Weiterbildung AG', 'Provider of higher vocational training in the field of economics and computer science', 'https://www.ifa.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 14), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Part-time lecturer, based on mandates.', '2019-10-01', null, @CurriculumID)
INSERT INTO [Experience]           
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'Senior Software Engineer', 'Quilvest (Switzerland) Ltd.', 'Multi-Family Office (Private Banking)', 'http://quilvest.com', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 10), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Architecture and full-stack development of electronic business processes, realized with the latest .NET web technologies. Technical lead and project owner of self-developed .NET application systems. Technical project management for internal and external projects. Regular training and coaching of employees (in German and English).', '2012-02-01', null, @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 'Software Engineer', 'Ruf Telematik AG', 'Provider of passenger information systems', 'http://ruf.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 9), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Architecture, specification, design, implementation, test and documentation of software components and multimedia applications. Project management: Requirement speci-fication, implementation, engineering and test with direct contact with customers.', '2008-11-01', '2012-01-31', @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'Freelancer', 'Ruf Telematik AG', 'Provider of passenger information systems', 'http://ruf.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 9), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Development of tools and diagnostic programs for embedded devices.', '2005-09-01', '2008-10-01', @CurriculumID)
INSERT INTO [Experience]          
VALUES (NEWID(), 4, @CurrLangId_en, GETDATE(), 'Trainee', 'ABB Schweiz AG', 'Solutions for protection and control in electronic switchgear', 'http://abb.ch', (SELECT [HierarchyLevelID] FROM [HierarchyLevel] WHERE [HierarchyLevelCode] =  1), (SELECT [IndustryID] FROM [Industry] WHERE [IndustryCode] = 3), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Development of system tools for electrical switchgear (IEC 61850). Development of various multimedia applications for company presentations.', '2003-08-01', '2005-08-01', @CurriculumID)
                                  
/* EDUCATION */                    
INSERT INTO [Education]            
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Master of Advances Studies (MAS)', 'Wirtschaftsinformatik',  'Berufsbegleitendes Nachdiplomstudium mit Schwerpunkten in: Betriebswirtschaft, Software Engineering, Projektmanagement und Coaching. Masterarbeit: "Wissenstransfer beim Stellenwechsel".', '5.4', '2010-02-01', '2011-11-01', @CurriculumID)
INSERT INTO [Education]            
VALUES (NEWID(), 1, @CurrLangId_de, GETDATE(), 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Diplomstudium (Dipl. Ing. FH)', 'Informationstechnologie', 'Vollzeit Diplomstudium mit Schwerpunkten in: Software Architekturen / Modellierung und Softwareentwicklung. Diplomarbeit: "Multi-Agenten-Plattform für die Simulation von Finanzmärkten".', '5.3', '2005-09-01', '2008-10-01', @CurriculumID)
INSERT INTO [Education]             
VALUES (NEWID(), 2, @CurrLangId_de, GETDATE(), 'GIBB', 'https://gibb.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Bern', 'Fachausweis (FA)', 'Applikationsentwicklung', 'Die Lehre zum Applikationsentwickler für Maturanden (way-up.ch) bot eine zweijährige Praxiserfahrung in verschiedenen Firmen und ebnete den Weg zur Fachhochschule. Abschlussarbeit: "Dynamisch generierte Fahrplantabelle unter Verwendung von .Net und XSLT".', '5.5', '2003-08-01', '2005-08-01', @CurriculumID)
INSERT INTO [Education]            
VALUES (NEWID(), 3, @CurrLangId_de, GETDATE(), 'Kantonsschule SH', 'https://kanti.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schaffhausen', 'Matura', 'Naturwissenschaften (Profil N)', 'Naturwissenschaftlich–mathematische Matura mit Schwerpunkten in Chemie / Biologie. Maturaarbeit: "Sind die Schaffhauser Kantischüler fit?".', '4.5', '1998-08-01', '2002-07-01', @CurriculumID)
                                   
INSERT INTO [Education]            
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Master of Advances Studies (MAS)', 'Business Information Technology',  'Post-graduate study with focus on: Business Administration, Software Engineering, Project Management and Coaching.', '5.4', '2010-02-01', '2011-11-01', @CurriculumID)
INSERT INTO [Education]            
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Diplomstudium (Dipl. Ing. FH)', 'Information Technology', 'Diploma study with focus on: Softwarearchitecture / modeling and software development. Diploma thesis: "Multi-agent platform for the simulation of financial markets".', '5.3', '2005-09-01', '2008-10-01', @CurriculumID)
INSERT INTO [Education]          
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 'GIBB', 'https://gibb.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Bern', 'Apprenticeship', 'Application development', 'The apprenticeship in application development for high school graduates (wayup.ch) offered a two-year practical experience in various companies and paved the way for the University of Applied Sciences.', '5.5', '2003-08-01', '2005-08-01', @CurriculumID)
INSERT INTO [Education]           
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'Kantonsschule SH', 'https://kanti.ch', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schaffhausen', 'Matura', 'Naturwissenschaften (Profil N)', 'Profile N (scientific and mathematical) with focus on chemistry and biology. Matura thesis: "Are the high school students of Schaffhausen fit?".', '4.5', '1998-08-01', '2002-07-01', @CurriculumID)


/* LANGUAGE SKILL */
INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 0, @CurrLangId_de , GETDATE(), 4, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), @CurriculumID)
INSERT INTO [LanguageSkill]      
VALUES(NEWID(), 1, @CurrLangId_de , GETDATE(), 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'en'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                      
VALUES(NEWID(), 2, @CurrLangId_de , GETDATE(), 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'el'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                        
VALUES(NEWID(), 3, @CurrLangId_de , GETDATE(), 2, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'fr'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                                       
VALUES(NEWID(), 0, @CurrLangId_en , GETDATE(), 4, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                   
VALUES(NEWID(), 1, @CurrLangId_en , GETDATE(), 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'en'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                    
VALUES(NEWID(), 2, @CurrLangId_en , GETDATE(), 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'el'), @CurriculumID)
INSERT INTO [LanguageSkill]                                                                                      
VALUES(NEWID(), 3, @CurrLangId_en , GETDATE(), 2, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'fr'), @CurriculumID)

/* AWARD */
INSERT INTO [Award]
VALUES(NEWID(), 0, @CurrLangId_de, GETDATE(), 'ZHAW Alumni Award', 'Bestnote im Studiengang MAS Wirtschaftsinformatik', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'https://www.zhaw.ch', '2012-01-01', @CurriculumID)
INSERT INTO [Award]             
VALUES(NEWID(), 1, @CurrLangId_de, GETDATE(), 'ZHAW Alumni Award', 'Beste Masterarbeit im Studiengang MAS Wirtschaftsinformatik', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'https://www.zhaw.ch', '2012-01-01', @CurriculumID)
                                
INSERT INTO [Award]             
VALUES(NEWID(), 0, @CurrLangId_en, GETDATE(), 'ZHAW Alumni Award', 'Best grade in MAS Business Information Management program', 'ZHAW School of Engineering', 'https://www.zhaw.ch', '2012-01-01', @CurriculumID)
INSERT INTO [Award]              
VALUES(NEWID(), 1, @CurrLangId_en, GETDATE(), 'ZHAW Alumni Award', 'Best grade in MAS Business Information Management Master Thesis', 'ZHAW School of Engineering', 'https://www.zhaw.ch', '2012-01-01', @CurriculumID)
                                
/* INTEREST */                  
INSERT INTO [Interest]          
VALUES(NEWID(), 0, @CurrLangId_de, GETDATE(), 'Sport', null, 'Schwimmen, Tennis und Badminton', null, @CurriculumID)
INSERT INTO [Interest]           
VALUES(NEWID(), 1, @CurrLangId_de, GETDATE(), 'Kochen', null, 'Beim Kochen kann die Kreativität auf die Probe gestellt werden', null, @CurriculumID)
INSERT INTO [Interest]          
VALUES(NEWID(), 2, @CurrLangId_de, GETDATE(), 'Reisen', null, 'Reisen, neue Länder und Kulturen haben mich seit jeher fasziniert', null, @CurriculumID)
INSERT INTO [Interest]           
VALUES(NEWID(), 3, @CurrLangId_de, GETDATE(), 'Kinder', null, 'Meine beiden Kinder halten mich stets auf Trab', null, @CurriculumID)
                                
INSERT INTO [Interest]          
VALUES(NEWID(), 0, @CurrLangId_en, GETDATE(), 'Sports', null, 'Swimming, tennis and badminton', null, @CurriculumID)
INSERT INTO [Interest]          
VALUES(NEWID(), 1, @CurrLangId_en, GETDATE(), 'Cooking', null, 'Cooking can be a test of creativity', null, @CurriculumID)
INSERT INTO [Interest]         
VALUES(NEWID(), 2, @CurrLangId_en, GETDATE(), 'Travelling', null, 'Travel, new countries and cultures have always fascinated me', null, @CurriculumID)
INSERT INTO [Interest]         
VALUES(NEWID(), 3, @CurrLangId_en, GETDATE(), 'Children', null, 'My two children always keep me on my toes', null, @CurriculumID)

/* SKILL */
INSERT INTO [Skill]
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), 'Programmiersprachen', 'C#, Java, Visual Basic, TypeScript / JavaScript, VB', @CurriculumID)
INSERT INTO [Skill]              
VALUES (NEWID(), 1, @CurrLangId_de, GETDATE(), 'Frameworks', '.NET Framework (v4.8), .NET Core (v3.1), Entity Framework (v6.4), Entity Framework Core (v3.1), ADO.Net, WinForms', @CurriculumID)
INSERT INTO [Skill]              
VALUES (NEWID(), 2, @CurrLangId_de, GETDATE(), 'Webtechnologien', 'ASP.NET Core, ASP.NET Web API, ASP.NET MVC, ASP.NET WebForms, TypeScript, HTML, XML, SASS', @CurriculumID)
INSERT INTO [Skill]              
VALUES (NEWID(), 3, @CurrLangId_de, GETDATE(), 'Datenbanktechnologien', 'Microsoft SQL Server, Oracle, T-SQL, PL/SQL', @CurriculumID)
INSERT INTO [Skill]             
VALUES (NEWID(), 4, @CurrLangId_de, GETDATE(), 'IDE/Tools', 'Visual Studio, Eclipse, Azure DevOps, Git, Subversion (SVN)', @CurriculumID)
INSERT INTO [Skill]              
VALUES (NEWID(), 5, @CurrLangId_de, GETDATE(), 'Projektmethoden', 'Agile, Scrum, RUP', @CurriculumID)
INSERT INTO [Skill]             
VALUES (NEWID(), 6, @CurrLangId_de, GETDATE(), 'Entwicklungsmethoden', 'Domain driven Development, Prototyping', @CurriculumID)
                                  
INSERT INTO [Skill]               
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Programming languages', 'C#, Java, Visual Basic, TypeScript / JavaScript, VB', @CurriculumID)
INSERT INTO [Skill]              
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'Frameworks', '.NET Framework (v4.8), .NET Core (v3.1), Entity Framework (v6.4), Entity Framework Core (v3.1), ADO.Net, WinForms', @CurriculumID)
INSERT INTO [Skill]               
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 'Web technologies', 'ASP.NET Core, ASP.NET Web API, ASP.NET MVC, ASP.NET WebForms, TypeScript, HTML, XML, SASS', @CurriculumID)
INSERT INTO [Skill]               
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'Database technlogies', 'Microsoft SQL Server, Oracle, T-SQL, PL/SQL', @CurriculumID)
INSERT INTO [Skill]           
VALUES (NEWID(), 4, @CurrLangId_en, GETDATE(), 'IDE/Tools', 'Visual Studio, Eclipse, Azure DevOps, Git, Subversion (SVN)', @CurriculumID)
INSERT INTO [Skill]           
VALUES (NEWID(), 5, @CurrLangId_en, GETDATE(), 'Project methods', 'Agile, Scrum, RUP', @CurriculumID)
INSERT INTO [Skill]               
VALUES (NEWID(), 6, @CurrLangId_en, GETDATE(), 'Development methods', 'Domain driven Development, Prototyping', @CurriculumID)

/* ABROAD */
INSERT INTO [Abroad]
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'gb'), 'Bath', 'Dreiwöchiger Sprachaufenthalt in einer EF-Sprachschule mit Unterkunft in einer Gastfamilie.', '2001-04-01', '2001-04-01', @CurriculumID)
INSERT INTO [Abroad]
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'gb'), 'Bath', 'Three weeks of study in an EF language school with host family accommodation.', '2001-04-01', '2001-04-01', @CurriculumID)

/* REFERENCE */
INSERT INTO [Reference]
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), 'Wolfgang', 'Schmidt', 1, 'ABB (Schweiz) AG', 'https://abb.ch', 'Ehemaliger Vorgesetzter bei der Ruf Telematik AG', 'wolfgang.schmidt@abb.ch', '+41', '58 585 00 00', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'de'), 0, @CurriculumID)
INSERT INTO [Reference]
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Wolfgang', 'Schmidt', 1, 'ABB (Schweiz) AG', 'https://abb.ch', 'Former supervisor at Ruf Telematik AG', 'wolfgang.schmidt@abb.ch', '+41', '58 585 00 00', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'de'), 0, @CurriculumID)

/* CERTIFICATE */
INSERT INTO [Certificate]
VALUES (NEWID(), 0, @CurrLangId_de, GETDATE(), 'Cambridge English Level 2 Certificate in ESOL International', 'Certificate in Advanced English', 'Cambridge Assessment English', 'https://www.cambridgeenglish.org', '2014-03-01', null, @CurriculumID)
INSERT INTO [Certificate]         
VALUES (NEWID(), 1, @CurrLangId_de, GETDATE(), 'Microsoft Certified Professional (MCP)', 'Microsoft Specialist: Programming in C#', 'Microsoft', 'https://www.microsoft.com', '2015-04-13', null, @CurriculumID)
INSERT INTO [Certificate]         
VALUES (NEWID(), 2, @CurrLangId_de, GETDATE(), 'Professional Scrum Master I (PSM I)', 'Microsoft Specialist: Programming in C#', 'Scrum.org', 'https://www.scrum.org/certificates/205995', '2016-09-06', null, @CurriculumID)
INSERT INTO [Certificate]        
VALUES (NEWID(), 3, @CurrLangId_de, GETDATE(), 'SVEB Kursleiter Stufe 1', 'Lernveranstaltungen mit Erwachsenen durchführen / AdA FA-M 1', 'Klubschule Migros', 'https://alice.ch/de/ausbilden-als-beruf/ada-abschluesse/sveb-zertifikat-kursleiterin', '2019-05-16', null, @CurriculumID)
                                 
INSERT INTO [Certificate]        
VALUES (NEWID(), 0, @CurrLangId_en, GETDATE(), 'Cambridge English Level 2 Certificate in ESOL International', 'Certificate in Advanced English', 'Cambridge Assessment English', 'https://www.cambridgeenglish.org', '2014-03-01', null, @CurriculumID)
INSERT INTO [Certificate]        
VALUES (NEWID(), 1, @CurrLangId_en, GETDATE(), 'Microsoft Certified Professional (MCP)', 'Microsoft Specialist: Programming in C#', 'Microsoft', 'https://www.microsoft.com', '2015-04-13', null, @CurriculumID)
INSERT INTO [Certificate]         
VALUES (NEWID(), 2, @CurrLangId_en, GETDATE(), 'Professional Scrum Master I (PSM I)', 'Microsoft Specialist: Programming in C#', 'Scrum.org', 'https://www.scrum.org/certificates/205995', '2016-09-06', null, @CurriculumID)
INSERT INTO [Certificate]         
VALUES (NEWID(), 3, @CurrLangId_en, GETDATE(), 'SVEB instructor level 1', 'Conduct learning events with adults / AdA FA-M 1', 'Klubschule Migros', 'https://alice.ch/de/ausbilden-als-beruf/ada-abschluesse/sveb-zertifikat-kursleiterin', '2019-05-16', null, @CurriculumID)

/* COURSE */
INSERT INTO [Course]
VALUES(NEWID(), 0, @CurrLangId_de, GETDATE(), 'ETH Zürich', 'https://ethz.ch', 'XSLT - Einführung mit Übungen', 'Herkunft und Anwendungsgebiet von XSLT', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2004-09-20', '2004-09-21', @CurriculumID)
INSERT INTO [Course]             
VALUES(NEWID(), 1, @CurrLangId_de, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Neues in .NET 4.5 und Visual Studio 2013', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2014-11-03', '2014-11-04', @CurriculumID)
INSERT INTO [Course]              
VALUES(NEWID(), 2, @CurrLangId_de, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Entwicklung von Webapplikationen mit MVC 5', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2015-11-12', '2015-11-13', @CurriculumID)
INSERT INTO [Course]            
VALUES(NEWID(), 3, @CurrLangId_de, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Datenzugriff in .NET mit dem Entity Framework und ADO.NET', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2016-02-24', '2016-02-26', @CurriculumID)
INSERT INTO [Course]            
VALUES(NEWID(), 4, @CurrLangId_de, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Application Lifecycle Management Basis', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2017-10-31', null, @CurriculumID)
INSERT INTO [Course]           
VALUES(NEWID(), 5, @CurrLangId_de, GETDATE(),'Digicomp Academy AG', 'https://www.digicomp.ch', 'Sichere Websites entwickeln', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-01-17', null, @CurriculumID)
INSERT INTO [Course]              
VALUES(NEWID(), 6, @CurrLangId_de, GETDATE(),'Digicomp Academy AG', 'https://www.digicomp.ch', 'Entwicklung von Webapplikationen mit ASP.NET Core 3.0', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-11-07', '2019-11-08', @CurriculumID)
INSERT INTO [Course]              
VALUES(NEWID(), 7, @CurrLangId_de, GETDATE(),' IFA Weiterbildung AG', 'https://www.ifa.ch', 'Einsatz digitaler Medien im Unterricht', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-11-07', '2019-11-08', @CurriculumID)
                                  
INSERT INTO [Course]              
VALUES(NEWID(), 0, @CurrLangId_en, GETDATE(), 'ETH Zürich', 'https://ethz.ch', 'XSLT - Introduction with exercises', 'Origin and application area from XSLT', (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2004-09-20', '2004-09-21', @CurriculumID)
INSERT INTO [Course]            
VALUES(NEWID(), 1, @CurrLangId_en, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'What''s new in .NET 4.5 and Visual Studio 2013', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2014-11-03', '2014-11-04', @CurriculumID)
INSERT INTO [Course]           
VALUES(NEWID(), 2, @CurrLangId_en, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Web application development with MVC 5', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2015-11-12', '2015-11-13', @CurriculumID)
INSERT INTO [Course]             
VALUES(NEWID(), 3, @CurrLangId_en, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Data Access in .NET with the Entity Framework and ADO.NET', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2016-02-24', '2016-02-26', @CurriculumID)
INSERT INTO [Course]              
VALUES(NEWID(), 4, @CurrLangId_en, GETDATE(), 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Application Lifecycle Management Basis', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2017-10-31', null, @CurriculumID)
INSERT INTO [Course]               
VALUES(NEWID(), 5, @CurrLangId_en, GETDATE(),'Digicomp Academy AG', 'https://www.digicomp.ch', 'Develop secure websites', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-01-17', null, @CurriculumID)
INSERT INTO [Course]              
VALUES(NEWID(), 6, @CurrLangId_en, GETDATE(),'Digicomp Academy AG', 'https://www.digicomp.ch', 'Web application development with ASP.NET Core 3.0', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-11-07', '2019-11-08', @CurriculumID)
INSERT INTO [Course]               
VALUES(NEWID(), 7, @CurrLangId_en, GETDATE(),' IFA Weiterbildung AG', 'https://www.ifa.ch', 'Use of digital media in education', null, (SELECT [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-11-07', '2019-11-08', @CurriculumID)

/* PUBLICATION */
INSERT INTO [Publication]
VALUES(NEWID(), 0, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_de), GETDATE(), NEWID(), 0, null, @CurriculumID, '')
INSERT INTO [Publication]
VALUES(NEWID(), 1, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_en), GETDATE(), NEWID(), 0, null, @CurriculumID, '')

--------------------------------

COMMIT TRANSACTION -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
		SELECT ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage
        ROLLBACK TRAN --RollBack in case of Error
END CATCH
