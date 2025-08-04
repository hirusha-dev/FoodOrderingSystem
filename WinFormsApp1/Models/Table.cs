using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Table.cs
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystem.Models
{
    public class Table
    {
        [Key]
        public int TableID { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Available"; // "Available", "Occupied"

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}