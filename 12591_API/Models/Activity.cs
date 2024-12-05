using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _12591_API.Modals
{
    public class Activity
    {
        [Key]
        public int Id { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key to the user
        public string ActivityType { get; set; } // Type of the activity (e.g., Running, Cycling, etc.)
        public double Duration { get; set; } // Duration in minutes
        public double CaloriesBurned { get; set; } // Calories burned during the activity
        public DateTime Date { get; set; } // The date when the activity was done
    }
}
