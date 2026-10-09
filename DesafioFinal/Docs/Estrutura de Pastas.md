# Estrutura de Pastas: DesafioFinal

## Visão geral

```text
DesafioFinal/
├── DesafioFinal.slnx
└── DesafioFinal/
    ├── Docs/
    ├── Properties/
    │   └── launchSettings.json
    ├── Adapter/
    │   ├── In/
    │   │   ├── Controllers/
    │   │   └── Filters/
    │   └── Out/
    │       └── Database/
    │           ├── Configuration/
    │           ├── Context/
    │           ├── Entity/
    │           │   └── Common/
    │           ├── Mapper/
    │           └── Repository/
    │               └── Common/
    ├── Application/
    │   ├── DTO/
    │   │   ├── Cliente/
    │   │   ├── Pedido/
    │   │   ├── Produto/
    │   │   └── Common/
    │   │       └── Converters/
    │   ├── Mapper/
    │   ├── Port/
    │   │   ├── In/
    │   │   └── Out/
    │   │       └── Common/
    │   └── Usecase/
    ├── Domain/
    │   ├── Model/
    │   │   └── Common/
    │   └── Service/
    ├── Config/
    │   ├── DatabaseConfig.cs
    │   ├── DependencyInjection.cs
    │   └── GlobalExceptionHandler.cs
    ├── Program.cs
    ├── appsettings.json
    └── appsettings.Development.json
```

## Raiz

| Item | Descrição |
|------|-----------|
| `DesafioFinal.slnx` | Arquivo de solução da aplicação (.NET 10) |
| `DesafioFinal/` | Projeto principal da API |

## Pastas do projeto `DesafioFinal/`

### Nível superior

| Pasta | O que representa |
|-------|------------------|
| `Docs/` | Documentação do projeto: C4 Model (MarkDown, PDF e draw.io) e estrutura de pastas |
| `Properties/` | Configurações de execução local do projeto |
| `Adapter/` | Camada de adaptadores: a ligação da aplicação com o mundo externo (entrada HTTP e saída para o banco de dados) |
| `Application/` | Camada de aplicação: casos de uso, contratos (portas), DTOs e mapeamentos |
| `Domain/` | Camada de domínio: regras e entidades de negócio, sem dependência de infraestrutura |
| `Config/` | Configurações de infraestrutura e inicialização da aplicação |

### Adapter

| Pasta | O que representa |
|-------|------------------|
| `Adapter/In/` | Adaptadores de entrada: tudo que recebe requisições externas |
| `Adapter/In/Controllers/` | Controladores HTTP REST de Clientes, Pedidos e Produtos |
| `Adapter/In/Filters/` | Filtros de requisição e middlewares (ex.: `EnableBufferingAttribute`) |
| `Adapter/Out/` | Adaptadores de saída: tudo que a aplicação chama para fora |
| `Adapter/Out/Database/` | Infraestrutura de persistência em banco de dados (SQLite com EF Core) |
| `Adapter/Out/Database/Configuration/` | Mapeamentos do EF Core via Fluent API (um por entidade) |
| `Adapter/Out/Database/Context/` | Contexto do banco de dados (`SqlLiteDbContext`) |
| `Adapter/Out/Database/Entity/` | Entidades de banco de dados (representação das tabelas) |
| `Adapter/Out/Database/Entity/Common/` | Classe base compartilhada pelas entidades (`EntityBase`) |
| `Adapter/Out/Database/Mapper/` | Mapeadores entre modelos de domínio e entidades de persistência |
| `Adapter/Out/Database/Repository/` | Implementações concretas dos repositórios |
| `Adapter/Out/Database/Repository/Common/` | Repositório base reaproveitado pelos demais (`BaseRepository`) |

### Application

| Pasta | O que representa |
|-------|------------------|
| `Application/DTO/` | Objetos de transferência de dados trocados com a API |
| `Application/DTO/Cliente/`, `Pedido/`, `Produto/` | DTOs de requisição (POST, PUT, PATCH) e de resposta de cada recurso |
| `Application/DTO/Common/` | DTO base de resposta compartilhado |
| `Application/DTO/Common/Converters/` | Conversores customizados de serialização (CPF, data formatada, decimal com duas casas) |
| `Application/Mapper/` | Conversão entre DTOs e modelos de domínio |
| `Application/Port/` | Portas: contratos de comunicação da aplicação (arquitetura hexagonal) |
| `Application/Port/In/` | Interfaces de entrada expostas aos Controllers (`I*Facade`) |
| `Application/Port/Out/` | Interfaces de saída implementadas pela infraestrutura (`I*Repository`) |
| `Application/Port/Out/Common/` | Contrato base dos repositórios (`IBaseRepository`) |
| `Application/Usecase/` | Implementação dos casos de uso e orquestração do fluxo de negócio (`*Facade`) |

### Domain

| Pasta | O que representa |
|-------|------------------|
| `Domain/Model/` | Modelos de domínio: Cliente, Pedido e Produto |
| `Domain/Model/Common/` | Classe base dos modelos (`ModelBase`) |
| `Domain/Service/` | Serviços de domínio com as validações das regras de negócio |

## Arquivos importantes

### Raiz do projeto

| Arquivo | Descrição |
|---------|-----------|
| `Program.cs` | Ponto de entrada da aplicação: configura o servidor e o pipeline HTTP |
| `appsettings.json` | Configurações de ambiente da aplicação |
| `appsettings.Development.json` | Sobrescreve as configurações do `appsettings.json` no ambiente de desenvolvimento |

### `Properties/`

| Arquivo | Descrição |
|---------|-----------|
| `launchSettings.json` | Perfis de execução local (URLs, portas, variáveis de ambiente e comportamento ao iniciar a aplicação no Visual Studio / `dotnet run`) |

### `Config/`

| Arquivo | Descrição |
|---------|-----------|
| `DatabaseConfig.cs` | Configuração e inicialização do banco de dados (SQLite) |
| `DependencyInjection.cs` | Registro das dependências no container de injeção (facades, repositórios, serviços de domínio, etc.) |
| `GlobalExceptionHandler.cs` | Tratamento global de exceções, padronizando as respostas de erro da API |
