using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Profiles")]
    public class ApplicantProfilePoco : IPoco
    {
        [Key]
        [Column("Id")]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(SecurityLoginPoco.Id))]
        [Column("Login")]
        [Required]
        public  Guid Login {get; set; }

        [Column("Current_Salary", TypeName ="decimal(18,2)")]
        public decimal?  CurrentSalary {get; set;}

        [Column("Current_Rate", TypeName = "decimal(18,2)")]
        public decimal? CurrentRate { get; set;}

        [Column("Currency")]
        public string? Currency {  get; set; }

        [ForeignKey(nameof(SystemCountryCodePoco.Code))]
        [Column("Country_Code")]
        public string? Country { get; set; }
        
        [Column("State_Province_Code")]
        public string? Province { get; set; } 

        [Column("Street_Address")]
        public string? Street { get; set; }

        [Column("City_Town")]
        public string? City { get; set; }

        [Column("Zip_Postal_Code")]
        public string? PostalCode { get; set; }

        [Required]
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

        // Navigation Properties
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; } 
        public virtual ICollection<ApplicantResumePoco> ApplicantResumes { get; set; }
        public virtual ICollection<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
        public virtual ICollection<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
    }
}
