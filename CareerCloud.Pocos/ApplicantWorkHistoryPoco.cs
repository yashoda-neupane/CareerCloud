using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(ApplicantProfilePoco.Id))]
        [Column("Applicant")]
        [Required]
        public  Guid Applicant { get; set; }
        
        [Column("Company_Name", TypeName = "nvarchar(150)")]
        [Required]
        public  string CompanyName { get; set; }

        [ForeignKey(nameof(SystemCountryCodePoco.Code))]
        [Column("Country_Code", TypeName = "char(10)")]
        [Required]
        public  string CountryCode { get; set; }
        
        [Column("Location", TypeName = "nvarchar(50)")]
        [Required]
        public  string Location { get; set; }

        [Column("Job_Title", TypeName = "nvarchar(50)")]
        [Required]
        public  string JobTitle { get; set; }

        [Column("Job_Description", TypeName = "nvarchar(500)")]
        [Required]
        public  string JobDescription { get; set; }

        [Column("Start_Month", TypeName = "smallint")]
        [Required]
        public  short StartMonth { get; set; }

        [Column("Start_Year", TypeName = "int")]
        [Required]
        public  int StartYear { get; set; }

        [Column("End_Month", TypeName = "smallint")]
        [Required]
        public  short EndMonth { get; set; }

        [Column("End_Year", TypeName = "int")]
        [Required]
        public  int EndYear { get; set; }

        [Required]
        [Timestamp]
        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation Properties
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; } 
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; } 
    }
}
