using Microsoft.EntityFrameworkCore;
using OzzaimAhsapMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzzaimAhsap.Models
{
    public class AhsapContext : DbContext
    {
        public DbSet<Geridonusler> Geridonusler { get; set; }
        public DbSet<Products> Products { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("workstation id=OzzaimAhsapDb.mssql.somee.com;packet size=4096;user id=bilalanil_SQLLogin_1;pwd=o7mobs5tsh;data source=OzzaimAhsapDb.mssql.somee.com;persist security info=False;initial catalog=OzzaimAhsapDb;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

      

      
    }

}