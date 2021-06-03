<h1 align="center">API Completa em ASP.NET Core</h1>

<p align="center">Aplicação completa em ASP.NET Core Web API</p>

---

### :dart: Objetivo

Tenho como objetivo implementar uma API completa em ASP.NET Core com C# contendo um CRUD completo dos dados, serializando em JSON, validando JWT, utilizando Entity Framework para persistência de dados, Fluent API e outras tecnologias. 

### Clone

Clone este repositório em sua máquina local usando:  

```
git clone https://github.com/YuriSiman/complete-api-aspnetcore-webapi.git
```

### :pencil2: Progresso

- [x] [Setup Inicial da Aplicação](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#setup-inicial-da-aplicação)  
- [x] [Instalar Pacotes](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#instalar-pacotes)  
- [x] [Adicionar Referências aos Projetos](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#adicionar-referências-aos-projetos)  
- [x] [Definir as entidades da aplicação](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#definir-as-entidades-da-aplicação)  
- [x] [Configurando Variáveis de Ambiente](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#configurando-variáveis-de-ambiente)  
- [x] [Configurations](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#configurations)  
- [x] [Configurar seu DbContext](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#configurar-seu-dbcontext)  
- [x] [Gerar Migrations, Data Base e Scripts](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#gerar-migrations-data-base-e-scripts)  
- [x] [ViewModels ou DTOs](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#viewmodels-ou-dtos)  
- [x] [AutoMapper](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#automapper)  
- [x] [Controllers](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#controllers)  

---

## :rocket: Vamos Começar 

## Setup Inicial da Aplicação 

A aplicação consiste em três camadas:  

**Api** - configuração do projeto ASP.NET Core Web API. Nele está contido as configurações da aplicação, nossas Controllers, o Identity para autenticação de usuários, configurações de ambiente, nossa classe Startup e nosso método Main. Ela será a camada que fará toda a comunicação e tráfego de dados.  
**Business** - configuração de uma Class Library .NET Core para as regras de negócio da aplicação, camada de domínio. Onde se encontra as Entidades de negócio, notificações, validações e serviços.  
**Data** - configuração de uma Class Library .NET Core para a camada de dados da aplicação, nele está contido o DbContext para o contexto de dados, as referências ao Entity Framework, Mappings, Migrations e Repositórios.  

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Instalar Pacotes

Pacotes a serem instalados pelo Package Manager Console ou Manage NuGet Packages:  

Projeto - Camada Api  
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Automapper.Extensions.Microsoft.DependencyInjection
```
  
Projeto - Camada Data  
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Relational
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Adicionar Referências aos Projetos  

Projeto - Camada Api

- Referência com o projeto Business e Data

Projeto - Camada Data

- Referência com o projeto Business

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Definir as entidades da aplicação

Modelo Entidade-Relacionamento conforme a utilização do Entity Framework.

<img src="./readme-images/entidade-relacionamento.png" />

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Configurando Variáveis de Ambiente

Alterando o **construtor da Startup**, para que se possa permitir a configuração de **appsettings** para cada tipo de ambiente:

```
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

Craindo arquivos **appsettings** para cada tipo de ambiente: 

- appsettings.Development.json
- appsettings.Staging.json
- appsettings.Production.json

Modificando arquivo **launchSettings.json** para cada tipo de ambiente:

```
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

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Configurations

Implementando pasta Configurations onde serão criadas as classes de configuração da Startup, tendo como objetivo desacoplar a classe Startup, deixando-a mais limpa e reduzida. As classes de Configuração precisarão implementar métodos de extensão do IServiceCollection, IConfiguration, IApplicationBuilder e IHostEnvironment. As configurações irão variar conforme a sua necessidade. Segue abaixo exemplo de configuração do DbContext.

DbContextConfig:

```
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

```
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

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Configurar seu DbContext

#### Contexto de Dados

O seu contexto de dados deve herdar da classe DbContext, implementando as propriedades DbSet referente a cada entidade da sua aplicação. Deve-se sobrescrever o método OnModelCreating, para que nele possamos pegar nosso contexto de dados, buscar todas as entidades mapeadas pelo DbSet e buscar classes que implementam a interface IEntityTypeConfiguration, ou seja, ele pegará cada um dos Mappings a serem implementados e fará o mapeamento de uma vez só.  

No método OnModelCreating também podemos **desabilitar** o **Cascade Delete**, ou seja, desabilitar a exclusão de objetos ligados diretamente a uma outra entidade. Ex: excluir um fornecedor e todos os seus produtos juntos.

#### Configurando seu DbContext na configuração da classe Startup - DbContextConfig

É necessário configurar o serviço do seu contexto de dados dentro da sua classe Startup, no método ConfigureServices. Para isso, iremos implementar dentro da classe de configuração DbContextConfig. Segue exemplo de implementação abaixo.

DbContextConfig:

```
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

```
	services.AddDbContextConfiguration(Configuration);

```

Também é preciso configurar o serviço para injeção de dependência do seu DbContext na classe Startup, no método ConfigureServices, para isso, criaremos uma nova classe de configuração chamada DependencyInjectionConfig. E lá, faremos a injeção de dependência.

```
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

``` 
	services.ResolveDependencies();
```

#### Configurando o arquivo appsettings.json

Após a implementação do DbContext na Startup, é necessário passar as informações do banco de dados na **ConnectionStrings** dentro do arquivo appsettings.json. Essa **ConnectionString** possui a **DefaultConnection** que é chamada dentro do serviço que adiciona o seu contexto de dados na sua classe Startup.  

```
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SeuDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Gerar Migrations, Data Base e Scripts

Package Manager Console  

Gerando Migrations  

```
Add-Migration NomeMigration -Context SeuDbContext
```

Gerando Base de Dados  

```
Update-Database -Context SeuDbContext
```

Gerando Scripts Idempotentes  

```
Script-Migration -Idempotent
```

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## ViewModels ou DTOs

Implementar nossas ViewsModels para não expor diretamente nossas entidades para a camada de apresentação da API, nós podemos personalizar a forma como queremos tratar as propriedades de nossas Models por meio de ViewModels (DTOs).

Obs: Tomar cuidado com referências cíclicas dentro das ViewModels (DTOs), pois, na hora de formatar o JSON, caso duas DTOs possuam uma referência para cada uma, seria como um nó dentro de outro nó em um loop infinito. Então não precisamos carregar o tipo da entidade, e sim, por exemplo, somente o seu nome (ou outra propriedade).

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## AutoMapper

Package Manager Console  

Projeto - Camada App  

```
Install-Package Automapper.Extensions.Microsoft.DependencyInjection
```

Fazendo a transformação de **ViewModel** para **Model** e **Model** para **ViewModel** com **Automapper**.

#### Configurando o Automapper na classe Startup

É preciso configurar o serviço do AutoMapper na classe Startup, no método ConfigureServices, conforme o exemplo abaixo:  

```
services.AddAutoMapper(typeof(Startup));
```

Devemos criar uma classe AutoMapperConfig para configuração do Automapper, a classe deverá herdar de **Profile**. Nesta classe será definido o mapeamento das ViewModels e Models, segue exemplo abaixo:  

```
public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
        }
```

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## Controllers

Ao criar cada Controller nós devemos chamar o repositório referente a cada uma delas pela interface para que tenhamos o meio de acesso a dados, também chamaremos nosso AutoMapper para fazer o mapeamento de Model e ViewModel.

* [Voltar ao Início](https://github.com/YuriSiman/complete-api-aspnetcore-webapi#api-completa-em-aspnet-core)  

---

## :vertical_traffic_light: Status do Projeto

:construction: Projeto sendo implementado :construction:

---

## :thinking: Contribuindo

> Para começar...

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

[![Github](https://img.shields.io/badge/github-profile-%237159c1?style=for-the-badge&logo=github)](https://github.com/YuriSiman)  
[![Curriculum](https://img.shields.io/badge/site-curriculum-%23563D7C?style=for-the-badge&logo=bootstrap)](https://yurisiman.com.br)  

---

## :pencil: Licença

[![License](https://img.shields.io/badge/license-mit-%23A6CE39?style=for-the-badge&logo=github)](https://github.com/YuriSiman/complete-api-aspnetcore-webapi/blob/master/LICENSE)   

---

Code your life...
