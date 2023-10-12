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

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報識別子
        /// </summary>
        public Guid Identifire { get; }

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
            : this(Guid.NewGuid(), name, birthDay)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="identifier">個人情報識別子</param>
        /// <param name="name">氏名</param>
        /// <param name="birthDay">誕生日</param>
        public PersonM(Guid identifier, NameVO name, BirthdayVO birthDay)
        {
            Identifire = identifier;
            Birthday = new ReactivePropertySlim<BirthdayVO>(birthDay);
            Name = new ReactivePropertySlim<NameVO>(name);

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

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
