# Sistema de Estoque FTF
Este projeto freelance foi desenvolvido para uma loja especializada na venda de itens digitais do jogo Flee the Facility.
<br><br>
A necessidade do sistema surgiu porque os vendedores utilizam várias contas para comercializar os itens e, devido
ao limite de itens por conta, tornou-se essencial criar uma solução que centralizasse todos os itens em um único
estoque. O sistema inclui funcionalidades essenciais para auxiliar a gestão, como a adição e remoção de lotes inteiros,
priorizando categorias mais comercializadas.

## Demonstração do Sistema

![flee](https://github.com/user-attachments/assets/94be0884-36cd-4a36-97c2-8e8cbbf36947)

### Computador
![pc](https://github.com/user-attachments/assets/fb1673a7-c675-46c2-8d42-34f6cf349345)

### Celular
![mobile](https://github.com/user-attachments/assets/b7be4cf9-a3bb-4d76-88ea-fa468fcfe0eb)

## Funcionalidades
- Criação, edição e exclusão de itens
- Possiblidade de adicionar fotos
- Permite mudar a quantidade do estoque facilmente pelos botões "-" e "+"
- Adiciona ou remove uma unidade de todos os itens da mesma categoria de uma única vez
- Possui barra de busca para facilitar a procura em grandes listas

## Pré-requisitos
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server instalado (ou acesso a um servidor SQL)

## Configuração do Projeto

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/luanspataro/SistemaEstoqueFTF.git
   cd SistemaEstoqueFTF
   ```

2. **Restaure as dependências:**
    ```bash
    dotnet restore
    ```

3. **Configure o banco de dados:**
   - Certifique-se de que o SQL Server está em execução.
   - Atualize a string de conexão no arquivo `appsettings.json`:
     ```json
     {
       "Logging": {
         "LogLevel": {
           "Default": "Information",
           "Microsoft.AspNetCore": "Warning"
         }
       },
       "AllowedHosts": "*",
       "ConnectionStrings": {
         "DefaultConnection": "Data Source=NomeDoBanco;Initial Catalog=NomeDaDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"
       }
     }
     ```

4. **Aplique as migrations para criar o banco de dados:**
   ```bash
   dotnet ef database update
   ```

5. **Execute o projeto:**
   ```bash
   dotnet run
   ```

## Tecnologias Utilizadas
- .NET 8.0
- ASP.NET MVC
- Entity Framework Core
- Bootstrap
- SQL Server

## Licença
Este projeto está sob a licença [MIT](LICENSE).

