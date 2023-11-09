using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// Entity全体のデータパケット
    /// </summary>
    public class EntityPacket
    {
        /// <summary>
        /// <see cref="Entity.Persons.People"/>クラスのデータパケット
        /// </summary>
        public PeoplePacket People { get; set; }
    }
}
