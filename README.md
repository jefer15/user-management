## Gestión de usuarios

Sistema de gestión de usuarios desarrollado con **.NET 9**, **SQL Server** y **Docker**, aplicando una **arquitectura en capas** que separa la capa de presentación, lógica de negocio y acceso a datos.

### 🧱 Arquitectura del Proyecto

```
UserManagement
├── UserManagement.Web → Capa de presentación (MVC)
│   ├── Views/User/Create.cshtml
│   └── Views/User/Manage.cshtml
│
├── UserManagement.Api → Capa de negocio (API REST)
│   ├── Controllers/UsersController.cs
│   └── Program.cs
│
└── UserManagement.Infrastructure → Capa de datos
    ├── Entities/User.cs
    ├── Data/AppDbContext.cs
    └── Migrations/
```

### ⚙️ Tecnologías Utilizadas

| Tecnología | Uso |
|------------|-----|
| **.NET 9 (ASP.NET Core)** | API REST y aplicación MVC |
| **Entity Framework Core** | ORM y acceso a datos |
| **SQL Server 2022** | Base de datos relacional |
| **Docker & Docker Compose** | Contenedorización del entorno |
| **Swagger (Swashbuckle)** | Documentación interactiva de la API |
| **HTML5 / CSS3 / JS** | Interfaz visual moderna y funcional |

### 🚀 Ejecución con Docker

#### 1️⃣ Clonar el repositorio
```bash
git clone https://github.com/jefer15/user-management.git
cd UserManagement
```

#### 2️⃣ Construir y ejecutar los contenedores
```bash
docker-compose build
docker-compose up -d
```

Esto levantará tres contenedores:
- **api** → ASP.NET Core API (puerto 8080)
- **web** → Aplicación MVC (puerto 8081)
- **sqlserver** → SQL Server 2022 (puerto 1433)

### 🧩 Conexión a la Base de Datos

| Campo | Valor |
|-------|-------|
| Server name | localhost,1433 |
| Login | sa |
| Password | YourStrong!Passw0rd |
| Database | UserManagementDb |

### 🛠️ Procedimiento Almacenado

Ejecuta este script dentro de la base `UserManagementDb`:

```sql
CREATE OR ALTER PROCEDURE sp_Users_CRUD
    @Action NVARCHAR(10),
    @Id INT = NULL,
    @Name NVARCHAR(100) = NULL,
    @BirthDate DATE = NULL,
    @Gender CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 'CREATE'
    BEGIN
        INSERT INTO Users (Name, BirthDate, Gender)
        VALUES (@Name, @BirthDate, @Gender);
    END

    ELSE IF @Action = 'READ'
    BEGIN
        IF @Id IS NULL
            SELECT * FROM Users;
        ELSE
            SELECT * FROM Users WHERE Id = @Id;
    END

    ELSE IF @Action = 'UPDATE'
    BEGIN
        UPDATE Users
        SET Name = @Name,
            BirthDate = @BirthDate,
            Gender = @Gender
        WHERE Id = @Id;
    END

    ELSE IF @Action = 'DELETE'
    BEGIN
        DELETE FROM Users WHERE Id = @Id;
    END
END;
```

### 🌐 Endpoints Principales

**Swagger:** [http://localhost:8080/swagger](http://localhost:8080/swagger)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/users` | Obtiene todos los usuarios |
| GET | `/api/users/{id}` | Obtiene un usuario por ID |
| POST | `/api/users` | Crea un nuevo usuario |
| PUT | `/api/users/{id}` | Actualiza un usuario |
| DELETE | `/api/users/{id}` | Elimina un usuario |

### 💻 Interfaz Web

- **Crear Usuario:** [http://localhost:8081/User/Create](http://localhost:8081/User/Create)
- **Gestionar Usuarios:** [http://localhost:8081/User/Manage](http://localhost:8081/User/Manage)

### 🧠 Decisiones Técnicas

- Se implementó un solo procedimiento almacenado (`sp_Users_CRUD`) para cumplir el requerimiento de la prueba técnica
- Entity Framework Core se usa únicamente como puente para ejecutar el SP, manteniendo la estructura por capas
- El diseño del frontend utiliza CSS moderno sin frameworks externos, con un estilo limpio y responsivo
- La solución completa se ejecuta mediante Docker Compose, garantizando portabilidad y fácil despliegue

---