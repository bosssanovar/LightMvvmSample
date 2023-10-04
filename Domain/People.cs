﻿using Reactive.Bindings;

namespace Domain
{
    /// <summary>
    /// 集団クラス
    /// </summary>
    public class People
    {
        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        public ReactiveCollection<Person> Persons { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public People()
        {
            Persons = new ReactiveCollection<Person>()
            {
                new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)),
                new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)),
            };
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
            if (Persons.Contains(person))
            {
                Persons.Remove(person);
            }
        }

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
