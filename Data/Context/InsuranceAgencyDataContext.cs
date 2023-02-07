using InsuranceAgency__.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class InsuranceAgencyDataContext: DbContext
    {
        public InsuranceAgencyDataContext(DbContextOptions<InsuranceAgencyDataContext> options)
        : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public DbSet<UserSystem> UserSystems { get; set; }
        public DbSet<TypeTreaty> TypeTreatys { get; set; }
        public DbSet<TypeClient> TypeClients { get; set; }
        public DbSet<Treaty> Treatys { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<InsuranceAgent> InsuranceAgents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<PersonalInfo> Personalinfos { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Applications> Applicationss { get; set; }
        public DbSet<PaymentDocument> PaymentDocuments { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<ImageModel> ImageModels { get; set; }
    }
}
