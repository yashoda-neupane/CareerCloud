using CareerCloud.Poco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Locations")]
    public class CompanyLocationPoco : IPoco
    {
        [Key]
        [Required]
        public  Guid Id { get; set; }

        [ForeignKey(nameof(CompanyProfilePoco.Id))]
        [Column("Company", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Company { get; set; }

        [ForeignKey(nameof(SystemCountryCodePoco.Code))]
        [Column("Country_Code", TypeName = "char(10)")]
        [Required]
        public  string CountryCode { get; set; }

        [Column("State_Province_Code", TypeName = "char(10)")]
        public string? Province { get; set; }  

        [Column("Street_Address", TypeName = "nvarchar(100)")]
        public string? Street { get; set; }

        [Column("City_Town", TypeName = "nvarchar(100)")]
        public string? City { get; set; }

        [Column("Zip_Postal_Code", TypeName = "char(20)")]
        [Required]
        public  string PostalCode { get; set; }

        [Required]
        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public  byte[] TimeStamp { get; set; }

        // Navigation Property
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }

    }
}
