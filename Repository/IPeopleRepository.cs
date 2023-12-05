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
        /// <see cref="People"/>エンティティの複製を取得します。
        /// </summary>
        /// <returns>Peopleエンティティの複製インスタンス</returns>
        People LoadPeople();

        /// <summary>
        /// 削除予定
        /// </summary>
        /// <returns>  aa</returns>
        IPeople LoadPersonsGetter();

        /// <summary>
        /// <see cref="People"/>エンティティを保存します。
        /// </summary>
        /// <param name="people">Peopleエンティティ</param>
        void SavePeople(People people);

        #endregion --------------------------------------------------------------------------------------------
    }
}