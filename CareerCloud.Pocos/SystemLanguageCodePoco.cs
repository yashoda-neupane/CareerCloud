using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco 
    {
        [Key]
        [Column("LanguageId", TypeName = "char(10)")]
        [Required]
        public  string LanguageID { get; set; }

        [Column("Name", TypeName = "nvarchar(50)")]
        [Required]
        public  string Name { get; set; }

        [Column("Native_Name", TypeName = "nvarchar(50)")]
        [Required]
        public  string NativeName { get; set; }

        // Navigation Property
        public virtual ICollection<CompanyDescriptionPoco> CompanyDescription { get; set; }

    }
}
