# Back-end pronto - Sistema de Gestão Financeira

Estrutura completa com **Solution (.sln)**, organizada em camadas e já preparada para conectar com o front React enviado junto.

## O que está incluído
- Solution `Financeiro.sln`
- API .NET 8 com Swagger, JWT e CORS
- Camadas `API`, `Application`, `Domain` e `Infrastructure`
- EF Core + MySQL
- Login real usando tabela `usuarios`
- CRUD de transações
- Script SQL para criação do banco

## Como rodar

### Banco de dados
Execute o arquivo `database/financeiro_db.sql` em um MySQL 8.

### API
```bash
dotnet restore Financeiro.sln
dotnet build Financeiro.sln
dotnet run --project src/Financeiro.API/Financeiro.API.csproj
```

### Front
O front consome por padrão a URL:
```bash
http://localhost:5190/api
```

## Credenciais padrão
```json
{
  "email": "admin@massinilabs.local",
  "password": "Admin@123"
}
```
