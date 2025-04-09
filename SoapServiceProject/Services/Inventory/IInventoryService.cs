using CoreWCF;

namespace SoapServiceProject.Services.Inventory {
    [ServiceContract]
    public interface IInventoryService {
        [OperationContract]
        string CheckInventory(string productId);

        [OperationContract]
        bool UpdateInventory(string productId, int quantityChange);

        [OperationContract]
        bool SetReorderLevel(string productId, int reorderLevel);
    }
}
