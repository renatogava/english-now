# Configuração de Secrets

Este arquivo contém os comandos para configurar os secrets necessários para o projeto.

## Configuração Inicial

### 1. Inicializar o sistema de secrets (se necessário):
```bash
dotnet user-secrets init
```

### 2. Configurar connection strings:

#### Para MySQL:
```bash
dotnet user-secrets set "ConnectionStrings:MySqlConnection" "Server=localhost;Database=englishnow;Uid=root;Pwd=SUA_SENHA_AQUI;"
```

#### Para SQL Server:
```bash
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow;Trusted_Connection=true;TrustServerCertificate=true;"
```

#### Para SQL Server com autenticação SQL:
```bash
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow;User Id=sa;Password=SUA_SENHA_AQUI;TrustServerCertificate=true;"
```

## Exemplos de Connection Strings

### MySQL - Desenvolvimento Local
```bash
dotnet user-secrets set "ConnectionStrings:MySqlConnection" "Server=localhost;Database=englishnow_dev;Uid=root;Pwd=;"
```

### MySQL - Com senha
```bash
dotnet user-secrets set "ConnectionStrings:MySqlConnection" "Server=localhost;Database=englishnow;Uid=root;Pwd=minhasenha123;"
```

### SQL Server - Windows Authentication
```bash
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow;Trusted_Connection=true;TrustServerCertificate=true;"
```

### SQL Server - SQL Authentication
```bash
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=EnglishNow;User Id=sa;Password=minhasenha123;TrustServerCertificate=true;"
```

### SQL Server - Instância nomeada
```bash
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost\\SQLEXPRESS;Database=EnglishNow;Trusted_Connection=true;TrustServerCertificate=true;"
```

## Verificação

### Listar todos os secrets:
```bash
dotnet user-secrets list
```

### Verificar um secret específico:
```bash
dotnet user-secrets list | findstr "MySqlConnection"
dotnet user-secrets list | findstr "SqlServerConnection"
```

## Limpeza

### Remover um secret específico:
```bash
dotnet user-secrets remove "ConnectionStrings:MySqlConnection"
dotnet user-secrets remove "ConnectionStrings:SqlServerConnection"
```

### Limpar todos os secrets:
```bash
dotnet user-secrets clear
```

## Configuração por Ambiente

### Desenvolvimento
Use os comandos acima com connection strings de desenvolvimento.

### Produção
Em produção, use variáveis de ambiente ou Azure Key Vault:

```bash
# Variáveis de ambiente
export ConnectionStrings__MySqlConnection="Server=prod-mysql;Database=englishnow_prod;Uid=app_user;Pwd=senha_segura;"
export ConnectionStrings__SqlServerConnection="Server=prod-sqlserver;Database=EnglishNow_Prod;User Id=app_user;Password=senha_segura;TrustServerCertificate=true;"
```

## Troubleshooting

### Erro: "No user secrets ID found"
```bash
dotnet user-secrets init
```

### Erro: "Connection string not found"
Verifique se o secret foi configurado corretamente:
```bash
dotnet user-secrets list
```

### Erro de conexão
Verifique se:
1. O servidor de banco está rodando
2. A connection string está correta
3. As credenciais estão corretas
4. O banco de dados existe
