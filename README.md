# 🏦 API Banco Chu S.A.
### *Desafio Backend BMP TEC*

Para executar os contêineres da aplicação (API, MySQL e REDIS): 
```bash
docker-compose up --build

```

Os testes são rodados uma vez em um container isolado sempre que o docker-compose é iniciado

Para rodar os testes unitários localmente:
```bash
dotnet test
```

Por padrão o Swagger e o modo desenvolvimento estão habilitados. Para desabilitar e utilizar em modo produção, basta altera a variável de ambiente no docker-compose.yaml. [Acesse o Swagger aqui](https://localhost:8080/swagger/index.html)
```bash
ASPNETCORE_ENVIRONMENT=Production
```

Caso queria realizar testes E2E, há um arquivo .json na raíz do projeto que contém alguns requests no Postman já previamente configurados.


## Rotas da API
| Função | Rota | Método |
| :--- | :--- | :--- |
| **Criar Conta** | localhost:8080/api/v1/conta | POST |
| **Fazer Login** | localhost:8080/api/v1/auth/login | POST |
| **Fazer Transferencia** | localhost:8080/api/v1/transferencia | POST |
| **Gerar Extrato** | localhost:8080/api/v1/conta/extrato | POST |


## Especificações técnicas do projeto
| Componente | Ferramenta |
| :--- | :--- |
| **Framework** | .NET 6.0 |
| **Banco de Dados** | MySQL |
| **Cache** | Redis |
| **Containerização** | Docker / Docker Compose |
| **Autenticação/Autorização** | JWT |
| **Testes Unitários** | XUnit |
| **Documentação** | Swagger |
| **Fluent Validation** | FluentValidator.AspNetCore |
| **Testes E2E** | Postman |