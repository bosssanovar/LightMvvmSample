using Reactive.Bindings;
using System.Collections.ObjectModel;

namespace Entity.Persons
{
    /// <summary>
    /// 集団クラス
    /// </summary>
    public class People
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly List<Person> _persons;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        public ReadOnlyCollection<Person> Persons
        {
            get
            {
                return _persons.AsReadOnly();
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public People()
        {
            _persons = new List<Person>();
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
            _persons.RemoveAll(x => x == person);
        }

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="person">更新データ</param>
        public void UpdatePersons(Person person)
        {
            if (!Persons.Any(x => x == person))
            {
                return;
            }

            Person p = Persons.Single(x => x == person);
            person.CopyTo(p);
        }

        /// <summary>
        /// 個人情報を追加します。
        /// </summary>
        /// <param name="person">個人情報</param>
        public void AddPerson(Person person)
        {
            _persons.Add(person);
        }

        /// <summary>
        /// 個人情報を取得します。
        /// </summary>
        /// <param name="person">個人情報識別子</param>
        /// <returns>個人情報</returns>
        public Person GetPerson(Person person)
        {
            return Persons.First(x => x == person);
        }

        /// <summary>
        /// 複製を取得します。
        /// </summary>
        /// <returns>複製されたインスタンス</returns>
        public People Clone()
        {
            var ret = new People();

            foreach (var person in Persons)
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
