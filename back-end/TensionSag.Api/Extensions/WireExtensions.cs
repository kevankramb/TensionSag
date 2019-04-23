using TensionSag.Api.Models;

namespace TensionSag.Api.Extensions
{
  public static class WireExtensions
  {
    public static double CalculateTension(this Wire wire)
    {
      return wire.TotalCrossSection * wire.TotalLinearWeight;
    }

    public static double CalculateSag(this Wire wire)
    {
      return wire.TotalCrossSection * wire.TotalLinearWeight;
    }
  }
}