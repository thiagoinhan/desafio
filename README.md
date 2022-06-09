## Desafio Toro Fullstack .NET Core / Angular

Este repositório tem como propósito implementar a user story [TORO-003](https://github.com/ToroInvestimentos/desafio-toro-fullstack/blob/master/README.md#hist%C3%B3rias-de-usu%C3%A1rio) proposta no desafio Toro FullStack .NET Core / Angular.

### Pré-requisitos: 
- .NET Core 6.0 - [Download](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/) 
- Node: 16.15.0 - [Download](https://nodejs.org/en/)
- NPM 8.5.5 - [Download](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)
- Angular CLI 14.0.0 - [Download](https://angular.io/cli)
- Docker 20.10.5 - [Download](https://www.docker.com/get-started/)

## ⚡️ Quick Start

Para rodar a aplicação localmente, basta executar o arquivo docker-compose.yml que está no repositório. Para isso, é necessário primeiro clonar o repositório:

```
git clone https://github.com/thiagoinhan/desafio-toro-fullstack.git
```

Então, basta entrar dentro do repositório e digitar:

```
docker-compose up
```

Após isso, a aplicação já estará pronta para utilização.

## 🧪 Testes

Para esta aplicação, foram criados quatro projetos de testes, onde o projeto Commom é o projeto onde os recursos utilizados por todos os outros projetos foram centralizados. Além dele, foram criados os projetos Domain, Integration e EndToEnd.

Para facilitar a execução dos testes, foi utilizada a ferramenta Cake, que executa o build e os testes da aplicação de forma bem simples. Para isso, basta rodar o comando abaixo:

```
dotnet cake
```

Após o término, serão apresentados os resultados dos testes, com informações detalhadas







