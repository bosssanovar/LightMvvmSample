using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Persons
{
    /// <summary>
    /// <see cref="People"/>のデータパケット
    /// </summary>
    public class PeoplePacket
    {
        /// <summary>
        /// <see cref="Person"/>のデータパケット　リスト
        /// </summary>
        public List<PersonPacket> Persons { get; set; }
    }
}
