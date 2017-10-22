namespace RahyabServices.Business.Domain.Models.Delinquent
{
    public class Notification : IDelinquentEntity{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CustomerDelinquent CustomerDelinquent { get; set; }
        public int CustomerDelinquentId { get; set; }
        public bool IsDone { get; set; }
        public bool IsSeen { get; set; }
        public NotificationType NotificationType { get; set; }
        public Notification SetCustomerDelinquentId(int id){
            CustomerDelinquentId = id;
            return this;
        }
    }
    
}