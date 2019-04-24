namespace TensionSag.Api.Models
{
  public class Wire
  {
    public string Name { get; }
    public double TotalCrossSection { get; }
    public double CoreCrossSection { get; }
    public double TotalDiameter { get; }
    public double TotalLinearWeight { get; }
    public double MaxRateStrength { get; }
    public double Elasticity { get; }
    public double ThermalCoefficient { get; }
    public double StartingTension { get; }
    public double StartingTemp { get; }
    public double StartingSpanLength { get; }
    public double StartingElevation { get; }
    public bool InitialStartingTension {get; }
    public WireMaterial Material { get; }

    private Wire(string name, double totalCrossSection, double totalLinearWeight, double elasticity, double thermalCoefficient, 
      double startingTension, double startingTemp, double startingSpanLength, double startingElevation, WireMaterial material)
    {
      Name = name;
      TotalCrossSection = totalCrossSection;
      TotalLinearWeight = totalLinearWeight;
      Elasticity = elasticity;
      ThermalCoefficient = thermalCoefficient;
      StartingTension = startingTension;
      StartingTemp = startingTemp;
      StartingSpanLength = startingSpanLength;
      StartingElevation = startingElevation;
      InitialStartingTension = InitialStartingTension;
      Material = material;
    }

    public static Wire Create(string name, double totalCrossSection, double totalLinearWeight, double elasticity, double thermalCoefficient, 
      double startingTension, double startingTemp, double startingSpanLength, double startingElevation, WireMaterial material)
    {
      return new Wire(name, totalCrossSection, totalLinearWeight, elasticity, thermalCoefficient, startingTension, startingTemp, startingSpanLength, startingElevation, material);
    }

  }
}