Create Table Usuarios (
	IdUsuario Int Not Null Identity(1,1),
	EmailUsuario varchar(150),
	SenhaUsuario varchar(20),
	Primary Key (IdUsuario)
)

Create Table Campeonatos (	
	IdCampeonato Int Not Null Identity(1,1),
	DescricaoCampeonato varchar(150),
	IdUsuario Int Foreign Key References Usuarios(IdUsuario),
	Primary Key (IdCampeonato)
)

Create Table MensagensPadrao (
	IdMensagem Int Not Null Identity(1,1),
	TituloMensagem varchar(150),
	Mensagem varchar(1280),
	Primary Key (IdMensagem)
)

Create Table Quadra (
	IdCampeonato Int Foreign Key References Campeonatos(IdCampeonato),
	IdQuadra Int Not Null Identity(1,1),
	DescricaoQuadra varchar(150),
	Localizacao varchar(300),
	Primary Key (IdQuadra, IdCampeonato)
)

Create Table ModalidadesVisiveis(
	IdCampeonato Int Foreign Key References Campeonatos(IdCampeonato),
	CodigoModalidadeBase Int,
	Visivel Bit,
	Primary Key (IdCampeonato, CodigoModalidadeBase)
)