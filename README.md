<div id="top"></div>

<br/>
<div align="center">
    <img src="./readme-img/csharp-original.svg" alt="Logo" width="100" height="100" />
    <img src="./readme-img/dotnetcore-original.svg" alt="Logo" width="100" height="100" />
    <h1 align="center">ASP.NET Core Web API</h1>
    <p align="center">Desenvolvimento de uma Web API em ASP.NET Core</p>
</div>

<br/>

<div align="center">
    <a href="https://github.com/YuriSiman/complete-api-aspnetcore-webapi/blob/master/LICENSE" target="_blank">
      <img alt="LICENSE" src="https://img.shields.io/badge/license-mit-%23A6CE39?style=for-the-badge&logo=github" />
    </a>
    <a href="https://github.com/YuriSiman" target="_blank">
      <img alt="GitHub" src="https://img.shields.io/badge/github-perfil-%237159c1?style=for-the-badge&logo=github" />
    </a>
    <a href="https://yurisiman.com.br" target="_blank">
      <img alt="Site" src="https://img.shields.io/badge/site-yurisiman-E0A80D?style=for-the-badge&logo=Purism" />
    </a>
    <a href="https://www.linkedin.com/in/yurisiman/" target="_blank">
      <img alt="Linkedin" src="https://img.shields.io/badge/linkedin-social-0A66C2?style=for-the-badge&logo=LinkedIn" />
    </a>
    <a href="mailto:contato@yurisiman.com.br" target="_blank">
      <img alt="Gmail" src="https://img.shields.io/badge/email-contato-EA4335?style=for-the-badge&logo=Gmail" />
    </a>
</div>

<br/>

## :clipboard: Sobre o Projeto

O objetivo deste projeto é implementar uma API em ASP.NET Core utilizando C#, a aplicação contém um CRUD completo dos dados, serializando em JSON, validando JWT, utilizando Entity Framework para persistência, Fluent API, HealthChecks e outras tecnologias.

Use este projeto para aprender e contribua com melhorias! Bora estudar! :computer::coffee:

---

## :pencil: Pré-requisitos

1. Construído com .NET 6.0 e codificado em C#, se você não possui o dotnet instalado, acesse [aqui](https://dotnet.microsoft.com/) e instale a versão mais recente.
2. Clone este repositório em sua máquina local

   ```sh
   git clone https://github.com/YuriSiman/complete-api-aspnetcore-webapi.git
   ```

---

## :dart: Tópicos

<details>
  <summary>Configurações Iniciais</summary>
  <ul>
    <li><a href="#setup-inicial-da-aplicacao">Setup Inicial da Aplicação</a></li>
    <li><a href="#instalacao-de-pacotes">Instalação de Pacotes</a></li>
    <li><a href="#referencia-de-projetos">Referência de Projetos</a></li>
    <li><a href="#entidades">Entidades</a></li>
    <li><a href="#variaveis-de-ambiente">Variáveis de Ambiente</a></li>
    <li><a href="#configurations">Configurations</a></li>
  </ul>
</details>
<details>
  <summary>Banco de Dados</summary>
  <ul>
    <li><a href="#dbcontext">DbContext</a></li>
    <li><a href="#migrations">Migrations</a></li>
    <li><a href="#mappings">Mappings</a></li>
  </ul>
</details>
<details>
  <summary>Mapeamento de Objetos</summary>
  <ul>
    <li><a href="#viewmodels">ViewModels (DTO)</a></li>
    <li><a href="#automapper">AutoMapper</a></li>
  </ul>
</details>
<details>
  <summary>Controllers</summary>
  <ul>

  </ul>
</details>

---

## :rocket: Vamos Começar

### Configurações Iniciais

<div id="setup-inicial-da-aplicacao"></div>

### Setup Inicial da Aplicação    

A aplicação consiste em três camadas:

Api - configuração do projeto ASP.NET Core Web API. Nele está contido as configurações da aplicação, nossas Controllers, o Identity para autenticação de usuários, configurações de ambiente, nossa classe Startup e nosso método Main. Ela será a camada que fará toda a comunicação e tráfego de dados.

Business - configuração de uma Class Library .NET Core para as regras de negócio da aplicação, camada de domínio. Onde se encontra as Entidades de negócio, notificações, validações e serviços.

Data - configuração de uma Class Library .NET Core para a camada de dados da aplicação, nele está contido o DbContext para o contexto de dados, as referências ao Entity Framework, Mappings, Migrations e Repositórios.

