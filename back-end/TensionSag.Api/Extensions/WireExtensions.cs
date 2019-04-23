using TensionSag.Api.Models;

namespace TensionSag.Api.Extensions
{
  public static class WireExtensions
  {
    public static double CalculateTension(this Wire wire)
    {
      return wire.Length * wire.Temperature;
    }

    public static double CalculateSag(this Wire wire)
    {
      return wire.Length * wire.Temperature;
    }
  }
}