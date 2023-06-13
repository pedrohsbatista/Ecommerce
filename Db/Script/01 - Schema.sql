CREATE TABLE [dbo].[Usuario] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Nome] VARCHAR(70) NOT NULL,
	[Email] VARCHAR(100) NOT NULL,
	[Sexo] CHAR(1) NULL,
	[RG] VARCHAR(15) NULL,
	[CPF] CHAR(14) NULL,
	[NomeMae] VARCHAR(70) NULL,
	[SituacaoCadastro] CHAR(1) NOT NULL,
	[DataCadastro] DATETIMEOFFSET NOT NULL,

	CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([Id] ASC)
);

/*One-To-One*/
CREATE TABLE [dbo].[Contato] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UsuarioId] INT NOT NULL,
	[Telefone] VARCHAR(15) NULL,
	[Celular] VARCHAR(15) NULL,

	CONSTRAINT [PK_Contato] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Contato_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuario] ([Id]) ON DELETE CASCADE
);

/*One-To-Many*/
CREATE TABLE [dbo].[EnderecoEntrega] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UsuarioId] INT NOT NULL,
	[NomeEndereco] VARCHAR(100) NOT NULL,
	[CEP] CHAR(10) NOT NULL,
	[Estado] CHAR(2) NOT NULL,
	[Cidade] VARCHAR(120) NOT NULL,
	[Bairro] VARCHAR(200) NOT NULL,
	[Endereco] VARCHAR(200) NOT NULL,
	[Numero] VARCHAR(20) NULL,
	[Complemento] VARCHAR(30) NULL,
	
	CONSTRAINT [PK_EnderecoEntrega] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_EnderecoEntrega_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuario] ([Id]) ON DELETE CASCADE

);

/*Many-To-Many*/
CREATE TABLE [dbo].[Departamento] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Nome] VARCHAR(100) NOT NULL,
	CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[UsuarioDepartamento] (
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UsuarioId] INT NOT NULL,
	[DepartamentoId] INT NOT NULL,

	CONSTRAINT [PK_UsuarioDepartamento] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UsuarioDepartamento_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuario] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_UsuarioDepartamento_Departamento] FOREIGN KEY ([DepartamentoId]) REFERENCES [dbo].[Departamento] ([Id]) ON DELETE CASCADE
);
go


-- Store Procedures na tabela de Usuarios
CREATE PROCEDURE dbo.GetAllUsuario
AS
    SELECT * FROM [dbo].[Usuario]
go

CREATE PROCEDURE dbo.GetByIdUsuario
(
	@id int
)
AS
    SELECT * FROM [dbo].[Usuario] WHERE Id = @id
go

CREATE PROCEDURE dbo.InsertUsuario
(
	@nome varchar(70),
	@email varchar(100),
	@sexo char(1),
	@rg varchar(15),
	@cpf char(14),
	@nomeMae varchar(70),
	@situacaoCadastro char(1),
	@dataCadastro datetimeoffset
)
AS
	INSERT INTO [dbo].[Usuario] (Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) VALUES
	(@nome, @email, @sexo, @rg, @cpf, @nomeMae, @situacaoCadastro, @dataCadastro); SELECT CAST(scope_identity() AS int)
go



CREATE PROCEDURE dbo.UpdateUsuario 
(
	@id int,
	@nome varchar(70),
	@email varchar(100),
	@sexo char(1),
	@rg varchar(15),
	@cpf char(14),
	@nomeMae varchar(70),
	@situacaoCadastro char(1),
	@dataCadastro datetimeoffset
)
AS
	UPDATE [dbo].[Usuario] SET 
	Nome = @nome,
	Email = @email,
	Sexo = @sexo,
	RG = @rg,
	CPF = @cpf,
	NomeMae = @nomeMae,
	SituacaoCadastro = @situacaoCadastro, 
	DataCadastro = @dataCadastro
	WHERE Id = @id
go

CREATE PROCEDURE dbo.DeleteUsuario
(
	@id int
)
AS
	DELETE FROM [dbo].[Usuario] WHERE Id = @id
go
