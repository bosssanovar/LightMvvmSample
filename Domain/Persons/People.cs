using System.Collections.ObjectModel;

namespace Entity.Persons
{
    /// <summary>
    /// 集団クラス
    /// </summary>
    public class People : IGetPersons
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
                return _persons.ToList()
                    .AsReadOnly();
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
            _persons.RemoveAll(x => x.SameIdentityAs(person));
        }

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="person">更新データ</param>
        public void UpdatePersons(Person person)
        {
            if (!_persons.Any(x => x.SameIdentityAs(person)))
            {
                return;
            }

            Person p = _persons.Single(x => x.SameIdentityAs(person));
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
            return _persons.First(x => x.SameIdentityAs(person));
        }

        /// <summary>
        /// 個人情報が既に登録済みかを取得します。
        /// </summary>
        /// <param name="person">確認する個人情報</param>
        /// <returns>登録済みの場合 true</returns>
        public bool IsContain(Person person)
        {
            return _persons.Any(x => x.SameIdentityAs(person));
        }

        /// <summary>
        /// データパケットを取り込みます。
        /// </summary>
        /// <param name="packet">データパケット</param>
        public void ImportPacket(PeoplePacket packet)
        {
            _persons.Clear();
            foreach(var person in packet.Persons)
            {
                AddPerson(person.Get());
            }
        }

        /// <summary>
        /// データパケットを出力します。
        /// </summary>
        /// <returns>データパケット</returns>
        public PeoplePacket ExportPacket() => new() { Persons = _persons.Select(x => x.ExportPacket()).ToList() };

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
