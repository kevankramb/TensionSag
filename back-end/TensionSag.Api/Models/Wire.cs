namespace TensionSag.Api.Models
{
  public class Wire
  {
    public string Name { get; }
    public double Length { get; }
    public double Temperature { get; }
    public WireMaterial Material { get; }

    public Wire(string name, double length, double temperature, WireMaterial material)
    {
      Name = name;
      Length = length;
      Temperature = temperature;
      Material = material;
    }
  }
}