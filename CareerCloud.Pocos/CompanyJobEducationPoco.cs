using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(CompanyJobPoco.Id))]
        [Column("Job", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Job { get; set; }
        
        [Column("Major", TypeName = "nvarchar(100)")]
        [Required]
        public  string Major {  get; set; }

        [Column("Importance", TypeName = "smallint")]
        [Required]
        public  short Importance { get; set; }

        [Required]
        [Column("Time_Stamp", TypeName = "timestamp")]
        public  byte[]? TimeStamp { get; set; }

        // Navigation Property
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
