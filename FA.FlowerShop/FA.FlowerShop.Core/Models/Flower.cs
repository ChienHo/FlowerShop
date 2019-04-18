using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.FlowerShop.Core
{
    public class Flower
    {
        [Key]
        public int FlowerId { get; set; }

        [Required(ErrorMessage = "The Flower name must be required")]
        [StringLength(100, ErrorMessage = "The flower name must be between 3 and 100 characters", MinimumLength = 3)]
        public string FlowerName { get; set; }

        [StringLength(1024, ErrorMessage = "The description of flower must be between 3 and 1024 characters", MinimumLength = 3)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public Color Color { get; set; }

        [DisplayName("Store Date")]
        public DateTimeOffset StoreDate { get; set; }

        [DisplayName("Store Inventory")]
        public int StoreInventory { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
