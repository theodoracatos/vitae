USE Vitae
GO

BEGIN TRY
    BEGIN TRANSACTION

DECLARE @LanguageCode_de varchar(2) = 'de'
DECLARE @LanguageCode_en varchar(2) = 'en'
DECLARE @Email varchar(50) = 'theodoracatos@gmail.com'
DECLARE @CurriculumID uniqueidentifier = (SELECT [cla].[ClaimValue] FROM [AspNetUsers] [usr] INNER JOIN [AspNetUserClaims] [cla] ON [cla].[UserId] = [usr].[Id] WHERE [usr].[Email] = @Email)
DECLARE @UserID uniqueidentifier = (SELECT [cla].[UserId] FROM [AspNetUsers] [usr] INNER JOIN [AspNetUserClaims] [cla] ON [cla].[UserId] = [usr].[Id] WHERE [usr].[Email] = @Email)

IF NOT EXISTS(SELECT 1 FROM [Curriculum] WHERE [CurriculumID] = @CurriculumID)
BEGIN
INSERT INTO [Curriculum]
VALUES (@CurriculumID, @UserID, GETDATE(), @LanguageCode_de, GETDATE())

INSERT INTO [CurriculumLanguage] VALUES (@CurriculumID, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_de), 0)
INSERT INTO [CurriculumLanguage] VALUES (@CurriculumID, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = @LanguageCode_en), 1)
END

DECLARE @CurrLangId_de uniqueidentifier = (SELECT [LanguageID] FROM [CurriculumLanguage] WHERE [CurriculumID] = @CurriculumID AND [Order] = 0)
DECLARE @CurrLangId_en uniqueidentifier = (SELECT [LanguageID] FROM [CurriculumLanguage] WHERE [CurriculumID] = @CurriculumID AND [Order] = 1)

/* ABOUT */
INSERT INTO [About]
VALUES(NEWID(), 0, @CurrLangId_de, 'Dipl.-Ing. FH | MAS ZFH', '"Das Ganze ist mehr als die Summe seiner Teile." Aristoteles',  '', null, @CurriculumID)

INSERT INTO [About]
VALUES(NEWID(), 0, @CurrLangId_en, 'Dipl.-Ing. FH | MAS ZFH', '"The whole is more than the sum of its parts." Aristotle',  '', null, @CurriculumID)

/* PERSONAL DETAIL */
INSERT INTO [PersonalDetail]
VALUES (NEWID(), 0, @CurrLangId_de, 'Alexandros', 'Theodoracatos', '1983-06-23', 1, 'Zwischenbächen', 143, 'Zürich', 'Zürich', '8048', 'theodoracatos@gmail.com', '787044438', 'Zürich ZH, Eschenbach SG', (SELECT TOP 1 [MaritalStatusID] FROM [MaritalStatus] WHERE [MaritalStatusCode] = 3), (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), (SELECT TOP 1 [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), @CurriculumID)
INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 0)
INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'gr'), 1)
INSERT INTO [Child]
VALUES(NEWID(), 'Marilena', '2014-08-14', 0, (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]))
INSERT INTO [Child]
VALUES(NEWID(), 'Aris', '2017-08-13', 0, (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]))

