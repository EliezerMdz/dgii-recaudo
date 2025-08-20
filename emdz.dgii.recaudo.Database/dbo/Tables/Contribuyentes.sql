CREATE TABLE [dbo].[Contribuyentes] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [IdTipoContribuyente] INT           NOT NULL,
    [IdTipoDocumento]     INT           NOT NULL,
    [NumeroDocumento]     NVARCHAR (25) NOT NULL,
    [Estado]              BIT           NOT NULL,
    CONSTRAINT [PK_Contribuyentes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contribuyentes_TiposContribuyentes] FOREIGN KEY ([IdTipoContribuyente]) REFERENCES [dbo].[TiposContribuyentes] ([Id]),
    CONSTRAINT [FK_Contribuyentes_TiposDocumentos] FOREIGN KEY ([IdTipoDocumento]) REFERENCES [dbo].[TiposDocumentos] ([Id]),
    CONSTRAINT [IX_Contribuyentes] UNIQUE NONCLUSTERED ([IdTipoDocumento] ASC, [NumeroDocumento] ASC)
);

