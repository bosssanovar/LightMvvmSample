using Entity.Organization;

namespace Repository
{
    /// <summary>
    /// 組織データリポジトリのインターフェース
    /// </summary>
    public interface IOrganizationRepository
    {
        // TODO K.I : 不要メソッド削除や、戻り値・引数のインターフェース化

        /// <summary>
        /// 組織に社員をアサインするEntityを取得します。
        /// </summary>
        /// <returns>組織に社員をアサインするEntity</returns>
        IOrganization LoadAssigner();

        /// <summary>
        /// <see cref="Organization"/>エンティティの複製を取得します。
        /// </summary>
        /// <returns><see cref="Organization"/>エンティティの複製インスタンス</returns>
        Organization LoadOrganization();

        /// <summary>
        /// 削除対処う
        /// </summary>
        /// <returns>削除</returns>
        IOrganization LoadProblemChecker();

        /// <summary>
        /// 組織に社員をアサインするEntityを保存します。
        /// </summary>
        /// <param name="assigner">組織に社員をアサインするEntity</param>
        void SaveAssigner(IOrganization assigner);

        /// <summary>
        /// <see cref="Organization"/>エンティティを保存します。
        /// </summary>
        /// <param name="organization"><see cref="Organization"/>エンティティ</param>
        void SaveOrganizaion(Organization organization);

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}