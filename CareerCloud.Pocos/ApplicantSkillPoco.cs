using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Skills")]
    public class ApplicantSkillPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(ApplicantProfilePoco.Id))]
        [Column("Applicant")]
        [Required]
        public  Guid Applicant { get; set; }

        [Column("Skill", TypeName = "nvarchar(100)")]
        [Required]
        public  string Skill { get; set; }

        [Column("Skill_Level", TypeName = "nvarchar(10)")]
        [Required]
        public  string SkillLevel { get; set; }

        [Column("Start_Month", TypeName = "tinyint")]
        [Required]
        public  byte StartMonth {  get; set; }

        [Column("Start_Year", TypeName = "int")]
        [Required]
        public  int StartYear { get; set; }

        [Column("End_Month", TypeName = "tinyint")]
        [Required]
        public  byte EndMonth { get; set; }

        [Column("End_Year", TypeName = "int")]
        [Required]
        public  int EndYear { get; set; }

        [Required]
        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation Property
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; } 
    }
}
