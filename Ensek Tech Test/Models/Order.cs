using System;

namespace Ensek_Tech_Test.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Fuel { get; set; }
        public int Quantity { get;set; }
        public DateTimeOffset Time { get; set; }
    }
}
