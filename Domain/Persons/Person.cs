namespace Entity.Persons
{
    /// <summary>
    /// 個人情報クラス
    /// </summary>
    public class Person
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly Guid _identifier;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 名称を取得または設定します。
        /// </summary>
        public NameVO Name { get; private set; }

        /// <summary>
        /// 誕生日を取得または設定します。
        /// </summary>
        public BirthdayVO Birthday { get; private set; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">氏名</param>
        /// <param name="birthDay">誕生日</param>
        public Person(NameVO name, BirthdayVO birthDay)
        {
            _identifier = Guid.NewGuid();
            Birthday = birthDay;
            Name = name;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">元データ</param>
        /// <param name="name">氏名</param>
        /// <param name="birthDay">誕生日</param>
        public Person(Person person, NameVO name, BirthdayVO birthDay)
        {
            _identifier = person._identifier;
            Birthday = birthDay;
            Name = name;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 内部値をコピーします。
        /// </summary>
        /// <param name="other">コピー先</param>
        internal void CopyTo(Person other)
        {
            other.Name = Name;
            other.Birthday = Birthday;
        }

        /// <summary>
        /// 同一性を有ているかを判定します。
        /// </summary>
        /// <param name="target">確認対象</param>
        /// <returns>同一性を有している場合 true</returns>
        internal bool SameIdentityAs(Person target)
        {
            return _identifier == target._identifier;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}