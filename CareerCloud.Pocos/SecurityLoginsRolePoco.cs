using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        public required Guid Id { get; set; }

        [ForeignKey(nameof(SecurityLoginPoco.Id))]
        [Column("Login", TypeName = "uniqueIdentifier")]
        public required Guid Login { get; set; }

        [ForeignKey(nameof(SecurityRolePoco.Id))]
        [Column("Role", TypeName = "uniqueIdentifier")]
        public required Guid Role { get; set; }

        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public byte[]? TimeStamp { get; set; }

        // Navigation properties
        public virtual SecurityRolePoco SecurityRole { get; set; }
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
    }
}
