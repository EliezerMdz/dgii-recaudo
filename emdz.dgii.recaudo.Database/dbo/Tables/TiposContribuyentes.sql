CREATE TABLE [dbo].[TiposContribuyentes] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Codigo]      NVARCHAR (50)  NOT NULL,
    [Descripcion] NVARCHAR (250) NOT NULL,
    [Estado]      BIT            NOT NULL,
    CONSTRAINT [PK_TiposContribuyentes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_TiposContribuyentes] UNIQUE NONCLUSTERED ([Codigo] ASC)
);

