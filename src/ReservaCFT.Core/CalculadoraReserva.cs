namespace ReservaCFT.Core;

public class CalculadoraReserva
{
    public decimal CalcularTotal(string tipo, int horas)
    {
        if (horas < 1 || horas > 4)
            throw new ArgumentOutOfRangeException(nameof(horas), "Las horas deben estar entre 1 y 4.");

        decimal tarifa = tipo.ToLowerInvariant() switch
        {
            "sala" => 20000m,
            "auditorio" => 40000m,
            _ => throw new ArgumentException("Tipo de espacio no valido.", nameof(tipo))
        };

        decimal subtotal = tarifa * horas;
        return horas >= 3 ? subtotal * 0.90m : subtotal;
    }
}
