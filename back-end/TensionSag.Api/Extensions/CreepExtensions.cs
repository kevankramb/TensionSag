using TensionSag.Api.Models;

namespace TensionSag.Api.Extensions
{
  public static class CreepExtensions
  {
    public static double Calculate(this Creep creep)
    {
      return creep.NumberOne * creep.NumberTwo;
    }
  }
}