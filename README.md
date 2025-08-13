# ClientesApi — ASP.NET Core 8 + EF Core + SQLite

[![Build](https://github.com/USERNAME/clientes-api-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/USERNAME/clientes-api-dotnet/actions)

# ClientesApi — ASP.NET Core 8 + EF Core + SQLite

## Requisitos
- .NET 8 SDK

## Ejecutar
```bash
dotnet restore
dotnet build
dotnet run
```
La API aplicará la migración automáticamente al iniciar y creará `clientes.db`.

Swagger: `https://localhost:5001/swagger` o `http://localhost:5000/swagger` (según puertos).

## Seguridad por API KEY
- Header: `X-API-KEY`
- Valor por defecto (puedes cambiarlo en `appsettings.json`): `HI1P4zCurijDzPGJJyXa0SylOKwsdTeO`

## Endpoints
- `GET /api/clientes`
- `GET /api/clientes/{id}`
- `POST /api/clientes`
- `PUT /api/clientes/{id}`
- `DELETE /api/clientes/{id}`

### Ejemplo de petición (PowerShell)
```powershell
curl -H "X-API-KEY: HI1P4zCurijDzPGJJyXa0SylOKwsdTeO" http://localhost:5000/api/clientes
```

### Modelo
```json
{
  "id": 0,
  "nombre": "Juan Pérez",
  "correo": "juan@example.com",
  "telefono": "+525512345678"
}
```

## Notas
- Validaciones con DataAnnotations.
- Índice único en `Correo`.
- Manejo de errores: 400 (validación), 401 (API Key), 404 (no encontrado), 409 (conflicto por correo duplicado).
