using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyContacts.API.Domain.Models;

namespace MyContacts.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>().ToTable("Contacts");
            builder.Entity<Contact>().HasKey(x => x.Id);
            builder.Entity<Contact>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Contact>().Property(x => x.FirstName).IsRequired();
            builder.Entity<Contact>().Property(x => x.LastName).IsRequired();
            builder.Entity<Contact>().Property(x => x.Email).IsRequired();
            builder.Entity<Contact>().HasMany(x => x.Phone).WithOne(y => y.Contact).HasForeignKey(y => y.ContactId);

            builder.Entity<Contact>().HasData
            (
                new Contact
                {
                    Id = 101,
                    FirstName = "Harold",
                    MiddleName = "Francis",
                    LastName = "Gilkey",
                    Street = "8360 High Autumn Row",
                    City = "Cannon",
                    State = "Delaware",
                    Zip = "19797",
                    Email = "harold.gilkey@yahoo.com"
                }
            );

            builder.Entity<Phone>().ToTable("Phones");
            builder.Entity<Phone>().HasKey(x => x.Id);
            builder.Entity<Phone>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Phone>().Property(x => x.Number).IsRequired();
            builder.Entity<Phone>().Property(x => x.Type).IsRequired();

            builder.Entity<Phone>().HasData
            (
                new Phone
                {
                    Id = 101,
                    Number = "302-611-9148",
                    Type = PhoneType.Home,
                    ContactId = 101
                },
                new Phone
                {
                    Id = 102,
                    Number = "302-532-9427",
                    Type = PhoneType.Mobile,
                    ContactId = 101
                }
            );
        }
    }
}
