USE [Vitae]
GO

ALTER DATABASE [Vitae] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
RESTORE DATABASE [Vitae] 
	FROM  DISK = N'C:\Projects\Vitae\Backup\Vitae.bak' WITH  FILE = 1,  
	MOVE N'Vitae' 
	TO N'C:\Users\theod\Vitae.mdf',  
	MOVE N'Vitae_log' 
	TO N'C:\Projects\Vitae\Backup\Vitae.ldf',  
	NOUNLOAD,  REPLACE,  STATS = 10
GO