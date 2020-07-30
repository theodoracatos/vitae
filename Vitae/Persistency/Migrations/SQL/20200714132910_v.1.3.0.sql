ALTER TABLE [About] DROP CONSTRAINT [FK_About_Vfile_VfileID];

GO

DROP INDEX [IX_About_VfileID] ON [About];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[About]') AND [c].[name] = N'VfileID');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [About] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [About] DROP COLUMN [VfileID];

GO

ALTER TABLE [Vfile] ADD [AboutID] bigint NULL;

GO

ALTER TABLE [Publication] ADD [EnableCVDownload] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Publication] ADD [EnableDocumentsDownload] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

CREATE UNIQUE INDEX [IX_Vfile_AboutID] ON [Vfile] ([AboutID]) WHERE [AboutID] IS NOT NULL;

GO

ALTER TABLE [Vfile] ADD CONSTRAINT [FK_Vfile_About_AboutID] FOREIGN KEY ([AboutID]) REFERENCES [About] ([AboutID]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200714132910_v.1.3.0', N'3.1.5');

GO

