<h1 align="center">
   Api InvoiceImporter
</h1>

</br>
  <p align="center">Api grava dados extraidos de arquivo CSV extraidos de uma fatura de cartão de crédito utilizando ASP.NET CORE</p>
  
## :white_check_mark: Features

* Web Api construída com ASP.Net Core API
* CRUD utilizando ORM Entity Framework Core
* Utilizado em repository patterns e interfaces para fazer gestão de desacoplamento


## :globe_with_meridians: Tecnologias e Conceitos Implementados

Esse projeto foi desenvolvido usando as seguintes tecnologias:

- ASP.NET Core 7
- Entity Framework Core 7.0

Conceitos/Técnicas utilizadas:
- Arquitetura Limpa;
- Command and Query Responsibility Segregation (CQRS);
- Repository Pattern;
- Injeção de Dependências;
- Test Driven Development (TDD),
- Driven Domain Design (DDD).

## :gear: Arquitetura

```🌐
InvoiceImporter.API
├── 📂 Controllers      [Rotas para endpoints]
```🌐
InvoiceImporter.Domain
├── 📂 Adapters         [Interfaces]
├── 📂 Commands         [Dominío dos Comandos]
├── 📂 Entities         [Dominío de Entidades]
├── 📂 Enum             [Enumerados]
├── 📂 Handlers         [Manipuladores] 
├── 📂 Settings         [Modelo de Configuração]
├── 📂 ValueObjects     [Objetos de Valor]
```🌐
InvoiceImporter.Domain.Infra
├── 📂 Context          [Classes base para comunicação com o BD]
├── 📂 Mapping          [Mapeamento das Entidades para a persistência no BD através do Entity Framework]
├── 📂 Migrations       [Migrações geradas através do Entity Framework]
├── 📂 Repositories     [Repository Pattern]
```🌐 InvoiceImporter.Domain.Shared
├── 📂 Command          [Interface para comandos]
├── 📂 Entities         [Classe base para entidades]  
├── 📂 Handler          [Interface para manipulador]
```🧪 InvoiceImporter.Domain.Tests
├── 📂 Common           [Classe com Funções comuns para testes]
├── 📂 Files            [Arquivos utilitários para uso em testes]
├── 📂 Tests            [Testes unitários das entidades]




