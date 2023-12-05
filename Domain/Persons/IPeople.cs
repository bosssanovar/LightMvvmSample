using Entity.Persons.DataPackets;
using System.Collections.ObjectModel;

namespace Entity.Persons
{
    /// <summary>
    /// 社員リストのインターフェース
    /// </summary>
    public interface IPeople
    {
        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        ReadOnlyCollection<Person> Persons { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報を追加します。
        /// </summary>
        /// <param name="person">個人情報</param>
        void AddPerson(Person person);

        /// <summary>
        /// 社員を全て削除します。
        /// </summary>
        void ClearAll();

        /// <summary>
        /// データパケットを出力します。
        /// </summary>
        /// <returns>データパケット</returns>
        PeoplePacket ExportPacket();

        /// <summary>
        /// 個人情報を取得します。
        /// </summary>
        /// <param name="person">個人情報識別子</param>
        /// <returns>個人情報</returns>
        Person GetPerson(Person person);

        /// <summary>
        /// データパケットを取り込みます。
        /// </summary>
        /// <param name="packet">データパケット</param>
        void ImportPacket(PeoplePacket packet);

        /// <summary>
        /// 個人情報が既に登録済みかを取得します。
        /// </summary>
        /// <param name="person">確認する個人情報</param>
        /// <returns>登録済みの場合 true</returns>
        bool IsContain(Person person);

        /// <summary>
        /// 個人情報を削除する
        /// </summary>
        /// <param name="person">個人情報</param>
        void RemovePerson(Person person);

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="person">更新データ</param>
        void UpdatePersons(Person person);

        #endregion --------------------------------------------------------------------------------------------
    }
}