/* SOCIAL LINK */
INSERT INTO [SocialLink]
VALUES (NEWID(), 0, @CurrLangId_de, 1, 'https://www.facebook.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 1, @CurrLangId_de, 2, 'https://twitter.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 2, @CurrLangId_de, 3, 'https://www.linkedin.com/in/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 3, @CurrLangId_de, 4, 'https://github.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 4, @CurrLangId_de, 5, 'https://www.xing.com/profile/Alexandros_Theodoracatos/cv', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 5, @CurrLangId_de, 7, 'https://stackoverflow.com/users/4369275/grecool', @CurriculumID)

INSERT INTO [SocialLink]
VALUES (NEWID(), 0, @CurrLangId_en, 1, 'https://www.facebook.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 1, @CurrLangId_en, 2, 'https://twitter.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 2, @CurrLangId_en, 3, 'https://www.linkedin.com/in/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 3, @CurrLangId_en, 4, 'https://github.com/theodoracatos', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 4, @CurrLangId_en, 5, 'https://www.xing.com/profile/Alexandros_Theodoracatos/cv', @CurriculumID)
INSERT INTO [SocialLink]
VALUES (NEWID(), 5, @CurrLangId_en, 7, 'https://stackoverflow.com/users/4369275/grecool', @CurriculumID)

/* EXPERIENCE */
INSERT INTO [Experience]
VALUES (NEWID(), 0, @CurrLangId_de, 'Dozent', 'IFA Weiterbildungs AG', 'https://www.ifa.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Teilzeit-Dozent auf Mandatsbasis', '2019-10-01', null, @CurriculumID)
INSERT INTO [Experience]
VALUES (NEWID(), 1, @CurrLangId_de, 'Senior Softwareingenieur', 'Quilvest (Switzerland) Ltd.', 'http://quilvest.com', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Architektur und Fullstack-Entwicklung elektronischer Businessprozesse, realisiert mit neuesten .NET Web-Technologien. Technischer Lead folgender eigenentwickelter Applikationssysteme: Intranet, eBanking, MBO-System, Client-OnBoard-Dokumentenerstellung. Projektleitung mit fachlicher Führung von 5 – 7 Mitarbeitern (länderübergreifend). Regelmässige Schulung und Coaching von Mitarbeitern (auf Deutsch und Englisch)', '2012-02-01', null, @CurriculumID)
INSERT INTO [Experience]
VALUES (NEWID(), 2, @CurrLangId_de, 'Software Ingenieur', 'Ruf Telematik AG', 'http://ruf.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Architektur, Spezifikation, Design, Implementation, Test und Dokumentation von Softwarekomponenten und Multimediaapplikationen. Projektarbeit: Anforderungsspezifikation, Umsetzung, Engineering, Test und Projektleitung mit direktem Kundenkontakt', '2008-11-01', '2012-01-31', @CurriculumID)
INSERT INTO [Experience]
VALUES (NEWID(), 3, @CurrLangId_de, 'Freelancer', 'Ruf Telematik AG', 'http://ruf.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Entwicklung von Tools und Diagnoseprogrammen für Embedded-Geräte', '2005-09-01', '2008-10-01', @CurriculumID)
INSERT INTO [Experience]
VALUES (NEWID(), 4, @CurrLangId_de, 'Trainee', 'ABB Schweiz AG', 'http://abb.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Entwicklung von Systemtools für elektrische Schaltanlagen (IEC 61850). Entwicklung von diversen Multimediaapplikationen für Firmenpräsentationen (Demos)', '2003-08-01', '2005-08-01', @CurriculumID)

INSERT INTO [Experience]
VALUES (NEWID(), 0, @CurrLangId_en, 'Lecturer', 'IFA Weiterbildungs AG', 'https://www.ifa.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Part-time lecturer (mandates)', '2019-10-01', null, @CurriculumID)
INSERT INTO [Experience]
VALUES (NEWID(), 1, @CurrLangId_de, 'Senior Software Engineer', 'Quilvest (Switzerland) Ltd.', 'http://quilvest.com', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Architecture and implementation of electronic business processes, realized with latest Microsoft .NET technologies. Main and technical project management for internal and eBanking projects. Lead engineer of several self-developed application systems.', '2012-02-01', null, @CurriculumID)

-- TODO...





/* EDUCATION */
INSERT INTO [Education]
VALUES (NEWID(), 0, @CurrLangId_de, 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Master of Advances Studies (MAS)', 'Wirtschaftsinformatik',  'Berufsbegleitendes Nachdiplomstudium mit Schwerpunkten in: Betriebswirtschaft, Software Engineering, Projektmanagement und Coaching', '5.4', '2010-02-01', '2011-11-01', @CurriculumID)

INSERT INTO [Education]
VALUES (NEWID(), 1, @CurrLangId_de, 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Diplomstudium (Dipl. Ing. FH)', 'Informationstechnologie', 'Vollzeit Diplomstudium mit Schwerpunkten in: Software Architekturen / Modellierung und Softwareentwicklung', '5.3', '2005-09-01', '2008-10-01', @CurriculumID)

INSERT INTO [Education]
VALUES (NEWID(), 2, @CurrLangId_de, 'GIBB', 'https://gibb.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Bern', 'Fachausweis (FA)', 'Applikationsentwicklung', 'Die Lehre zum Applikationsentwickler für Maturanden (way-up.ch) bot eine zweijährige Praxiserfahrung in verschiedenen Firmen und ebnete den Weg zur Fachhochschule', '5.5', '2003-08-01', '2005-08-01', @CurriculumID)

INSERT INTO [Education]
VALUES (NEWID(), 3, @CurrLangId_de, 'Kantonsschule SH', 'https://kanti.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schaffhausen', 'Matura', 'Naturwissenschaften (Profil N)', 'Naturwissenschaftlich–mathematische Matura mit Schwerpunkten in Chemie / Biologie', '4.5', '1998-08-01', '2002-07-01', @CurriculumID)

/* LANGUAGE SKILL */
INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 0, @CurrLangId_de, 4, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), @CurriculumID)

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 1, @CurrLangId_de, 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'en'), @CurriculumID)

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 2, @CurrLangId_de, 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'el'), @CurriculumID)

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 3,@CurrLangId_de, 2, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'fr'), @CurriculumID)

