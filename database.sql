create table usuarios(
	id int not null auto_increment,
	nombre varchar(100) unique,
	clave varchar(100),
	fecha_registro varchar(100),
	primary key(id)
);

create table peliculas(
	id int not null auto_increment,
	nombre varchar(100) unique,
	comentario varchar(100),
	imagen varchar(100),
	links text,
	estado int,
	fecha_vista varchar(100),
	fecha_registro varchar(100),
	fecha_ultima_actualizacion varchar(100),
	primary key(id)
);

create table series(
	id int not null auto_increment,
	nombre varchar(100) unique,
	comentario varchar(100),
	imagen varchar(100),
	links text,
	ultima_temporada int,
	ultimo_capitulo int,
	estado int,
	fecha_final_vista varchar(100),
	fecha_registro varchar(100),
	fecha_ultima_actualizacion varchar(100),
	primary key(id)
);

insert into usuarios(nombre,clave,fecha_registro) 
	values('supervisor','$2a$11$n3wJp8J589XUawMW2tdk4eghkRqMys.NA7YfaoMq6.jLzOHF8QjBa','2023-04-02 11:12:03');