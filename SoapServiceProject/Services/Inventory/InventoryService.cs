using CoreWCF;
using System.Collections.Concurrent;

namespace SoapServiceProject.Services.Inventory {
    public class InventoryService : IInventoryService {
        
        private static ConcurrentDictionary<string, int> Stocks = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<string, int> ReorderLevels = new ConcurrentDictionary<string, int>();


        static InventoryService() {
            Stocks["P001"] = 500;
            ReorderLevels["P001"] = 100;
        }

        public string CheckInventory(string productId) {
            if(Stocks.TryGetValue(productId, out int stock)) {
                string result = $"Product ID: {productId}, Stock: {stock}";
                if(ReorderLevels.TryGetValue(productId, out int reorderLevel)) {
                    result += $", Reorder Level: {reorderLevel}";
                }
                return result;
            }
            else {
                return "Product not found";              
            }
        }

        public bool UpdateInventory(string productId, int quantityChange)
        {
            if (Stocks.ContainsKey(productId))
            {
                Stocks.AddOrUpdate(productId, quantityChange, (id, oldStock) => oldStock + quantityChange);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetReorderLevel(string productId, int reorderLevel)
        {
            if (reorderLevel < 0)
                return false;

            ReorderLevels.AddOrUpdate(productId, reorderLevel, (id, oldThreshold) => reorderLevel);
            return true;
        }
        
    
        
    }
}
