using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Job_Applications")]
    public class ApplicantJobApplicationPoco : IPoco
    {
        [Key]
        [Required]
        [Column("Id", TypeName = "uniqueIdentifier")]
        public  Guid Id { get; set; }


        [ForeignKey(nameof(ApplicantProfilePoco.Id))]
        [Required]
        [Column("Applicant", TypeName = "uniqueIdentifier")]
        public  Guid Applicant { get; set; }

        [ForeignKey(nameof(CompanyJobPoco.Id))]
        [Column("Job", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Job { get; set; }


        [Column("Application_Date", TypeName = "datetime2")]
        [Required]
        public  DateTime ApplicationDate { get; set; }

        [Required]
        [Column("Time_Stamp", TypeName = "timestamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation Properties
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; } 
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
