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
    public WireMaterial Material { get; }

    private Wire(string name, double totalCrossSection, double totalLinearWeight, double elasticity, double thermalCoefficient, WireMaterial material)
    {
      Name = name;
      TotalCrossSection = totalCrossSection;
      TotalLinearWeight = totalLinearWeight;
      Elasticity = elasticity;
      ThermalCoefficient = thermalCoefficient;
      Material = material;
    }

    public static Wire Create(string name, double totalCrossSection, double totalLinearWeight, double elasticity, double thermalCoefficient, WireMaterial material)
    {
      return new Wire(name, totalCrossSection, totalLinearWeight, elasticity, thermalCoefficient, material);
    }

  }
}