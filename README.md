# PaymentMicroservices

regra de negócio:

criar um microservices que, através de um http post efetue uma operação de debito (origem) e credito (destino) nas contas correntes.

entidades: contacorrente, lancamentos (voce pode incrementar com outras entidades se achar necessário)

Parâmetros de entrada:
conta origem
conta destino
valor

Parâmetros de saída:
http status code

informações adicionais:
o método “post” devera receber os parametros no body da requisição em formato json

UTILIZE Domain Driven Design

serão avaliados critérios de arquitetura como separação de responsabilidade, clean code, segurança e testes

tecnologias que você pode utilizar .net core 2.X, c#, xunits (testes)

no término do projeto, publique o código em um repositório no git-hub

# Technoligia utilizada

- .Net Core sdk 3.1
- TDD, DDD, Clean Archtecture, clean code, Design patterns, Swagger, 
- Docker
- MySql


# Manual de instalação

- Para iniciar o projeto, primeiramente subir o banco MySql utilizando o arquivo ```docker-compose-mysql``` na pasta ```docker```
Após banco inicializado, executar o script para criação de tabela e popular a mesma, script se encontra na pasta:
```Docker/Database/CreateTable.sql```

- Apos subir o banco e instalar as dependencias do projeto
- Execute os Testes para validar que esta tudo OK.

- Ao colocar o projeto para executar, ele irá abrir a pagina com o Swagger para visualização.

Para fazer o teste inicial, por favor utilize o seguinte json:

```
{
	"Valor" : 100.00,
	"Origem": 
	{
		"Agencia": "0001",
		"Numero": "123456",
		"Digito" : "7"
	},
		"Destino": 
	{
		"Agencia": "0001",
		"Numero": "654321",
		"Digito" : "0"
	}
}
```