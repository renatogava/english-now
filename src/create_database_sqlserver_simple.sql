-- Script simples para criação do banco EnglishNow no SQL Server
-- Use este script se quiser uma versão mais direta

-- Criar banco de dados
CREATE DATABASE [EnglishNow];
GO

USE [EnglishNow];
GO

-- Criar tabela papel
CREATE TABLE [papel] (
    [papel_id] INT IDENTITY(1,1) NOT NULL,
    [nome] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_papel] PRIMARY KEY ([papel_id])
);
GO

-- Criar tabela usuario
CREATE TABLE [usuario] (
    [usuario_id] INT IDENTITY(1,1) NOT NULL,
    [login] NVARCHAR(50) NOT NULL,
    [senha] NVARCHAR(50) NOT NULL,
    [papel_id] INT NOT NULL,
    CONSTRAINT [PK_usuario] PRIMARY KEY ([usuario_id]),
    CONSTRAINT [FK_usuario_papel] FOREIGN KEY ([papel_id]) REFERENCES [papel] ([papel_id])
);
GO

-- Criar tabela aluno
CREATE TABLE [aluno] (
    [aluno_id] INT IDENTITY(1,1) NOT NULL,
    [nome] NVARCHAR(200) NOT NULL,
    [email] NVARCHAR(45) NOT NULL,
    [usuario_id] INT NOT NULL,
    CONSTRAINT [PK_aluno] PRIMARY KEY ([aluno_id]),
    CONSTRAINT [FK_aluno_usuario] FOREIGN KEY ([usuario_id]) REFERENCES [usuario] ([usuario_id])
);
GO

-- Criar tabela professor
CREATE TABLE [professor] (
    [professor_id] INT IDENTITY(1,1) NOT NULL,
    [nome] NVARCHAR(200) NOT NULL,
    [email] NVARCHAR(45) NOT NULL,
    [usuario_id] INT NOT NULL,
    CONSTRAINT [PK_professor] PRIMARY KEY ([professor_id]),
    CONSTRAINT [FK_professor_usuario] FOREIGN KEY ([usuario_id]) REFERENCES [usuario] ([usuario_id])
);
GO

-- Criar tabela turma
CREATE TABLE [turma] (
    [turma_id] INT IDENTITY(1,1) NOT NULL,
    [semestre] INT NOT NULL,
    [ano] INT NOT NULL,
    [periodo] NVARCHAR(150) NOT NULL,
    [nivel] NVARCHAR(30) NOT NULL,
    [professor_id] INT NOT NULL,
    CONSTRAINT [PK_turma] PRIMARY KEY ([turma_id]),
    CONSTRAINT [FK_turma_professor] FOREIGN KEY ([professor_id]) REFERENCES [professor] ([professor_id])
);
GO

-- Criar tabela aluno_turma_boletim
CREATE TABLE [aluno_turma_boletim] (
    [aluno_turma_boletim_id] INT IDENTITY(1,1) NOT NULL,
    [nota_bim1_escrita] DECIMAL(4,2) NULL,
    [nota_bim1_leitura] DECIMAL(4,2) NULL,
    [nota_bim1_conversacao] DECIMAL(4,2) NULL,
    [nota_bim1_final] DECIMAL(4,2) NULL,
    [nota_bim2_leitura] DECIMAL(4,2) NULL,
    [nota_bim2_escrita] DECIMAL(4,2) NULL,
    [nota_bim2_conversacao] DECIMAL(4,2) NULL,
    [nota_bim2_final] DECIMAL(4,2) NULL,
    [nota_final_semestre] DECIMAL(4,2) NULL,
    [faltas_semestre] INT NULL,
    [aluno_id] INT NOT NULL,
    [turma_id] INT NOT NULL,
    CONSTRAINT [PK_aluno_turma_boletim] PRIMARY KEY ([aluno_turma_boletim_id]),
    CONSTRAINT [FK_aluno_boletim] FOREIGN KEY ([aluno_id]) REFERENCES [aluno] ([aluno_id]),
    CONSTRAINT [FK_turma_boletim] FOREIGN KEY ([turma_id]) REFERENCES [turma] ([turma_id])
);
GO

-- Inserir dados iniciais
-- Habilitar inserção manual de IDs
SET IDENTITY_INSERT [papel] ON;
INSERT INTO [papel] ([papel_id], [nome]) VALUES (1, 'Administrador');
INSERT INTO [papel] ([papel_id], [nome]) VALUES (2, 'Professor');
INSERT INTO [papel] ([papel_id], [nome]) VALUES (3, 'Aluno');
SET IDENTITY_INSERT [papel] OFF;
GO

SET IDENTITY_INSERT [usuario] ON;
INSERT INTO [usuario] ([usuario_id], [login], [senha], [papel_id]) VALUES (1, 'admin', '123', 1);
SET IDENTITY_INSERT [usuario] OFF;
GO

-- Verificar criação
SELECT 'Banco de dados EnglishNow criado com sucesso!' AS Status;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;
GO
