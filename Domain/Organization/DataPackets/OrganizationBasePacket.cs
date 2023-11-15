using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization.DataPackets
{
    /// <summary>
    /// 組織単位のデータパケット
    /// </summary>
    public class OrganizationBasePacket
    {
        /// <summary>
        /// 識別子
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        /// 直属社員のIDリスト
        /// </summary>
        public List<Guid> MemberIds { get; set; }

        /// <summary>
        /// 組織長の識別子
        /// </summary>
        public Guid BossId { get; set; }
    }
}
