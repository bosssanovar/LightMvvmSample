using Entity.Persons;
using Entity.Service.OrganizationVisitor;

namespace Entity.Organization
{
    /// <summary>
    /// 末端組織クラス
    /// </summary>
    internal class TerminalOrganization : OrganizationBase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">社名</param>
        public TerminalOrganization(OrganizationNameVO name)
            : base(name, Lanks.Team)
        {
        }

        private TerminalOrganization(int identifier, OrganizationNameVO name, Person boss)
            : base(identifier, name, Lanks.Team, boss)
        {
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製インスタンス</returns>
        public override TerminalOrganization Clone()
        {
            return new TerminalOrganization(Identifier, Name.Clone(), Boss.Clone());
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
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
