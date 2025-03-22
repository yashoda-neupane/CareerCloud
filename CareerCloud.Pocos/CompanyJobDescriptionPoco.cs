using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs_Descriptions")]
    public class CompanyJobDescriptionPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(CompanyJobPoco.Id))]
        [Column("Job", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Job { get; set; }

        [Column("Job_Name", TypeName = "nvarchar(100)")]
        public string? JobName { get; set; }

        [Column("Job_Descriptions", TypeName = "nvarchar(100)")]
        public string? JobDescriptions { get; set; }

        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public byte[]? TimeStamp { get; set; }

        // Navigation Property
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
