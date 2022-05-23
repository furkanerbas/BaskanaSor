using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AltinorduWebProject.Models.Managers
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Birim> Birimler { get; set; }
        public DbSet<MesajYanit> MesajYanitlari { get; set; }
        public DbSet<MesajHavale> MesajlarHavale { get; set; }
    }
}