/* AWARD */
INSERT INTO [Award]
VALUES(NEWID(), 0, @CurrLangId_de, 'ZHAW Alumni Award', 'Bestnote im Studiengang MAS Wirtschaftsinformatik', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'https://www.zhaw.ch', '2012-01-01', @CurriculumID)

INSERT INTO [Award]
VALUES(NEWID(), 1, @CurrLangId_de, 'ZHAW Alumni Award', 'Beste Masterarbeit im Studiengang MAS Wirtschaftsinformatik', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'https://www.zhaw.ch', '2012-01-01', @CurriculumID)

/* INTEREST */
INSERT INTO [Interest]
VALUES(NEWID(), 0, @CurrLangId_de, 'Sport', null, 'Schwimmen, Tennis und Badminton', null, @CurriculumID)

INSERT INTO [Interest]
VALUES(NEWID(), 1, @CurrLangId_de, 'Kochen', null, 'Beim Kochen kann ich abschalten und stets Neues ausprobieren', null, @CurriculumID)

INSERT INTO [Interest]
VALUES(NEWID(), 2, @CurrLangId_de, 'Reisen', null, 'Reisen, neue Länder und Kulturen haben mich seit jeher fasziniert', null, @CurriculumID)

INSERT INTO [Interest]
VALUES(NEWID(), 3, @CurrLangId_de, 'Kinder', null, 'Meine beiden Kinder halten mich stets auf Trab', null, @CurriculumID)

/* SKILL */
INSERT INTO [Skill]
VALUES (NEWID(), 0, @CurrLangId_de, 'Programmiersprachen', 'C#, Java, Visual Basic, TypeScript / JavaScript, VB', @CurriculumID)

INSERT INTO [Skill]
VALUES (NEWID(), 1, @CurrLangId_de, 'Frameworks', '.NET Framework, .NET Core, Entity Framework, Entity Framework Core, ADO.Net, WinForms', @CurriculumID)

INSERT INTO [Skill]
VALUES (NEWID(), 2, @CurrLangId_de, 'Webtechnologien', 'ASP.NET Core, ASP.NET Web API, ASP.NET MVC, ASP.NET WebForms, TypeScript, HTML, XML, SASS', @CurriculumID)

INSERT INTO [Skill]
VALUES (NEWID(), 3, @CurrLangId_de, 'Datenbanktechnologien', 'Microsoft SQL Server, Oracle, T-SQL, PL/SQL', @CurriculumID)

INSERT INTO [Skill]
VALUES (NEWID(), 4, @CurrLangId_de,'IDE/Tools', 'Visual Studio, Eclipse, Azure DevOps, Git, Subversion (SVN)', @CurriculumID)

INSERT INTO [Skill]
VALUES (NEWID(), 5, @CurrLangId_de,'Projektmethoden', 'Agile, Scrum, RUP', @CurriculumID)

INSERT INTO [Skill]
VALUES (NEWID(), 6, @CurrLangId_de, 'Entwicklungsmethoden', 'Domain driven Development, Prototyping', @CurriculumID)

