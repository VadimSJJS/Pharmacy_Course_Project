using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Project.Models;

namespace Pharmacy_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Pharmacy_Project.Models.Disease>? Disease { get; set; }
        public DbSet<Pharmacy_Project.Models.Incoming>? Incoming { get; set; }
        public DbSet<Pharmacy_Project.Models.Medicine>? Medicine { get; set; }
        public DbSet<Pharmacy_Project.Models.MedicinesForDisease>? MedicinesForDisease { get; set; }
        public DbSet<Pharmacy_Project.Models.Outgoing>? Outgoing { get; set; }
        public DbSet<Pharmacy_Project.Models.Producer>? Producer { get; set; }
    }
}