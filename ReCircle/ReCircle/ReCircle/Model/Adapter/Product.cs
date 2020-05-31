using System;
namespace ReCircle.Model.Adapter
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string UrlImage { get; set; }

    }
}
