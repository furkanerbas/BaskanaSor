using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Personel")]
    public class Personel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Mesaj { get; set; }
        public string MesajTuru { get; set; }
        public DateTime MesajTarihi { get; set; }
        public bool Okundu { get; set; }
        public int birim_Id { get; set; }
        [ForeignKey("birim_Id")]
        public virtual Birim birim { get; set; }
    }
}
