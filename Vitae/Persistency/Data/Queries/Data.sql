USE Vitae
GO

DECLARE @CurriculumID uniqueidentifier ='4eb60de2-17e3-4281-b42d-26648d3aecbd';

BEGIN TRY
    BEGIN TRANSACTION

INSERT INTO [About]
VALUES(NEWID(), '"Wer hohe Türme bauen will, muss lange beim Fundament verweilen." Aristoteles',  null, null)

select * from PersonalDetail

INSERT INTO [PersonalDetail]
VALUES (NEWID(), 'Alexandros', 'Theodoracatos', '1983-06-23', 1, 'Zwischenbächen', 143, 8048, 'Zürich', 'Zürich', 'theodoracatos@gmail.com', '787044438', 'Zürich ZH, Eschenbach SG', 2, (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), (SELECT TOP 1 [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'))

INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 0)

INSERT INTO [PersonCountry]
VALUES ((SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'gr'), 1)

INSERT INTO [Child]
VALUES(NEWID(), 'Marilena', '2014-08-14', 0, (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]))

INSERT INTO [Child]
VALUES(NEWID(), 'Aris', '2017-08-13', 0, (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]))

INSERT INTO [Person]
VALUES (NEWID(), 'de', (SELECT TOP 1 [PersonalDetailID] FROM [PersonalDetail]), (SELECT TOP 1 [AboutID] FROM [About]))

