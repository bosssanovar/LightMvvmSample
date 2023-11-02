using Entity.Organization;
using Entity.Persons;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase;
using WpfApp1.MainWindow;

namespace WpfApp1.EditWindow
{
    /// <summary>
    /// EditWindow画面のモデル
    /// </summary>
    public class EditWindowM
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly Person _person;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 名称の苗字を取得します。
        /// </summary>
        public ReactivePropertySlim<string> FamilyName { get; }

        /// <summary>
        /// 名称の名前を取得します。
        /// </summary>
        public ReactivePropertySlim<string> FirstName { get; }

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
                return new(_person, new(FamilyName.Value, FirstName.Value), new(Year.Value, Month.Value, Day.Value));
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
        public EditWindowM(NameVO name, BirthdayVO birthDay)
            : this(new Person(name, birthDay))
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">個人情報</param>
        public EditWindowM(Person person)
        {
            _person = person;

            Year = new ReactivePropertySlim<int>(person.Birthday.Year)
                .AddTo(_disposables);
            Month = new ReactivePropertySlim<int>(person.Birthday.Month)
                .AddTo(_disposables);
            Day = new ReactivePropertySlim<int>(person.Birthday.Day)
                .AddTo(_disposables);

            // Name
            FamilyName = new ReactivePropertySlim<string>(person.Name.Family)
                .AddTo(_disposables);
            FirstName = new ReactivePropertySlim<string>(person.Name.First)
                .AddTo(_disposables);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

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
