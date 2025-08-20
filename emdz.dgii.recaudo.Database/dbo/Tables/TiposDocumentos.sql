CREATE TABLE [dbo].[TiposDocumentos] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Codigo]      NVARCHAR (50)  NOT NULL,
    [Descripcion] NVARCHAR (250) NOT NULL,
    [Estado]      BIT            NOT NULL,
    CONSTRAINT [PK_TiposDocumentos_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_TiposDocumentos_Codigo] UNIQUE NONCLUSTERED ([Codigo] ASC)
);

