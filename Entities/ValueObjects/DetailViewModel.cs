using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ValueObjects
{
    public class DetailViewModel
    {
        public string receiverEmail { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public virtual Personel Personels { get; set; }
    }
}
