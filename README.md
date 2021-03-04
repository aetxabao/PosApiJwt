# PosApiJwt

Servidor REST con seguridad JWT para implementar el servicio de mensajería POS.

## Descripción

El *funcionamiento* del servidor con el cliente [unit-testing-pos-api-jwt-consumer](https://github.com/aetxabao/unit-testing-pos-api-jwt-consumer "unit-testing-pos-api-jwt-consumer (GitHub)") se explica en el siguiente vídeo.

[![IMAGE ALT TEXT](https://img.youtube.com/vi/t0W5lKJcerQ/0.jpg)](https://www.youtube.com/watch?v=t0W5lKJcerQ&list=PLK_BHw0Wm4MKJKynoZf1ph-KpBbzZti_m&index=5 "04. POS Api JWT Consumer")

## Consideraciones

La base de datos SQLite está en el fichero Messages.db. Para ejecutar el servidor es necesario disponer del paquete correspondiente:

```
dotnet add package Microsoft.EntityFrameworkCore.SQLite
```

El API del servicio puede ser visible en [Swagger](https://localhost:5001/swagger/index.html "API Swagger") cuando se está ejecutando.


