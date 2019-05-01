namespace TensionSag.Api.Models
{
  public class Weather
  {
    public double Temperature { get; }
    public double IceRadius { get; }
    public double WindPressure { get; }
    public double FinalSpanLength { get; }
    public double FinalElevation { get; }

        //final span length and final elevation are not strictly weather related but they fit best here as design case variables. 
        //all units metric mks
    public Weather(double temperature, double iceRadius, double windPressure, double finalSpanLength, double finalElevation)
    {
      Temperature = temperature;
      IceRadius = iceRadius;
      WindPressure = windPressure;
      FinalSpanLength = finalSpanLength;
      FinalElevation = finalElevation;
    }
  }
}