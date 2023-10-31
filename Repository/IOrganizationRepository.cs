using Entity.Organization;

namespace Repository
{
    /// <summary>
    /// <see cref="Organization"/>エンティティのリポジトリインターフェース
    /// </summary>
    public interface IOrganizationRepository
    {
        /// <summary>
        /// <see cref="Organization"/>エンティティの複製を取得します。
        /// </summary>
        /// <returns><see cref="Organization"/>エンティティの複製インスタンス</returns>
        Organization LoadOrganization();

        /// <summary>
        /// <see cref="Organization"/>エンティティを保存します。
        /// </summary>
        /// <param name="organization"><see cref="Organization"/>エンティティ</param>
        void SaveOrganizaion(Organization organization);
    }
}