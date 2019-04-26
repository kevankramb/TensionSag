namespace TensionSag.Api.Models
{
  public class Wire
  {
    public string Name { get; }
    public double TotalCrossSection { get; }
    public double CoreCrossSection { get; }
    public double InitialWireDiameter { get; }
    public double FinalWireDiameter { get; }
    public double InitialWireLinearWeight { get; }
    public double FinalWireLinearWeight { get; }
    public double MaxRateStrength { get; }
    public double OuterElasticity { get; }
    public double OuterThermalCoefficient { get; }
    public double CoreElasticity { get; }
    public double CoreThermalCoefficient { get; }
    public double OuterStressStrainK0 { get; }
    public double OuterStressStrainK1 { get; }
    public double OuterStressStrainK2 { get; }
    public double OuterStressStrainK3 { get; }
    public double OuterStressStrainK4 { get; }
    public double OuterCreepK0 { get; }
    public double OuterCreepK1 { get; }
    public double OuterCreepK2 { get; }
    public double OuterCreepK3 { get; }
    public double OuterCreepK4 { get; }
    public double CoreStressStrainK0 { get; }
    public double CoreStressStrainK1 { get; }
    public double CoreStressStrainK2 { get; }
    public double CoreStressStrainK3 { get; }
    public double CoreStressStrainK4 { get; }
    public double CoreCreepK0 { get; }
    public double CoreCreepK1 { get; }
    public double CoreCreepK2 { get; }
    public double CoreCreepK3 { get; }
    public double CoreCreepK4 { get; }
    public double StartingTension { get; }
    public double StartingTemp { get; }
    public double StartingSpanLength { get; }
    public double StartingElevation { get; }
    public bool StartingTensionType {get; }
    public WireMaterial Material { get; }

    private Wire(string name, double totalCrossSection, double initialWireDiameter, double finalWireDiameter, double initialWireLinearWeight, double finalWireLinearWeight, double elasticity, double thermalCoefficient, 
        double outerElasticty, double outerThermalCoefficient, double coreElasticity, double coreThermalCoefficient,
        double outerStressStrainK0, double outerStressStrainK1, double outerStressStrainK2, double outerStressStrainK3, double outerStressStrainK4,
        double outerCreepK0, double outerCreepK1, double outerCreepK2, double outerCreepK3, double outerCreepK4,
        double coreStressStrainK0, double coreStressStrainK1, double coreStressStrainK2, double coreStressStrainK3, double coreStressStrainK4,
        double coreCreepK0, double coreCreepK1, double coreCreepK2, double coreCreepK3, double coreCreepK4,
        double startingTension, double startingTemp, double startingSpanLength, double startingElevation, bool startingTensionType , WireMaterial material)
    {
      Name = name;
      TotalCrossSection = totalCrossSection;
      InitialWireDiameter = initialWireDiameter;
      FinalWireDiameter = finalWireDiameter;
      InitialWireLinearWeight = initialWireLinearWeight;
      FinalWireLinearWeight = finalWireLinearWeight;
      OuterElasticity = outerElasticty;
      OuterThermalCoefficient = outerThermalCoefficient;
      CoreElasticity = coreElasticity;
      CoreThermalCoefficient = coreThermalCoefficient;
      OuterStressStrainK0 = outerStressStrainK0;
      OuterStressStrainK1 = outerStressStrainK1;
      OuterStressStrainK2 = outerStressStrainK2;
      OuterStressStrainK3 = outerStressStrainK3;
      OuterStressStrainK4 = outerStressStrainK4;
      OuterCreepK0 = outerCreepK0;
      OuterCreepK1 = outerCreepK1;
      OuterCreepK2 = outerCreepK2;
      OuterCreepK3 = outerCreepK3;
      OuterCreepK4 = outerCreepK4;
      CoreStressStrainK0 = coreStressStrainK0;
      CoreStressStrainK1 = coreStressStrainK1;
      CoreStressStrainK2 = coreStressStrainK2;
      CoreStressStrainK3 = coreStressStrainK3;
      CoreStressStrainK4 = coreStressStrainK4;
      CoreCreepK0 = coreCreepK0;
      CoreCreepK1 = coreCreepK1;
      CoreCreepK2 = coreCreepK2;
      CoreCreepK3 = coreCreepK3;
      CoreCreepK4 = coreCreepK4;
      StartingTension = startingTension;
      StartingTemp = startingTemp;
      StartingSpanLength = startingSpanLength;
      StartingElevation = startingElevation;
      StartingTensionType = startingTensionType;
      Material = material;
    }

    public static Wire Create(string name, double totalCrossSection, double initialWireDiameter, double finalWireDiameter, double initialWireLinearWeight, double finalWireLinearWeight, 
        double outerElasticity, double outerThermalCoefficient, double coreElasticity, double coreThermalCoefficient,
        double outerStressStrainK0, double outerStressStrainK1, double outerStressStrainK2, double outerStressStrainK3, double outerStressStrainK4,
        double outerCreepK0, double outerCreepK1, double outerCreepK2, double outerCreepK3, double outerCreepK4,
        double coreStressStrainK0, double coreStressStrainK1, double coreStressStrainK2, double coreStressStrainK3, double coreStressStrainK4,
        double coreCreepK0, double coreCreepK1, double coreCreepK2, double coreCreepK3, double coreCreepK4,
        double startingTension, double startingTemp, double startingSpanLength, double startingElevation, bool startingTensionType, WireMaterial material)
    {
      return new Wire(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, 
          outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
          outerStressStrainK0, outerStressStrainK1, outerStressStrainK2, outerStressStrainK3, outerStressStrainK4,
          outerCreepK0, outerCreepK1, outerCreepK2, outerCreepK3, outerCreepK4,
          coreStressStrainK0, coreStressStrainK1, coreStressStrainK2, coreStressStrainK3, coreStressStrainK4,
          coreCreepK0, coreCreepK1, coreCreepK2, coreCreepK3, coreCreepK4,
          startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
    }

  }
}