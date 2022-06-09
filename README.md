## Desafio Toro Fullstack .NET Core / Angular

Este repositório tem como propósito implementar a user story [TORO-003](https://github.com/ToroInvestimentos/desafio-toro-fullstack/blob/master/README.md#hist%C3%B3rias-de-usu%C3%A1rio) proposta no desafio Toro FullStack .NET Core / Angular.

### Pré-requisitos: 
- .NET Core 6.0 - [Download](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/) 
- Node: 16.15.0 - [Download](https://nodejs.org/en/)
- NPM 8.5.5 - [Download](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)
- Angular CLI 14.0.0 - [Download](https://angular.io/cli)
- Docker 20.10.5 - [Download](https://www.docker.com/get-started/)

## ⚡️ Quick Start

Para entrar direto na demo da aplicação, basta acessar a url http://129.153.22.240/.

Para rodar a aplicação localmente, basta executar o arquivo docker-compose.yml que está no repositório. Para isso, é necessário primeiro clonar o repositório:

```
git clone https://github.com/thiagoinhan/desafio-toro-fullstack.git
```

Então, basta entrar na raíz do repositório e digitar:

```
docker-compose up -d
```

Após isso, a aplicação já estará pronta para utilização. As urls serão:

- Frontend:  http://localhost:4200/
- Backend: http://localhost:7029/swagger/index.html
- Banco de dados (MongoDb): http://localhost:27017/
- Db Admin Interface (Mongo Express): http://localhost:8081/

## Backend (.NET Core)

Para a execução do projeto de backend, basta entrar na raíz do projeto e executa os comandos:

```
dotnet restore
dotnet run --project src/Toro.Accounting.API/Toro.Accounting.API.csproj
```

Após isso, basta navegar para a url https://localhost:7029/swagger/index.html e utilizar o swagger para realizar os acessos às apis.

## Frontend (Angular)

Para a execução do projeto de frontend, estando na raíz do diretório, basta entrar na basta do projeto Toro.Accounting.WebUI e executar o comando ng serve, como à seguir:

```
cd .\src\Toro.Accounting.WebUI\
ng serve
```

A aplicação será iniciada na porta 4200 e poderá ser acessada pela url http://localhost:4200/. Ao entrar, será exibida a lista de clientes com os detalhes de suas contas

## 🧪 Testes

Para esta aplicação, foram criados quatro projetos de testes, onde o projeto Commom é o projeto onde os recursos utilizados por todos os outros projetos foram centralizados. Além dele, foram criados os projetos Domain, Integration e EndToEnd.

Para facilitar a execução dos testes, foi utilizada a ferramenta Cake, que executa o build e os testes da aplicação de forma bem simples. Para isso, basta rodar o comando abaixo:

```
dotnet cake
```

Após o término, serão apresentados os resultados dos testes, com informações as detalhadas.







