using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shortener.Web.Entities;

namespace Shortener.Web.Database
{
    public class ShortenerDbContext : DbContext
    {
        public ShortenerDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UrlEntity> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

    }
}
