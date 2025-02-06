using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Biogas_BackendEF.Models
{
    [Table("WasteContributor")]
    public class WasteContributor
    {

        [JsonIgnore]
        [Key]
        [Column("ContributorId", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int ContributorId { get; set; }


        [Column("UserId", TypeName = "int")]
        [ForeignKey("User")] // Assuming relation with User entity
        public int userId { get; set; }

        [Column("WasteType", TypeName = "varchar(50)")]
        [Required]
        public string WasteType { get; set; } = null!;

        [Column("WasteQuantity", TypeName = "int")]
        [Required]
        public int WasteQuantity { get; set; }

        [JsonIgnore]
        [Column("CollectedBy", TypeName = "int")]
        [ForeignKey("Producer")]
        public int? CollectedBy { get; set; }

        [Column("ContributionDate", TypeName = "date")]
        [Required]
        public DateOnly ContributionDate { get; set; }

        [Column("Status", TypeName = "varchar(20)")]
        [Required]
        public string Status { get; set; } = null!;

        // Navigation properties (optional)
        [JsonIgnore]
        public virtual User? User { get; set; }

        [JsonIgnore]
        public virtual Producer? Producer { get; set; }

    }
}

