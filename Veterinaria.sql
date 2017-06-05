
CREATE DATABASE Veterinaria
Use Veterinaria

create table Persona(
	Id_Persona int Not Null IDENTITY(1,1) PRIMARY KEY,
	Nombre varchar(50) Not Null,
	Apellidos varchar(100) Not Null,
	Direccion varchar(300) Not Null,
	Telefono varchar(8) Not Null,
	Id_Estado int Not Null
);

create table Usuarios(
	Id_Usuario int Not Null IDENTITY(1,1) PRIMARY KEY,
	Id_Empleado int Not Null,
	Correo varchar(60) Not Null,
	Usuario varchar(50) Not Null,
	Contraseña varchar(50) Not Null,
	Id_Rol int Not Null,
	Id_Estado int Not Null
);

create table Empleado(
	Id_Empleado int Not Null IDENTITY(1,1) PRIMARY KEY,
	Id_Persona int Not Null,
	Id_Estado int Not Null
);

create table Proveedor(
	Id_Proveedor int Not Null IDENTITY(1,1) PRIMARY KEY,
	Id_Persona int Not Null,
	Id_Estado int Not Null
);

create table Cliente(
	Id_Cliente int Not Null IDENTITY(1,1) PRIMARY KEY,
	Id_Persona int Not Null,
	Id_Estado int Not Null
);

create table Producto(
	Id_Producto int Not Null IDENTITY(1,1) PRIMARY KEY,
	Nombre varchar(50) Not Null,
	Descripcion varchar(50) Not Null,
	Precio float Not Null,
	Cantidad int Not Null,
	Id_Proveedor int Not Null,
	Id_Estado int Not Null
);

create table Estado(
	Id_Estado int Not Null IDENTITY(1,1) PRIMARY KEY,
	Descripcion varchar(50) Not Null,
);

create table Roles(
	Id_Rol int Not Null IDENTITY(1,1) PRIMARY KEY,
	Descripcion varchar(50) Not Null,
	Id_Estado int Not Null
);

create table Factura(
	Numero_Factura int Not Null IDENTITY(1,1) PRIMARY KEY,
	Id_Empleado int Not Null,
	Id_Cliente int Not Null,
	Id_Proveedor int Not Null,
	Estado int Not Null,
	Fecha date Not Null,
	Precio_Total int Not Null,
	Id_Estado int Not Null
);

create table Detalle_Factura(
	Id_Detalle int Not Null IDENTITY(1,1) PRIMARY KEY,
	Numero_Factura int Not Null,
	Id_Producto int Not Null,
	Cantidad int Not Null,
	Precio int Not Null,
	Precio_Total_Producto int Not Null,
	Precio_Unitario int Not Null,
);

create table Paciente(
	Id_Paciente int Not Null IDENTITY(1,1) PRIMARY KEY,
	Id_Cliente int Not Null,
	Animal varchar(50) Not Null,
	Raza varchar(50) Not Null,
	Edad int Not Null,
	Peso int Not Null,
	Id_Estado int Not Null
);

create table Cita_Medica(
	Id_Cita_Medica int Not Null IDENTITY(1,1) PRIMARY KEY,
	Fecha date Not Null,
	Hora time Not Null,
	Id_Medicina int Not Null,
	Id_Cliente int Not Null,
	Id_Paciente int Not Null,
	Descripcion Varchar(600) Not Null,
	Id_Estado int Not Null
);

create table Medicina(
	Id_Medicina int Not Null IDENTITY(1,1) PRIMARY KEY,
	Nombre varchar(50) Not Null,
	Id_Estado int Not Null
);


ALTER TABLE Cliente     
ADD CONSTRAINT FK_Persona_Cliente FOREIGN KEY (Id_Persona)     
    REFERENCES Persona (Id_Persona)     

ALTER TABLE Empleado     
ADD CONSTRAINT FK_Persona_Empleado FOREIGN KEY (Id_Persona)     
    REFERENCES Persona (Id_Persona)     

ALTER TABLE Proveedor     
ADD CONSTRAINT FK_Persona_Proveedor FOREIGN KEY (Id_Persona)     
    REFERENCES Persona (Id_Persona) 

ALTER TABLE Detalle_Factura     
ADD CONSTRAINT FK_Factura_Detalle_Factura FOREIGN KEY (Numero_Factura)     
    REFERENCES Factura (Numero_Factura) 

ALTER TABLE Cita_Medica    
ADD CONSTRAINT FK_Cita_Medica_Medicina FOREIGN KEY (Id_Medicina)     
    REFERENCES Medicina (Id_Medicina)          

ALTER TABLE Cita_Medica    
ADD CONSTRAINT FK_Cita_Medica_Cliente FOREIGN KEY (Id_Cliente)     
    REFERENCES Cliente (Id_Cliente)     

ALTER TABLE Cita_Medica    
ADD CONSTRAINT FK_Cita_Medica_Paciente FOREIGN KEY (Id_Paciente)     
    REFERENCES Paciente (Id_Paciente)     

ALTER TABLE Detalle_Factura    
ADD CONSTRAINT FK_Producto_Detalle_Factura FOREIGN KEY (Id_Producto)     
REFERENCES Producto (Id_Producto) 

ALTER TABLE Factura    
ADD CONSTRAINT FK_Factura_Empleado FOREIGN KEY (Id_Empleado)     
REFERENCES Empleado (Id_Empleado) 

ALTER TABLE Factura    
ADD CONSTRAINT FK_Factura_Cliente FOREIGN KEY (Id_Cliente)     
REFERENCES Cliente (Id_Cliente) 

ALTER TABLE Paciente    
ADD CONSTRAINT FK_Paciente_Cliente FOREIGN KEY (Id_Cliente)     
REFERENCES Cliente (Id_Cliente) 

ALTER TABLE Usuarios    
ADD CONSTRAINT FK_Usuarios_Empleado FOREIGN KEY (Id_Empleado)     
REFERENCES Empleado (Id_Empleado) 

ALTER TABLE Usuarios    
ADD CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (Id_Rol)     
REFERENCES Roles (Id_Rol) 

ALTER TABLE Persona    
ADD CONSTRAINT FK_Persona_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado) 

ALTER TABLE Cita_Medica    
ADD CONSTRAINT FK_Cita_Medica_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado) 

ALTER TABLE Cliente    
ADD CONSTRAINT FK_Clientea_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado) 

ALTER TABLE Empleado    
ADD CONSTRAINT FK_Empleado_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado) 

ALTER TABLE Factura    
ADD CONSTRAINT FK_Factura_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado)

ALTER TABLE Medicina    
ADD CONSTRAINT FK_Medicina_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado)

ALTER TABLE Paciente    
ADD CONSTRAINT FK_Paciente_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado)

ALTER TABLE Producto    
ADD CONSTRAINT FK_Producto_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado)

ALTER TABLE Proveedor    
ADD CONSTRAINT FK_Proveedor_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado)

ALTER TABLE Roles    
ADD CONSTRAINT FK_Roles_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado)

ALTER TABLE Usuarios    
ADD CONSTRAINT FK_Usuarios_Estado FOREIGN KEY (Id_Estado)     
REFERENCES Estado (Id_Estado)

ALTER TABLE Producto    
ADD CONSTRAINT FK_Proveedor_Producto FOREIGN KEY (Id_Proveedor)     
REFERENCES Proveedor (Id_Proveedor)

ALTER TABLE Factura    
ADD CONSTRAINT FK_Proveedor_Factura FOREIGN KEY (Id_Proveedor)     
REFERENCES Proveedor (Id_Proveedor)