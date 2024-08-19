# Branef.Api
# Introdução 
Projeto criado para persistencia e listagem dos dados de cliente

# Tecnologias utilizadas
.Net 6
Sql Server
MongoDb
Dapper
MediatR
Swagger

#Padrões e metodologias Utilizadas
CQRS,
Repository Pattern
ILooger para logs da app.
Propriedades da Entidade todas private, alteradas apenas pelo construtor ou métodos de acesso.
Commands se auto validam, não necessitando de bibliotecas de terceiros para isso.
Response padronizadao, facilitando a integração com o front-end
Sumario nos end-points
Builder da entidade feito na mão, ao invés de usar biblioteca de terceiros.
Injeção de depedencia toda feita fora da classe Program.

#Projeto
Entidade persistida no Mongo e no SQL, leitura dos dados no Mongo 
Necessário ajuste das connection string e configuração do no mongo no AppSettings antes de rodar a app.


