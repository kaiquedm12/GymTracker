# GymTracker API

API backend para gerenciamento de treinos, exercícios, alunos e personal trainers. Construída com **.NET 9**, **C#** e **PostgreSQL**.

[![.NET](https://img.shields.io/badge/dotnet-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

---

## Funcionalidades

- **Personais** — cadastro e gerenciamento de personal trainers
- **Alunos** — cada aluno vinculado a um personal
- **Treinos** — sessões de treino com data, duração e exercícios associados (M:N via tabela join)
- **Exercícios** — nome, repetições, séries e peso (decimal)
- **Autenticação JWT** — proteção de todos os endpoints
- **Validação** — FluentValidation em todos os DTOs
- **Rate Limiting** — 100 requisições/minuto por IP
- **Health Check** — endpoint `/health` para monitoramento
- **Swagger** — documentação interativa da API

---

## Tecnologias

| Tecnologia | Versão |
|-----------|--------|
| .NET | 9.0 |
| ASP.NET Core | 9.0 |
| Entity Framework Core | 9.0 |
| PostgreSQL (Npgsql) | 9.0 |
| AutoMapper | 12.0 |
| FluentValidation | 11.3 |
| JWT Bearer | 9.0 |
| xUnit + Moq | testes |

---

## Estrutura do projeto

```
GymTrackerApi/
├── Controllers/           # Endpoints da API
│   ├── PersonalController.cs
│   ├── AlunoController.cs
│   ├── TreinoController.cs
│   └── ExercicioController.cs
├── Data/
│   └── AppDbContext.cs     # EF Core DbContext
├── DTOs/                   # Objetos de transferência
│   ├── AlunoDTOs/
│   ├── PersonalDTOs/
│   ├── ExercicioDTOs/
│   ├── TreinoDTOs/
│   └── TreinoExercicioDTOs/
├── Mappings/               # Perfis do AutoMapper
├── Migrations/             # Migrations do EF Core
├── Models/                 # Entidades do domínio
│   ├── Exercicios/
│   ├── Pessoas/            # Personal e Aluno
│   ├── Relacionamentos/
│   └── Treinos/
├── Profiles/               # Perfis do AutoMapper (alternativo)
├── Services/               # Regras de negócio
│   └── Interfaces/
├── Validators/             # FluentValidation
├── Program.cs              # Entry point
└── appsettings.json
```

---

## Modelagem do banco

```
Personas (1) ──→ (N) Alunos ──→ (N) Treinos ──→ (N) Exercicios
                                                    (via TreinosExercicios)
                               Alunos ──→ (N) Exercicios (standalone)
```

### Tabelas

- **Personais** — `Id`, `Nome`, `Email`, `Telefone`, `UserId`, `CreatedAt`
- **Alunos** — `Id`, `Nome`, `Email`, `Telefone`, `DataNascimento`, `PersonalId` (FK), `UserId`, `CreatedAt`
- **Treinos** — `Id`, `Nome`, `Data`, `DuracaoMinutos`, `AlunoId` (FK), `UserId`
- **Exercicios** — `Id`, `Nome`, `Repeticoes`, `Series`, `Peso (decimal(5,2))`, `TreinoId` (FK), `AlunoId` (FK), `UserId`
- **TreinosExercicios** — join table (`TreinoId`, `ExercicioId`) PK composta

---

## Endpoints

### Personal

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/personal` | Lista todos |
| GET | `/api/personal/me` | Dados do personal logado |
| GET | `/api/personal/{id}` | Busca por ID |
| POST | `/api/personal` | Criar |
| PUT | `/api/personal/{id}` | Atualizar |
| DELETE | `/api/personal/{id}` | Excluir |

### Aluno

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/aluno?personalId=X` | Lista alunos de um personal |
| GET | `/api/aluno/{id}` | Busca por ID |
| POST | `/api/aluno?personalId=X` | Criar aluno |
| PUT | `/api/aluno/{id}` | Atualizar |
| DELETE | `/api/aluno/{id}` | Excluir |

### Treino

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/treino?alunoId=X` | Lista treinos (filtrados por aluno) |
| GET | `/api/treino/{id}` | Busca por ID (com exercícios) |
| POST | `/api/treino` | Criar (associa exercícios via `ExerciciosIds`) |
| PUT | `/api/treino/{id}` | Atualizar |
| DELETE | `/api/treino/{id}` | Excluir |

### Exercício

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/exercicio?alunoId=X` | Lista exercícios (filtrados por aluno) |
| GET | `/api/exercicio/{id}` | Busca por ID |
| POST | `/api/exercicio` | Criar |
| PUT | `/api/exercicio/{id}` | Atualizar |
| DELETE | `/api/exercicio/{id}` | Excluir |

### Utilitários

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/health` | Health check do banco |

> **Nota:** Todos os endpoints exigem token JWT (exceto `/health`). Envie no header: `Authorization: Bearer <token>`

---

## Exemplos de uso (curl)

### Fluxo completo

#### 1. Health check (sem token)

```bash
curl http://localhost:5076/health
```

#### 2. Criar personal trainer

```bash
curl -X POST http://localhost:5076/api/personal \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Carlos Personal",
    "email": "carlos@email.com",
    "telefone": "11999999999"
  }'
```

#### 3. Obter token JWT

A API não possui endpoint de login próprio. Gere o token manualmente usando a chave do `appsettings.json`:

```bash
# Exemplo com jwt-cli (ou use https://jwt.io)
# Payload: { "sub": "SEU_USER_ID", "name": "Carlos Personal" }
# Assine com a chave: "gymtracker-super-secret-key-2024-min-32-chars!!"
```

> Para desenvolvimento, use o Swagger ou implemente um endpoint de login.

#### 4. Criar aluno (personalId = 1)

```bash
curl -X POST "http://localhost:5076/api/aluno?personalId=1" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN" \
  -d '{
    "nome": "João Aluno",
    "email": "joao@email.com",
    "telefone": "11988888888",
    "dataNascimento": "1998-05-20T00:00:00Z"
  }'
