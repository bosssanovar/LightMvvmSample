using Entity.Organization;
using Entity.Organization.DataPackets;
using Entity.Persons;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DomainService.OrganizationVisitor
{
    /// <summary>
    /// データパケットをインポートするVisitor
    /// </summary>
    internal class ImportPacketVisitor : IOrganizationVisitor
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly List<Person> _persons;

        private readonly OrganizationPacket _organizationPacket;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="persons">社員リスト</param>
        /// <param name="organizationPacket">組織データパケット</param>
        public ImportPacketVisitor(List<Person> persons, OrganizationPacket organizationPacket)
        {
            _persons = persons;
            _organizationPacket = organizationPacket;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public void Visit(OrganizationBase target)
        {
            OrganizationBasePacket organizationPacket = _organizationPacket.Organizations
                .Find(x => x.Identifier == target.Identifier)
                ?? throw new ArgumentException("対象がありません。", nameof(target));

            // TODO K.I : Boss存在チェックパラメータをデータパケットに追加
            if (organizationPacket.BossId != Guid.Empty)
            {
                Person boss = _persons.Find(x => x.Identifier == organizationPacket.BossId)
                    ?? throw new ArgumentException("対象がありません。", nameof(target));
                target.SetBoss(boss);
            }

            foreach(var employeeId in organizationPacket.MemberIds)
            {
                Person employee = _persons.Find(x => x.Identifier == employeeId)
                    ?? throw new ArgumentException("対象がありません。", nameof(target));
                target.AddMember(employee);
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
