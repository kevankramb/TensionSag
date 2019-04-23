using TensionSag.Api.Models;

namespace TensionSag.Api.Extensions
{
  public static class WeatherExtensions
  {
    public static double CalculateTension(this Weather weather, Wire wire)
    {
      return 0.0;
    }

    public static double CalculateSag(this Weather weather, Wire wire)
    {
      return 0.0;
    }
  }
}