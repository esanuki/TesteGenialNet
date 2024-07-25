BEGIN TRANSACTION;
GO

CREATE TABLE [Fornecedores] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(200) NOT NULL,
    [CNPJ] varchar(20) NOT NULL,
    [Endereco] varchar(200) NOT NULL,
    [Telefone] varchar(20) NOT NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] varchar(200) NOT NULL,
    [Marca] nvarchar(max) NULL,
    [UnidadeMedida] int NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProdutosFornecedores] (
    [FornecedorId] int NOT NULL,
    [ProdutoId] int NOT NULL,
    [ValorCompra] decimal(10,2) NOT NULL,
    CONSTRAINT [PK_ProdutosFornecedores] PRIMARY KEY ([ProdutoId], [FornecedorId]),
    CONSTRAINT [FK_ProdutosFornecedores_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProdutosFornecedores_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ProdutosFornecedores_FornecedorId] ON [ProdutosFornecedores] ([FornecedorId]);
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

SET IDENTITY_INSERT [dbo].[Fornecedores] ON
INSERT INTO [dbo].[Fornecedores] ([Id], [Nome], [CNPJ], [Endereco], [Telefone]) VALUES (1, N'SuperMercado Bom dia', N'23.712.293/0001-35', N'Rua Corypheu de Azevedo Marques, Vila Santo Antônio, 87030-250, Maringá-PR', N'4430200596')
INSERT INTO [dbo].[Fornecedores] ([Id], [Nome], [CNPJ], [Endereco], [Telefone]) VALUES (2, N'SuperMercado Amigão', N'80.159.538/0001-82', N'Avenida Morangueira, Jardim Alvorada, 87033-070, Maringá-PR', N'4433445710')
SET IDENTITY_INSERT [dbo].[Fornecedores] OFF
GO

SET IDENTITY_INSERT [dbo].[Produtos] ON
INSERT INTO [dbo].[Produtos] ([Id], [Descricao], [Marca], [UnidadeMedida]) VALUES (1, N'Café Tradicional', N'3 Corações', 0)
INSERT INTO [dbo].[Produtos] ([Id], [Descricao], [Marca], [UnidadeMedida]) VALUES (2, N'Feijao Carioca', N'Camil', 1)
INSERT INTO [dbo].[Produtos] ([Id], [Descricao], [Marca], [UnidadeMedida]) VALUES (3, N'Toalha de Banho', N'Karsten', 2)
INSERT INTO [dbo].[Produtos] ([Id], [Descricao], [Marca], [UnidadeMedida]) VALUES (4, N'Café Gourmet', N'Melitta', 0)
INSERT INTO [dbo].[Produtos] ([Id], [Descricao], [Marca], [UnidadeMedida]) VALUES (5, N'Arroz Oriental', N'Prato Fino', 1)
INSERT INTO [dbo].[Produtos] ([Id], [Descricao], [Marca], [UnidadeMedida]) VALUES (6, N'Tecido Linho', N'Armarinhos S.A', 2)
SET IDENTITY_INSERT [dbo].[Produtos] OFF

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [dbo].[ProdutosFornecedores] ([FornecedorId], [ProdutoId], [ValorCompra]) VALUES (1, 1, CAST(21.99 AS Decimal(10, 2)))
INSERT INTO [dbo].[ProdutosFornecedores] ([FornecedorId], [ProdutoId], [ValorCompra]) VALUES (1, 2, CAST(10.95 AS Decimal(10, 2)))
INSERT INTO [dbo].[ProdutosFornecedores] ([FornecedorId], [ProdutoId], [ValorCompra]) VALUES (1, 3, CAST(29.90 AS Decimal(10, 2)))
INSERT INTO [dbo].[ProdutosFornecedores] ([FornecedorId], [ProdutoId], [ValorCompra]) VALUES (2, 4, CAST(34.49 AS Decimal(10, 2)))
INSERT INTO [dbo].[ProdutosFornecedores] ([FornecedorId], [ProdutoId], [ValorCompra]) VALUES (2, 5, CAST(58.90 AS Decimal(10, 2)))
INSERT INTO [dbo].[ProdutosFornecedores] ([FornecedorId], [ProdutoId], [ValorCompra]) VALUES (2, 6, CAST(26.90 AS Decimal(10, 2)))

COMMIT;
GO