/* ABROAD */
INSERT INTO [Abroad]
VALUES (NEWID(), 0, @CurrLangId_de, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'sg'), 'Singapore', 'Auslandaufenthalt in Singapur', '2015-01-01', '2018-01-01', @CurriculumID)

/* REFERENCE */
INSERT INTO [Reference]
VALUES (NEWID(), 0, @CurrLangId_de, 'Wolfgang', 'Schmidt', 1, 'ABB (Schweiz) AG', 'https://abb.ch', 'Ehemaliger Vorgesetzter bei der Ruf Telematik AG', 'wolfgang.schmidt@abb.ch', '78 704 44 38', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'de'), 0, @CurriculumID)

/* CERTIFICATE */
INSERT INTO [Certificate]
VALUES (NEWID(), 0, @CurrLangId_de, 'Cambridge English Level 2 Certificate in ESOL International', 'Certificate in Advanced English', 'Cambridge Assessment English', 'https://www.cambridgeenglish.org', '2014-03-01', null, @CurriculumID)

INSERT INTO [Certificate]
VALUES (NEWID(), 1, @CurrLangId_de, 'Microsoft Certified Professional (MCP)', 'Microsoft Specialist: Programming in C#', 'Microsoft', 'https://www.microsoft.com', '2015-04-13', null, @CurriculumID)

INSERT INTO [Certificate]
VALUES (NEWID(), 2, @CurrLangId_de, 'Professional Scrum Master I (PSM I)', 'Microsoft Specialist: Programming in C#', 'Scrum.org', 'https://www.scrum.org/certificates/205995', '2016-09-06', null, @CurriculumID)

INSERT INTO [Certificate]
VALUES (NEWID(), 3, @CurrLangId_de, 'SVEB Kursleiter Stufe 1', 'Lernveranstaltungen mit Erwachsenen durchführen / AdA FA-M 1', 'Klubschule Migros', 'https://alice.ch/de/ausbilden-als-beruf/ada-abschluesse/sveb-zertifikat-kursleiterin', '2019-05-16', null, @CurriculumID)

/* COURSE */
INSERT INTO [Course]
VALUES(NEWID(), 0, @CurrLangId_de, 'ETH Zürich', 'https://ethz.ch', 'XSLT - Einführung mit Übungen', 'Herkunft und Anwendungsgebiet from XSLT', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2004-09-20', '2004-09-21', @CurriculumID)

INSERT INTO [Course]
VALUES(NEWID(), 1, @CurrLangId_de, 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Neues in .NET 4.5 und Visual Studio 2013', null, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2014-11-03', '2014-11-04', @CurriculumID)

INSERT INTO [Course]
VALUES(NEWID(), 2, @CurrLangId_de, 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Entwicklung von Webapplikationen mit MVC 5', null, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2015-11-12', '2015-11-13', @CurriculumID)

INSERT INTO [Course]
VALUES(NEWID(), 3, @CurrLangId_de, 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Datenzugriff in .NET mit dem Entity Framework und ADO.NET', null, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2016-02-24', '2016-02-26', @CurriculumID)

INSERT INTO [Course]
VALUES(NEWID(), 4, @CurrLangId_de, 'Digicomp Academy AG', 'https://www.digicomp.ch', 'Application Lifecycle Management Basis', null, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2017-10-31', null, @CurriculumID)

INSERT INTO [Course]
VALUES(NEWID(), 5, @CurrLangId_de,'Digicomp Academy AG', 'https://www.digicomp.ch', 'Sichere Websites entwickeln', null, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-01-17', null, @CurriculumID)

INSERT INTO [Course]
VALUES(NEWID(), 6, @CurrLangId_de,'Digicomp Academy AG', 'https://www.digicomp.ch', 'Entwicklung von Webapplikationen mit ASP.NET Core 3.0', null, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-11-07', '2019-11-08', @CurriculumID)

INSERT INTO [Course]
VALUES(NEWID(), 7, @CurrLangId_de,' IFA Weiterbildung AG', 'https://www.ifa.ch', 'Einsatz digitaler Medien im Unterricht', null, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', '2019-11-07', '2019-11-08', @CurriculumID)

--------------------------------

COMMIT TRANSACTION -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
		SELECT ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage
        ROLLBACK TRAN --RollBack in case of Error
END CATCH