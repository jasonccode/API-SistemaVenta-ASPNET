# API Sistema de Ventas ASP.NET Core
Este proyecto es un sistema de ventas desarrollado con las tecnologías Angular y ASP.NET Core 7. La aplicación consta de una API REST construida en ASP.NET Core 7 que maneja las operaciones de registro de usuarios, gestión de productos, ventas y generación de informes. Además, se ha desarrollado una interfaz de usuario moderna utilizando Angular Material en el repositorio https://github.com/jasonccode/API-SistemaVenta-ASPNET

Contenido

## 1. Presentación, Inicio y Estructura - API REST
1.1 Presentación y Base de Datos
En esta sección, se introduce el proyecto y se presenta la estructura del sistema. También se describe la base de datos utilizada, que está implementada en PostgreSQL.

### 1.2 Creación de API REST y su Estructura
Se explica cómo se crea la API REST en ASP.NET Core 7. Se detallan los pasos para configurar los controladores, rutas y se muestra la estructura general de la aplicación.

### 2. Desarrollo Capa DAL - API REST
### 2.1 Implementación de Repositorios
En esta fase, se realiza la implementación de la capa de acceso a datos (DAL) mediante la creación de repositorios. Se describen los métodos necesarios para interactuar con la base de datos.

### 3. Desarrollo Capa DTO y Utility - API REST
### 3.1 Implementación de DTOs y AutoMapper
Se introduce la capa de Transferencia de Datos (DTO) y la utilidad AutoMapper. Se explican los DTOs utilizados para la comunicación entre la API y la interfaz de usuario y cómo se realiza el mapeo de entidades.

### 4. Desarrollo Capa BLL - API REST
### 4.1 Implementación de Servicios
En esta sección, se desarrolla la capa de lógica de negocios (BLL) mediante la implementación de servicios. Se detallan las operaciones de negocio relacionadas con usuarios, productos y ventas.

### 5. Desarrollo Capa API - Final de Creación API REST
### 5.1 Implementación de APIs
Se concluye la creación de la API REST explicando las operaciones finales relacionadas con usuarios, productos y ventas. Se revisa la funcionalidad completa de la API antes de pasar al siguiente paso.

## Estructura del Proyecto

- SistemaVenta.API: Contiene los controladores y configuraciones de la API.
- SistemaVenta.BLL: Capa de lógica de negocios que contiene la implementación de servicios.
- SistemaVenta.Utility: Contiene utilidades y funciones comunes.
- SistemaVenta.DTO: Capa de Transferencia de Datos que define los objetos utilizados en la comunicación.
- SistemaVenta.DAL: Capa de Acceso a Datos que contiene los repositorios para interactuar con la base de datos.
- SistemaVenta.IOC: Configuración de la Inyección de Dependencias.
- SistemaVenta.Model: Define los modelos de datos.
- APISistemaVenta.csproj: Proyecto principal de la API.
- APISistemaVenta.sln: Solución del proyecto.
- README.md: Documentación del proyecto (este archivo).
- Program.cs: Punto de entrada de la aplicación.
- appsettings.json: Configuración principal de la aplicación.
- appsettings.Development.json: Configuración específica para entornos de desarrollo.
- Properties: Directorio que contiene archivos de configuración y propiedades.