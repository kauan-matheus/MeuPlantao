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
</p>

---

## 📋 Sobre o projeto

O Hospital Beneficente Unimar (HBU) realizou uma apresentação em sala de aula na qual conseguimos elaborar o levantamento de requisitos. Durante a apresentação, foram mostradas várias propostas de sistemas; dentre as opções, optamos pelo desenvolvimento de um **Sistema de Gestão de Plantões**, utilizando como base o funcionamento geral de plataformas existentes voltadas para intermediação de plantões médicos.

### Problemas identificados

O hospital já utiliza um sistema de terceiros para gestão de plantões, o que gera:

- Dependência de fornecedor externo
- Dificuldade de personalização conforme necessidades internas
- Possíveis limitações de funcionalidades
- Falta de integração com outros processos internos
- Processos manuais, como comunicação de trocas de plantão

### Objetivos

- ✅ Centralizar o gerenciamento das escalas de plantão
- ✅ Permitir o controle e solicitação de trocas de plantão
- ✅ Gerenciar profissionais e setores do hospital
- ✅ Oferecer uma alternativa personalizada ao sistema atual

---

## 🛠️ Tecnologias

### Mobile (React Native)
| Tecnologia | Versão | Uso |
|---|---|---|
| [React Native](https://reactnative.dev/) | 0.83 | Framework mobile |
| [Expo](https://expo.dev/) | 55 | Plataforma de desenvolvimento |
| [Expo Router](https://expo.github.io/router/) | 55 | Navegação baseada em arquivos |
| [TypeScript](https://www.typescriptlang.org/) | 5.9 | Tipagem estática |
| [React Native Calendars](https://github.com/wix/react-native-calendars) | 1.x | Componente de calendário |
| [React Native Reanimated](https://docs.swmansion.com/react-native-reanimated/) | 4.2 | Animações |

### Backend
| Tecnologia | Versão | Uso |
|---|---|---|
| [ASP.NET Core](https://dotnet.microsoft.com/) | .NET 8.0 | API REST |
| [Entity Framework Core](https://learn.microsoft.com/ef/core/) | 8.0 | ORM |
| [FluentValidation](https://github.com/FluentValidation/FluentValidation) | 11.3.0 | Validação |
| [Npgsql](https://www.npgsql.org/) | 8.0 | Provider PostgreSQL para EF |
| [JWT Bearer](https://learn.microsoft.com/aspnet/core/security/authentication/jwt-authn) | 8.0 | Autenticação via token |
| [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) | 4.1 | Hash de senhas |
| [Swagger / Swashbuckle](https://swagger.io/) | 6.6 | Documentação da API |

### Banco de dados
| Tecnologia | Uso |
|---|---|
| [PostgreSQL](https://www.postgresql.org/) | Banco de dados relacional |

### Infraestrutura
| Tecnologia | Uso |
|---|---|
| [Docker](https://www.docker.com/) | Containerização do projeto via docker compose |
| [AWS](https://aws.amazon.com/pt/) | Futuro deploy na AWS Web Services |

---

## 📁 Estrutura do projeto

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
└── MeuPlantao.sln                 # Solution do Visual Studio
```

---

## 🚀 Como rodar o projeto

### Pré-requisitos

Certifique-se de ter instalado:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/) e npm
- [PostgreSQL](https://www.postgresql.org/download/) rodando localmente
- [Expo CLI](https://docs.expo.dev/get-started/installation/): `npm install -g expo-cli`
- [Expo Go](https://expo.dev/client) no celular (para testar no dispositivo) **ou** um emulador Android/iOS configurado

---

### 🗄️ Configurando o banco de dados

1. Crie o banco de dados PostgreSQL:

```sql
CREATE DATABASE "MeuPlantaoDB";
```

2. Verifique as credenciais em `MeuPlantao/appsettings.json` (padrão: usuário `postgres`, senha `8643`, porta `5432`):

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=MeuPlantaoDB;Username=postgres;Password=8643"
  }
}
```

Ajuste conforme o seu ambiente se necessário.

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

A API estará disponível em `https://localhost:7XXX` (porta exibida no terminal).

A documentação Swagger estará em: `https://localhost:7XXX/swagger`

---

### 📱 Rodando o Frontend (React Native + Expo)

```bash
# Entrar na pasta do app mobile
cd frontend/MeuPlantao

# Instalar as dependências
npm install

# Iniciar o servidor de desenvolvimento
npm start
```

Com o servidor rodando, você pode:
- **Dispositivo físico:** escanear o QR Code com o app **Expo Go**
- **Emulador Android:** pressionar `a` no terminal
- **Simulador iOS:** pressionar `i` no terminal (requer macOS)

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

## 👥 Contribuidores

Projeto desenvolvido como trabalho acadêmico.
