using Xunit;
using ReservaCFT.Core;

namespace ReservaCFT.Tests;

public class CalculadoraReservaTests
{
    private readonly CalculadoraReserva _calculadora = new();

    [Fact]
    public void Sala_1Hora_Retorna20000()
    {
        var resultado = _calculadora.CalcularTotal("Sala", 1);
        Assert.Equal(20000m, resultado);
    }

    [Fact]
    public void Sala_3Horas_AplicaDescuento()
    {
        var resultado = _calculadora.CalcularTotal("Sala", 3);

        // ATENCION: esta expectativa es deliberadamente incorrecta para la evaluacion.
        Assert.Equal(54000m, resultado);
    }

    [Fact]
    public void Horas0_LanzaExcepcion()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculadora.CalcularTotal("Sala", 0));
    }

    [Theory]
    [InlineData("Auditorio", 1, 40000)]
    [InlineData("Auditorio", 2, 80000)]
    [InlineData("Auditorio", 4, 144000)]
    public void Auditorio_DatosValidos_RetornaEsperado(string tipo, int horas, decimal esperado)
    {
        var resultado = _calculadora.CalcularTotal(tipo, horas);
        Assert.Equal(esperado, resultado);
    }
}
