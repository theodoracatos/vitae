/* backup life database(s) to localhost */
BACKUP DATABASE [Vitae] TO  DISK = N'C:\Projects\Vitae\Backup\Vitae.bak' WITH NOFORMAT, INIT,  NAME = N'Vitae-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO