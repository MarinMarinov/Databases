using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1CreateDbContextForNorthwind;

namespace Task6NorthwindTwin
{
    public class Startup
    {
        public static void Main()
        {
            var db = new NorthwindEntities();

            db.Database.CreateIfNotExists();
        }
    }
}
