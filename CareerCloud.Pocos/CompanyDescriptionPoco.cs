using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;

namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    { 
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(CompanyProfilePoco.Id))]
        [Column("Company", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Company { get; set; }

        [ForeignKey(nameof(SystemLanguageCodePoco.LanguageID))]
        [Column("LanguageID", TypeName = "char(10)")]
        [Required]
        public  string LanguageId { get; set; }

        [Column("Company_Name", TypeName = "nvarchar(50)")]
        [Required]
        public  string CompanyName { get; set; }
       
        [Column("Company_Description", TypeName = "nvarchar(100)")]
        [Required]
        public  string CompanyDescription { get; set; }
        
        [Required]
        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation Properties
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual SystemLanguageCodePoco SystemLanguageCode { get; set; }
    }
}
