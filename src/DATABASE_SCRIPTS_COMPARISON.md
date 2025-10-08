# Comparação dos Scripts de Banco de Dados

Este documento compara os scripts de criação de banco de dados para MySQL e SQL Server.

## Arquivos

- **MySQL**: `create_database.sql`
- **SQL Server**: `create_database_sqlserver.sql`

## Principais Diferenças

### 1. Criação do Banco de Dados

#### MySQL
```sql
CREATE SCHEMA IF NOT EXISTS `english_now` DEFAULT CHARACTER SET utf8;
```

#### SQL Server
```sql
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EnglishNow')
BEGIN
    CREATE DATABASE [EnglishNow];
END
GO

USE [EnglishNow];
GO
```

### 2. Auto Increment

#### MySQL
```sql
`papel_id` INT NOT NULL AUTO_INCREMENT
```

#### SQL Server
```sql
[papel_id] INT IDENTITY(1,1) NOT NULL
```

### 3. Verificação de Existência de Tabelas

#### MySQL
```sql
CREATE TABLE IF NOT EXISTS `english_now`.`papel` (
```

#### SQL Server
```sql
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='papel' AND xtype='U')
BEGIN
    CREATE TABLE [papel] (
```

### 4. Chaves Primárias

#### MySQL
```sql
PRIMARY KEY (`papel_id`)
```

#### SQL Server
```sql
CONSTRAINT [PK_papel] PRIMARY KEY ([papel_id])
```

### 5. Chaves Estrangeiras

#### MySQL
```sql
CONSTRAINT `usuario_papel`
    FOREIGN KEY (`papel_id`)
    REFERENCES `english_now`.`papel` (`papel_id`)
```

#### SQL Server
```sql
CONSTRAINT [FK_usuario_papel] FOREIGN KEY ([papel_id]) REFERENCES [papel] ([papel_id])
```

### 6. Tipos de Dados

#### MySQL
```sql
VARCHAR(50) NOT NULL
DECIMAL(4,2) NULL
```

#### SQL Server
```sql
NVARCHAR(50) NOT NULL
DECIMAL(4,2) NULL
```

### 7. Inserção de Dados com ID Específico

#### MySQL
```sql
INSERT INTO `english_now`.`papel`
(`papel_id`, `nome`)
VALUES
(1, 'Administrador');
```

#### SQL Server
```sql
SET IDENTITY_INSERT [papel] ON;
GO

INSERT INTO [papel] ([papel_id], [nome])
VALUES (1, 'Administrador');

SET IDENTITY_INSERT [papel] OFF;
GO
```

### 8. Verificação de Dados Existentes

#### MySQL
```sql
-- Não há verificação automática
```

#### SQL Server
```sql
IF NOT EXISTS (SELECT 1 FROM [papel] WHERE [papel_id] = 1)
BEGIN
    INSERT INTO [papel] ([papel_id], [nome])
    VALUES (1, 'Administrador');
END
```

## Características Específicas do SQL Server

### 1. Delimitadores GO
- SQL Server usa `GO` como delimitador de lote
- MySQL não precisa de delimitadores especiais

### 2. Nomes de Objetos
- SQL Server usa colchetes `[nome]` para nomes de objetos
- MySQL usa backticks `` `nome` ``

### 3. Verificação de Existência
- SQL Server usa `sysobjects` para verificar tabelas
- MySQL usa `IF NOT EXISTS` diretamente no CREATE TABLE

### 4. IDENTITY vs AUTO_INCREMENT
- SQL Server: `IDENTITY(1,1)` (início, incremento)
- MySQL: `AUTO_INCREMENT` (sempre inicia em 1, incremento de 1)

### 5. Tipos de Dados Unicode
- SQL Server: `NVARCHAR` para Unicode
- MySQL: `VARCHAR` com `CHARACTER SET utf8`

## Como Executar os Scripts

### MySQL
```bash
mysql -u root -p < create_database.sql
```

### SQL Server
```bash
sqlcmd -S localhost -i create_database_sqlserver.sql
```

Ou usando SQL Server Management Studio:
1. Abra o arquivo `create_database_sqlserver.sql`
2. Execute o script completo

## Estrutura das Tabelas

Ambos os scripts criam as mesmas tabelas:

1. **papel** - Tipos de usuário (Administrador, Professor, Aluno)
2. **usuario** - Usuários do sistema
3. **aluno** - Dados dos alunos
4. **professor** - Dados dos professores
5. **turma** - Turmas/classes
6. **aluno_turma_boletim** - Boletins dos alunos

## Dados Iniciais

Ambos os scripts inserem os mesmos dados iniciais:

- **Papéis**: Administrador (1), Professor (2), Aluno (3)
- **Usuário**: admin/123 com papel de Administrador

## Compatibilidade

- ✅ **Estrutura**: Idêntica em ambos os bancos
- ✅ **Relacionamentos**: Mesmos relacionamentos e constraints
- ✅ **Dados**: Mesmos dados iniciais
- ✅ **Funcionalidade**: Ambos suportam todas as operações da aplicação
