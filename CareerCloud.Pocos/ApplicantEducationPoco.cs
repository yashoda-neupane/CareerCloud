using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Educations")]
    public class ApplicantEducationPoco : IPoco
    {
        [Key]
        [Required]
        [Column("Id", TypeName = "uniqueIdentifier")]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(ApplicantProfilePoco.Id))]
        [Required]
        [Column("Applicant", TypeName = "uniqueIdentifier")]
        public  Guid Applicant { get; set; }

        [Required]
        [Column("Major", TypeName = "nvarchar(100)")]
        public  string Major { get; set; }

        [Column("Certificate_Diploma", TypeName = "nvarchar(100)")]
        public string? CertificateDiploma { get; set; }

        [Column("Start_Date", TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column("Completion_Date", TypeName = "date")]
        public DateTime? CompletionDate { get; set; }

        [Column("Completion_Percent", TypeName = "tinyint")]
        public byte? CompletionPercent { get; set; }

        [Required]
        [Column("Time_Stamp", TypeName = "timestamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation Property
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; } 
    }
}
