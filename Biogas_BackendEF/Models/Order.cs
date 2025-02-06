using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Biogas_BackendEF.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("OrderId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        // Foreign Key to User
        [Column("UId")]
        public int UserId { get; set; }

        // Foreign Key to Biogas
        [Column("BiogasId")]
        public int BiogasId { get; set; }

        [Column("Quantity")]
        public double Quantity { get; set; }

        [Column("Status", TypeName = "varchar(50)")]
        public string Status { get; set; } = "Pending";

        [Column("TransactionId", TypeName = "varchar(100)")]
        public string TransactionId { get; set; } = string.Empty;

        // Navigation property for User
        [JsonIgnore]
        public virtual User User { get; set; }


        [JsonIgnore]
        public virtual Biogas Biogas { get; set; }
    }
}
