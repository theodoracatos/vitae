USE Vitae
GO

BEGIN TRY
    BEGIN TRANSACTION

-- Roles
IF NOT EXISTS(SELECT 1 FROM [dbo].[AspNetRoles])
BEGIN
    INSERT INTO [dbo].[AspNetRoles]([Id], [Name], [NormalizedName]) VALUES (NEWID(), 'Admin', 'ADMIN')
    INSERT INTO [dbo].[AspNetRoles]([Id], [Name], [NormalizedName]) VALUES (NEWID(), 'User', 'USER')
END

INSERT INTO [About]
VALUES(NEWID(), '','"Great things in business are never done by one person. They’re done by a team of people." Steve Jobs',  null)

INSERT INTO [Person]
VALUES (NEWID(), 'Alexandros', 'Theodoracatos', '1983-06-23', 1, 'Zwischenbächen', 143, 8048, 'Zürich', 'Zürich', 'theodoracatos@gmail.com', '787044438',  (SELECT TOP 1 [CountryID] FROM [Country]), (SELECT TOP 1 [AboutID] FROM [About]),  (SELECT TOP 1 [LanguageID] FROM [Language]))

INSERT INTO [Curriculum]
VALUES (NEWID(), 'a05c13a8-21fb-42c9-a5bc-98b7d94f464a', 'a05c13a8-21fb-42c9-a5bc-98b7d94f464a', 'a05c13a8', 'theodoracatos', null, GETDATE(), GETDATE(), (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [SocialLink]
VALUES (NEWID(), 1, 'https://www.facebook.com/theodoracatos', 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 2, 'https://twitter.com/theodoracatos', 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 3, 'https://www.linkedin.com/in/theodoracatos', 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 4, 'https://www.xing.com/profile/Alexandros_Theodoracatos/cv', 4, (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [Experience]
VALUES (NEWID(), 'Senior Softwareingenieur', 'Quilvest (Switzerland) Ltd.', 'http://quilvest.com', 'Zürich', 'Architektur und Fullstack-Entwicklung elektronischer Businessprozesse, realisiert mit neuesten .NET Web-Technologien. Technischer Lead folgender eigenentwickelter Applikationssysteme: Intranet, eBanking, MBO-System, Client-OnBoard-Dokumentenerstellung. Projektleitung mit fachlicher Führung von 5 – 7 Mitarbeitern (länderübergreifend). Regelmässige Schulung und Coaching von Mitarbeitern (auf Deutsch und Englisch)', '2011-02-01', null, 1, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 1 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Software Ingenieur', 'Ruf Telematik AG', 'http://ruf.ch', 'Schlieren', 'Architektur, Spezifikation, Design, Implementation, Test und Dokumentation von Softwarekomponenten und Multimediaapplikationen. Projektarbeit: Anforderungsspezifikation, Umsetzung, Engineering, Test und Projektleitung mit direktem Kundenkontakt', '2008-11-01', '2011-01-31', 2, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 2 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Freelancer', 'Ruf Telematik AG', 'http://ruf.ch', 'Schlieren', 'Entwicklung von Tools und Diagnoseprogrammen für Embedded-Geräte', '2005-09-01', '2008-10-01', 3, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 3 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Trainee', 'ABB Schweiz AG', 'http://abb.ch', 'Zürich', 'Entwicklung von Systemtools für elektrische Schaltanlagen (IEC 61850). Entwicklung von diversen Multimediaapplikationen für Firmenpräsentationen (Demos)', '2003-08-01', '2005-08-01', 4, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 4 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [Education]
VALUES (NEWID(), 'ZHAW School of Engineering', 'https://zhaw.ch', 'Winterthur', 'Master of Advances Studies (MAS)', 'Wirtschaftsinformatik',  'Berufsbegleitendes Nachdiplomstudium mit Schwerpunkten in: Betriebswirtschaft, Software Engineering, Projektmanagement und Coaching', '5.4', '2010-02-01', '2011-11-01', 1, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 1 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'ZHAW School of Engineering', 'https://zhaw.ch', 'Winterthur', 'Diplomstudium (Dipl. Ing. FH)', 'Informationstechnologie', 'Vollzeit Diplomstudium mit Schwerpunkten in: Software Architekturen / Modellierung und Softwareentwicklung', '5.3', '2005-09-01', '2008-10-01', 2, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 2 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'GIBB', 'https://gibb.ch', 'Bern', 'Fachausweis (FA)', 'Applikationsentwicklung', 'Die Lehre zum Applikationsentwickler für Maturanden (way-up.ch) bot eine zweijährige Praxiserfahrung in verschiedenen Firmen und ebnete den Weg zur Fachhochschule', '5.5', '2003-08-01', '2005-08-01', 3, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 3 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'Kantonsschule SH', 'https://kanti.ch', 'Schaffhausen', 'Matura', 'Naturwissenschaften (Profil N)', 'Naturwissenschaftlich–mathematischeMatura mit Schwerpunkten in Chemie / Biologie', '4.5', '1998-08-01', '2002-07-01', 4, (SELECT [CountryID] FROM [Country] ORDER BY [CountryID] OFFSET 4 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 4, 1, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 1 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 3, 2, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 2 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 2, 3, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 3 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 1, 4, (SELECT [LanguageID] FROM [Language] ORDER BY [LanguageID] OFFSET 4 ROWS FETCH FIRST 1 ROW ONLY), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Award]
VALUES(NEWID(), 'ZHAW Alumni Award', 'Best award ever from ZHAW', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'www.zhaw.ch', '2012-01-01', 1,  (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Award]
VALUES(NEWID(), 'ZHAW Alumni Award', 'Secend award from ZHAW', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'www.zhaw.ch', '2011-01-01', 2,  (SELECT TOP 1 [PersonID] FROM [Person]))


INSERT INTO [Interest]
VALUES(NEWID(), 'Handball', 'Kadetten Schaffhausen', 'https://kadettensh.ch', 1, (SELECT TOP 1 [PersonID] FROM [Person]))


-------------------

--select * from [Person]
--select * from [About]
--select * from [Vfile]
--select * from [Curriculum]
--select * from [SocialLink]
--select * from [Experience]
--select * from [Education]
--select * from [Language]
--select * from [LanguageSkill]

--delete [About]
--DBCC CHECKIDENT ('[About]', RESEED, 0);
--GO

--delete [Person]
--DBCC CHECKIDENT ('[Person]', RESEED, 0);
--GO

--delete [Curriculum]
--DBCC CHECKIDENT ('[Curriculum]', RESEED, 0);
--GO

--delete [SocialLink]
--DBCC CHECKIDENT ('[Curriculum]', RESEED, 0);
--GO

--------------------------------


COMMIT TRANSACTION -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        RAISERROR (50001,1,1)
        ROLLBACK TRAN --RollBack in case of Error
END CATCH