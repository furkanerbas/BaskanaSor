using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltinorduWebProject.Models
{
    public class MesajDurum
    {
        public string msjTuru { get; set; }
        public int? msjSayi { get; set; }

        public string msjSikayet { get; set; }
        public int? msjSikayetSayi { get; set; }

        public int? msjTesekkurSayi { get; set; }

        public int? msjSayiCevaplanmadi { get; set; }
    }
}