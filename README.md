# TaskFlow

**TaskFlow** é uma API REST desenvolvida em ASP.NET Core com o objetivo de gerenciar tarefas colaborativas. O projeto está em fase inicial, com foco na criação de um MVP funcional que permita o cadastro e gerenciamento de tarefas, subtarefas, comentários e atribuições de usuários.

## ✨ Objetivo

Construir uma aplicação backend robusta para gerenciamento de tarefas, com suporte a:

- Criação e edição de tarefas principais e subtarefas
- Atribuição de usuários às tarefas
- Adição de comentários nas tarefas
- Controle de status das tarefas (ToDo, Doing, Done)
- Registro de data e hora com fuso horário (TIMESTAMPTZ no PostgreSQL)

## 🚧 Status do Projeto

Atualmente, o projeto está em desenvolvimento, com as seguintes funcionalidades implementadas:

- Estruturação das entidades principais (`MainTask`, `Subtask`, `Comment`, `UserTask`)
- Configuração do Entity Framework Core com PostgreSQL
- Mapeamento de propriedades `DateTime` para `TIMESTAMPTZ`
- Utilização de `DateTime.UtcNow` para consistência temporal
- Organização do projeto com pastas para `Domain`, `Infra`, `Configurations`, `Controllers`, etc.

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Docker (configuração inicial)
- C# 12
- .NET 8

## 🚀 Como Executar

1. Clone o repositório:

   ```bash
   git clone https://github.com/GSOFelix/TaskFlow.git
   cd TaskFlow

2. Configure o banco de dados PostgreSQL e adicione a variável de ambiente: 

   ```bash
   DB_CONNECTION=SUA-STRING-DE-CONEXAO

3. Aplique as migrações:

   ```bash
    dotnet ef database update

4. Execute a aplicação: 

   ```bash
    dotnet run

## 🧭 Próximos Passos

- Implementar autenticação e autorização de usuários

- Criar endpoints para gerenciamento completo das tarefas

- Desenvolver testes unitários e de integração

- Integrar com frontend (futuro)

- Melhorar a documentação da API (Swagger)