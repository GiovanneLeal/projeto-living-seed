# 🏛️ Arquitetura da Plataforma Living Seed

## 1. Introdução

Este documento descreve a arquitetura técnica da plataforma Living Seed. O objetivo é fornecer uma visão geral das tecnologias, componentes principais e fluxos de dados que guiarão o desenvolvimento e a evolução do sistema. A arquitetura foi projetada com foco em escalabilidade, segurança e manutenibilidade.

## 2. Tech Stack (Pilha de Tecnologias)

A plataforma será construída utilizando uma pilha de tecnologias moderna, baseada em JavaScript/TypeScript, para garantir um desenvolvimento coeso e eficiente.

* **Front-end:** **React (Next.js)**
    * **Linguagem:** TypeScript
    * **Por quê?** Next.js oferece renderização do lado do servidor (SSR) e geração de sites estáticos (SSG), o que é excelente para SEO e performance. React possui um ecossistema gigante e facilita a criação de interfaces de usuário reativas e componentizadas.

* **Back-end:** **Node.js (NestJS)**
    * **Linguagem:** TypeScript
    * **Por quê?** NestJS é um framework Node.js robusto que impõe uma arquitetura modular e escalável (fortemente inspirado no Angular). Ele simplifica a criação de APIs eficientes e confiáveis, com suporte nativo a TypeScript.

* **Banco de Dados:** **PostgreSQL**
    * **Por quê?** Um banco de dados relacional poderoso e confiável, ideal para gerenciar dados estruturados como usuários, projetos e transações. Sua extensibilidade (com PostGIS para dados geográficos, por exemplo) pode ser útil no futuro.

* **Autenticação:** **Auth0** ou **NextAuth.js**
    * **Por quê?** Utilizar um serviço dedicado de autenticação aumenta a segurança e simplifica a implementação de login social (Google, LinkedIn), login com senha e autenticação de dois fatores.

* **Hospedagem & Infraestrutura (Cloud):** **Vercel** (para o Front-end) e **AWS** ou **Google Cloud** (para o Back-end e Banco de Dados)
    * **Por quê?** Vercel oferece uma integração perfeita com Next.js, facilitando o deploy e a escalabilidade do front-end. A AWS/GCP oferece um ecossistema completo de serviços (RDS para PostgreSQL, EC2/Fargate para o back-end) que garantem a escalabilidade e a segurança da aplicação.

## 3. Visão Geral dos Componentes (Arquitetura de Microsserviços)

A plataforma será dividida em serviços independentes que se comunicam via API. Isso facilita a manutenção e permite que cada parte do sistema escale de forma independente.

* **Serviço de Identidade (Authentication Service)**
    * **Responsabilidade:** Gerenciar o cadastro, login e perfis de todos os usuários (Empreendedores, Investidores, Administradores). Lida com senhas, tokens de acesso e dados pessoais.

* **Serviço de Projetos (Projects Service)**
    * **Responsabilidade:** Lida com todo o ciclo de vida de um projeto: criação, submissão para análise, atualização de status e visualização. Armazena todas as informações do projeto (descrição, métricas de impacto, documentos, etc.).

* **Serviço de Análise (Verification Service)**
    * **Responsabilidade:** O coração da credibilidade da Living Seed. Este serviço implementa as regras e o fluxo de trabalho para a verificação dos critérios de sustentabilidade. Ele se integra ao Serviço de Projetos para atribuir o "Selo Living Seed".

* **Serviço de Conexão (Matching & Messaging Service)**
    * **Responsabilidade:** Contém a lógica para sugerir projetos a investidores com base em suas teses de investimento. Também gerencia o sistema de mensagens seguras para a comunicação inicial entre as partes.

* **API Gateway**
    * **Responsabilidade:** Ponto de entrada único para todas as requisições do front-end. Ele direciona o tráfego para o serviço apropriado, simplificando a comunicação e adicionando uma camada extra de segurança.

## 4. Fluxo de Dados (Exemplo Simplificado)

Para ilustrar como os componentes interagem, vamos analisar o fluxo de um empreendedor cadastrando um novo projeto:

1.  **Cadastro de Projeto (Front-end):** O empreendedor preenche um formulário no aplicativo React.
2.  **Requisição à API:** Ao submeter, o front-end envia uma requisição `POST` para o **API Gateway**.
3.  **Roteamento:** O **API Gateway** verifica o token de autenticação do usuário com o **Serviço de Identidade** e, se válido, encaminha a requisição para o **Serviço de Projetos**.
4.  **Processamento:** O **Serviço de Projetos** valida os dados recebidos, cria uma nova entrada no banco de dados PostgreSQL com o status "Pendente de Análise" e notifica o **Serviço de Análise**.
5.  **Análise e Verificação:** O **Serviço de Análise** inicia seu fluxo de trabalho. Uma vez verificado, ele atualiza o status do projeto para "Verificado", permitindo que ele seja visível para os investidores através do **Serviço de Conexão**.
6.  **Resposta:** Uma resposta de sucesso é enviada de volta pelo mesmo caminho até o front-end, que exibe uma mensagem de confirmação ao usuário.
