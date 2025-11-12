# 🏋️‍♂️ GymTracker API

Uma API backend simples e moderna para gerenciar treinos e exercícios, construída com .NET 8, C# e PostgreSQL. Ideal como base para um front-end em React ou para uso como serviço standalone.

[![.NET](https://img.shields.io/badge/dotnet-8.0-blue)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

---

## ✨ Destaques

- Estrutura limpa com Controllers, Services, DTOs e AutoMapper
- Persistência com Entity Framework Core e PostgreSQL
- Documentação interativa via Swagger

---

## 🧰 Tecnologias

- .NET 8 (ASP.NET Core Web API)
- C#
- Entity Framework Core
- PostgreSQL
- AutoMapper
- Swagger (Swashbuckle)

---

## 🗂 Estrutura resumida

Principais pastas e arquivos:

- `Controllers/` — endpoints (Exercicio, Treino)
- `Data/` — `AppDbContext`
- `DTOs/` — objetos de transferência
- `Models/` — entidades do domínio
- `Services/` — regras de negócio
- `Mappings/` — perfis do AutoMapper
- `Program.cs`, `appsettings.json`

---

## ▶️ Pré-requisitos

- .NET 8 SDK
- PostgreSQL
- (Opcional) dotnet-ef tools para migrations: `dotnet tool install --global dotnet-ef`

---

## ⚡ Instalação e execução (Windows / PowerShell)

1. Clone o repositório e entre na pasta da API:

```powershell
git clone https://github.com/kaiquedm12/GymTracker.git
cd GymTracker\GymTrackerApi
```

2. Crie um arquivo `.env` na raiz da pasta `GymTrackerApi` com a string de conexão (exemplo):

```text
DB_CONNECTION=Host=localhost;Port=5432;Database=gymtrackerdb;Username=postgres;Password=123456
```

Altere `Username` e `Password` conforme seu PostgreSQL.

3. Aplicar migrations e atualizar o banco:

```powershell
dotnet ef database update
```

4. Executar a API:

```powershell
dotnet run
```

Por padrão o Swagger ficará disponível em: http://localhost:5076/swagger (valide a porta mostrada no console).

---

## � Endpoints principais

Exemplos básicos:

- Exercícios
  - GET /api/exercicio — listar todos
  - GET /api/exercicio/{id} — obter por id
  - POST /api/exercicio — criar
  - PUT /api/exercicio/{id} — atualizar
  - DELETE /api/exercicio/{id} — remover

- Treinos
  - GET /api/treino — listar todos com exercícios
  - GET /api/treino/{id} — obter por id
  - POST /api/treino — criar (associar exercícios existentes)
  - PUT /api/treino/{id} — atualizar
  - DELETE /api/treino/{id} — remover

Use o Swagger para testar os endpoints interativamente.

---

## ✅ Boas práticas e segurança

- Validações via Data Annotations nas DTOs
- Uso de variáveis de ambiente para credenciais
- CORS configurado para permitir front-ends autorizados
- Recomendado habilitar HTTPS em produção

---

## 🤝 Como contribuir

1. Faça um fork
2. Crie uma branch: `git checkout -b feature/minha-feature`
3. Commit: `git commit -m "feat: descrição"`
4. Push e abra um Pull Request

Pequenas melhorias como documentação, mais testes e pipelines de CI são bem-vindas.

---

## 📄 Licença

Projeto licenciado sob MIT. Veja o arquivo `LICENSE` para detalhes.

---

## 👨‍💻 Autor

Kaique Demetrio — Desenvolvedor Full Stack

GitHub: https://github.com/kaiquedm12

---
