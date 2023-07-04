using Microsoft.EntityFrameworkCore;

namespace InTech_MVC.Models
{
    public class DataContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<CardSection> CardSection { get; set; }
        public DbSet<HeaderSection> HeaderSection { get; set; }
        public DbSet<PhotosSection> PhotosSections { get; set; }
        public DbSet<Photo2Section> Photo2Sections { get; set; }
        public DbSet<OurTeamSection> OurTeamSections { get; set; }
        public DbSet<ServicesSection> ServicesSections { get; set; }
        public DbSet<SosialMediaIcon> SosialMediaIcons { get; set; }
        public DbSet<OurTeamTitle1> OurTeamTitle1s { get; set; }
        public DbSet<OurTeamTitle2> OurTeamTitle2s { get; set; }
        public DbSet<OurTeamTitleMember> OurTeamTitleMember1s { get; set; }
        public DbSet<OurTeamMember> OurTeamMember { get; set; }
        public DbSet<ServicesSectionTitle> ServicesSectionTitle { get; set; }
        public DbSet<SeeMoreLink> SeeMoreLinks { get; set; }
        public DbSet<Photos2Title1> Photos2Title1s { get; set; }
        public DbSet<Photos2Title2> Photos2Title2s { get; set; }
        public DbSet<ClientsTitle1> clientsTitle1s { get; set; }
        public DbSet<ClientsTitle2> clientsTitle2s { get; set; }
        public DbSet<ClientsMember> clientsMember1s { get; set; }
        public DbSet<ClientsMembers1cs> ClientsMembers1Cs { get; set; }
        public DbSet<ProjectTitle1> ProjectTitle1s { get; set; }

    }
}