<p align="right"><a href="#top">Início ↑</a></p>

---

<div id="instalacao-de-pacotes"></div>

### Instalação de Pacotes  

Pacotes a serem instalados pelo Package Manager Console ou Manage NuGet Packages:

Projeto - Camada Api

```sh
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Automapper.Extensions.Microsoft.DependencyInjection
```

Projeto - Camada Data

```sh
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Relational
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

<p align="right"><a href="#top">Início ↑</a></p>

---

<div id="referencia-de-projetos"></div>

### Referência de Projetos

Projeto - Camada Api: Referência com o projeto Business e Data  
Projeto - Camada Data: Referência com o projeto Business  

<p align="right"><a href="#top">Início ↑</a></p>

---

<div id="entidades"></div>

### Entidades 

Modelo Entidade-Relacionamento

<img src="./readme-img/mer.png" alt="Logo" width="500" height="500" />

<p align="right"><a href="#top">Início ↑</a></p>

---

<div id="variaveis-de-ambiente"></div>

### Variáveis de Ambiente  

Alterando o construtor da Startup, para que se possa permitir a configuração de appsettings para cada tipo de ambiente.

```csharp
public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }
```

Craindo arquivos appsettings para cada tipo de ambiente:

- appsettings.Development.json
- appsettings.Staging.json
- appsettings.Production.json

Modificando arquivo launchSettings.json para cada tipo de ambiente:

```csharp
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:50162",
      "sslPort": 44363
    }
  },
  "profiles": {
    "IIS - Dev": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation"
      }
    },
    "IIS - Staging": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging",
        "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation"
      }
    },
    "IIS - Prod": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Production",
        "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation"
      }
    },
    "Self Hosting": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation"
      },
      "dotnetRunMessages": "true",
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    },
    "Docker": {
      "commandName": "Docker",
      "launchBrowser": true,
      "launchUrl": "{Scheme}://{ServiceHost}:{ServicePort}",
      "publishAllPorts": true,
      "useSSL": true
    }
  }
}
```

<p align="right"><a href="#top">Início ↑</a></p>  

---

<div id="configurations"></div>

### Configurations  

Implementando pasta Configurations onde serão criadas as classes de configuração da Startup, tendo como objetivo desacoplar a classe Startup, deixando-a mais limpa e reduzida. As classes de Configuração precisarão implementar métodos de extensão do IServiceCollection, IConfiguration, IApplicationBuilder e IHostEnvironment. As configurações irão variar conforme a sua necessidade. Segue abaixo exemplo de configuração do DbContext.

DbContextConfig:

```csharp
namespace CompleteApi.Api.Configurations
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MvcDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
```

Startup:

```csharp
public void ConfigureServices(IServiceCollection services)
{
   services.AddDbContextConfiguration(Configuration);
}
```

Exemplos de Configurations a serem implementadas:

- DbContextConfig
- DependencyInjectionConfig
- SwaggerConfig
- MvcConfig

<p align="right"><a href="#top">Início ↑</a></p>

---

### Banco de Dados

<div id="dbcontext"></div>

### DbContext    

Contexto de Dados
O seu contexto de dados deve herdar da classe DbContext, implementando as propriedades DbSet referente a cada entidade da sua aplicação. Deve-se sobrescrever o método OnModelCreating, para que nele possamos pegar nosso contexto de dados, buscar todas as entidades mapeadas pelo DbSet e buscar classes que implementam a interface IEntityTypeConfiguration, ou seja, ele pegará cada um dos Mappings a serem implementados e fará o mapeamento de uma vez só.

No método OnModelCreating também podemos desabilitar o Cascade Delete, ou seja, desabilitar a exclusão de objetos ligados diretamente a uma outra entidade. Ex: excluir um fornecedor e todos os seus produtos juntos.

Configurando seu DbContext na configuração da classe Startup - DbContextConfig
É necessário configurar o serviço do seu contexto de dados dentro da sua classe Startup, no método ConfigureServices. Para isso, iremos implementar dentro da classe de configuração DbContextConfig. Segue exemplo de implementação abaixo.

DbContextConfig:

```csharp
namespace CompleteApi.Api.Configurations
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MvcDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
```

Startup:

```csharp
services.AddDbContextConfiguration(Configuration);
```

Também é preciso configurar o serviço para injeção de dependência do seu DbContext na classe Startup, no método ConfigureServices, para isso, criaremos uma nova classe de configuração chamada DependencyInjectionConfig. E lá, faremos a injeção de dependência.

```csharp
namespace CompleteApi.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MvcDbContext>();

            return services;
        }
    }
}
```

Depois, chamaremos o serviço dentro da Startup:

```csharp
services.ResolveDependencies();
```

Configurando o arquivo appsettings.json

Após a implementação do DbContext na Startup, é necessário passar as informações do banco de dados na ConnectionStrings dentro do arquivo appsettings.json. Essa ConnectionString possui a DefaultConnection que é chamada dentro do serviço que adiciona o seu contexto de dados na sua classe Startup.

```csharp
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SeuDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

