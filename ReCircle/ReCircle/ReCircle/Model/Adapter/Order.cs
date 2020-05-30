using System;
namespace ReCircle.Model.Adapter
{
    public class Order
    {
        public string Products { get; set; }
        public string TypePay { get; set; }
        public string TotalPrice { get; set; }
        public string Address { get; set; }
        public string ClientName { get; set; }
        public string AllyName { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
    }
}
