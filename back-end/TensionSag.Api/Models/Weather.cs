namespace TensionSag.Api.Models
{
  public class Weather
  {
    public double Temperature { get; }
    public double IceRadius { get; }
    public double WindPressure { get; }

    public Weather(double temperature, double iceRadius, double windPressure)
    {
      Temperature = temperature;
      IceRadius = iceRadius;
      WindPressure = windPressure;
    }
  }
}