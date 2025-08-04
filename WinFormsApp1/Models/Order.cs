using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Order.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public DateTime OrderTime { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // "Pending", "InProgress", "Ready", "Served"

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        // Foreign Keys
        public int WaiterID { get; set; }
        public int? ChefID { get; set; }
        public int TableID { get; set; }

        // Navigation properties
        [ForeignKey("WaiterID")]
        public virtual Waiter Waiter { get; set; } = null!;

        [ForeignKey("ChefID")]
        public virtual Chef? Chef { get; set; }

        [ForeignKey("TableID")]
        public virtual Table Table { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}