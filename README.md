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

## Deploy automático no servidor

O workflow `.github/workflows/deploy.yml` constrói a API em cada `push` para `main`, publica `renanmassini/financas-api:latest` e uma tag pelo SHA do commit no Docker Hub e, depois, conecta por SSH para atualizar o container. A rota `/health` é verificada antes de considerar o deploy bem-sucedido; se falhar, tenta restaurar a imagem anterior.

### GitHub Actions

O job usa o Environment `FinanceiroBack`. Em **Settings → Environments → FinanceiroBack**, cadastre os seguintes **Environment secrets**. Secrets de repositório também funcionam, se você já os usa:

| Secret | Valor |
| --- | --- |
| `DOCKERHUB_USERNAME` | `renanmassini` |
| `DOCKERHUB_TOKEN` | Access Token Docker Hub com permissão `Read & Write` |
| `DEPLOY_HOST` | IP público ou hostname do servidor |
| `DEPLOY_USER` | `deploy` |
| `DEPLOY_SSH_KEY` | Chave privada autorizada para o usuário `deploy` |
| `DEPLOY_SSH_PORT` | Porta SSH, geralmente `22` |
| `DEPLOY_KNOWN_HOSTS` | Chave do host SSH previamente validada |

Você pode reutilizar os mesmos valores de usuário/chave/host SSH e token Docker Hub usados no workflow do frontend. Como este workflow usa o Environment `FinanceiroBack`, copie/cadastre os respectivos secrets também nesse Environment, a menos que os tenha cadastrado como Repository secrets. Não salve credenciais no repositório.

Em **Settings → Secrets and variables → Actions → Variables**, configure:

| Variable | Valor | Padrão |
| --- | --- | --- |
| `DOCKER_PLATFORM` | Arquitetura do servidor | `linux/amd64` |
| `DEPLOY_DOCKER_NETWORK` | Rede Docker compartilhada com o banco, se aplicável | vazio |

Neste servidor, o workflow atualiza o container existente `financas_api`, publicado na porta `127.0.0.1:5010` no host e na porta `8080` dentro do container. O proxy reverso deve encaminhar as requisições `/api` para essa porta e manter o caminho da URL.

### Configuração de produção no servidor

Antes do primeiro deploy, crie `/home/deploy/financas-api.env` no servidor com a **mesma conexão de banco e os mesmos parâmetros JWT atualmente usados em produção**:

```dotenv
ConnectionStrings__DefaultConnection=Server=HOST_DO_BANCO;Port=3306;Database=NOME_DO_BANCO;User=USUARIO;Password=SENHA;
JwtSettings__Key=CHAVE_JWT_ATUAL
JwtSettings__Issuer=ISSUER_JWT_ATUAL
JwtSettings__Audience=AUDIENCE_JWT_ATUAL
JwtSettings__ExpirationInMinutes=180
```

Edite como `root` ou com `sudo`, depois proteja o arquivo para que somente `deploy` possa lê-lo:

```bash
sudo chown deploy:deploy /home/deploy/financas-api.env
sudo chmod 600 /home/deploy/financas-api.env
```

Use os valores reais do ambiente atual, não os exemplos. Preserve especialmente `JwtSettings__Key`, `Issuer` e `Audience`: trocá-los invalida tokens existentes. O valor de `ConnectionStrings__DefaultConnection` deve apontar para um banco alcançável **a partir do container**; `localhost` dentro do container aponta para o próprio container. Se o MySQL também roda em Docker, conecte os containers à mesma rede com `DEPLOY_DOCKER_NETWORK` e use o nome do container/serviço do MySQL como host.

O Dockerfile exclui os arquivos `appsettings*.json` publicados da imagem final. A API recebe configurações de runtime do arquivo protegido acima, evitando incluir configuração local na nova imagem pública. Como imagens publicadas anteriormente podem ter incluído `appsettings.json`, confira se esse arquivo continha credenciais reais; se continha, trate-as como expostas e troque a senha do banco/chaves afetadas antes do deploy. Trocar a chave JWT invalida tokens ativos. O workflow não executa scripts SQL automaticamente. Antes de ativar os endpoints de investimentos em produção, aplique `database/20261124_investimentos.sql` no banco de produção, com backup e procedimento controlado.

### Teste do deploy

Confirme que `deploy` pertence ao grupo `docker` e que `curl` está instalado no servidor. O workflow substitui o container `financas_api` existente na porta `5010`; não remova esse container manualmente antes do deploy. Faça push para `main` e acompanhe **Actions → Deploy backend**. O frontend usa `https://financeiro.macroloapp.com.br/api`; o proxy reverso deve encaminhar `/api` para `127.0.0.1:5010`.
