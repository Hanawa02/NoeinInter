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