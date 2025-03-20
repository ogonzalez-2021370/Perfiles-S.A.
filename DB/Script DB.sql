CREATE DATABASE Empresa_Perfiles;
GO

USE Empresa_Perfiles;
GO

CREATE TABLE Departamentos (
    DepartamentoID INT PRIMARY KEY IDENTITY(1,1),
    NombreDepartamento NVARCHAR(100) NOT NULL
);

Select * from Departamentos;

CREATE TABLE Empleados (
    EmpleadoID INT PRIMARY KEY IDENTITY(1,1),
    Nombres NVARCHAR(100) NOT NULL,
    DPI NVARCHAR(20) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Genero NVARCHAR(10) NOT NULL,
    FechaIngreso DATE NOT NULL,
    Edad AS DATEDIFF(YEAR, FechaNacimiento, GETDATE()),
    Direccion NVARCHAR(200),
    NIT NVARCHAR(20),
    DepartamentoID INT FOREIGN KEY REFERENCES Departamentos(DepartamentoID)
);

Select * from Empleados;

GO
--Procedimientos Almacenados
-- Insertar un nuevo empleado
CREATE PROCEDURE InsertarEmpleado (
    @Nombres NVARCHAR(100),
    @DPI NVARCHAR(20),
    @FechaNacimiento DATE,
    @Genero NVARCHAR(10),
    @FechaIngreso DATE,
    @Direccion NVARCHAR(200),
    @NIT NVARCHAR(20),
    @DepartamentoID INT
)
AS
BEGIN
    INSERT INTO Empleados (Nombres, DPI, FechaNacimiento, Genero, FechaIngreso, Direccion, NIT, DepartamentoID)
    VALUES (@Nombres, @DPI, @FechaNacimiento, @Genero, @FechaIngreso, @Direccion, @NIT, @DepartamentoID);
END;
GO

-- Procedimiento almacenado para obtener todos los empleados con su departamento
CREATE PROCEDURE ObtenerEmpleadosConDepartamento
AS
BEGIN
    SELECT 
        e.EmpleadoID,
        e.Nombres,
        e.DPI,
        e.FechaNacimiento,
        e.Genero,
        e.FechaIngreso,
        e.Edad,
        e.Direccion,
        e.NIT,
        d.NombreDepartamento
    FROM Empleados e
    INNER JOIN Departamentos d ON e.DepartamentoID = d.DepartamentoID;
END;
GO

-- Procedimiento almacenado para actualizar la información de un empleado
CREATE PROCEDURE ActualizarEmpleado (
    @EmpleadoID INT,
    @Nombres NVARCHAR(100),
    @DPI NVARCHAR(20),
    @FechaNacimiento DATE,
    @Genero NVARCHAR(10),
    @FechaIngreso DATE,
    @Direccion NVARCHAR(200),
    @NIT NVARCHAR(20),
    @DepartamentoID INT
)
AS
BEGIN
    UPDATE Empleados
    SET 
        Nombres = @Nombres,
        DPI = @DPI,
        FechaNacimiento = @FechaNacimiento,
        Genero = @Genero,
        FechaIngreso = @FechaIngreso,
        Direccion = @Direccion,
        NIT = @NIT,
        DepartamentoID = @DepartamentoID
    WHERE EmpleadoID = @EmpleadoID;
END;
GO

--Vista para el reporte
CREATE VIEW VistaReporteEmpleados AS
SELECT 
    e.EmpleadoID,
    e.Nombres,
    e.DPI,
    e.FechaNacimiento,
    e.Genero,
    e.FechaIngreso,
    DATEDIFF(YEAR, e.FechaNacimiento, GETDATE()) AS Edad, -- Calcula la edad automáticamente
    e.Direccion,
    e.NIT,
    d.NombreDepartamento,
    CASE 
        WHEN e.FechaIngreso <= GETDATE() THEN 'Activo'
        ELSE 'Inactivo'
    END AS Estatus -- Define el estatus del empleado
FROM 
    Empleados e 
INNER JOIN 
    Departamentos d ON e.DepartamentoID = d.DepartamentoID;

	---------------------------------------------
	select * from Empleados