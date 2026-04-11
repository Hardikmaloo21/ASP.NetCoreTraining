using System.ComponentModel.DataAnnotations;
using SmartPhones.models.attributes;


namespace SmartPhones.models.dto
{
    public class CustomerDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [PriceAttribute]
        public double Price { get; set; }
    }
}
