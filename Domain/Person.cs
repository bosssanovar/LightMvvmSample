using Reactive.Bindings;

namespace Entity
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
        /// 識別子を取得します。
        /// </summary>
        public Guid Identifier { get; }

        /// <summary>
        /// 名称を取得または設定します。
        /// </summary>
        public ReactivePropertySlim<NameVO> Name { get; }

        /// <summary>
        /// 誕生日を取得または設定します。
        /// </summary>
        public ReactivePropertySlim<BirthdayVO> Birthday { get; }

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
            : this(Guid.NewGuid(), name, birthDay)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="name">氏名</param>
        /// <param name="birthDay">誕生日</param>
        public Person(Guid identifier, NameVO name, BirthdayVO birthDay)
        {
            Identifier = identifier;
            Birthday = new ReactivePropertySlim<BirthdayVO>(birthDay);
            Name = new ReactivePropertySlim<NameVO>(name);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 内部値をコピーします。
        /// </summary>
        /// <param name="other">コピー先</param>
        public void CopyTo(Person other)
        {
            other.Name.Value = Name.Value.Clone();
            other.Birthday.Value = Birthday.Value.Clone();
        }

        /// <summary>
        /// 複製を行います。
        /// </summary>
        /// <returns>複製したインスタンス</returns>
        public Person Clone() => new(Identifier, Name.Value.Clone(), Birthday.Value.Clone());

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}