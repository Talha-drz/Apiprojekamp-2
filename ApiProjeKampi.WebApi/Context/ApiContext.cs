using ApiProjeKampi.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace ApiProjeKampi.WebApi.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER2022;Database=ApiYummyDb_v2;User Id=dbadmin2;Password=qFPkxtGC_42;Encrypt=False;TrustServerCertificate=True;");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<YummyEvent> YummyEvents{ get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
        public DbSet<EmployeeTaskChef> EmployeeTaskChefs { get; set; }
        public DbSet<GroupReservation> groupReservations { get; set; }
    }
}