```

#### 5. Listar alunos do personal

```bash
curl "http://localhost:5076/api/aluno?personalId=1" \
  -H "Authorization: Bearer SEU_TOKEN"
```

#### 6. Criar exercício (para o aluno 1)

```bash
curl -X POST http://localhost:5076/api/exercicio \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN" \
  -d '{
    "nome": "Supino Reto",
    "repeticoes": 12,
    "series": 4,
    "peso": 80.00,
    "alunoId": 1
  }'
```

#### 7. Criar treino (para o aluno 1, com exercícios 1 e 2)

```bash
curl -X POST http://localhost:5076/api/treino \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN" \
  -d '{
    "nome": "Treino A - Peito e Tríceps",
    "data": "2026-06-11T08:00:00Z",
    "duracaoMinutos": 60,
    "alunoId": 1,
    "exerciciosIds": [1, 2]
  }'
```

#### 8. Listar treinos de um aluno

```bash
curl "http://localhost:5076/api/treino?alunoId=1" \
  -H "Authorization: Bearer SEU_TOKEN"
```

#### 9. Buscar treino com exercícios

```bash
curl "http://localhost:5076/api/treino/1" \
  -H "Authorization: Bearer SEU_TOKEN"
```

#### 10. Atualizar exercício

```bash
curl -X PUT http://localhost:5076/api/exercicio/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN" \
  -d '{
    "nome": "Supino Inclinado",
    "repeticoes": 10,
    "series": 4,
    "peso": 60.00
  }'
```

#### 11. Excluir treino

```bash
curl -X DELETE http://localhost:5076/api/treino/1 \
  -H "Authorization: Bearer SEU_TOKEN"
```

---

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- PostgreSQL (local ou [Supabase](https://supabase.com))
- EF Core CLI (opcional): `dotnet tool install --global dotnet-ef`

---

## Instalação e execução

### 1. Clone

```bash
git clone https://github.com/kaiquedm12/GymTracker.git
cd GymTracker/GymTrackerApi
```

### 2. Configure o banco

Crie um arquivo `.env` na pasta `GymTrackerApi`:

```env
DB_CONNECTION=Host=localhost;Port=5432;Database=gymtrackerdb;Username=postgres;Password=123456
```

Para usar Supabase, substitua pelos seus dados:

```env
DB_CONNECTION=Host=db.xxxxx.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=sua-senha
```

### 3. Crie as tabelas no banco

Via EF Core migrations:

```bash
dotnet ef database update
```

Ou execute o script SQL manualmente (veja `docs/schema.sql`).

### 4. Rode a API

```bash
dotnet run
```

Swagger disponível em: `http://localhost:5076/swagger`

### 5. Gerar token JWT (para testar)

A API usa JWT. Para testes, gere um token com seu secret (`appsettings.json` → `Jwt:Key`):

```bash
dotnet run --project GymTrackerApi -- --generate-token
```

> Ou use o Swagger para criar um personal e obter o token.

---

## Testes

```bash
dotnet test
```

O projeto de testes (`GymTrackerApi.Tests`) usa xUnit + Moq + InMemoryDatabase.

---

## Variáveis de ambiente

| Variável | Obrigatório | Descrição |
|----------|-------------|-----------|
| `DB_CONNECTION` | Sim | String de conexão PostgreSQL |

> As configurações de JWT ficam em `appsettings.json` → seção `Jwt`.

---

## Boas práticas implementadas

- Autenticação JWT em todos os endpoints
- Validação com FluentValidation
- Rate limiting (100 req/min)
- Health check do banco
- CORS configurável
- Camada de serviços separada dos controllers
- AutoMapper para DTOs
- Testes unitários
- Variáveis sensíveis via `.env` (gitignorado)

---

## Licença

MIT. Veja o arquivo [LICENSE](LICENSE).

---

## Autor

Kaique Demetrio — [@kaiquedm12](https://github.com/kaiquedm12)
