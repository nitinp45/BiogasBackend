using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Biogas_BackendEF.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace Biogas_BackendEF.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("UId", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UId { get; set; }

        [Column("Username", TypeName = "varchar(50)")]
        [Required]
        public string Username { get; set; } = null!;

        [Column("Email", TypeName = "varchar(50)")]
        [Required]
        public string Email { get; set; } = null!;


        [Column("Password", TypeName = "varchar(50)")]
        [Required]
        
        public string Password { get; set; } = null!;

        [Column("Role", TypeName = "varchar(50)")]
        [Required]
        public string Role { get; set; } = null!;
    }

}