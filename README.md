# 🏦 API Banco Chu S.A.
### *Desafio Backend BMP TEC*

Para executar os contêineres da aplicação (API, MySQL e REDIS): 
```bash
cd BancoChu
docker-compose up --build

```

Para rodar os testes unitários
```bash
dotnet test
```

## Rotas da API
| Função | Rota | Método |
| :--- | :--- | :--- |
| **Criar Conta** | localhost:8080/api/conta | POST |
| **Fazer Login** | localhost:8080/api/auth/login | POST |
| **Fazer Transferencia** | localhost:8080/api/transferencia | POST |
| **Gerar Extrato** | localhost:8080/api/conta/extrato | POST |


## Especificações técnicas do projeto
| Componente | Ferramenta |
| :--- | :--- |
| **Framework** | .NET 6.0 |
| **Banco de Dados** | MySQL |
| **Cache** | Redis |
| **Containerização** | Docker / Docker Compose |
| **Autenticação/Autorização** | JWT (JSON Web Tokens) |
| **Testes Unitários** | XUnit |