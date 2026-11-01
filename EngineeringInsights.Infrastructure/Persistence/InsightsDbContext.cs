using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace EngineeringInsights.Infrastructure.Persistence
{
    public class InsightsDbContext : DbContext
    {
        public InsightsDbContext(DbContextOptions<InsightsDbContext> options) : base(options)
        {
        }

        public DbSet<EngineeringInsights.Application.DTOs.Repository> Repositories => Set<EngineeringInsights.Application.DTOs.Repository>();
    }
}
