using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    
    public class SystemCountryCodePoco 
    {   
        [Key]
        [Column("Code", TypeName = "char(10)")]
        [Required]
        public  string Code {  get; set; }

        [Column("Name", TypeName = "nvarchar(50)")]
        [Required]
        public  string Name { get; set; }

        // Navigation properties 
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }
    }
}
