using Reactive.Bindings;

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
        public NameVO Name { get; set; }

        /// <summary>
        /// 誕生日を取得または設定します。
        /// </summary>
        public BirthdayVO Birthday { get; set; }

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        public Posts Post { get; private set; } = Posts.Employee;
        // TODO K.I : ポストは組織構成から引き当てるようにし、個人情報で保持しない

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        public string PostText => Post.GetDisplayText();

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

        private Person(Guid identifier, NameVO name, BirthdayVO birthday, Posts post)
        {
            _identifier = identifier;
            Name = name;
            Birthday = birthday;
            Post = post;
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
            other.Name = Name.Clone();
            other.Birthday = Birthday.Clone();
            other.Post = Post;
        }

        /// <summary>
        /// 複製を行います。
        /// </summary>
        /// <returns>複製したインスタンス</returns>
        public Person Clone() => new(_identifier, Name.Clone(), Birthday.Clone(), Post);

        /// <summary>
        /// 役職上位者かを確認します。
        /// </summary>
        /// <param name="target">比較対象</param>
        /// <returns>比較対象の方が役職レベルが高ければtrue</returns>
        public bool IsHigherPostThan(Person target)
        {
            return Post < target.Post;
        }

        /// <summary>
        /// 役職を更新します。
        /// </summary>
        /// <param name="post">変更後の値</param>
        public void UpdatePost(Posts post)
        {
            Post = post;
        }

        /// <summary>
        /// 同一性を有ているかを判定します。
        /// </summary>
        /// <param name="target">確認対象</param>
        /// <returns>同一性を有している場合 true</returns>
        public bool SameIdentityAs(Person target)
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