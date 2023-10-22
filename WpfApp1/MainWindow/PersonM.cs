using Entity.Persons;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// 個人情報のModel
    /// </summary>
    public class PersonM
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly Person _person;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        public ReactivePropertySlim<NameVO> Name { get; }

        /// <summary>
        /// 名称の苗字を取得します。
        /// </summary>
        public ReactivePropertySlim<string> FamilyName { get; }

        /// <summary>
        /// 名称の名前を取得します。
        /// </summary>
        public ReactivePropertySlim<string> FirstName { get; }

        /// <summary>
        /// 誕生日を取得します。
        /// </summary>
        public ReactivePropertySlim<BirthdayVO> Birthday { get; }

        /// <summary>
        /// 誕生日　年を取得します。
        /// </summary>
        public ReactivePropertySlim<int> Year { get; }

        /// <summary>
        /// 誕生日　月を取得します。
        /// </summary>
        public ReactivePropertySlim<int> Month { get; }

        /// <summary>
        /// 誕生日　日を取得します。
        /// </summary>
        public ReactivePropertySlim<int> Day { get; }

        /// <summary>
        /// 役職を取得します。
        /// </summary>
        public ReactivePropertySlim<Posts> Post { get; }

        /// <summary>
        /// 所属組織の組織名を取得します。
        /// </summary>
        public ReactivePropertySlim<string> AssignedOrgaizationName { get; }

        /// <summary>
        /// 編集後の個人情報を取得します。
        /// </summary>
        public Person Person
        {
            get
            {
                var ret = _person.Clone();
                ret.Name = Name.Value;
                ret.Birthday = Birthday.Value;
                return ret;
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">氏名</param>
        /// <param name="birthDay">誕生日</param>
        /// <param name="post">役職</param>
        /// <param name="organizationName">所属組織名称</param>
        public PersonM(NameVO name, BirthdayVO birthDay, Posts post, string organizationName)
            : this(new Person(name, birthDay), post, organizationName)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">個人情報</param>
        /// <param name="post">役職</param>
        /// <param name="organizationName">所属組織名称</param>
        public PersonM(Person person, Posts post, string organizationName)
        {
            _person = person;

            // Birthday
            Birthday = new ReactivePropertySlim<BirthdayVO>(person.Birthday.Clone())
                .AddTo(_disposables);
            Year = new ReactivePropertySlim<int>(Birthday.Value.Year)
                .AddTo(_disposables);
            Month = new ReactivePropertySlim<int>(Birthday.Value.Month)
                .AddTo(_disposables);
            Day = new ReactivePropertySlim<int>(Birthday.Value.Day)
                .AddTo(_disposables);

            Year.Subscribe(y => Birthday.Value = new(y, Birthday.Value.Month, Birthday.Value.Day));
            Month.Subscribe(m => Birthday.Value = new(Birthday.Value.Year, m, Birthday.Value.Day));
            Day.Subscribe(d => Birthday.Value = new(Birthday.Value.Year, Birthday.Value.Month, d));

            // Name
            Name = new ReactivePropertySlim<NameVO>(person.Name.Clone())
                .AddTo(_disposables);
            FamilyName = new ReactivePropertySlim<string>(Name.Value.Family)
                .AddTo(_disposables);
            FirstName = new ReactivePropertySlim<string>(Name.Value.First)
                .AddTo(_disposables);

            FamilyName.Subscribe(f => Name.Value = new(f, Name.Value.First));
            FirstName.Subscribe(f => Name.Value = new(Name.Value.Family, f));

            Post = new ReactivePropertySlim<Posts>(post)
                .AddTo(_disposables);

            AssignedOrgaizationName = new ReactivePropertySlim<string>(organizationName)
                .AddTo(_disposables);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製したインスタンス</returns>
        public PersonM Clone() => new(Person.Clone(), Post.Value, AssignedOrgaizationName.Value);

        /// <summary>
        /// 各種破棄処理
        /// </summary>
        public void Dispose()
        {
            _disposables.Dispose();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
