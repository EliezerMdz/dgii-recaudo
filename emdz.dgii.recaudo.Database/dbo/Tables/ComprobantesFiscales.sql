CREATE TABLE [dbo].[ComprobantesFiscales] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [IdContribuyente] INT           NOT NULL,
    [NCF]             NVARCHAR (50) NOT NULL,
    [Monto]           MONEY         NOT NULL,
    [ITBIS]           BIT           NOT NULL,
    CONSTRAINT [PK_ComprobantesFiscales] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ComprobantesFiscales_Contribuyentes] FOREIGN KEY ([IdContribuyente]) REFERENCES [dbo].[Contribuyentes] ([Id]),
    CONSTRAINT [IX_ComprobantesFiscales] UNIQUE NONCLUSTERED ([NCF] ASC)
);

