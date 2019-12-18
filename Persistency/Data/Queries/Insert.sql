INSERT INTO [About]
VALUES(1234, '"Great things in business are never done by one person. They’re done by a team of people." Steve Jobs', null)

INSERT INTO [Person]
VALUES ('Alexandros', 'Theodoracatos', '1983-06-23', 1, 'Zwischenbächen', 143, 8048, 'Zurich', 'theodoracatos@gmail.com', '+41 78 704 44 38', 1)

INSERT INTO [Curriculum]
VALUES ('a05c13a8-21fb-42c9-a5bc-98b7d94f464a', 'theodoracatos', null, GETDATE(), GETDATE(), 1)




select * from [Person]
select * from [About]
select * from [Curriculum]

delete [About]
DBCC CHECKIDENT ('[About]', RESEED, 0);
GO

delete [Person]
DBCC CHECKIDENT ('[Person]', RESEED, 0);
GO

delete [Curriculum]
DBCC CHECKIDENT ('[Curriculum]', RESEED, 0);
GO