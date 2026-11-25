# Controle Financeiro Simples - Back

Sistema web simples inspirado na planilha mensal: períodos de pagamento, salários, cartões e contas fixas.

## Rodar
```bash
dotnet restore ControleFinanceiroSimplificado.sln
dotnet build ControleFinanceiroSimplificado.sln
dotnet run --project src/Financeiro.API/Financeiro.API.csproj
```

## Banco
Para uma instalação nova, execute `database/controle_financeiro_simples.sql`. Para adicionar investimentos a um banco existente sem recriar as tabelas atuais, execute `database/20261124_investimentos.sql`.

## Investimentos
As rotas abaixo exigem autenticação Bearer. O script de atualização do banco cria as tabelas `investimentos` e `atualizacoes_investimento`.

- `GET /api/investimentos` retorna o envelope `{ "success": true, "data": [...] }`.
- `POST /api/investimentos` cadastra um ativo com `nome`, `tipo`, `instituicao`, `valorInvestido`, `saldoAtual` e `metaValor` (opcional).
- `PATCH /api/investimentos/{id}` atualiza parcialmente esses campos.
- `DELETE /api/investimentos/{id}` remove o ativo e seu histórico de atualizações.
- `POST /api/investimentos/{id}/atualizacoes` recebe `saldo` (novo saldo total), `data` (opcional; padrão UTC atual) e `observacao` (opcional), atualiza o saldo do ativo e registra o histórico.

A listagem inclui `atualizacoes` por ativo. Projeções e resumo da carteira são derivados no frontend; projeções são estimativas e não garantem retorno.
