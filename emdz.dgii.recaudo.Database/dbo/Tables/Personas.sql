CREATE TABLE [dbo].[Personas] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [IdTipoDocumento] INT           NOT NULL,
    [NumeroDocumento] NVARCHAR (25) NOT NULL,
    [PrimerNombre]    NVARCHAR (50) NOT NULL,
    [SegundoNombre]   NVARCHAR (50) NULL,
    [PrimerApellido]  NVARCHAR (50) NOT NULL,
    [SegundoApellido] NVARCHAR (50) NULL,
    [Estado]          BIT           NOT NULL,
    CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Personas_TiposDocumentos] FOREIGN KEY ([IdTipoDocumento]) REFERENCES [dbo].[TiposDocumentos] ([Id]),
    CONSTRAINT [IX_Personas] UNIQUE NONCLUSTERED ([IdTipoDocumento] ASC, [NumeroDocumento] ASC)
);

