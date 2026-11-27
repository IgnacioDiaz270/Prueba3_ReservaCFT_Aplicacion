using ReservaCFT.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5080");
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

var calculadora = new CalculadoraReserva();
var reservas = new Dictionary<int, ReservaRespuesta>();
var siguienteId = 1;

app.MapGet("/api/espacios", () => Results.Ok(new[]
{
    new { tipo = "Sala", tarifaHora = 20000, capacidad = 12 },
    new { tipo = "Auditorio", tarifaHora = 40000, capacidad = 60 }
}));

app.MapPost("/api/reservas", (ReservaSolicitud solicitud) =>
{
    if (string.IsNullOrWhiteSpace(solicitud.Nombre) || solicitud.Nombre.Trim().Length < 3)
        return Results.BadRequest(new { error = "El nombre es obligatorio y debe tener al menos 3 caracteres." });

    try
    {
        var total = calculadora.CalcularTotal(solicitud.Tipo, solicitud.Horas);
        var respuesta = new ReservaRespuesta(siguienteId++, solicitud.Nombre.Trim(), solicitud.Tipo, solicitud.Horas, total);
        reservas[respuesta.Id] = respuesta;
        return Results.Created($"/api/reservas/{respuesta.Id}", respuesta);
    }
    catch (ArgumentOutOfRangeException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapGet("/api/reservas/{id:int}", (int id) =>
    reservas.TryGetValue(id, out var reserva)
        ? Results.Ok(reserva)
        : Results.NotFound(new { error = "Reserva no encontrada." }));

app.Run();

record ReservaSolicitud(string Nombre, string Tipo, int Horas);
record ReservaRespuesta(int Id, string Nombre, string Tipo, int Horas, decimal Total);
