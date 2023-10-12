using Entity;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 編集後の個人情報を取得します。
        /// </summary>
        public Person Person
        {
            get
            {
                var ret = _person;
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
        public PersonM(NameVO name, BirthdayVO birthDay)
            : this(new Person(name, birthDay))
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">個人情報</param>
        public PersonM(Person person)
        {
            _person = person;
            Birthday = new ReactivePropertySlim<BirthdayVO>(person.Birthday.Clone());
            Name = new ReactivePropertySlim<NameVO>(person.Name.Clone());

            Year = new ReactivePropertySlim<int>(Birthday.Value.Year);
            Month = new ReactivePropertySlim<int>(Birthday.Value.Month);
            Day = new ReactivePropertySlim<int>(Birthday.Value.Day);
            FamilyName = new ReactivePropertySlim<string>(Name.Value.Family);
            FirstName = new ReactivePropertySlim<string>(Name.Value.First);

            Year.Subscribe(y => Birthday.Value = new(y, Birthday.Value.Month, Birthday.Value.Day));
            Month.Subscribe(m => Birthday.Value = new(Birthday.Value.Year, m, Birthday.Value.Day));
            Day.Subscribe(d => Birthday.Value = new(Birthday.Value.Year, Birthday.Value.Month, d));
            FamilyName.Subscribe(f => Name.Value = new(f, Name.Value.First));
            FirstName.Subscribe(f => Name.Value = new(Name.Value.Family, f));
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製したインスタンス</returns>
        public PersonM Clone() => new(Person.Clone());

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
