# 📱 Gestión de Stock TP6 — Frontend (.NET MAUI Blazor)

Aplicación cliente multiplataforma desarrollada con **.NET MAUI Blazor Hybrid** y **MudBlazor**. Permite la consulta del catálogo de productos, gestión de compras mediante un carrito responsivo y un flujo de autenticación seguro basado en JWT.

## 🚀 Características Principales

### 🔐 Autenticación y Seguridad
* **Protección contra Enumeración:** Mensajes de error unificados en la pantalla de inicio de sesión (`Login.razor`) para proteger la privacidad de las cuentas.
* **Interceptor HTTP Global:** Implementación de `GlobalHttpInterceptor.cs` que inyecta automáticamente el token JWT en cada petición y maneja errores HTTP. Incluye un *bypass* inteligente que evita el cierre de sesión forzado al fallar las credenciales en el endpoint de login.
* **Manejo de Estado UI:** Uso de `CustomAuthStateProvider` para sincronizar los *Claims* almacenados en el dispositivo (`SecureStorage`) con el árbol de componentes de Blazor.

### 🛒 Catálogo e Interfaz de Cliente
* **Catálogo Dinámico (`Catalogo.razor`):** Vista adaptativa basada en `MudGrid` que renderiza el inventario (`ProductoModel`) recuperado desde la API. Incluye validación de autenticación y verificación del rol "Cliente" antes de permitir agregar ítems al carrito.
* **Carrito Responsivo (`Carrito.razor`):** Rediseño utilizando el sistema de grillas de MudBlazor para evitar el solapamiento visual en pantallas móviles. Destaca los importes parciales y totales con la paleta corporativa (color `#28424f`). El *checkout* extrae dinámicamente el ID del cliente para generar la petición de salida.

### 🎨 Optimizaciones de Experiencia de Usuario (UX)
* **Animación de Carga Nativa:** Reemplazo del texto por defecto de Blazor en `index.html` por un *spinner* CSS personalizado que respeta la paleta del tema (`#607D8B`).
* **Control de Notificaciones:** Configuración centralizada de `MudSnackbarProvider` para evitar la duplicación de alertas (ej. al hacer clics rápidos en "Agregar al carrito").

## 🛠️ Estructura del Proyecto

El proyecto se organiza bajo la arquitectura estándar de Blazor Hybrid:

* **`Models/`**[cite: 10]
  * `CarritoItem.cs`: Estructura temporal para los productos seleccionados en memoria[cite: 10].
  * `CheckoutRequest.cs`: DTO (Data Transfer Object) para procesar la compra final[cite: 10].
  * `ProductoModel.cs`: Representación del catálogo con propiedades de imagen, stock, categoría y precio[cite: 10].
* **`Pages/`**
  * `Catalogo.razor`: Muestra el inventario en formato de tarjetas e interactúa con `CarritoService`.
  * `Carrito.razor`: Gestión de ítems a comprar y envío del *checkout* a la API.
  * `Login.razor` / `Registro.razor` / `Logout.razor`: Flujo completo de identidad.
  * `Ingresos.razor` / `Reportes.razor`: Vistas administrativas protegidas por rol.
* **`Services/`**
  * Contiene los servicios inyectados (`AuthService`, `CarritoService`, `CustomAuthStateProvider`, `GlobalHttpInterceptor`) registrados mediante inyección de dependencias en `MauiProgram.cs`.

## 💻 Tecnologías y Plataformas

El proyecto está compilado sobre **.NET 10** y soporta múltiples plataformas nativas de destino[cite: 7, 8, 9]:
* `net10.0-windows10.0.19041.0`[cite: 7]
* `net10.0-android`[cite: 9]
* `net10.0-ios`[cite: 9]
* `net10.0-maccatalyst`[cite: 8]

**Paquetes NuGet Principales:**
* `MudBlazor (9.11.0)`: Biblioteca principal de componentes de UI basados en Material Design[cite: 7].
* `Microsoft.Maui.Controls (10.0.110)`: Controles nativos y alojamiento del WebView[cite: 7].
* `Microsoft.AspNetCore.Components.WebView.Maui (10.0.110)`: Puente de integración entre MAUI y Blazor[cite: 7].
* `Microsoft.AspNetCore.Components.Authorization (10.0.13)`: Gestión integral de estado de autenticación y `<AuthorizeView>`[cite: 7].

## 👨‍💻 Autores y Contexto Académico

Desarrollo correspondiente al **Trabajo Práctico N° 6 (TP6)** de la materia **Programación Móvil**.

* **Autores:** Joaquín Seitz, Alex Falkenstein
* **Institución:** Instituto Tecnológico de Educación Superior (ITES)
