using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Roles")]
    public class SecurityRolePoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [Column("Role", TypeName = "varchar(50)")]
        [Required]
        public  string  Role {  get; set ; }

        [Column("Is_Inactive", TypeName = "bit")]
        [Required]
        public  bool IsInactive { get; set ; }

        // Navigation Property
        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
    }
}
