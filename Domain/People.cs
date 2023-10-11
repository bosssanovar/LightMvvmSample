using Reactive.Bindings;

namespace Entity
{
    /// <summary>
    /// 集団クラス
    /// </summary>
    public class People
    {
        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        public List<Person> Persons { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public People()
        {
            Persons = new List<Person>();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報を削除する
        /// </summary>
        /// <param name="personIdentifier">個人情報識別子</param>
        public void RemovePerson(Guid personIdentifier)
        {
            if (Persons.Select(x => x.Identifier).Contains(personIdentifier))
            {
                Persons.RemoveAll(x => x.Identifier == personIdentifier);
            }
        }

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="person">更新データ</param>
        public void UpdatePersons(Person person)
        {
            var p = Persons.Single(x => x.Identifier == person.Identifier);

            if (p != null)
            {
                person.CopyTo(p);
            }
        }

        /// <summary>
        /// 個人情報を追加します。
        /// </summary>
        /// <param name="person">個人情報</param>
        public void AddPerson(Person person)
        {
            Persons.Add(person);
        }

        /// <summary>
        /// 複製を取得します。
        /// </summary>
        /// <returns>複製されたインスタンス</returns>
        public People Clone()
        {
            var ret = new People();

            foreach(var person in Persons)
            {
                ret.AddPerson(person.Clone());
            }

            return ret;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
