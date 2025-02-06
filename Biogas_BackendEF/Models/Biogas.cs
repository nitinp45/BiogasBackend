using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Biogas_BackendEF.Models
{
    [Table("BiogasInventory")]
    public class Biogas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures auto-increment
        public int BiogasId { get; set; } // Primary key

        [Required]
        [ForeignKey("Producer")]
        public int ProducerId { get; set; } // Foreign key to the Producer model

        [Required]
        public double Quantity { get; set; }

        [Required]
        public double Price { get; set; } // Price of the biogas

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [JsonIgnore]
        public virtual Producer? Producer { get; set; }
    }
}
