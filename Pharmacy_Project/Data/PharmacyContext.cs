using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy_Project.Models;

public partial class PharmacyContext : IdentityDbContext
{
    public PharmacyContext()
    {
    }

    public PharmacyContext(DbContextOptions<PharmacyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Incoming> Incomings { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicinesForDisease> MedicinesForDiseases { get; set; }

    public virtual DbSet<Outgoing> Outgoings { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }
}
