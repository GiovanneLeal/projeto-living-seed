# Arquitetura do Projeto: LivingSeed 🌱

Este documento descreve a arquitetura, as tecnologias e os padrões de projeto adotados no desenvolvimento da plataforma **LivingSeed**.

## 🏗️ Visão Geral

A solução foi projetada com uma arquitetura **API-First**, garantindo que o backend e o frontend sejam completamente desacoplados. Essa separação de responsabilidades permite maior escalabilidade, facilidade de manutenção e segurança na comunicação dos dados.

A solução principal é dividida em dois projetos centrais:
1. **LivingSeed.API**: Backend responsável pelas regras de negócio e persistência de dados.
2. **LivingSeed.Web**: Frontend interativo para a experiência do usuário.

---

## 💻 Tecnologias Utilizadas

### Backend (.NET 8.0)
* **Framework:** ASP.NET Core Web API
* **ORM:** Entity Framework Core (EF Core)
* **Banco de Dados:** SQL Server
* **Linguagem:** C#

### Frontend
* **Framework:** Blazor WebAssembly
* **Linguagem:** C# / HTML / CSS
* **Comunicação:** Chamadas HTTP consumindo a REST API

---

## 📂 Estrutura da Solução

### 1. Camada de API (Backend)
O backend foi construído visando a **Separação de Conceitos (Separation of Concerns)**. 

* **Controllers:** Responsáveis por receber as requisições HTTP do frontend e retornar as respostas adequadas (ex: status 200 OK, 404 Not Found).
* **Services / Regras de Negócio:** Onde a lógica principal da aplicação reside.
* **Acesso a Dados:** Utilização do EF Core para mapear o modelo de domínio (como a entidade principal `Projeto`) para o banco de dados SQL Server, realizando as operações fundamentais de CRUD (Create, Read, Update, Delete).

### 2. Camada de Apresentação (Frontend)
O frontend em **Blazor WebAssembly** roda diretamente no navegador do cliente, oferecendo uma experiência de Single Page Application (SPA). Ele se comunica com a `LivingSeed.API` através do `HttpClient` para renderizar as informações na tela de forma dinâmica.

---

## ⚙️ Padrões de Projeto Adotados

* **DTOs (Data Transfer Objects):** Implementados para garantir que apenas os dados estritamente necessários transitem entre o cliente e a API, evitando o vazamento de informações sensíveis do modelo de banco de dados e otimizando a carga de rede.
* **Injeção de Dependência (DI):** O .NET nativamente gerencia o ciclo de vida dos serviços e contextos de banco de dados, promovendo um código mais limpo e testável.

---

## 🚀 Como Executar o Projeto Localmente

### Pré-requisitos
Antes de começar, você precisará ter instalado em sua máquina:
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (Express ou Developer)
* Uma IDE de sua preferência (Visual Studio 2022, VS Code, etc.)

### Passo a Passo

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/ssilveiira/LivingSeed.git](https://github.com/ssilveiira/LivingSeed.git)
