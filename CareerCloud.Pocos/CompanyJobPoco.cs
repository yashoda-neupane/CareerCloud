using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        public required Guid Id { get; set; }

        [ForeignKey(nameof(CompanyJobPoco.Id))]
        [Column("Company", TypeName = "uniqueIdentifier")]
        public required Guid Company {  get; set; }

        [Column("Profile_Created", TypeName = "datetime2")]
        public required DateTime ProfileCreated { get; set; }

        [Column("Is_Inactive", TypeName = "bit")]
        public required bool IsInactive { get; set; }

        [Column("Is_Company_Hidden", TypeName = "bit")]
        public required bool IsCompanyHidden { get; set; }

        [Column("Time_Stamp", TypeName = "timestamp")]
        public byte[]? TimeStamp { get; set; }

        // Navigation Properties
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
    }
}
