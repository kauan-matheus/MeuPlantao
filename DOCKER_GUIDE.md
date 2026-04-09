# 🐳 Docker - MeuPlantão

## 📋 Pré-requisitos

- Docker Engine 20.10+
- Docker Compose 2.0+
- Git (opcional, para controle de versão)

Baixe Docker Desktop em: https://www.docker.com/products/docker-desktop

---

## 🚀 Como Usar

### 1️⃣ Build e Start (Primeira vez)

```bash
docker-compose up -d
```

**O que acontece:**
- ⬇️ Baixa imagens PostgreSQL e .NET necessárias
- 🔨 Constrói imagem do backend (.NET)
- 🆙 Cria containers de banco de dados e API
- ✅ API fica disponível em `http://localhost:8082`
- 🛢️ Banco de dados em `localhost:5432`

### 2️⃣ Parar os Containers

```bash
docker-compose down
```

✅ Containers são removidos, dados do banco persistem em volume

### 3️⃣ Resetar tudo (limpeza completa)

```bash
docker-compose down -v
```

⚠️ Isso DELETA dados do banco! Use só para resetar desenvolvimento.

### 4️⃣ Ver logs em tempo real

```bash
# Todos os serviços
docker-compose logs -f

# Apenas o backend
docker-compose logs -f backend

# Apenas o banco
docker-compose logs -f postgres

# Últimas 50 linhas
docker-compose logs --tail 50
```

### 5️⃣ Executar comando dentro do container

```bash
# Shell no backend
docker-compose exec backend /bin/sh

# Shell no PostgreSQL
docker-compose exec postgres psql -U postgres -d MeuPlantaoDB

# Listar tabelas do banco
docker-compose exec postgres psql -U postgres -d MeuPlantaoDB -c \\dt
```

---

## 🔐 Autenticação & Credenciais

### Admin User (Padrão)

Credenciais armazenadas no banco de dados

Para fazer login na API:

```bash
curl -X POST http://localhost:8082/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "newadmin@meuplantao.com.br",
    "password": "Admin@123"
  }'
```

Resposta (token JWT):
```json
{
  "message": "Login realizado com sucesso",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "usuario": {
    "id": 6,
    "email": "newadmin@meuplantao.com.br",
    "role": "Admin"
  }
}
```

Usar o token após em requisições:
```bash
-H "Authorization: Bearer {token_aqui}"
```

---

## 📊 Verificando Status

### Health Check da API

```bash
curl http://localhost:8082/swagger/index.html
```

Se retornar HTML do Swagger = API está saudável ✅

### Status dos Containers

```bash
docker-compose ps
```

Esperado:
```
NAME              STATUS
meuplantao-db     Up (healthy)
meuplantao-api    Up (healthy)
```

### Conectar ao Banco de Dados

**Opção 1: PgAdmin / DBeaver / TablePlus**
```
Host: localhost
Port: 5432
Database: MeuPlantaoDB
User: postgres
Password: (veja em docker-compose.yml)
```

**Opção 2: Linha de comando**
```bash
docker-compose exec postgres psql -U postgres -d MeuPlantaoDB
```

---

## 🔄 Workflow de Desenvolvimento

### Só alterou C# (.cs files)

```bash
docker-compose up -d --build backend
```

Rebuilda apenas o backend em 30-60s.

### Alterou banco de dados ou migrations

```bash
docker-compose down
docker-compose up -d --build
```

Recria ambos os containers do zero.

### Alterou docker-compose.yml ou variáveis de ambiente

```bash
docker-compose down
docker-compose up -d
```

Sem `--build`, usa imagens já construídas.

---

## 🧪 Comandos Úteis

```bash
# Listar imagens Docker
docker images

# Listar containers (rodando e parados)
docker ps -a

# Ver tamanho das imagens
docker images --format "table {{.Repository}}\t{{.Size}}"

# Limpar imagens não usadas
docker image prune -a

# Rebuild sem cache (força recompilação)
docker-compose build --no-cache backend
docker-compose up -d

# Copiar arquivo para container
docker-compose cp ./arquivo.sql postgres:/tmp/

# Executar script SQL
docker-compose exec postgres psql -U postgres -d MeuPlantaoDB -f /tmp/arquivo.sql

# Ver variáveis de ambiente do container
docker-compose exec backend env | grep ASPNETCORE

# Inspecionar container
docker inspect meuplantao-api
```

