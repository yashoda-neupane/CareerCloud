using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Resumes")]
    public class ApplicantResumePoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(ApplicantProfilePoco.Id))]
        [Column("Applicant")]
        [Required]
        public  Guid Applicant { get; set; }

        [Column("Resume", TypeName ="nvarchar(max)")]
        [Required]
        public  string Resume { get; set; }

        [Column("Last_Updated", TypeName ="datetime2")]
        public DateTime? LastUpdated { get; set; }

        // Navigation Property
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; } 

    }
}
