using CastGroupTurmas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CastGroupTurmas.Data
{
    public class CastContext : DbContext
    {
        public CastContext() : base("StrConn")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Curso> Curso { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //não colocar o nome das tabelas no plural
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}