---

## 📝 Estrutura do docker-compose

| Serviço | Porta | Função | Status |
|---------|-------|--------|--------|
| **postgres** | 5432 | Banco de dados PostgreSQL | Healthcheck cada 10s |
| **backend** | 8082 | API .NET 8 | Healthcheck cada 30s |

**Dependências:**
- Backend espera postgres estar `service_healthy` antes de iniciar
- Backend conecta ao Postgres pelo hostname `postgres` (rede interna)

---

## 🎯 Próximos Passos (React Native Frontend)

Quando o frontend estiver pronto, adicione ao `docker-compose.yml`:

```yaml
  frontend:
    build:
      context: ./frontend/MeuPlantao
      dockerfile: Dockerfile
    container_name: meuplantao-mobile
    ports:
      - "8081:8081"
    depends_on:
      - backend
    networks:
      - meuplantao-network
```

Depois:
```bash
docker-compose up -d  # Sobe todos os 3 containers
```

---

## ❓ Troubleshooting

### Porta 8082 já está em uso

Mudar em `docker-compose.yml`:
```yaml
ports:
  - "8081:8082"  # Usa porta 8081 no host
```

### Backend conecta mas swagger não carrega

```bash
docker-compose logs backend | grep -i error
```

Common issues:
- JWT key muito curta (verif em docker-compose.yml)
- PostgreSQL não está healthy (`docker-compose logs postgres`)

### Resetar PostgreSQL

```bash
# Remover volume do banco
docker-compose down -v

# Recriar tudo
docker-compose up -d
```

⚠️ Todos os dados do banco são perdidos!

### Container crasheando

```bash
# Ver últimos logs
docker-compose logs --tail 100 backend

# Veríficar health
docker-compose ps

# Restartar
docker-compose restart backend
```

---

## 🔒 Segurança em Produção

⚠️ **NÃO use essas configs em produção!**

- JWT key está visível em docker-compose.yml
- Senhas simples do PostgreSQL
- Swagger habilitado em Production

Para produção:
- Use variáveis de ambiente secretas (Kubernetes secrets, AWS Secrets Manager)
- Mude senhas do PostgreSQL
- Desabilite Swagger em Production
- Use HTTPS/SSL
- Configure firewall adequadamente

---

## 📚 Referências

- [Dockerfile Reference](https://docs.docker.com/engine/reference/builder/)
- [Docker Compose Reference](https://docs.docker.com/compose/compose-file/)
- [.NET in Docker](https://docs.microsoft.com/en-us/dotnet/core/docker/introduction)
- [PostgreSQL Docker Image](https://hub.docker.com/_/postgres)

Quando o React Native estiver pronto, adicione ao `docker-compose.yml`:

```yaml
  frontend:
    build:
      context: ./frontend/MeuPlantao
      dockerfile: Dockerfile
    container_name: meuplantao-mobile
    ports:
      - "8081:8081"
    depends_on:
      - backend
    networks:
      - meuplantao-network
```

---

## ❓ Troubleshooting

### Porta 8080 já está em uso

```bash
# Mudar no docker-compose.yml
ports:
  - "8081:8080"  # Host:Container
```

### Erro de conexão com banco

```bash
# Verificar se postgres está saudável
docker-compose logs postgres

# Reiniciar apenas o backend
docker-compose restart backend
```

### Limpar tudo e começar novamente

```bash
docker-compose down -v
docker system prune -a
docker-compose up -d
```

---

## 🛠️ Desenvolvimento

Para desenvolvimento contínuo com hot-reload:

```bash
docker-compose -f docker-compose.yml -f docker-compose.dev.yml up -d
```

*(Crie `docker-compose.dev.yml` quando necessário)*

