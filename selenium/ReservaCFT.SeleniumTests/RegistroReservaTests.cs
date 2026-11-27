using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ReservaCFT.SeleniumTests;

public class RegistroReservaTests
{
    private const string BaseUrl = "http://localhost:5080";

    [Fact]
    public void ReservaValida_MuestraConfirmacion()
    {
        using IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl(BaseUrl);

        driver.FindElement(By.Id("nombre")).SendKeys("Ana Perez");
        new SelectElement(driver.FindElement(By.Id("tipo"))).SelectByText("Sala");
        new SelectElement(driver.FindElement(By.Id("horas"))).SelectByText("3");
        driver.FindElement(By.Id("btnReservar")).Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var texto = wait.Until(d =>
        {
            var value = d.FindElement(By.Id("resultado")).Text;
            return value.StartsWith("Reserva confirmada") ? value : null;
        });

        Assert.Contains("$54.000", texto);
    }

    [Fact]
    public void NombreVacio_MuestraMensajeError()
    {
        using IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl(BaseUrl);

        driver.FindElement(By.Id("nombre")).Clear();
        driver.FindElement(By.Id("btnReservar")).Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var texto = wait.Until(d =>
        {
            var value = d.FindElement(By.Id("resultado")).Text.Trim();
            if (string.IsNullOrWhiteSpace(value) || value == "Procesando...")
            {
                return null;
            }
            return value;
        });

        Assert.False(string.IsNullOrWhiteSpace(texto));
        Assert.DoesNotContain("Reserva confirmada", texto);
    }
}
