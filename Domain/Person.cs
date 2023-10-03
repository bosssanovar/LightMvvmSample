﻿namespace Domain
{
    /// <summary>
    /// 個人情報クラス
    /// </summary>
    public class Person
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly Name _name;
        private readonly Birthday _birthday;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 名前フルネームを取得します。
        /// </summary>
        public string Name { get => _name.FullName; }

        /// <summary>
        /// 年齢
        /// </summary>
        public int Age { get => _birthday.GetAge(DateTime.Today); }
        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">氏名</param>
        /// <param name="birthDay">誕生日</param>
        public Person(Name name, Birthday birthDay)
        {
            _name = name;
            _birthday = birthDay;
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