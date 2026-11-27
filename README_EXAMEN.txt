PRUEBA 3 - CALIDAD DEL SOFTWARE - APLICACION ESTANDAR INTERSEDES

Sistema: Reserva de espacios CFT
Tecnologias: .NET 8, xUnit, API REST, Postman, Selenium WebDriver, Git/GitHub Actions.

REGLAS DE NEGOCIO
- Sala: $20.000 por hora.
- Auditorio: $40.000 por hora.
- Duracion valida: 1 a 4 horas.
- Desde 3 horas se aplica 10% de descuento.
- Nombre del responsable: minimo 3 caracteres.

IMPORTANTE
Existe una prueba unitaria con una expectativa deliberadamente incorrecta. Debe detectarla al ejecutar dotnet test y corregir SOLO la expectativa de la prueba, sin cambiar la regla de negocio.

COMANDOS BASE
1) dotnet build src/ReservaCFT.Web/ReservaCFT.Web.csproj
2) dotnet test tests/ReservaCFT.Tests/ReservaCFT.Tests.csproj
3) dotnet run --project src/ReservaCFT.Web/ReservaCFT.Web.csproj
   La aplicacion queda en http://localhost:5060

Postman: importar los dos JSON de la carpeta postman.
Selenium: con la aplicacion ejecutandose, ejecutar:
   dotnet test selenium/ReservaCFT.SeleniumTests/ReservaCFT.SeleniumTests.csproj

GitHub: el paquete incluye .github/workflows/qa.yml. Al subirlo a GitHub, el workflow ejecuta build y pruebas unitarias.
