using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.Poco;
using System.Collections.Generic;

namespace CareerCloud.Pocos
{
    [Table("Company_Profiles")]
    public class CompanyProfilePoco : IPoco
    {
        [Key]
        [Column("Id", TypeName = "uniqueIdentifier")]
        [Required]
        public  Guid Id { get; set; }

        [Column("Registration_Date", TypeName = "DateTime")]
        [Required]
        public  DateTime RegistrationDate  { get; set; }

        [Column("Company_Website", TypeName = "varchar(100)")]
        public string? CompanyWebsite {  get; set; }

        [Column("Contact_Phone", TypeName = "varchar(20)")]
        [Required]
        public  string ContactPhone { get; set; }

        [Column("Contact_Name", TypeName = "varchar(50)")]
        public string? ContactName { get; set; }

        [Column("Company_Logo", TypeName = "varbinary(max)")]
        public byte[]? CompanyLogo { get; set; }

        [Column("Time_Stamp", TypeName = "TimeStamp")]
        public byte[] TimeStamp { get; set; } = new byte[8];

        // Navigation properties
        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }
        public virtual ICollection<CompanyJobPoco> CompanyJobs { get; set; }

    }
}
