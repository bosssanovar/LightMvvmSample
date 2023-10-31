using Entity.Persons;

namespace Repository
{
    /// <summary>
    /// <see cref="People"/>のRepositoryのインターフェース
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// <see cref="People"/>エンティティの複製を取得します。
        /// </summary>
        /// <returns>Peopleエンティティの複製インスタンス</returns>
        People LoadPeople();

        /// <summary>
        /// <see cref="People"/>エンティティを保存します。
        /// </summary>
        /// <param name="people">Peopleエンティティ</param>
        void SavePeople(People people);
    }
}