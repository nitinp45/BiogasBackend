using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Biogas_BackendEF.Models
{
    [Table("Producers")]
    public class Producer
    {

        [Key]
        [Column("ProducerId", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int ProducerId { get; set; }


        [JsonIgnore]
        [Column("UserId", TypeName = "int")]
        [ForeignKey("User")] // Foreign key reference to User table
        public int UserId { get; set; }

        [Column("ProductionCapacity", TypeName = "int")]
        [Required]
        public int ProductionCapacity { get; set; }

        [Column("Status", TypeName = "varchar(20)")]
        [Required]
        public string Status { get; set; } = null!;

        public virtual User? User { get; set; }
    }
}




