using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tribes.Database.Dal.Poco
{
    public class TribePoco
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<ClanPoco> Clans { get; set; }
    }

    
}
