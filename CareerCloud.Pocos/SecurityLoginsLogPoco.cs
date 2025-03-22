using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Log")]
    public class SecurityLoginsLogPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        public required Guid Id { get; set; }

        [ForeignKey(nameof(SecurityLoginPoco.Id))]
        [Column("Login", TypeName = "uniqueIdentifier")]
        public required Guid Login { get; set; }

        [Column("Source_IP", TypeName = "char(15)")]
        public required string SourceIP { get; set; }

        [Column("Logon_Date", TypeName = "Datetime")]
        public required DateTime LogonDate { get; set; }

        [Column("Is_Succesful", TypeName = "bit")]
        public bool IsSuccesful { get; set; }

        // Navigation Property
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
    }
}
