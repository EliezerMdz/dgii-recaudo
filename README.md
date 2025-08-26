# DGII Recaudo API

Solución de **recaudo DGII** basada en **DDD** y **arquitectura por capas**.

## Tecnologías

* **.NET Core** (backend)
* **Dapper** (acceso a datos ligero)
* **SQL Server** (base de datos)
* **AutoMapper** (mapeo a DTOs)

## Estructura

* **Domain** `emdz.dgii.recaudo.Domain`
  Núcleo del modelo: entidades, excepciones, interfaces de repositorio y servicios de dominio.

* **Application** `emdz.dgii.recaudo.Application`
  Casos de uso y orquestación entre dominio e infraestructura.

* **Infrastructure** `emdz.dgii.recaudo.Infrastructure`
  Persistencia e integraciones: repositorios con Dapper sobre SQL Server.

* **CrossCutting** `emdz.dgii.recaudo.CrossCutting`
  DTOs, perfiles de AutoMapper y utilidades comunes.

* **Database** `emdz.dgii.recaudo.Database`
  Proyecto y scripts de BD: tablas, SPs, migraciones.

* **WebAPI** `emdz.dgii.recaudo.WebAPI`
  API REST: controladores, endpoints y manejo de errores.

## Flujo

1. Cliente → **WebAPI** (endpoint).
2. **WebAPI** → **Application** (ejecuta caso de uso).
3. **Application** aplica reglas del **Domain** y usa **Infrastructure** (Dapper/SQL Server).
4. **AutoMapper** genera **DTOs** → respuesta al cliente.

## Principios

* Dominio independiente y centrado en el negocio.
* Dependencias dirigidas hacia el dominio (Application/Infrastructure → Domain).
* Separación clara de responsabilidades.
* Desacoplamiento mediante **interfaces**.
