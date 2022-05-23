using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<MesajTuru> MesajTurleri { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<MesajYanit> MesajYanitlari { get; set; }
        public DbSet<MesajHavale> MesajlarHavale { get; set; }
    }
}
