using _12591_API.Modals;

namespace _12591_API.Models
{
    public class FutureGoal
    {
        public int Id { get; set; }  
        public string GoalName { get; set; }
        public string Description { get; set; } 
        public DateTime TargetDate { get; set; } 
        public int UserId { get; set; }  // Foreign key to User
        public User User { get; set; } 
    }
}
