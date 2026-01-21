namespace GymApp.Models
{
    public class GymPass
    {
        public int GymPassId { get; set; }

        public int GymCustomerId { get; set; } // Foreign Key

        public string CustomerName { get; set; }

        public DateTime ExpiryDate { get; set; }

        public GymCustomer GymCustomer { get; set; } // Navigation Property
    }
}
