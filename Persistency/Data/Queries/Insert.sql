INSERT INTO [About]
VALUES(1234, 'Wer nichts wagt der nichts gewinnt', null)

INSERT INTO [Person]
VALUES ('Alexandros', 'Theodoracatos', '1983-06-23', 1, 'Zwischenbächen', 143, 8048, 'Zurich', 'theodoracatos@gmail.com', 1)

INSERT INTO [Curriculum]
VALUES (NEWID(), 'theodoracatos', null, GETDATE(), GETDATE(), 1)


delete ABout
DBCC CHECKIDENT ('[About]', RESEED, 0);
GO

select * from person
select * from About
select * from Curriculum