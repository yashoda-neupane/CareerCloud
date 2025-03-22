using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        protected readonly string _connStr = string.Empty;
        public CareerCloudContext()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        // DbSet properties for each POCO
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    optionsBuilder.UseSqlServer(_connStr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database connection failed: {ex.Message}");
                    throw;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(ap => ap.SecurityLogin)
                .WithMany(s => s.ApplicantProfiles)
                .HasForeignKey(ap => ap.Login);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .Ignore(ap => ap.TimeStamp)
                .HasOne(ap => ap.SystemCountryCode)
                .WithMany(c => c.ApplicantProfiles)
                .HasForeignKey(ap => ap.Country);

            modelBuilder.Entity<ApplicantEducationPoco>()
                .Ignore(ae => ae.TimeStamp)
                .HasOne(ae => ae.ApplicantProfile)
                .WithMany(a => a.ApplicantEducations)
                .HasForeignKey(ae => ae.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .Ignore(aja => aja.TimeStamp)
                .HasOne(aja => aja.ApplicantProfile)
                .WithMany(a => a.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(aja => aja.CompanyJob)
                .WithMany(cj => cj.ApplicantJobApplication)
                .HasForeignKey(aja => aja.Job);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(ar => ar.ApplicantProfile)
                .WithMany(a => a.ApplicantResumes)
                .HasForeignKey(ar => ar.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .Ignore(s => s.TimeStamp)
                .HasOne(s => s.ApplicantProfile)
                .WithMany(a => a.ApplicantSkills)
                .HasForeignKey(s => s.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(aw => aw.ApplicantProfile)
                .WithMany(a => a.ApplicantWorkHistorys)
                .HasForeignKey(aw => aw.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(aw => aw.SystemCountryCode)
                .WithMany(sc => sc.ApplicantWorkHistory)
                .HasForeignKey(aw => aw.CountryCode);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .Ignore(cd => cd.TimeStamp)
                .HasOne(cd => cd.CompanyProfile)
                .WithMany(c => c.CompanyDescriptions)
                .HasForeignKey(cd => cd.Company);

           
            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(cd => cd.SystemLanguageCode)
                .WithMany(sl => sl.CompanyDescription)
                .HasForeignKey(cd => cd.LanguageId);

            modelBuilder.Entity<CompanyJobPoco>()
                .Ignore(cj => cj.TimeStamp)
                .HasOne(cj => cj.CompanyProfile)
                .WithMany(c => c.CompanyJobs)
                .HasForeignKey(cj => cj.Company);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .Ignore(cje => cje.TimeStamp)
                .HasOne(cje => cje.CompanyJob)
                .WithMany(cj => cj.CompanyJobEducations)
                .HasForeignKey(cje => cje.Job);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .Ignore(cjd => cjd.TimeStamp)
                .HasOne(cjd => cjd.CompanyJob)
                .WithMany(cj => cj.CompanyJobDescriptions)
                .HasForeignKey(cjd => cjd.Job);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .Ignore(cjs => cjs.TimeStamp)
                .HasOne(cjs => cjs.CompanyJob)
                .WithMany(cj => cj.CompanyJobSkills)
                .HasForeignKey(cjs => cjs.Job);

            modelBuilder.Entity<CompanyLocationPoco>()
                .Ignore(cl => cl.TimeStamp)
                .HasOne(cl => cl.CompanyProfile)
                .WithMany(c => c.CompanyLocations)
                .HasForeignKey(cl => cl.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(cl => cl.SystemCountryCode)
                .WithMany(c => c.CompanyLocations)
                .HasForeignKey(cl =>cl.CountryCode);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(sll => sll.SecurityLogin)
                .WithMany(s => s.SecurityLoginsLogs)
                .HasForeignKey(sll => sll.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .Ignore(sl => sl.TimeStamp)
                .HasOne(sl => sl.SecurityLogin)
                .WithMany(s => s.SecurityLoginsRoles)
                .HasForeignKey(sl => sl.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(slr => slr.SecurityRole)
                .WithMany(sr => sr.SecurityLoginsRoles)
                .HasForeignKey(slr => slr.Role);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(cj => cj.CompanyProfile)
                .WithMany(cp => cp.CompanyJobs)
                .HasForeignKey(cj => cj.Company);

            modelBuilder.Entity<SecurityLoginPoco>()
                .Ignore(sl => sl.TimeStamp);

            modelBuilder.Entity<CompanyProfilePoco>()
                .Ignore(cp => cp.TimeStamp);
 
            base.OnModelCreating(modelBuilder);
        }

    }
}
