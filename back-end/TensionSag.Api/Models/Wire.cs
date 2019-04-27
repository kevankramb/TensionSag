using System.Collections.Generic;

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
        public double MaxRatedStrength { get; }
        public double OuterElasticity { get; }
        public double OuterThermalCoefficient { get; }
        public double CoreElasticity { get; }
        public double CoreThermalCoefficient { get; }
        public List<double> OuterStressStrainList { get; }
        public List<double> OuterCreepList { get; }
        public List<double> CoreStressStrainList { get; }
        public List<double> CoreCreepList { get; }
        public double StartingTension { get; }
        public double StartingTemp { get; }
        public double StartingSpanLength { get; }
        public double StartingElevation { get; }
        public bool StartingTensionType { get; }
        public WireMaterial Material { get; }

        private Wire(string name, double totalCrossSection, double initialWireDiameter, double finalWireDiameter, double initialWireLinearWeight, double finalWireLinearWeight, double maxRatedStrength,
            double outerElasticty, double outerThermalCoefficient, double coreElasticity, double coreThermalCoefficient,
            List<double> outerStressStrainList, List<double> outerCreepList, List<double> coreStressStrainList, List<double> coreCreepList,
            double startingTension, double startingTemp, double startingSpanLength, double startingElevation, bool startingTensionType, WireMaterial material)
        {
            Name = name;
            TotalCrossSection = totalCrossSection;
            InitialWireDiameter = initialWireDiameter;
            FinalWireDiameter = finalWireDiameter;
            InitialWireLinearWeight = initialWireLinearWeight;
            FinalWireLinearWeight = finalWireLinearWeight;
            MaxRatedStrength = maxRatedStrength;
            OuterElasticity = outerElasticty;
            OuterThermalCoefficient = outerThermalCoefficient;
            CoreElasticity = coreElasticity;
            CoreThermalCoefficient = coreThermalCoefficient;
            OuterStressStrainList = outerStressStrainList;
            OuterCreepList = outerCreepList;
            CoreStressStrainList = coreStressStrainList;
            CoreCreepList = coreCreepList;
            StartingTension = startingTension;
            StartingTemp = startingTemp;
            StartingSpanLength = startingSpanLength;
            StartingElevation = startingElevation;
            StartingTensionType = startingTensionType;
            Material = material;
        }

        public static Wire Create(string name, double totalCrossSection, double initialWireDiameter, double finalWireDiameter, double initialWireLinearWeight, double finalWireLinearWeight, double maxRatedStrength,
            double outerElasticity, double outerThermalCoefficient, double coreElasticity, double coreThermalCoefficient,
            List<double> outerStressStrainList, List<double> outerCreepList, List<double> coreStressStrainList, List<double> coreCreepList,
            double startingTension, double startingTemp, double startingSpanLength, double startingElevation, bool startingTensionType, WireMaterial material)
        {
            return new Wire(name, totalCrossSection, initialWireDiameter, finalWireDiameter, initialWireLinearWeight, finalWireLinearWeight, maxRatedStrength,
                outerElasticity, outerThermalCoefficient, coreElasticity, coreThermalCoefficient,
                outerStressStrainList, outerCreepList, coreStressStrainList, coreCreepList,
                startingTension, startingTemp, startingSpanLength, startingElevation, startingTensionType, material);
        }

    }
}