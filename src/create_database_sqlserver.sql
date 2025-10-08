-- Criação do banco de dados EnglishNow para SQL Server
-- Este script cria as tabelas e insere os registros iniciais

-- Criar o banco de dados se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EnglishNow')
BEGIN
    CREATE DATABASE [EnglishNow];
END
GO

-- Usar o banco de dados
USE [EnglishNow];
GO

-- Criar tabela papel
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='papel' AND xtype='U')
BEGIN
    CREATE TABLE [papel] (
        [papel_id] INT IDENTITY(1,1) NOT NULL,
        [nome] NVARCHAR(50) NOT NULL,
        CONSTRAINT [PK_papel] PRIMARY KEY ([papel_id])
    );
END
GO

-- Criar tabela usuario
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='usuario' AND xtype='U')
BEGIN
    CREATE TABLE [usuario] (
        [usuario_id] INT IDENTITY(1,1) NOT NULL,
        [login] NVARCHAR(50) NOT NULL,
        [senha] NVARCHAR(50) NOT NULL,
        [papel_id] INT NOT NULL,
        CONSTRAINT [PK_usuario] PRIMARY KEY ([usuario_id]),
        CONSTRAINT [FK_usuario_papel] FOREIGN KEY ([papel_id]) REFERENCES [papel] ([papel_id])
    );
END
GO

-- Criar tabela aluno
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aluno' AND xtype='U')
BEGIN
    CREATE TABLE [aluno] (
        [aluno_id] INT IDENTITY(1,1) NOT NULL,
        [nome] NVARCHAR(200) NOT NULL,
        [email] NVARCHAR(45) NOT NULL,
        [usuario_id] INT NOT NULL,
        CONSTRAINT [PK_aluno] PRIMARY KEY ([aluno_id]),
        CONSTRAINT [FK_aluno_usuario] FOREIGN KEY ([usuario_id]) REFERENCES [usuario] ([usuario_id])
    );
END
GO

-- Criar tabela professor
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='professor' AND xtype='U')
BEGIN
    CREATE TABLE [professor] (
        [professor_id] INT IDENTITY(1,1) NOT NULL,
        [nome] NVARCHAR(200) NOT NULL,
        [email] NVARCHAR(45) NOT NULL,
        [usuario_id] INT NOT NULL,
        CONSTRAINT [PK_professor] PRIMARY KEY ([professor_id]),
        CONSTRAINT [FK_professor_usuario] FOREIGN KEY ([usuario_id]) REFERENCES [usuario] ([usuario_id])
    );
END
GO

-- Criar tabela turma
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='turma' AND xtype='U')
BEGIN
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
END
GO

-- Criar tabela aluno_turma_boletim
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aluno_turma_boletim' AND xtype='U')
BEGIN
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
END
GO

-- Inserir dados iniciais na tabela papel
IF NOT EXISTS (SELECT 1 FROM [papel] WHERE [papel_id] = 1)
BEGIN
    INSERT INTO [papel] ([papel_id], [nome])
    VALUES (1, 'Administrador');
END
GO

IF NOT EXISTS (SELECT 1 FROM [papel] WHERE [papel_id] = 2)
BEGIN
    INSERT INTO [papel] ([papel_id], [nome])
    VALUES (2, 'Professor');
END
GO

IF NOT EXISTS (SELECT 1 FROM [papel] WHERE [papel_id] = 3)
BEGIN
    INSERT INTO [papel] ([papel_id], [nome])
    VALUES (3, 'Aluno');
END
GO

-- Inserir dados iniciais na tabela usuario
IF NOT EXISTS (SELECT 1 FROM [usuario] WHERE [usuario_id] = 1)
BEGIN
    INSERT INTO [usuario] ([usuario_id], [login], [senha], [papel_id])
    VALUES (1, 'admin', '123', 1);
END
GO

-- Habilitar IDENTITY_INSERT para permitir inserção manual de IDs
SET IDENTITY_INSERT [papel] ON;
GO

-- Verificar se os dados já existem antes de inserir
IF NOT EXISTS (SELECT 1 FROM [papel] WHERE [papel_id] = 1)
BEGIN
    INSERT INTO [papel] ([papel_id], [nome])
    VALUES (1, 'Administrador');
END

IF NOT EXISTS (SELECT 1 FROM [papel] WHERE [papel_id] = 2)
BEGIN
    INSERT INTO [papel] ([papel_id], [nome])
    VALUES (2, 'Professor');
END

IF NOT EXISTS (SELECT 1 FROM [papel] WHERE [papel_id] = 3)
BEGIN
    INSERT INTO [papel] ([papel_id], [nome])
    VALUES (3, 'Aluno');
END
GO

SET IDENTITY_INSERT [papel] OFF;
GO

SET IDENTITY_INSERT [usuario] ON;
GO

IF NOT EXISTS (SELECT 1 FROM [usuario] WHERE [usuario_id] = 1)
BEGIN
    INSERT INTO [usuario] ([usuario_id], [login], [senha], [papel_id])
    VALUES (1, 'admin', '123', 1);
END
GO

SET IDENTITY_INSERT [usuario] OFF;
GO

-- Verificar se as tabelas foram criadas corretamente
SELECT 'Tabelas criadas com sucesso!' AS Status;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;
GO
