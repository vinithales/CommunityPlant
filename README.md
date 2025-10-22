# CommunityPlant - Plataforma de Plantação Comunitária

## Visão Geral

O CommunityPlant é uma plataforma web desenvolvida em .NET 8 que permite o gerenciamento colaborativo de jardins comunitários. A plataforma facilita o planejamento, cultivo e manutenção de plantas de forma colaborativa entre membros de uma comunidade.

## Funcionalidades Implementadas

### 1. Gestão de Usuários
- **Cadastro e autenticação** de usuários com diferentes níveis de acesso
- **Perfis de usuário** com informações pessoais e de contato
- **Tipos de usuário**: Administrator, Manager, Voluntary
- **Sistema de senhas** com hash SHA256 para segurança

### 2. Gestão de Jardins Comunitários
- **Criação de jardins** com informações detalhadas (nome, localização, área, tipo de solo)
- **Jardins públicos e privados** para controle de acesso
- **Associação de usuários** aos jardins através do sistema de participação
- **Histórico de atividades** em cada jardim

### 3. Sistema de Participação
- **Adesão a jardins** com diferentes papéis (Admin, Volunteer, Observer)
- **Controle de participações ativas** e históricas
- **Gerenciamento de permissões** baseado no papel do usuário

### 4. Catálogo de Plantas
- **Banco de dados de plantas** com informações botânicas completas
- **Classificação por tipos** (Vegetable, Fruit, Herb, Flower, etc.)
- **Informações de cultivo**: dias para colheita, época de plantio, frequência de irrigação
- **Requisitos ambientais**: luz solar, tipo de solo, espaçamento
- **Sistema de busca** por nome científico ou comum

### 5. Gestão de Cultivos
- **Registro de plantios** com data, quantidade e localização no jardim
- **Acompanhamento do crescimento** com status do cultivo
- **Previsão de colheita** baseada nos dados da planta
- **Histórico de colheitas** com datas reais vs. previstas

### 6. Sistema de Tarefas
- **Criação de tarefas** relacionadas ao jardim
- **Atribuição de responsáveis** para cada tarefa
- **Controle de status** (Pending, In Progress, Completed)
- **Priorização** (Low, Medium, High)
- **Histórico de ações** em cada tarefa

### 7. Dados Meteorológicos
- **Registro de condições climáticas** por jardim
- **Dados de temperatura, umidade e precipitação**
- **Histórico meteorológico** para análise de padrões
- **Informações de vento** (direção e velocidade)

## Arquitetura Técnica

### Estrutura do Projeto
```
CommunityPlant/
├── Domain/
│   ├── Entities/          # Modelos de domínio
│   ├── Enums/            # Enumeradores
│   └── Interfaces/       # Contratos de repositório
├── Application/
│   ├── DTOs/             # Objetos de transferência de dados
│   ├── Services/         # Lógica de negócio
│   ├── Mappings/         # Mapeamentos AutoMapper
│   └── API/Controllers/  # Controllers da API
└── Infrastructure/
    ├── Data/             # Contexto do banco de dados
    └── Repositories/     # Implementações dos repositórios
```

### Tecnologias Utilizadas
- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM para acesso a dados
- **MySQL** - Banco de dados relacional
- **AutoMapper** - Mapeamento de objetos
- **Swagger/OpenAPI** - Documentação da API

### Padrões Implementados
- **Clean Architecture** - Separação de responsabilidades
- **Repository Pattern** - Abstração do acesso a dados
- **Service Pattern** - Encapsulamento da lógica de negócio
- **DTO Pattern** - Transferência de dados entre camadas
- **Dependency Injection** - Inversão de controle

## API Endpoints

### Usuários (`/api/user`)
- `POST /` - Criar usuário
- `GET /{id}` - Buscar por ID
- `GET /email/{email}` - Buscar por email
- `GET /` - Listar todos usuários
- `PUT /{id}` - Atualizar usuário
- `DELETE /{id}` - Desativar usuário
- `POST /validate-credentials` - Validar credenciais

### Jardins (`/api/garden`)
- `POST /` - Criar jardim
- `GET /{id}` - Buscar por ID
- `GET /` - Listar todos jardins
- `GET /public` - Listar jardins públicos
- `GET /user/{userId}` - Jardins do usuário
- `PUT /{id}` - Atualizar jardim
- `DELETE /{id}` - Desativar jardim

### Plantas (`/api/plant`)
- `POST /` - Cadastrar planta
- `GET /{id}` - Buscar por ID
- `GET /` - Listar todas plantas
- `GET /type/{type}` - Buscar por tipo
- `GET /search/{term}` - Buscar por termo
- `PUT /{id}` - Atualizar planta
- `DELETE /{id}` - Desativar planta

### Tarefas (`/api/task`)
- `POST /` - Criar tarefa
- `GET /{id}` - Buscar por ID
- `GET /garden/{gardenId}` - Tarefas do jardim
- `PUT /{id}/complete` - Completar tarefa

## Modelo de Dados

### Principais Entidades

#### User (Usuário)
- Informações pessoais e de contato
- Tipo de usuário (Administrator, Manager, Voluntary)
- Sistema de autenticação com senha hasheada
- Relacionamentos com jardins e tarefas

#### Garden (Jardim)
- Informações básicas (nome, localização, descrição)
- Dados técnicos (área, tipo de solo)
- Controle de visibilidade (público/privado)
- Relacionamentos com usuários, plantas e tarefas

#### Plant (Planta)
- Informações botânicas (nome científico, tipo)
- Dados de cultivo (dias para colheita, época de plantio)
- Requisitos ambientais (luz, solo, água)
- Instruções de cuidado

#### PlantedCrop (Cultivo)
- Registro de plantio específico
- Acompanhamento de crescimento
- Previsão e registro de colheita
- Localização no jardim

#### Task (Tarefa)
- Descrição e prioridade
- Atribuição de responsáveis
- Controle de status e datas
- Histórico de alterações

## Próximos Passos

### Funcionalidades Sugeridas
1. **Sistema de Notificações** - Alertas para colheitas próximas
2. **Relatórios e Analytics** - Produtividade dos jardins
3. **Sistema de Trocas** - Troca de sementes/mudas entre usuários
4. **Integração com APIs Climáticas** - Dados meteorológicos automáticos
5. **Aplicativo Mobile** - Acesso via smartphone
6. **Sistema de Gamificação** - Pontuações e conquistas
7. **Marketplace** - Venda de excedentes da produção

### Melhorias Técnicas
1. **Autenticação JWT** - Sistema de tokens para segurança
2. **Testes Unitários** - Cobertura de testes
3. **Cache** - Performance com Redis
4. **Logs Estruturados** - Monitoramento com Serilog
5. **CI/CD Pipeline** - Deploy automatizado
6. **Docker** - Containerização da aplicação

## Como Executar

1. **Configurar Connection String** no `appsettings.json`
2. **Executar Migrations**: `dotnet ef database update`
3. **Executar a aplicação**: `dotnet run`
4. **Acessar Swagger**: `https://localhost:7xxx/swagger`

A plataforma CommunityPlant fornece uma base sólida para o gerenciamento de jardins comunitários, promovendo a colaboração entre usuários e facilitando o cultivo sustentável de plantas.