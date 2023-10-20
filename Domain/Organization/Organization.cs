using Entity.DomainService;
using Entity.DomainService.OrganizationVisitor;
using Entity.Persons;
using Entity.Service;
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
    public class Organization
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
        /// <param name="organization">社員を追加する組織</param>
        public void RelocateEmployee(Person person, OrganizationBase organization)
        {
            // TODO K.I : 既に所属している場合には、そこから抜く
            var visitor = new AddDirectEmployeeVisitor(person, organization);
            _topOrganization.Accept(visitor);

            if (!visitor.IsCompleted)
            {
                throw new ArgumentException("追加対象の組織がありません。", nameof(organization));
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

            if(visitor.AssignedOrganization is null)
            {
                throw new ArgumentException("指定社員は組織内に存在しません。", nameof(person));
            }

            return visitor.AssignedOrganization;
        }

        /// <summary>
        /// 社員の現在の役職を取得します。
        /// </summary>
        /// <param name="person">対象社員</param>
        /// <returns>役職</returns>
        /// <exception cref="ArgumentException">指定社員は組織内に存在しない場合</exception>
        public Posts GetPost(Person person)
        {
            //var visitor = new GetCurrentPositionVisitor(person);
            //_topOrganization.Accept(visitor);

            //if (visitor.AssignedOrganization is null)
            //{
            //    throw new ArgumentException("指定社員は組織内に存在しません。", nameof(person));
            //}

            //return visitor.Post;
            throw new NotFiniteNumberException();
        }

        /// <summary>
        /// 組織長を変更します。
        /// </summary>
        /// <param name="newBoss">対象社員</param>
        /// <param name="organization">対象組織</param>
        /// <returns>前の組織長</returns>
        /// <exception cref="ArgumentException">指定社員は組織内に存在しない場合</exception>
        public Person UpdateBoss(Person newBoss, OrganizationBase organization)
        {
            // TODO K.I : 既に所属している組織から抜く。新規配属（中途採用）にも対応。
            var visitor = new ChangeBossVisitor(newBoss, organization);
            _topOrganization.Accept(visitor);

            throw new NotFiniteNumberException();
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
