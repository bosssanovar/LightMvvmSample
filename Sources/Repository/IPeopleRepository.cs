using Entity.Persons;

namespace Repository
{
    /// <summary>
    /// 社員データリポジトリのインターフェース
    /// </summary>
    public interface IPeopleRepository
    {
        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// <see cref="IPeople"/>エンティティの複製を取得します。
        /// </summary>
        /// <returns>Peopleエンティティの複製インスタンス</returns>
        IPeople LoadPeople();

        /// <summary>
        /// <see cref="IPeople"/>エンティティを保存します。
        /// </summary>
        /// <param name="people">Peopleエンティティ</param>
        void SavePeople(IPeople people);

        #endregion --------------------------------------------------------------------------------------------
    }
}