using Entity.Persons;
using Entity.Service.OrganizationVisitor;

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

        private readonly List<OrganizationBase> _lowerOrganizations;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 下部組織数を取得する。
        /// </summary>
        public int OrganizationCount
        {
            get
            {
                return _lowerOrganizations.Count;
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
        /// <param name="lank">組織ランク</param>
        /// <param name="boss">組織長</param>
        /// <param name="lowerOrganizations">下位組織</param>
        public ManagementOrganization(OrganizationNameVO name, Lanks lank, Person boss, List<OrganizationBase> lowerOrganizations)
            : base(name, lank, boss)
        {
            _lowerOrganizations = lowerOrganizations;
        }

        private ManagementOrganization(Guid identifier, OrganizationNameVO name, Lanks lank, Person boss, List<OrganizationBase> lowerOrganizations)
            : base(identifier, name, lank, boss)
        {
            _lowerOrganizations = lowerOrganizations;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製したインスタンス</returns>
        public override ManagementOrganization Clone()
        {
            return new ManagementOrganization(Identifier, Name.Clone(), Lank, Boss.Clone(), _lowerOrganizations.Select(x => x.Clone()).ToList());
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        /// <summary>
        /// <see cref="IOrganizationVisitor"/>を受け入れる抽象メソッド
        /// </summary>
        /// <param name="visitor"><see cref="IOrganizationVisitor"/>インスタンス</param>
        internal override void Accept(IOrganizationVisitor visitor)
        {
            visitor.Visit(this);

            _lowerOrganizations.ForEach(x => x.Accept(visitor));
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
