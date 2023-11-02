using Entity.DomainService;
using Entity.DomainService.OrganizationVisitor;
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
    public class Organization : IAssign, ICheckProblem
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly OrganizationBase _topOrganization;

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

        private Organization(OrganizationBase topOrganization)
        {
            _topOrganization = topOrganization;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製する
        /// </summary>
        /// <returns>複製インスタンス</returns>
        public Organization Clone()
        {
            var ret = new Organization(_topOrganization.Clone());

            return ret;
        }

        /// <summary>
        /// 複製する
        /// </summary>
        /// <returns>複製インスタンス</returns>
        IAssign IAssign.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// 組織情報を取得します。
        /// </summary>
        /// <returns>組織情報一覧</returns>
        public ReadOnlyCollection<OrganizationInfo> GetOrganizationInfos()
        {
            var visitor = new GetOrganizationListVisitor();
            _topOrganization.Accept(visitor);

            return visitor.Oganizations;
        }

        /// <summary>
        /// 直属社員を指定組織に異動します。新規配属にも対応。
        /// </summary>
        /// <param name="person">異動する社員</param>
        /// <param name="newOrganization">社員を追加する組織</param>
        public void RelocateEmployee(Person person, OrganizationBase newOrganization)
        {
            // 既に所属している場合には、そこから抜く
            var removeVisitor = new RemovePersonVisitor(person);
            _topOrganization.Accept(removeVisitor);

            if (!removeVisitor.IsRemoved)
            {
                // 新規配属。現状、機能なし
            }

            Assign(person, newOrganization, false);
        }

        /// <summary>
        /// 組織にアサインする。
        /// </summary>
        /// <param name="person">社員</param>
        /// <param name="newOrganization">社員を追加する組織</param>
        /// <param name="isBoss">組織長としてアサインする場合 true</param>
        /// <exception cref="ArgumentException">追加対象の組織がない場合</exception>
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

        /// <summary>
        /// 社員の所属組織を取得します。
        /// </summary>
        /// <param name="person">ターゲット社員</param>
        /// <returns>所属している組織</returns>
        public OrganizationBase GetAssignedOrganization(Person person)
        {
            var visitor = new GetCurrentPositionVisitor(person);
            _topOrganization.Accept(visitor);

            if (visitor.AssignedOrganization is null)
            {
                throw new ArgumentException("指定社員は組織内に存在しません。", nameof(person));
            }

            return visitor.AssignedOrganization.Clone();
        }

        /// <summary>
        /// 社員の現在の役職を取得します。
        /// </summary>
        /// <param name="person">対象社員</param>
        /// <returns>役職</returns>
        /// <exception cref="ArgumentException">指定社員は組織内に存在しない場合</exception>
        public Posts GetPost(Person person)
        {
            var visitor = new GetCurrentPositionVisitor(person);
            _topOrganization.Accept(visitor);

            if (visitor.AssignedOrganization is null)
            {
                throw new ArgumentException("指定社員は組織内に存在しません。", nameof(person));
            }

            return visitor.Post;
        }

        /// <summary>
        /// 所属する組織の組織名を取得します。
        /// </summary>
        /// <param name="person">対象社員</param>
        /// <returns>組織名称</returns>
        public string GetOrganizationName(Person person)
        {
            var visitor = new GetCurrentPositionVisitor(person);
            _topOrganization.Accept(visitor);
            if (visitor.AssignedOrganization is null)
            {
                throw new ArgumentException("検索対象が組織内に存在しません", nameof(person));
            }

            var organization = visitor.AssignedOrganization;

            var infosVisitor = new GetOrganizationListVisitor();
            _topOrganization.Accept(infosVisitor);
            var infos = infosVisitor.Oganizations;

            return infos.Single(x => x.Organization.SameIdentityAs(organization)).FullName;
        }

        /// <summary>
        /// 組織長を設定します。
        /// 新しい組織長に指定された社員は、元居た部署から削除されます。
        /// </summary>
        /// <param name="newBoss">対象社員</param>
        /// <param name="organization">対象組織</param>
        public void SetBoss(Person newBoss, OrganizationBase organization)
        {
            var removeVisitor = new RemovePersonVisitor(newBoss);
            _topOrganization.Accept(removeVisitor);

            var visitor = new SetBossVisitor(newBoss, organization);
            _topOrganization.Accept(visitor);

            if (!visitor.IsSetted)
            {
                throw new ArgumentException("指定した組織が存在しません。", nameof(organization));
            }
        }

        /// <summary>
        /// 組織長を取得します。
        /// </summary>
        /// <param name="organization">対象組織</param>
        /// <returns>組織長</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:メンバーを static に設定します", Justification = "<保留中>")]
        public Person GetBoss(OrganizationBase organization)
        {
            if (organization.Boss is null)
            {
                throw new ArgumentException("引数で指定された組織に、組織長が設定されていません。");
            }

            return organization.Boss.Clone();
        }

        /// <summary>
        /// 退職します。
        /// </summary>
        /// <param name="targetPerson">退職する社員</param>
        public void Leave(Person targetPerson)
        {
            var visitor = new RemovePersonVisitor(targetPerson);
            _topOrganization.Accept(visitor);

            if (!visitor.IsRemoved)
            {
                throw new ArgumentException("指定した社員は組織内に存在しません。", nameof(targetPerson));
            }
        }

        /// <summary>
        /// 未所属社員の一覧を取得します。
        /// </summary>
        /// <param name="persons">社員一覧</param>
        /// <returns>未所属社員の一覧</returns>
        public List<Person> GetUnAssignedPersons(List<Person> persons)
        {
            var visitor = new GetUnAssignedPersonsVisitor(persons);
            _topOrganization.Accept(visitor);

            return visitor.UnAssignedPersons;
        }

        /// <summary>
        /// 組織長不在組織の一覧を取得します。
        /// </summary>
        /// <returns>組織長不在組織の一覧</returns>
        public List<OrganizationBase> GetNoBossOrganizaiotns()
        {
            var visitor = new GetNoBossOrganizaiotnsVisitor();
            _topOrganization.Accept(visitor);

            return visitor.NoBossOrganizations;
        }

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
