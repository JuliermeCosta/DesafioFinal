### Arquitetura da API (C4 Model)

#### Nível 1: Diagrama de Contexto
```mermaid
flowchart LR
    classDef boundaryStyle fill:#2d3748,stroke:#4a5568,stroke-width:2px,color:#fff,font-weight:bold;
    classDef actorStyle fill:#08427b,stroke:#052e56,stroke-width:2px,color:#fff,font-weight:bold;
    classDef systemStyle fill:#1168bd,stroke:#0b4884,stroke-width:2px,color:#fff;
    classDef extSystemStyle fill:#2b6cb0,stroke:#1a365d,stroke-width:2px,color:#fff;

    Cliente["<b>Cliente / Consumidor API</b><br/><i>(Usuário / Sistema)</i><br/>Consome a API REST"]
        
    DesafioFinal["<b>DesafioFinal</b><br/><i>(.NET 10 API)</i>"]

    class Title boundaryStyle;
    class Cliente actorStyle;
    class DesafioFinal systemStyle;
    class SQLite extSystemStyle;

    Cliente -->|"Requisições REST<br/><i>[HTTP / JSON]</i>"| DesafioFinal
```

#### Nível 2: Diagrama de Contêineres
```mermaid
flowchart LR
    classDef boundaryStyle fill:#2d3748,stroke:#4a5568,stroke-width:2px,color:#fff,font-weight:bold;
    classDef actorStyle fill:#08427b,stroke:#052e56,stroke-width:2px,color:#fff,font-weight:bold;
    classDef containerStyle fill:#1168bd,stroke:#0b4884,stroke-width:2px,color:#fff;
    classDef dbStyle fill:#2b6cb0,stroke:#1a365d,stroke-width:2px,color:#fff;

    Cliente["<b>Cliente / Consumidor API</b><br/><i>(Usuário / Sistema)</i><br/>Envia requisições"]

    subgraph SystemBoundary ["DesafioFinal"]
        API["<b>DesafioFinal (.NET 10)</b><br/><i>(ASP.NET Core Web API)</i><br/>Endpoints Clientes, Pedidos e Produtos"]
            
        SQLite[("<b>SQLite Engine</b><br/><i>(SQLite In-Memory)</i><br/>Persistência em memória")]
    end

    class Title,SystemBoundary boundaryStyle;
    class Cliente actorStyle;
    class API containerStyle;
    class SQLite dbStyle;

    Cliente -->|"Consome REST<br/><i>[HTTP / JSON]</i>"| API
    API -->|"Consultas e persistência<br/><i>[EF Core / SQL]</i>"| SQLite
```

#### Nível 3: Diagrama de Componentes (Hexagonal / Ports e Adapters)
```mermaid
flowchart TD
    classDef boundaryStyle fill:#2d3748,stroke:#4a5568,stroke-width:2px,color:#fff,font-weight:bold;
    classDef componentStyle fill:#3182ce,stroke:#2b6cb0,stroke-width:1px,color:#fff;
    classDef dbStyle fill:#2b6cb0,stroke:#1a365d,stroke-width:2px,color:#fff;

    subgraph API ["DesafioFinal (.NET 10)"]
            
        subgraph AdapterIn ["Adapter - In"]
            Controllers["<b>Controllers</b><br/><i>(Clientes, Pedidos, Produtos)</i><br/>Endpoints REST"]
            Filters["<b>Filters</b><br/><i>(EnableBufferingAttribute)</i><br/>Buffering HTTP para PATCH"]
        end

        subgraph Application ["Application"]
            PortsIn["<b>Ports In e Facades</b><br/><i>(*IFacade / *Facade)</i><br/>Orquestração dos casos de uso"]
            AppMappers["<b>DTOs e Mappers</b><br/><i>(DTOs, Converters, Mappers)</i><br/>Transformação de dados"]
            PortsOut["<b>Ports Out</b><br/><i>(*IRepository)</i><br/>Contratos de persistência"]
        end

        subgraph Domain ["Domain"]
            Models["<b>Models e Services</b><br/><i>(Entities e Validators)</i><br/>Regras de negócio e validações"]
        end

        subgraph AdapterOut ["Adapter - Out"]
            Repos["<b>Repositories</b><br/><i>(Cliente, Pedido, Produto Repos)</i><br/>EF Core / Change Tracker"]
            DbContext["<b>Database Context</b><br/><i>(SqlLiteDbContext)</i><br/>Sessões e mapeamento SQLite"]
        end
    end

SQLite[("<b>SQLite Engine</b><br/><i>(SQLite In-Memory)</i><br/>Banco de dados relacional")]

    class Title,API,AdapterIn,Application,Domain,AdapterOut boundaryStyle;
    class Controllers,Filters,PortsIn,AppMappers,PortsOut,Models,Repos,DbContext componentStyle;
    class SQLite dbStyle;

    Controllers --> Filters
    Controllers --> PortsIn
    PortsIn --> AppMappers
    PortsIn --> Models
    PortsIn --> PortsOut
    PortsOut --> Repos
    Repos --> DbContext
    DbContext --> SQLite
```
