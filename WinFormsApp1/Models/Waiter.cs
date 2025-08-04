using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Waiter.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Waiter
    {
        [Key]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [MaxLength(100)]
        public string? Specialty { get; set; }

        [MaxLength(50)]
        public string? ShiftTime { get; set; }

        // Navigation property
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}