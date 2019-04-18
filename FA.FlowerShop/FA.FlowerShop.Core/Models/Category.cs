using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.FlowerShop.Core
{
   public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The category name must be required")]
        [DisplayName("Category Name")]
        [StringLength(100, ErrorMessage = "The category name must be between 3 and 100 characters", MinimumLength = 3)]
        public string CategoryName { get; set; }

        [DefaultValue(1)]
        public int Order { get; set; }

        [StringLength(1024, ErrorMessage = "The note of category must be between 3 and 1024 characters", MinimumLength = 10)]
        public string Notes { get; set; }

        public ICollection<Flower> Flowers { get; set; }
    }
}
