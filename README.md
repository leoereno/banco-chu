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

## Especificações técnicas do projeto
| Componente | Ferramenta |
| :--- | :--- |
| **Framework** | .NET 6.0 |
| **Banco de Dados** | MySQL |
| **Cache** | Redis |
| **Containerização** | Docker / Docker Compose |
| **Autenticação/Autorização** | JWT (JSON Web Tokens) |
| **Testes Unitários** | XUnit |