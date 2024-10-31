# ChurnAnalytics API

## Integrantes do Grupo
- **Macirander** - RM551416 - 2TDSPF
- **Carlos** - RM97528 - 2TDSPX
- **Munir** - RM550893 - 2TDSPF
- **Kaique** - RM551165 - 2TDSPX
- **Vinicius** - RM98839 - 2TDSPX

## Visão Geral
O **ChurnAnalytics** é uma API desenvolvida para gerenciar dados de empresas, permitindo operações CRUD (Create, Read, Update, Delete) e integração com funcionalidades de análise de sentimento para monitorar o feedback dos clientes. A arquitetura foi projetada para ser escalável e de fácil manutenção, aplicando **Clean Code** e princípios **SOLID**.

## Arquitetura

### Camada de Dados
Utiliza o **Entity Framework Core** para interagir com o banco de dados. O modelo de dados é definido na classe `CadastroEmpresa`, e o contexto do banco é gerenciado pela classe `DataContext`.

### Camada de Serviços
A lógica de negócios é manipulada em serviços dedicados:
- **PaymentService** para integração com o **Stripe**, permitindo a criação e gerenciamento de intents de pagamento.
- **SentimentModelTrainer** para análise de sentimento com **ML.NET**, que prevê o sentimento associado ao texto de feedback.
- **Auth0** para autenticação segura de usuários, configurada no `Program.cs` e utilizada nos endpoints protegidos da API.

### Camada de Apresentação
A API expõe endpoints RESTful usando **ASP.NET Core**. Cada endpoint é responsável por uma operação específica (CRUD, análise de sentimento e pagamento) e retorna dados no formato JSON.

## Design Patterns Utilizados

- **Repository Pattern**: Encapsula a lógica de acesso a dados e fornece uma interface para a camada de negócios interagir com o banco de dados.
- **Unit of Work Pattern**: Garante que todas as alterações de dados sejam feitas em uma transação, gerenciada pelo Entity Framework Core.
- **Controller**: Os controladores expõem as APIs e são responsáveis por gerenciar as solicitações HTTP e retornar respostas apropriadas.

## Arquitetura da API

### Abordagem Monolítica
O projeto está implementado como uma aplicação monolítica, onde toda a lógica da aplicação está centralizada em um único projeto.

#### Justificativas para a Abordagem Monolítica
- **Simplicidade na Arquitetura**
  - Menor Complexidade Inicial: Fácil de implementar e gerenciar.
  - Facilidade de Desenvolvimento e Testes: Desenvolvimento e testes mais diretos.

- **Custo e Recursos**
  - Menor Overhead de Infraestrutura: Reduz custos operacionais.
  - Facilidade de Implantação: Implantação simplificada.

- **Coerência e Coesão**
  - Integração e Consistência: A lógica e os dados estão encapsulados em uma única aplicação, garantindo consistência e integridade das operações.

## Práticas de Clean Code e SOLID

1. **Responsabilidade Única (SRP)**: Cada classe tem uma responsabilidade única, como `PaymentService` para pagamento e `SentimentModelTrainer` para análise de sentimento.
2. **Inversão de Dependência (DIP)**: Utilizamos injeção de dependência para desacoplar dependências externas, permitindo testes unitários eficientes.
3. **Interface Segregation (ISP)** e **Encapsulamento**: As interfaces da camada de dados seguem os princípios de Clean Code, mantendo o código organizado e fácil de manter.

## Funcionalidades de IA Generativa

- **Análise de Sentimento**: Implementamos um modelo de análise de sentimento utilizando ML.NET para analisar feedbacks de clientes e prever o sentimento associado ao texto fornecido. O modelo é treinado com dados de sentimento e utiliza um `PredictionEngine` para fazer previsões.

## Integração com Serviços Externos

1. **Stripe**: Integrado para criar intents de pagamento e processar transações. Configurado no `PaymentService` e usa a API RESTful do Stripe.
2. **Auth0**: Utilizado para autenticação de usuários e controle de acesso seguro, configurado no `Program.cs`.

## Testes Implementados

- **Testes Unitários e de Integração**: Utilizamos o xUnit para testes de unidade e integração, abrangendo os principais serviços:
  - `PaymentServiceTests` e `PaymentsControllerTests` para validar a integração com o Stripe.
  - `SentimentControllerTests` para garantir a precisão do modelo de análise de sentimento.

## Instruções para Rodar a API

### Pré-requisitos:
1. **.NET Core SDK**: Certifique-se de ter o .NET Core SDK instalado. Baixe e instale o [.NET SDK](https://dotnet.microsoft.com/download).
2. **Banco de Dados**: A API usa um banco de dados configurado no contexto `DataContext`. Certifique-se de que o banco de dados está acessível.

### Configuração

1. **Clone o Repositório**
    ```bash
    git clone https://github.com/KaiqueToschi/APIChurnAnalytics.git
    cd APIChurnAnalytics
    ```

2. **Restaurar Pacotes**
    ```bash
    dotnet restore
    ```

3. **Configurar Variáveis de Ambiente**
   - **Stripe**: Configure a chave secreta do Stripe na variável `Stripe:SecretKey` no arquivo `appsettings.json`.
   - **Auth0**: Configure o domínio (`Auth0:Domain`), client ID (`Auth0:ClientId`) e client secret (`Auth0:ClientSecret`) no `appsettings.json`.

4. **Aplicar Migrações**
   Se você estiver usando Entity Framework Core e tiver migrações, aplique-as ao banco de dados:
    ```bash
    dotnet ef database update
    ```

5. **Executar a API**
    ```bash
    dotnet run
    ```
   A API estará disponível em `http://localhost:5004` para HTTP e `https://localhost:7280` para HTTPS, ou conforme configurado no arquivo `launchSettings.json`.

### Testando os Endpoints

- **Obter todas as empresas**: `GET http://localhost:5004/api/CadastroEmpresa`
- **Obter uma empresa por ID**: `GET http://localhost:5004/api/CadastroEmpresa/{id}`
- **Criar uma nova empresa**: `POST http://localhost:5004/api/CadastroEmpresa`
- **Atualizar uma empresa**: `PUT http://localhost:5004/api/CadastroEmpresa/{id}`
- **Deletar uma empresa**: `DELETE http://localhost:5004/api/CadastroEmpresa/{id}`
- **Analisar Sentimento**: `POST http://localhost:5004/api/Sentiment/analyze`  
  Envia um texto para análise de sentimento e recebe uma previsão de sentimento positivo ou negativo.
- **Processar Pagamento com Stripe**: `POST http://localhost:5004/api/Payments`  
  Envia uma requisição para criar um intent de pagamento usando o Stripe.

### Futuro Planejado
**Microserviços**  
Em um estágio futuro, planejamos migrar para uma arquitetura de microserviços, separando componentes de feedback e análise de sentimento para maior escalabilidade e modularidade.
