Kanban Web App (PAW_Kanban_G7)
Open-source Kanban board built with ASP.NET Core 8.0.
Includes a REST API, layered architecture, and a client-side MVC front end.

Arquitectura
Proyecto	Responsabilidad principal
K.Models	Entidades y DTOs para tableros, columnas, historias y etiquetas
KanbanData	KanbanDbContext y migraciones EF Core para SQL Server
K.Repositories	Repositorios de acceso a datos (Historias/Etiquetas)
K.Business	Servicios con lógica de negocio (p. ej. evitar etiquetas duplicadas)
K2.API	API REST con controladores y DI/CORS configurados
PAW_Kanban_G7	Interfaz MVC que consume la API vía JavaScript (fetch + drag‑and‑drop)
Esquema de base de datos
Tablas: Usuarios, Tableros, Columnas, Historias, Comentarios, Etiquetas y relación Historia–Etiqueta.

Restricciones y borrados en cascada/Restrict según relación

OnConfiguring incluye un aviso para mover la cadena de conexión a appsettings.json antes de producción

API principales
Método & Ruta	Función
GET /api/historias/board/{tableroId}	Devuelve el tablero completo con columnas e historias
POST /api/historias	Crea una nueva historia y responde con su id
PATCH /api/historias/{id}/status	Reordena o mueve una historia entre columnas
PUT /api/historias/{id} / DELETE /api/historias/{id}	Actualiza o elimina la historia indicada
GET /api/etiquetas/board/{tableroId}	Lista etiquetas del tablero
POST /api/etiquetas	Crea etiqueta, devolviendo 409 si el nombre está duplicado
Interfaz MVC
Carga tablero con Fetch y renderizado dinámico de columnas/tarjetas

Patch para mover tarjetas con drag‑and‑drop

Modales para crear/editar historias y asignar etiquetas

Administración de etiquetas desde el cliente (crear/eliminar)

Instalación y ejecución
Requisitos

.NET 8 SDK

SQL Server

Configurar cadena de conexión

Añadir DefaultConnection en K2.API/appsettings.json o vía variables de entorno.

Alternativamente, modificar KanbanDbContext para usar un origen distinto.

Aplicar migraciones

dotnet ef database update \
    --project KanbanData/K.Data.csproj \
    --startup-project K2.API/K2.API.csproj
Ejecutar API

dotnet run --project K2.API/K2.API.csproj
Ejecutar interfaz MVC

dotnet run --project PAW_Kanban_G7/PAW_Kanban_G7.csproj
Abrir https://localhost:{puerto}/Board?id={tableroId}.

Características
CRUD completo de historias con repositorio asíncrono.

Reordenamiento de historias recalculando índice interno.

Etiquetas coloreables con unicidad por tablero.

Interfaz drag‑and‑drop con modales y color picker.

Proyecto pensado para expansión (ej. añadir Identity).

Futuras mejoras
Autenticación con ASP.NET Identity.

Manejo centralizado de cadena de conexión y secrets.

Cobertura de pruebas e incorporación de más tableros/usuarios.

¡Listo para clonar, ejecutar y extender!
