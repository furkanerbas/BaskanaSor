using DataAccessLayer.EntityFramework;
using Entities;
using Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PersonelManager
    {
        private Repository<Personel> repo_personel = new Repository<Personel>();
    }

        
}