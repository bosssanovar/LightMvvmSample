using Entity.Persons;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Entity_Test")]

namespace Entity.Organization
{
    /// <summary>
    /// 管理組織クラス
    /// </summary>
    internal class ManagementOrganization : OrganizationBase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly List<OrganizationBase> _upperOrganizations;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 下部組織数を取得する。
        /// </summary>
        public int OrganizationCount
        {
            get
            {
                return _upperOrganizations.Count;
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">組織名</param>
        /// <param name="boss">組織長</param>
        /// <param name="upperOrganizations">下位組織</param>
        public ManagementOrganization(OrganizationNameVO name, Person boss, List<OrganizationBase> upperOrganizations)
            : base(name, boss)
        {
            _upperOrganizations = upperOrganizations;
        }

        private ManagementOrganization(Guid identifier, OrganizationNameVO name, Person boss, List<OrganizationBase> upperOrganizations)
            : base(identifier, name, boss)
        {
            _upperOrganizations = upperOrganizations;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製したインスタンス</returns>
        public override OrganizationBase Clone()
        {
            return new ManagementOrganization(Identifier, Name.Clone(), Boss.Clone(), _upperOrganizations.Select(x => x.Clone()).ToList());
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
