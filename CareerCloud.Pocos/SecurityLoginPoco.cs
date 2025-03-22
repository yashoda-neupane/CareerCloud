using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins")]
    public class SecurityLoginPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [Column("Login", TypeName = "varchar(50)")]
        [Required]
        public  string Login { get; set; }

        [Column("Password", TypeName = "varchar(100)")]
        [Required]
        public  string Password { get; set; }

        [Column("Created_Date", TypeName = "Datetime")]
        [Required]
        public  DateTime Created { get; set; }

        [Column("Password_Update_Date", TypeName = "Datetime")]
        public DateTime? PasswordUpdate { get; set; }

        [Column("Agreement_Accepted_Date", TypeName = "Datetime")]
        public DateTime? AgreementAccepted { get; set; }

        [Column("Is_Locked", TypeName = "bit")]
        [Required]
        public  bool IsLocked { get; set; }

        [Column("Is_Inactive", TypeName = "bit")]
        [Required]
        public  bool IsInactive { get; set; }

        [Column("Email_Address", TypeName = "varchar(50)")]
        [Required]
        public  string EmailAddress { get; set; }

        [Column("Phone_Number", TypeName = "varchar(20)")]
        public string? PhoneNumber { get; set; }

        [Column("Full_Name", TypeName = "varchar(100)")]
        public string? FullName { get; set; }

        [Column("Force_Change_Password", TypeName = "bit")]
        [Required]
        public  bool ForceChangePassword {  get; set; }

        [Column("Prefferred_Language", TypeName = "varchar(10)")]
        public string? PrefferredLanguage { get; set; }

        [Required]
        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation properties
        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
    }
}
