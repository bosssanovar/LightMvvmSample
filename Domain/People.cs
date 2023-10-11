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
        public ReactiveCollection<Person> Persons { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public People()
        {
            Persons = new ReactiveCollection<Person>();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報を削除する
        /// </summary>
        /// <param name="person">個人情報</param>
        public void RemovePerson(Person person)
        {
            if (Persons.Contains(person))
            {
                Persons.Remove(person);
            }
        }

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="target">更新対象</param>
        /// <param name="source">更新データ</param>
        public void UpdatePersons(Person target, Person source)
        {
            var parson = Persons.Single(x => x == target);

            if (parson != null)
            {
                source.CopyTo(parson);
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
