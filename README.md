Integrantes del grupo

-Jeremy Austin Hults Leandro
-
-

Repositorio
- https://github.com/fransalazar28/PAW_Kanban_G7

Arquitectura del proyecto

El sistema está desarrollado en arquitectura de múltiples capas y módulos:
-PAW_Kanban_G7: Aplicación ASP.NET Core MVC principal (controladores, vistas, autenticación)
-K.Models: Contiene todas las entidades de dominio como WidgetConfig, News, Guide, etc.
-K.Repositories: Implementación del patrón Repositorio para acceso a datos
-K.Business: Capa de lógica de negocio (preparada para futuras reglas)
-KanbanData: Implementa el DbContext de Entity Framework
-Kanban.API: Proyecto base para exponer APIs si se desea escalar

Paquetes NuGet utilizados

-Microsoft.EntityFrameworkCore.SqlServer
-Microsoft.EntityFrameworkCore.Tools
-Microsoft.AspNetCore.Identity.EntityFrameworkCore
-Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
-Microsoft.AspNetCore.Identity.UI
-Microsoft.VisualStudio.Web.CodeGeneration.Design

Funcionalidad principal implementada

Módulo WidgetConfig (K.Business)
Implementa un sistema de configuración de widgets del dashboard:

-Crear, editar, eliminar y listar widgets personalizados
-Control de visibilidad: público, privado, por grupo
-Permite marcar widgets como favoritos y ocultarlos
-Asociación de widgets al usuario autenticado
-Persistencia en base de datos usando Entity Framework Core
-Vistas Razor funcionales e integradas al menú del sitio

Autenticación
Uso de ASP.NET Core Identity
Solo los usuarios autenticados pueden acceder a las funciones del sistema

Principios SOLID aplicados
S: Separación de responsabilidades (modelo, repositorio, controlador, vistas)
O: La clase WidgetConfig se puede extender sin modificar, por ejemplo, con validaciones adicionales
L: Uso de interfaces ara inyectar dependencias (IWidgetConfigRepository)
I: Repositorio enfocado solo en operaciones de WidgetConfig
D: Inyección de dependencias configurada en Program.cs
