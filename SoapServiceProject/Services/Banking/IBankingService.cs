using CoreWCF;

namespace SoapServiceProject.Services.Banking {

    [ServiceContract]
    public interface IBankingService {
        [OperationContract]
        string GetAccountBalance(string accountId);

        [OperationContract]
        bool Deposit(string accountId, decimal amount);

        [OperationContract]
        bool Withdraw(string accountId, decimal amount);
    }
}
