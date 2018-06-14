using FHYL.Lottery.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FHYL.Lottery.BLL
{
    public class DbService
    {
        public int Savechange()
        {
            return DbContextFactory.CreateDbContext().SaveChanges();
            
        }

    }
}
