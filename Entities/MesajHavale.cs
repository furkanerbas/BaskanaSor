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
    [Table("MesajHavale")]
    public class MesajHavale
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Eposta { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public DateTime MesajTarihi { get; set; }
    }
}
