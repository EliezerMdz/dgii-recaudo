CREATE TABLE [dbo].[Entidades] (
    [Id]     INT            NOT NULL,
    [Rnc]    NVARCHAR (25)  NOT NULL,
    [Nombre] NVARCHAR (100) NOT NULL,
    [Estado] BIT            NOT NULL,
    CONSTRAINT [PK_Entidades] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Entidades] UNIQUE NONCLUSTERED ([Rnc] ASC)
);

