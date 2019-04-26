namespace TensionSag.Api.Models
{
  public class Creep
  {
    public double CreepRTSPercent { get; }
    public double NumberTwo { get; }

    public Creep(double creepRTSPercent, double numberTwo)
    {
            CreepRTSPercent = creepRTSPercent;
      NumberTwo = numberTwo;
    }
  }
}