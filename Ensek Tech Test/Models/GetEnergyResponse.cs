namespace Ensek_Tech_Test.Models
{
    public class GetEnergyResponse
    {
        public Electric Electric { get; set; }
        public Gas Gas { get; set; }
        public Nuclear Nuclear { get; set; }
        public Oil Oil { get; set; }
    }
}
