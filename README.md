<h1 align="center">🏥 Meu Plantão</h1>

<p align="center">
  Sistema de gestão de plantões médicos desenvolvido para o Hospital Beneficente Unimar (HBU).
</p>

<p align="center">
  <img src="https://img.shields.io/badge/React_Native-0.83-61DAFB?style=for-the-badge&logo=react&logoColor=black" />
  <img src="https://img.shields.io/badge/Expo-55-000020?style=for-the-badge&logo=expo&logoColor=white" />
  <img src="https://img.shields.io/badge/TypeScript-5.9-3178C6?style=for-the-badge&logo=typescript&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/PostgreSQL-336791?style=for-the-badge&logo=postgresql&logoColor=white" />
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" />
  <img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white" />
  <img src="https://img.shields.io/badge/AWS-232F3E?style=for-the-badge&logo=amazonaws&logoColor=white" />
  <img src="https://img.shields.io/badge/Terraform-7B42BC?style=for-the-badge&logo=terraform&logoColor=white" />
  <img src="https://img.shields.io/badge/Grafana-F46800?style=for-the-badge&logo=grafana&logoColor=white" />
  <img src="https://img.shields.io/badge/InfluxDB-22ADF6?style=for-the-badge&logo=influxdb&logoColor=white" />
  <img src="https://img.shields.io/badge/GitHub_Actions-2088FF?style=for-the-badge&logo=githubactions&logoColor=white" />
  <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" />
  <img src="https://img.shields.io/badge/Telegram-26A5E4?style=for-the-badge&logo=telegram&logoColor=white" />
</p>

---

## 📋 Sobre o projeto

O Hospital Beneficente Unimar (HBU) realizou uma apresentação em sala de aula na qual conseguimos elaborar o levantamento de requisitos. Durante a apresentação, foram mostradas várias propostas de sistemas; dentre as opções, optamos pelo desenvolvimento de um **Sistema de Gestão de Plantões**, utilizando como base o funcionamento geral de plataformas existentes voltadas para intermediação de plantões médicos.

### 🧩 Problemas identificados

O hospital já utiliza um sistema de terceiros para gestão de plantões, o que gera:

- Dependência de fornecedor externo
- Dificuldade de personalização conforme necessidades internas
- Possíveis limitações de funcionalidades
- Falta de integração com outros processos internos
- Processos manuais, como comunicação de trocas de plantão

### 🎯 Objetivos

- ✅ Centralizar o gerenciamento das escalas de plantão
- ✅ Permitir o controle e solicitação de trocas de plantão
- ✅ Gerenciar profissionais e setores do hospital
- ✅ Oferecer uma alternativa personalizada ao sistema atual

---

## 🧱 Stack (visão geral)

- 📱 **App Mobile**: React Native + Expo + TypeScript
- ⚙️ **API**: ASP.NET Core (.NET 8) + Swagger
- 🗄️ **Banco**: PostgreSQL
- 🐳 **Runtime/Deploy**: Docker
- ☁️ **Cloud**: AWS (EC2 + S3)
- 🏗️ **IaC**: Terraform
- 📊 **Observabilidade**: InfluxDB + Grafana
- 🔔 **Alertas**: Grafana → Telegram
- 🔐 **Segurança**: Autenticação JWT + acesso privado via NetBird + SSH (Termius)
- 🔄 **CI/CD**: GitHub Actions

---

## 🛠️ Tecnologias (com explicação)

### 📱 Mobile (React Native)

