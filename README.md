## Desafio Toro Fullstack .NET Core / Angular

Este repositório tem como propósito implementar a user story [TORO-003](https://github.com/ToroInvestimentos/desafio-toro-fullstack/blob/master/README.md#hist%C3%B3rias-de-usu%C3%A1rio) proposta no desafio Toro FullStack .NET Core / Angular.

### Pré-requisitos: 
- .NET Core 6.0 - [Download](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/) 
- Node: 16.15.0 - [Download](https://nodejs.org/en/)
- NPM 8.5.5 - [Download](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)
- Angular CLI 14.0.0 - [Download](https://angular.io/cli)
- Docker 20.10.5 - [Download](https://www.docker.com/get-started/)

## ⚡️ Quick Start

Para entrar direto na demo da aplicação, acesse http://129.153.22.240/.

Para rodar a aplicação localmente, devemos executar o arquivo docker-compose.yml que está no repositório. Para isso, é necessário primeiro clonar o repositório:

```
git clone https://github.com/thiagoinhan/desafio-toro-fullstack.git
```

Então, basta entrar na raiz do repositório e digitar:

```
docker-compose up -d
```

Após isso, a aplicação já estará pronta para utilização. As urls serão:

- Frontend:  http://localhost:4200/
- Backend: http://localhost:5000/swagger/index.html
- Banco de dados (MongoDb): http://localhost:27017/
- Db Admin Interface (Mongo Express): http://localhost:8081/

## 🗄️ Backend (.NET Core)

Para a execução do projeto de backend, estando na pasta **backend** do repositório, podemos executar os seguintes comandos:

```
dotnet restore
dotnet run --project src/Toro.Accounting.API/Toro.Accounting.API.csproj
```

Após isso, o porjeto de backend estará disponível na url https://localhost:7029/swagger/index.html, onde podemos utilizar o swagger para realizar os acessos às apis.

## 🌐 Frontend (Angular)

Para a execução do projeto de frontend, estando na pasta **frontend/Toro.Accounting.WebUI** do repositório, podemos executar o comando ng serve, como à seguir:

```
ng serve
```

A aplicação será iniciada na porta 4200 e poderá ser acessada pela url http://localhost:4200/. Ao entrar, será exibida a lista de clientes com os detalhes de suas contas

## 🧪 Testes

Para esta aplicação, foram criados quatro projetos de testes, onde o projeto Commom é o projeto onde os recursos utilizados por todos os outros projetos foram centralizados. Além dele, foram criados os projetos Domain, Integration e EndToEnd.

Para facilitar a execução dos testes, estando na pasta **backend** do repositório, podemos executar o comando abaixo.

```
dotnet test
```

Após o término, serão apresentados os resultados dos testes, com informações as detalhadas.

## 🏛️ Arquitetura

A arquitetura da aplicação é baseada na clean architecture, para facilitar a manutenção, mantendo o desacoplamento entre as camadas. Além disso, também foi implementada uma estrutura de CQRS, para que os comandos e querys da aplição sejam fluxos completamente separados, permitindo escalar a aplicação de uma forma mais fácil.

Na figura abaixo temos uma estrutura geral da aplicação e camadas, onde o frontend é desacoplado do backend:

![image](https://user-images.githubusercontent.com/48460079/172946801-53e59243-3219-46a8-9429-efaad2f0b858.png)

## ▶️ CI/CD

O deploy da aplicação é feito via github actions, utilizando um self-hosted agent, hopedado na nuvem da Oracle. Quando um push é feito, um workflow é disparado no github actions, onde o primeiro job (build-images) é responsável por fazer o build das imagens de front e backend. O segundo job (run-docker-compose) é o responsável por dar um pull e subir os novos containers via docker-compose.

![image](https://user-images.githubusercontent.com/48460079/173000654-bf1d8dde-ce0f-4ec4-8878-39134e1ad3fe.png)