<p align="right"><a href="#top">Início ↑</a></p>

---

<div id="migrations"></div>

### Migrations  

Package Manager Console

Gerando Migrations

```sh
Add-Migration NomeMigration -Context SeuDbContext
```

Gerando Base de Dados

```sh
Update-Database -Context SeuDbContext
```

Gerando Scripts Idempotentes

```sh
Script-Migration -Idempotent
```

<p align="right"><a href="#top">Início ↑</a></p>

---

<div id="mappings"></div>

### Mappings

Criando o mapeamento e relacionamento das entidades no banco de dados...

<p align="right"><a href="#top">Início ↑</a></p>

---

### Mapeamento de Objetos

<div id="viewmodels"></div>

### ViewModels (DTO)    

Implementar nossas ViewsModels para não expor diretamente nossas entidades para a camada de apresentação da API, nós podemos personalizar a forma como queremos tratar as propriedades de nossas Models por meio de ViewModels (DTOs).

**Obs:** Tomar cuidado com referências cíclicas dentro das ViewModels (DTOs), pois, na hora de formatar o JSON, caso duas DTOs possuam uma referência para cada uma, seria como um nó dentro de outro nó em um loop infinito. Então não precisamos carregar o tipo da entidade, e sim, por exemplo, somente o seu nome (ou outra propriedade).

<p align="right"><a href="#top">Início ↑</a></p>

---

<div id="automapper"></div>

### AutoMapper  

Package Manager Console

Projeto - Camada App

```sh
Install-Package Automapper.Extensions.Microsoft.DependencyInjection
```

Configurando o Automapper na classe Startup

É preciso configurar o serviço do AutoMapper na classe Startup, no método ConfigureServices, conforme o exemplo abaixo:

```csharp
services.AddAutoMapper(typeof(Startup));
```

Devemos criar uma classe AutoMapperConfig para configuração do Automapper, a classe deverá herdar de Profile. Nesta classe será definido o mapeamento das ViewModels e Models, segue exemplo abaixo:

```csharp
public AutoMapperConfig()
{
   CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
   CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
   CreateMap<Produto, ProdutoViewModel>().ReverseMap();
   CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
}
```

<p align="right"><a href="#top">Início ↑</a></p>

---

## :vertical_traffic_light: Status do Projeto

:construction: Projeto em construção :construction:

---

## :thinking: Contribuindo

> Passo a passo de como contribuir...

### Passo 1

* :fork_and_knife: Fork este repositório!

### Passo 2

* :dancers: Clone este repositório para sua máquina local usando `git clone https://github.com/YuriSiman/complete-api-aspnetcore-webapi.git`

### Passo 3

* :trident: Crie sua feature branch usando `git checkout -b minha-feature`

### Passo 4

* :white_check_mark: Commit suas mudanças usando `git commit -m "feat: Minha nova feature"`

### Passo 5

* :pushpin: Dê um push usando `git push -u origin minha-feature`

### Passo 6

* :arrows_clockwise: Crie um novo pull request

Depois que seu pull request for mesclado, você pode excluir sua feature branch  

> Caso tenha dúvidas, confira este guia de como [contribuir no GitHub](https://github.com/firstcontributions/first-contributions)  

---

## :speech_balloon: Suporte

> Entre em contato comigo...  

* Me chame pelo [Linkedin](https://www.linkedin.com/in/yurisiman/)  
* Me mande um e-mail [contato@yurisiman.com.br](mailto:contato@yurisiman.com.br)  

---

## :pencil: Licença

<a href="https://github.com/YuriSiman/complete-api-aspnetcore-webapi/blob/master/LICENSE" target="_blank">
  <img alt="LICENSE" src="https://img.shields.io/badge/license-mit-%23A6CE39?style=for-the-badge&logo=github" />
</a>

##

Code your life :octocat:

<p align="right"><a href="#top">Início ↑</a></p>
