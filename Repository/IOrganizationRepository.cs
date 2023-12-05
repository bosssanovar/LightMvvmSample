using Entity.Organization;

namespace Repository
{
    /// <summary>
    /// 組織データリポジトリのインターフェース
    /// </summary>
    public interface IOrganizationRepository
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// <see cref="Organization"/>エンティティの複製を取得します。
        /// </summary>
        /// <returns><see cref="Organization"/>エンティティの複製インスタンス</returns>
        IOrganization LoadOrganization();

        /// <summary>
        /// <see cref="Organization"/>エンティティを保存します。
        /// </summary>
        /// <param name="organization"><see cref="Organization"/>エンティティ</param>
        void SaveOrganizaion(IOrganization organization);

        #endregion --------------------------------------------------------------------------------------------
    }
}