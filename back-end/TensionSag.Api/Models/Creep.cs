namespace TensionSag.Api.Models
{
    public class Creep
    {
        public double CreepRTSPercent { get; }

        public Creep(double creepRTSPercent)
        {
            CreepRTSPercent = creepRTSPercent;
        }
    }
}