# 📌 Sistema de Control Administrativo para PERFILES S.A (Prueba Técnica)  

Este proyecto es un sistema de control administrativo desarrollado para la empresa **PERFILES S.A**.  
El sistema permite gestionar la información de los empleados, agrupados por departamentos, y genera reportes con filtros por rango de fechas de ingreso.  

## ✨ Características principales  

- 📝 **Registro de empleados**: Permite registrar nuevos empleados con información detallada, como nombres, DPI, fecha de nacimiento, género, fecha de ingreso, dirección, NIT y departamento asignado.  
- ✏️ **Edición de empleados**: Permite modificar la información de los empleados existentes.  
- 📊 **Reporte de empleados**: Genera un reporte de empleados agrupados por departamento, con la edad calculada automáticamente y un filtro por rango de fechas de ingreso.  
- ✅ **Validaciones**: Los campos obligatorios se validan tanto en el frontend como en el backend, y se muestran mensajes de error claros al usuario.  

## 🛠 Tecnologías utilizadas  

### 🎯 **Backend**  
- Lenguaje: C#  
- Framework: ASP.NET MVC  
- Base de datos: SQL Server  
- ORM: Entity Framework  

### 🎨 **Frontend**  
- HTML, CSS, JavaScript  
- Bootstrap para el diseño responsive  
- Razor para la generación de vistas dinámicas  

### 🛠 **Herramientas**  
- Visual Studio 2022  
- SQL Server Management Studio (SSMS)  
- Git para el control de versiones  

## 📂 Estructura del proyecto  

- **Models**: Contiene las clases que representan las entidades de la base de datos (Empleados, Departamentos, etc.).  
- **Controllers**: Contiene los controladores que manejan la lógica de la aplicación (EmpleadosController, etc.).  
- **Views**: Contiene las vistas Razor que generan la interfaz de usuario.  
- **Scripts**: Contiene los scripts de JavaScript y archivos relacionados.  
- **App_Data**: Contiene la configuración de la base de datos y archivos relacionados.  

## ⚙️ Configuración del proyecto  

### 📌 Requisitos previos  

- **Visual Studio 2022**: Para abrir y ejecutar el proyecto.  
- **SQL Server**: Para gestionar la base de datos.  
- **.NET Framework**: Asegúrate de tener instalado .NET Framework 4.8 o superior.  
