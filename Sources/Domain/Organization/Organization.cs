using Entity.DomainService;
using Entity.DomainService.OrganizationVisitor;
using Entity.Organization.DataPackets;
using Entity.Persons;
using Entity.Service;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 組織クラス
    /// </summary>
    public class Organization : IOrganization
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly OrganizationBase _topOrganization;

        private readonly UnAssignedMembersGroup _unAssignedMembersGroup = new();

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="builder">組織構成ビルダー</param>
        public Organization(IOrganizationBuilder builder)
        {
            _topOrganization = builder.Build();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public ReadOnlyCollection<OrganizationInfo> GetOrganizationInfos()
        {
            var visitor = new GetOrganizationListVisitor();
            _topOrganization.Accept(visitor);

            return visitor.Oganizations;
        }

        /// <inheritdoc/>
        public void AddNewMember(Person person)
        {
            _unAssignedMembersGroup.AddMember(person);
        }

        /// <inheritdoc/>
        public void RelocateEmployee(Person person, OrganizationBase newOrganization)
        {
            Leave(person);

            Assign(person, newOrganization, false);
        }

        /// <inheritdoc/>
        public void Assign(Person person, OrganizationBase newOrganization, bool isBoss)
        {
            if (!isBoss)
            {
                var addVisitor = new AddDirectEmployeeVisitor(person, newOrganization);
                _topOrganization.Accept(addVisitor);

                if (!addVisitor.IsAdded)
                {
                    throw new ArgumentException("追加対象の組織がありません。", nameof(newOrganization));
                }
            }
            else
            {
                SetBoss(person, newOrganization);
            }
        }

        /// <inheritdoc/>
        public OrganizationBase GetAssignedOrganization(Person person)
        {
            var visitor = new GetCurrentPositionVisitor(person);
            _unAssignedMembersGroup.Accept(visitor);
            _topOrganization.Accept(visitor);

            if (visitor.AssignedOrganization is null)
            {
                throw new ArgumentException("指定社員は組織内に存在しません。", nameof(person));
            }

            return visitor.AssignedOrganization;
        }

        /// <inheritdoc/>
        public Posts GetPost(Person person)
        {
            var visitor = new GetCurrentPositionVisitor(person);
            _unAssignedMembersGroup.Accept(visitor);
            _topOrganization.Accept(visitor);

            if (visitor.AssignedOrganization is null)
            {
                throw new ArgumentException("指定社員は組織内に存在しません。", nameof(person));
            }

            return visitor.Post;
        }

        /// <inheritdoc/>
        public string GetOrganizationName(Person person)
        {
            var visitor = new GetCurrentPositionVisitor(person);
            _unAssignedMembersGroup.Accept(visitor);
            _topOrganization.Accept(visitor);
            if (visitor.AssignedOrganization is null)
            {
                throw new ArgumentException("検索対象が組織内に存在しません", nameof(person));
            }

            var organization = visitor.AssignedOrganization;

            var infosVisitor = new GetOrganizationListVisitor();
            _unAssignedMembersGroup.Accept(infosVisitor);
            _topOrganization.Accept(infosVisitor);
            var infos = infosVisitor.Oganizations;

            return infos.Single(x => x.Organization.SameIdentityAs(organization)).FullName;
        }

        /// <inheritdoc/>
        public void SetBoss(Person newBoss, OrganizationBase organization)
        {
            var removeVisitor = new RemovePersonVisitor(newBoss);
            _unAssignedMembersGroup.Accept(removeVisitor);
            _topOrganization.Accept(removeVisitor);

            if (!removeVisitor.IsRemoved)
            {
                throw new ArgumentException("指定した社員が存在しません。", nameof(newBoss));
            }

            var visitor = new SetBossVisitor(newBoss, organization);
            _topOrganization.Accept(visitor);

            if (visitor.OldBoss is not null)
            {
                _unAssignedMembersGroup.AddMember(visitor.OldBoss);
            }

            if (!visitor.IsSetted)
            {
                throw new ArgumentException("指定した組織が存在しません。", nameof(organization));
            }
        }

        /// <inheritdoc/>
        public Person GetBoss(OrganizationBase organization)
        {
            if (organization.Boss is null)
            {
                throw new ArgumentException("引数で指定された組織に、組織長が設定されていません。");
            }

            return organization.Boss;
        }

        /// <inheritdoc/>
        public void Leave(Person targetPerson)
        {
            var visitor = new RemovePersonVisitor(targetPerson);
            _unAssignedMembersGroup.Accept(visitor);
            _topOrganization.Accept(visitor);

            if (!visitor.IsRemoved)
            {
                throw new ArgumentException("指定した社員は組織内に存在しません。", nameof(targetPerson));
            }
        }

        /// <inheritdoc/>
        public List<Person> GetUnAssignedPersons()
        {
            return _unAssignedMembersGroup.GetMembers();
        }

        /// <inheritdoc/>
        public List<OrganizationBase> GetNoBossOrganizaiotns()
        {
            var visitor = new GetNoBossOrganizaiotnsVisitor();
            _topOrganization.Accept(visitor);

            return visitor.NoBossOrganizations;
        }

        /// <inheritdoc/>
        public string GetOrganizationStructure()
        {
            var visitor = new GetOrganizationStructureVisitor();
            _topOrganization.Accept(visitor);

            return visitor.OrganizationStructureInfo;
        }

        /// <inheritdoc/>
        public void ClearAll()
        {
            var visitor = new ClearAllVisitor();
            _topOrganization.Accept(visitor);
            _unAssignedMembersGroup.RemoveAllMember();
        }

        /// <inheritdoc/>
        public OrganizationPacket ExportPacket()
        {
            var visitor = new GetOrganizationPacketVisitor();
            _topOrganization.Accept(visitor);
            return new()
            {
                Organizations = visitor.Packets,
                UnAssignedPersons = _unAssignedMembersGroup.GetMembers().Select(x => x.Identifier).ToList(),
            };
        }

        /// <inheritdoc/>
        public void ImportPacket(OrganizationPacket packet, List<Person> persons)
        {
            ImportToUnAssignedList(packet, persons);

            ImportToOrganizationMember(packet, persons);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void ImportToUnAssignedList(OrganizationPacket packet, List<Person> persons)
        {
            foreach (var unAssigned in packet.UnAssignedPersons)
            {
                _unAssignedMembersGroup.AddMember(
                    persons.Find(x => x.Identifier == unAssigned)
                    ?? throw new ArgumentException("社員データ不一致", nameof(persons)));
            }
        }

        private void ImportToOrganizationMember(OrganizationPacket packet, List<Person> persons)
        {
            var visitor = new ImportPacketVisitor(persons, packet);
            _topOrganization.Accept(visitor);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
