using Entity.Persons.DataPackets;

namespace Entity.Persons
{
    /// <summary>
    /// 個人情報クラス
    /// </summary>
    public class Person
    {
        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 識別子
        /// </summary>
        internal Guid Identifier { get; init; }

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
            Identifier = Guid.NewGuid();
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
            Identifier = person.Identifier;
            Birthday = birthDay;
            Name = name;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="name">名前</param>
        /// <param name="birthday">誕生日</param>
        internal Person(Guid identifier, NameVO name, BirthdayVO birthday)
        {
            Identifier = identifier;
            Name = name;
            Birthday = birthday;
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
            return Identifier == target.Identifier;
        }

        /// <summary>
        /// データパケットを出力します。
        /// </summary>
        /// <returns>データパケット</returns>
        internal PersonPacket ExportPacket() => new()
        {
            Identifier = Identifier,
            Birthday = Birthday.ExportPacket(),
            Name = Name.ExportPacket(),
        };

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}