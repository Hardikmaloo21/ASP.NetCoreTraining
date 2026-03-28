using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackerBank.API.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Date { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public float Amount { get; set; }

        [Required]
        [MaxLength(20)]
        public string Balance { get; set; } = string.Empty;
    }
}