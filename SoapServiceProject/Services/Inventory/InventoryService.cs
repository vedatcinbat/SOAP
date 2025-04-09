using CoreWCF;
using System.Collections.Concurrent;
using SoapServiceProject.Data.Entities;
using SoapServiceProject.Data;

namespace SoapServiceProject.Services.Inventory {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class InventoryService : IInventoryService {

        private readonly TestDbContext _context;

        public InventoryService(TestDbContext context) {
            _context = context;
        }
        
        private static ConcurrentDictionary<string, int> Stocks = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<string, int> ReorderLevels = new ConcurrentDictionary<string, int>();

        public string CreateProduct(string productId, string name, int stock, int reorderLevel) {
            var product = _context.Products.Find(productId);
            if(product != null) {
                return "Product already exists";
            }

            product = new Product {
                ProductId = productId,  
                Name = name,
                Stock = stock,
                ReorderLevel = reorderLevel
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return "Product created successfully";
        }


        public string CheckInventory(string productId) {
            var product = _context.Products.Find(productId);
            if (product == null) {
                return "Product not found";
            }
            return $"Product ID: {product.ProductId}, Name: {product.Name}, Stock: {product.Stock}";
        }

        public bool UpdateInventory(string productId, int quantityChange)
        {
            var product = _context.Products.Find(productId);

            if(product != null) {
                product.Stock += quantityChange;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SetReorderLevel(string productId, int reorderLevel)
        {
            var product = _context.Products.Find(productId);
            if(product != null) {
                product.ReorderLevel = reorderLevel;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        
        
    }
}
