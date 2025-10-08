# Configuração de Banco de Dados

Este projeto suporta dois provedores de banco de dados: **MySQL** e **SQL Server**.

## Configuração

### 1. Arquivo appsettings.json

Adicione a seguinte configuração no seu `appsettings.json`:

```json
{
  "DatabaseSettings": {
    "Provider": "mysql"
  }
}
```

### 2. Secrets (Connection Strings)

As connection strings são armazenadas de forma segura nos secrets do .NET:

```bash
# Para MySQL
dotnet user-secrets set "ConnectionStrings:MySqlConnection" "Server=localhost;Database=englishnow;Uid=root;Pwd=senha123;"

# Para SQL Server
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow;Trusted_Connection=true;TrustServerCertificate=true;"
```

### 3. Parâmetros de Configuração

- **Provider**: Define qual banco de dados será usado
  - `"mysql"` - Usa MySQL
  - `"sqlserver"` - Usa SQL Server

- **Secrets**: Contém as strings de conexão seguras
  - **MySqlConnection**: String de conexão para MySQL
  - **SqlServerConnection**: String de conexão para SQL Server

## Como Alterar o Banco de Dados

### Para usar MySQL:
```json
{
  "DatabaseSettings": {
    "Provider": "mysql"
  }
}
```

### Para usar SQL Server:
```json
{
  "DatabaseSettings": {
    "Provider": "sqlserver"
  }
}
```

## Exemplos de Connection Strings

### MySQL
```bash
dotnet user-secrets set "ConnectionStrings:MySqlConnection" "Server=localhost;Database=englishnow;Uid=root;Pwd=senha123;"
```

### SQL Server
```bash
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow;User Id=sa;Password=senha123;TrustServerCertificate=true;"
```

### SQL Server com Windows Authentication
```bash
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow;Trusted_Connection=true;TrustServerCertificate=true;"
```

## Ambientes

### Desenvolvimento
```json
{
  "DatabaseSettings": {
    "Provider": "mysql"
  }
}
```

**Secrets para desenvolvimento:**
```bash
dotnet user-secrets set "ConnectionStrings:MySqlConnection" "Server=localhost;Database=englishnow_dev;Uid=root;Pwd=;"
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow_Dev;Trusted_Connection=true;TrustServerCertificate=true;"
```

### Produção
```json
{
  "DatabaseSettings": {
    "Provider": "sqlserver"
  }
}
```

**Secrets para produção (usar variáveis de ambiente ou Azure Key Vault):**
```bash
# Em produção, use variáveis de ambiente ou Azure Key Vault
export ConnectionStrings__MySqlConnection="Server=prod-mysql;Database=englishnow_prod;Uid=app_user;Pwd=senha_segura;"
export ConnectionStrings__SqlServerConnection="Server=prod-sqlserver;Database=EnglishNow_Prod;User Id=app_user;Password=senha_segura;TrustServerCertificate=true;"
```

## Como Funciona

O sistema automaticamente:

1. Lê a configuração `DatabaseSettings.Provider` do `appsettings.json`
2. Busca a connection string apropriada nos secrets
3. Registra os repositórios corretos no container de DI:
   - **MySQL**: Usa `UsuarioRepository`, `ProfessorRepository`, etc.
   - **SQL Server**: Usa `UsuarioRepositorySqlServer`, `ProfessorRepositorySqlServer`, etc.

## Gerenciamento de Secrets

### Visualizar secrets configurados:
```bash
dotnet user-secrets list
```

### Remover um secret:
```bash
dotnet user-secrets remove "ConnectionStrings:MySqlConnection"
```

### Limpar todos os secrets:
```bash
dotnet user-secrets clear
```

### Inicializar secrets (se necessário):
```bash
dotnet user-secrets init
```

## Repositórios Disponíveis

### MySQL
- `UsuarioRepository`
- `ProfessorRepository`
- `AlunoRepository`
- `TurmaRepository`
- `AlunoTurmaBoletimRepository`

### SQL Server
- `UsuarioRepositorySqlServer`
- `ProfessorRepositorySqlServer`
- `AlunoRepositorySqlServer`
- `TurmaRepositorySqlServer`
- `AlunoTurmaBoletimRepositorySqlServer`

## Scripts de Criação do Banco

### MySQL
Use o arquivo `create_database.sql` para criar o banco MySQL:
```bash
mysql -u root -p < create_database.sql
```

### SQL Server
Use um dos arquivos SQL Server:

#### Versão Completa (com verificações)
```bash
sqlcmd -S localhost -i create_database_sqlserver.sql
```

#### Versão Simples
```bash
sqlcmd -S localhost -i create_database_sqlserver_simple.sql
```

### Estrutura das Tabelas
Ambos os scripts criam as mesmas tabelas:
- `papel` - Tipos de usuário (Administrador, Professor, Aluno)
- `usuario` - Usuários do sistema
- `aluno` - Dados dos alunos
- `professor` - Dados dos professores
- `turma` - Turmas/classes
- `aluno_turma_boletim` - Boletins dos alunos

### Dados Iniciais
- **Usuário admin**: login: `admin`, senha: `123`, papel: Administrador
- **Papéis**: Administrador (1), Professor (2), Aluno (3)

## Migração entre Bancos

Para migrar de um banco para outro:

1. Execute o script apropriado para criar o banco de destino
2. Altere o `Provider` no `appsettings.json`
3. Configure a connection string apropriada nos secrets
4. Reinicie a aplicação

**Nota**: Certifique-se de que o banco de dados de destino tenha a mesma estrutura de tabelas.
