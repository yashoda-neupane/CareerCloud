using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }


        [ForeignKey("Job")]
        [Column("Job", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Job { get; set; }

        [Column("Skill", TypeName = "nvarchar(100)")]
        [Required]
        public  string Skill {  get; set; }

        [Column("Skill_Level", TypeName = "nvarchar(10)")]
        [Required]
        public  string SkillLevel { get; set; }

        [Column("Importance", TypeName = "int")]
        [Required]
        public  int Importance { get; set; }

        [Required]
        [Column("Time_Stamp", TypeName = "timestamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation Property
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