INSERT INTO [Curriculum]
VALUES (NEWID(), @CurriculumID, @CurriculumID, 'alexis', 'theodoracatos', null, GETDATE(), GETDATE(), (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 1, 'https://www.facebook.com/theodoracatos', 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 2, 'https://twitter.com/theodoracatos', 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 3, 'https://www.linkedin.com/in/theodoracatos', 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 4, 'https://github.com/theodoracatos/vitae', 4, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 5, 'https://www.xing.com/profile/Alexandros_Theodoracatos/cv', 5, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [SocialLink]
VALUES (NEWID(), 7, 'https://stackoverflow.com/users/4369275/grecool', 5, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Dozent', 'IFA Weiterbildungs AG', 'https://www.ifa.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Teilzeit-Dozent auf Mandatsbasis', '2019-10-01', null, 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Senior Softwareingenieur', 'Quilvest (Switzerland) Ltd.', 'http://quilvest.com', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Architektur und Fullstack-Entwicklung elektronischer Businessprozesse, realisiert mit neuesten .NET Web-Technologien. Technischer Lead folgender eigenentwickelter Applikationssysteme: Intranet, eBanking, MBO-System, Client-OnBoard-Dokumentenerstellung. Projektleitung mit fachlicher Führung von 5 – 7 Mitarbeitern (länderübergreifend). Regelmässige Schulung und Coaching von Mitarbeitern (auf Deutsch und Englisch)', '2011-02-01', null, 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Software Ingenieur', 'Ruf Telematik AG', 'http://ruf.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Architektur, Spezifikation, Design, Implementation, Test und Dokumentation von Softwarekomponenten und Multimediaapplikationen. Projektarbeit: Anforderungsspezifikation, Umsetzung, Engineering, Test und Projektleitung mit direktem Kundenkontakt', '2008-11-01', '2011-01-31', 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Freelancer', 'Ruf Telematik AG', 'http://ruf.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schlieren', 'Entwicklung von Tools und Diagnoseprogrammen für Embedded-Geräte', '2005-09-01', '2008-10-01', 4, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Experience]
VALUES (NEWID(), 'Trainee', 'ABB Schweiz AG', 'http://abb.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Zürich', 'Entwicklung von Systemtools für elektrische Schaltanlagen (IEC 61850). Entwicklung von diversen Multimediaapplikationen für Firmenpräsentationen (Demos)', '2003-08-01', '2005-08-01', 5, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Master of Advances Studies (MAS)', 'Wirtschaftsinformatik',  'Berufsbegleitendes Nachdiplomstudium mit Schwerpunkten in: Betriebswirtschaft, Software Engineering, Projektmanagement und Coaching', '5.4', '2010-02-01', '2011-11-01', 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'ZHAW School of Engineering', 'https://zhaw.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Winterthur', 'Diplomstudium (Dipl. Ing. FH)', 'Informationstechnologie', 'Vollzeit Diplomstudium mit Schwerpunkten in: Software Architekturen / Modellierung und Softwareentwicklung', '5.3', '2005-09-01', '2008-10-01', 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'GIBB', 'https://gibb.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Bern', 'Fachausweis (FA)', 'Applikationsentwicklung', 'Die Lehre zum Applikationsentwickler für Maturanden (way-up.ch) bot eine zweijährige Praxiserfahrung in verschiedenen Firmen und ebnete den Weg zur Fachhochschule', '5.5', '2003-08-01', '2005-08-01', 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Education]
VALUES (NEWID(), 'Kantonsschule SH', 'https://kanti.ch', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'ch'), 'Schaffhausen', 'Matura', 'Naturwissenschaften (Profil N)', 'Naturwissenschaftlich–mathematischeMatura mit Schwerpunkten in Chemie / Biologie', '4.5', '1998-08-01', '2002-07-01', 4, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 4, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'de'), 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'en'), 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 3, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'el'), 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [LanguageSkill] 
VALUES(NEWID(), 2, (SELECT [LanguageID] FROM [Language] WHERE [LanguageCode] = 'fr'), 4, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Award]
VALUES(NEWID(), 'ZHAW Alumni Award', 'Bestnote im Studiengang MAS Wirtschaftsinformatik', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'https://www.zhaw.ch', '2012-01-01', 1,  (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Award]
VALUES(NEWID(), 'ZHAW Alumni Award', 'Beste Masterarbeit im Studiengang MAS Wirtschaftsinformatik', 'Zürcher Hochschule für Angewandte Wissenschaften (ZHAW)', 'https://www.zhaw.ch', '2012-01-01', 2,  (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Interest]
VALUES(NEWID(), 'Sport', 'Schwimmen, Tennis und Badminton', null, 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Interest]
VALUES(NEWID(), 'Kochen', 'Beim Kochen kann ich abschalten und stets Neues ausprobieren', null, 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Interest]
VALUES(NEWID(), 'Reisen', 'Reisen, neue Länder und Kulturen haben mich seit jeher fasziniert', null, 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Interest]
VALUES(NEWID(), 'Kinder', 'Meine beiden Kinder halten mich stets auf Trab', null, 4, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Skill]
VALUES (NEWID(), 'Programmiersprachen', 'C#, Java, TypeScript / JavaScript, VB', 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Skill]
VALUES (NEWID(), 'Frameworks', '.NET Framework, .NET Core, Entity Framework, Entity Framework Core, ADO.Net, WinForms', 2, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Skill]
VALUES (NEWID(), 'Webtechnologien', 'ASP.NET Core, ASP.NET Web API, ASP.NET MVC, ASP.NET WebForms, TypeScript, HTML, XML, SASS', 3, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Skill]
VALUES (NEWID(), 'Datenbanktechnologien', 'Microsoft SQL Server, Oracle, T-SQL, PL/SQL', 4, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Skill]
VALUES (NEWID(), 'IDE/Tools', 'Visual Studio, Eclipse, Azure DevOps, Git, Subversion (SVN)', 5, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Skill]
VALUES (NEWID(), 'Projektmethoden', 'Agile, Scrum, RUP', 6, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Skill]
VALUES (NEWID(), 'Entwicklungsmethoden', 'Domain driven Development, Prototyping', 7, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Abroad]
VALUES (NEWID(), (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'sg'), 'Singapore', 'Auslandaufenthalt in Singapur', '2015-01-01', '2018-01-01', 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Reference]
VALUES (NEWID(), 'Wolfgang', 'Schmidt', 1, 'ABB (Schweiz) AG', 'https://abb.ch', 'Ehemaliger Vorgesetzter bei der Ruf Telematik AG', 'wolfgang.schmidt@abb.ch', '78 704 44 38', (SELECT TOP 1 [CountryID] FROM [Country] WHERE [CountryCode] = 'de'), 1, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Certificate]
VALUES (NEWID(), 'Cambridge English Level 2 Certificate in ESOL International', 'Certificate in Advanced English', 'Cambridge Assessment English', 'https://www.cambridgeenglish.org/', '2014-03-01', null, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Certificate]
VALUES (NEWID(), 'Microsoft Certified Professional (MCP)', 'Microsoft Specialist: Programming in C#', 'Microsoft', 'https://www.microsoft.com', '2015-04-13', null, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Certificate]
VALUES (NEWID(), 'Professional Scrum Master I (PSM I)', 'Microsoft Specialist: Programming in C#', 'Scrum.org', 'https://www.scrum.org/certificates/205995', '2016-09-06', null, (SELECT TOP 1 [PersonID] FROM [Person]))

INSERT INTO [Certificate]
VALUES (NEWID(), 'SVEB Kursleiter Stufe 1', 'Lernveranstaltungen mit Erwachsenen durchführen / AdA FA-M 1', 'Klubschule Migros', 'https://alice.ch/de/ausbilden-als-beruf/ada-abschluesse/sveb-zertifikat-kursleiterin/', '2019-05-16', null, (SELECT TOP 1 [PersonID] FROM [Person]))

--------------------------------


COMMIT TRANSACTION -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        RAISERROR (50001,1,1)
        ROLLBACK TRAN --RollBack in case of Error
END CATCH