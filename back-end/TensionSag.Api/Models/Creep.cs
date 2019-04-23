namespace TensionSag.Api.Models
{
  public class Creep
  {
    public double NumberOne { get; }
    public double NumberTwo { get; }

    public Creep(double numberOne, double numberTwo)
    {
      NumberOne = numberOne;
      NumberTwo = numberTwo;
    }
  }
}