namespace RahyabServices.Business.Facades.Interfaces{
    public interface ISharepointFacade{
        void StartRequestSplitWorkFlow(int delinquentCustomerId, int requestStateHandlerId, string userNameEncrypted);
        void StartRequestGivingAChanceWorkFlow(int delinquentCustomerId, int requestStateHandlerId, string userNameEncrypted);
        void StartRequestClearingWorkFlow(int delinquentCustomerId, int requestStateHandlerId, string userNameEncrypted);
        void StartRequestImpunityForCrimesWorkFlow(int delinquentCustomerId, int requestStateHandlerId, string userNameEncrypted);
    }
}