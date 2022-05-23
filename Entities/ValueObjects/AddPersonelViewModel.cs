using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ValueObjects
{
    public class AddPersonelViewModel
    {
        public string Ad { get; set; }

        public string Soyad { get; set; }

        [Required(ErrorMessage ="Bu alanın doldurulması zorunludur")]
        public string Eposta { get; set; }
        
        public string MesajTuru { get; set; }
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur")]
        public string Mesaj { get; set; }

        public int birim_Id { get; set; }
        [ForeignKey("birim_Id")]
        public virtual Birim birim { get; set; }
    }
}