| Tecnologia | Versão | Para que serve |
|---|---:|---|
| [React Native](https://reactnative.dev/) | 0.83 | Construção do app mobile multiplataforma (iOS/Android) |
| [Expo](https://expo.dev/) | 55 | Ferramentas de desenvolvimento, execução e build do app |
| [Expo Router](https://expo.github.io/router/) | 55 | Navegação baseada em arquivos (padrão Next.js-like) |
| [TypeScript](https://www.typescriptlang.org/) | 5.9 | Tipagem estática para reduzir erros e melhorar manutenção |
| [React Native Calendars](https://github.com/wix/react-native-calendars) | 1.x | Calendário para visualização e interação com escalas |
| [React Native Reanimated](https://docs.swmansion.com/react-native-reanimated/) | 4.2 | Animações fluidas e performáticas |

### ⚙️ Backend

| Tecnologia | Versão | Para que serve |
|---|---:|---|
| [ASP.NET Core](https://dotnet.microsoft.com/) | .NET 8.0 | API REST e regras de negócio |
| [Entity Framework Core](https://learn.microsoft.com/ef/core/) | 8.0 | ORM para mapear entidades e simplificar operações no banco |
| [FluentValidation](https://github.com/FluentValidation/FluentValidation) | 11.3.0 | Validação de payloads e regras de entrada |
| [Npgsql](https://www.npgsql.org/) | 8.0 | Driver PostgreSQL para .NET/EF Core |
| [JWT Bearer](https://learn.microsoft.com/aspnet/core/security/authentication/jwt-authn) | 8.0 | Middleware de autenticação via token JWT |
| [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) | 4.1 | Hash seguro de senhas (com salt) |
| [Swagger / Swashbuckle](https://swagger.io/) | 6.6 | Documentação e teste de endpoints via UI |

### 🗄️ Banco de dados

| Tecnologia | Para que serve |
|---|---|
| [PostgreSQL](https://www.postgresql.org/) | Persistência relacional (usuários, plantões, trocas, históricos) |

### 🐳 Containerização

| Tecnologia | Para que serve |
|---|---|
| [Docker](https://www.docker.com/) | Empacotar API, banco e serviços auxiliares com consistência entre ambientes |

### ☁️ AWS (Infraestrutura)

| Serviço | Para que serve |
|---|---|
| [EC2](https://aws.amazon.com/ec2/) | Servidor virtual para hospedar a API (Linux) e executar containers |
| [S3](https://aws.amazon.com/s3/) | Armazenamento de objetos (ex.: backups, arquivos, logs/exportações) |

### 🏗️ Terraform (Infra as Code)

| Tecnologia | Para que serve |
|---|---|
| [Terraform](https://www.terraform.io/) | Provisionar infraestrutura na AWS de forma versionada (reprodutível e auditável) |

### 📊 Observabilidade

| Tecnologia | Para que serve |
|---|---|
| [InfluxDB](https://www.influxdata.com/) | Banco de séries temporais para métricas (telemetria) |
| [Grafana](https://grafana.com/) | Dashboards para visualizar métricas e criar alertas |

### 🔔 Alertas (Grafana → Telegram)

| Tecnologia | Para que serve |
|---|---|
| [Telegram Bot](https://core.telegram.org/bots) | Receber alertas automáticos (ex.: indisponibilidade, erro alto, uso de CPU) |

### 🔐 Acesso seguro e operações

| Tecnologia | Para que serve |
|---|---|
| [NetBird](https://netbird.io/) | VPN mesh (WireGuard) para acesso privado ao ambiente (sem expor SSH publicamente) |
| [Termius](https://termius.com/) | Cliente SSH para administrar a instância EC2 (acesso remoto organizado) |

### 🔄 CI/CD

| Tecnologia | Para que serve |
|---|---|
| [GitHub Actions](https://github.com/features/actions) | Automatizar build/test/deploy quando houver mudanças no repositório |

### 🔐 Autenticação

| Tecnologia | Para que serve |
|---|---|
| [JWT](https://jwt.io/) | Autenticação stateless entre app e API (token no `Authorization: Bearer`) |

---

## 🏛️ Estrutura do projeto

```
MeuPlantao/
├── MeuPlantao/                    # API principal (ASP.NET)
│   ├── Controllers/               # Endpoints da API
│   ├── Properties/
│   ├── Dockerfile
│   └── appsettings.json
├── MeuPlantao.Application/        # Camada de serviços / regras de negócio
│   └── Services/
├── MeuPlantao.Communication/      # DTOs e Enums compartilhados
│   ├── Dto/
│   └── Enums/
├── MeuPlantao.Domain/             # Entidades de domínio e interfaces
│   ├── Entities/
│   └── Interfaces/
├── MeuPlantao.Infrastructure/     # Acesso a dados
│   ├── Data/
│   ├── Migrations/
│   ├── Repository/
│   └── UnitOfWork/
├── frontend/
│   └── MeuPlantao/                # App mobile React Native + Expo
│       ├── src/
│       │   ├── app/               # Telas (Expo Router)
│       │   ├── components/        # Componentes reutilizáveis
│       │   ├── styles/            # Estilos globais
│       │   └── utils/
│       ├── app.json               # Configuração do Expo
│       └── package.json
├── infra/                         # Infraestrutura como código (Terraform, etc.)
└── .github/
    └── workflows/                 # Pipelines do GitHub Actions
```

---

## 🚀 Como rodar o projeto

### ✅ Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/) e npm
- [PostgreSQL](https://www.postgresql.org/download/) (local) **ou** Docker
- [Docker](https://www.docker.com/) + Docker Compose
- [Expo CLI](https://docs.expo.dev/get-started/installation/): `npm install -g expo-cli`
- [Expo Go](https://expo.dev/client) no celular **ou** emulador Android/iOS

---

### 🗄️ Configurando o banco (modo local)

1. Crie o banco de dados:

```sql
CREATE DATABASE "MeuPlantaoDB";
```

2. Verifique/ajuste as credenciais em `MeuPlantao/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Host=SEU_HOST;Port=SUA_PORTA;Database=SEU_BANCO;Username=SEU_BANCO;Password=SUA_SENHA"
  }
}
```

---

### ⚙️ Rodando o Backend (ASP.NET)

```bash
# Na raiz do projeto
dotnet restore

# Aplicar as migrações no banco de dados
dotnet ef database update --project MeuPlantao.Infrastructure --startup-project MeuPlantao

# Rodar a API
dotnet run --project MeuPlantao
```

- A API estará disponível em `https://localhost:XXXX` (porta exibida no terminal)
- Swagger: `https://localhost:XXXX/swagger`

---

### 📱 Rodando o Frontend (React Native + Expo)

```bash
cd frontend/MeuPlantao
npm install
npm start
```

Com o servidor rodando:
- **Dispositivo físico:** escaneie o QR Code com o **Expo Go**
- **Emulador Android:** pressione `a`
- **Simulador iOS:** pressione `i` (requer macOS)

---

## 📡 Endpoints da API

| Controller | Descrição |
|---|---|
| `AuthController` | Autenticação (login / registro) |
| `UserController` | Gestão de usuários |
| `ProfissionaisController` | Gestão de profissionais |
| `SetorController` | Gestão de setores |
| `PlantaoController` | Gestão de plantões |
| `TrocasController` | Solicitações de troca de plantão |
| `HistoricoPlantaoController` | Histórico de plantões |
| `HistoricoTrocasController` | Histórico de trocas |

> Acesse o Swagger em `/swagger` para ver todos os endpoints com detalhes de requisição e resposta.

---

## 🔐 Autenticação JWT (como funciona)

A API utiliza **JWT (JSON Web Token)** para autenticação **stateless**:

1. O app faz login e recebe um token JWT.
2. O app envia o token em cada requisição protegida:

```
Authorization: Bearer <seu_token>
```

3. A API valida assinatura/expiração/claims do token.

> As senhas são armazenadas com **BCrypt** (hash seguro), nunca em texto puro.

---

## ☁️ Deploy (AWS) + Operação

### 🖥️ AWS EC2

O backend é executado em uma instância **EC2** (Linux), normalmente rodando a API em **Docker** para facilitar:

- Padronização do ambiente
- Deploy mais previsível
- Atualizações e rollback mais simples

### 🪣 AWS S3

O **S3** pode ser usado para armazenar:

- Backups do banco
- Arquivos gerados/exportados
- Logs/artefatos de deploy (quando aplicável)

### 🏗️ Terraform

O **Terraform** descreve a infraestrutura em código, permitindo:

- Recriar ambientes com consistência
- Versionar mudanças de infra junto ao projeto
- Reduzir mudanças manuais no console

### 🔄 GitHub Actions

O **GitHub Actions** automatiza o fluxo de CI/CD, tipicamente:

- Build + testes
- Build de imagem Docker
- Publicação e deploy na EC2

> Os workflows ficam em `.github/workflows/`.

### 🌐 NetBird (VPN)

O **NetBird** cria uma rede privada (mesh) para que o time acesse a EC2 com segurança, evitando expor SSH na internet.

### 💻 Termius (SSH)

O **Termius** é utilizado para conexões SSH na EC2 (administração e troubleshooting), com gestão de hosts e chaves.

---

## 📊 Monitoramento (Grafana + InfluxDB) e Alertas no Telegram

### 📈 InfluxDB

O **InfluxDB** armazena métricas como séries temporais (ex.: uso de CPU/RAM, latência, erros).

### 📉 Grafana

O **Grafana** exibe dashboards e cria regras de alerta (thresholds, janelas de tempo, etc.).

### 🔔 Alertas no Telegram

O Grafana pode enviar alertas para um **bot/canal no Telegram** quando uma regra disparar (ex.: API fora do ar, pico de erros, CPU acima de X%).

---

## 👥 Contribuidores

Projeto desenvolvido como trabalho acadêmico.
