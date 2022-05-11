using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EvidencePojisteni.Models;

namespace EvidencePojisteni.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EvidencePojisteni.Models.Insurance> Insurance { get; set; }
        public DbSet<EvidencePojisteni.Models.Insured> Insured { get; set; }
        public DbSet<EvidencePojisteni.Models.AssignedInsurance> AssignedInsurance { get; set; }
    }
}
