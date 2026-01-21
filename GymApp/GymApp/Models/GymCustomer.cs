namespace GymApp.Models
{
    public class GymCustomer
    {
        public int GymCustomerId { get; set; }

        public int UserID { get; set; } // Foreign key for linking to a user, we need userID because userId is necessary for every model which stires personal data to a user. For example a workout plan.

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public ICollection<GymPass>? GymPass { get; set; } // ICOllection , its multiple because customers can have multiple GymPasses in their lifetime, they arent stuck to one
    }
}
