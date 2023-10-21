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
        /// <param name="rank">組織ランク</param>
        /// <param name="lowerOrganizations">下位組織</param>
        public ManagementOrganization(OrganizationNameVO name, Ranks rank, List<OrganizationBase> lowerOrganizations)
            : base(name, rank)
        {
            if (!IsValidLank(rank))
            {
                throw new ArgumentOutOfRangeException("指定が許可された範囲外です。", nameof(rank));
            }

            _lowerOrganizations = lowerOrganizations;
        }

        private ManagementOrganization(int identifier, OrganizationNameVO name, Ranks rank, Person? boss, List<OrganizationBase> lowerOrganizations)
            : base(identifier, name, rank, boss)
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
            return new ManagementOrganization(Identifier, Name.Clone(), Rank, Boss?.Clone(), _lowerOrganizations.Select(x => x.Clone()).ToList());
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private static bool IsValidLank(Ranks rank)
        {
            switch (rank)
            {
                case Ranks.Campany:
                case Ranks.Department:
                case Ranks.Section:
                    return true;
                case Ranks.Team:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rank));
            }
        }

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
