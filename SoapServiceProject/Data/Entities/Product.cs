namespace SoapServiceProject.Data.Entities {
    public class Product
    {
        public string ProductId { get; set; }  // Primary Key
        public string Name { get; set; }
        public int Stock { get; set; }
        public int ReorderLevel { get; set; }
    }
}
