Create Table Usuarios (
	IdUsuario Int Not Null Identity(1,1),
	EmailUsuario varchar(150),
	SenhaUsuario varchar(20),
	Primary Key (IdUsuario)
)

Create Table Campeonatos (	
	IdCampeonato Int Not Null Identity(1,1),
	DescricaoCampeonato varchar(150),
	IdUsuario Int Not Null Foreign Key References Usuarios(IdUsuario),
	Primary Key (IdCampeonato)
)

Create Table MensagensPadrao (
	IdMensagem Int Not Null Identity(1,1),
	TituloMensagem varchar(150),
	Mensagem varchar(1280),
	Primary Key (IdMensagem)
)

Create Table Quadra (
	IdCampeonato Int Not Null Foreign Key References Campeonatos(IdCampeonato),
	IdQuadra Int Not Null Identity(1,1),
	DescricaoQuadra varchar(150),
	Localizacao varchar(300),
	Primary Key (IdQuadra, IdCampeonato)
)

Create Table Horarios (
	IdHorario Int Not Null Identity(1,1),
	DataInicio Date Not Null,
	Intervalo Int Not Null,
	Primary Key(IdHorario)
)

Create Table ModalidadesBasicas (
	IdModalidadeBasica Int Not Null Identity(1,1),
	Descricao varchar(100),
	Primary Key(IdModalidadeBasica)
)

Create Table ModalidadesVisiveis(
	IdCampeonato Int Not Null Foreign Key References Campeonatos(IdCampeonato),
	IdModalidadeBasica Int Not Null Foreign Key References ModalidadesBasicas(IdModalidadeBasica),
	Visivel Bit,
	Primary Key (IdCampeonato, IdModalidadeBasica)
)
