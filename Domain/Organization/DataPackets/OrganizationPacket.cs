using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization.DataPackets
{
    /// <summary>
    /// 組織全体のデータパケット
    /// </summary>
    public class OrganizationPacket
    {
        /// <summary>
        /// 単位組織
        /// </summary>
        public List<OrganizationBasePackte> Organizations { get; set; }

        /// <summary>
        /// 無所属社員一覧
        /// </summary>
        public List<Guid> UnAssignedPersons { get; set; }
    }
}
