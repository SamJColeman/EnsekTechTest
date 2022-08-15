using Newtonsoft.Json;

namespace Ensek_Tech_Test.Models
{
    public class BaseEnergy
    {
        [JsonProperty("energy_id")]
        public int EnergyId { get; set; }
        [JsonProperty("price_per_unit")]
        public decimal PricePerUnit { get; set; }
        [JsonProperty("quantity_of_units")]
        public decimal QuantityOfUnits { get; set; }
        [JsonProperty("unit_type")]
        public string UnitType { get; set; } = string.Empty;
    }
}
