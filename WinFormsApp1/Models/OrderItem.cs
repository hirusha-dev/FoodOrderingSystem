using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/OrderItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(200)]
        public string? SpecialInstructions { get; set; }

        // Navigation properties
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; } = null!;

        [ForeignKey("ItemID")]
        public virtual MenuItem MenuItem { get; set; } = null!;
    }
}