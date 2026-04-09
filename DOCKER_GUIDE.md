# 🐳 Docker - MeuPlantão

## 📋 Pré-requisitos

- Docker Engine 20.10+
- Docker Compose 2.0+
- Git

Baixe em: https://www.docker.com/products/docker-desktop

---

## 🚀 Como Usar

### 1️⃣ Build e Start (Primeira vez)

```bash
docker-compose up -d
```

**O que acontece:**
- Baixa imagens necessárias
- Cria o containers do PostgreSQL e Backend
- Executa migrations automaticamente
- API fica disponível em `http://localhost:8080`
- Banco de dados em `localhost:5432`

### 2️⃣ Parar os Containers

```bash
docker-compose down
```

### 3️⃣ Parar mas manter o banco de dados

```bash
docker-compose down --volumes
```

### 4️⃣ Ver logs em tempo real

```bash
# Todos os serviços
docker-compose logs -f

# Apenas o backend
docker-compose logs -f backend

# Apenas o banco
docker-compose logs -f postgres
```

### 5️⃣ Executar comando dentro do container

```bash
# Bash no backend
docker-compose exec backend /bin/sh

# Executar dotnet command
docker-compose exec backend dotnet ef database update
```

---

## 🔧 Variáveis de Ambiente

O arquivo `.env` é usado para configurações. Para alterações:

1. Crie `.env` na raiz do projeto (baseado em `.env.example`)
2. Altere os valores conforme necessário
3. Execute `docker-compose down` e depois `docker-compose up -d`

---

## 📊 Verificando Status

### Health Check API

```bash
curl http://localhost:8080/health
```

### Status dos Containers

```bash
docker-compose ps
```

### Conectar ao Banco de Dados

```bash
# Com psql
psql -h localhost -U postgres -d MeuPlantaoDB

# Com qualquer cliente (DBeaver, pgAdmin)
Host: localhost
Port: 5432
User: postgres
Password: 8643
Database: MeuPlantaoDB
```

---

## 🔄 Reconstruir após mudanças no código

```bash
# Se alterou arquivos .cs
docker-compose up -d --build backend

# Se alterou dependencies (csproj)
docker-compose down
docker-compose up -d --build
```

---

## 🧪 Comandos Úteis

```bash
# Ver imagens
docker images

# Ver todos os containers (rodando e parados)
docker ps -a

# Remover container específico
docker-compose rm postgres

# Resetar tudo (containers + volumes)
docker-compose down -v

# Rebuild completo sem cache
docker-compose build --no-cache
docker-compose up -d
```

---

## 📝 Estrutura do docker-compose

| Serviço | Porta | Função |
|---------|-------|--------|
| **postgres** | 5432 | Banco de dados PostgreSQL |
| **backend** | 8080 | API .NET 8 |

---

## 🎯 Próximos Passos (Frontend)

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

