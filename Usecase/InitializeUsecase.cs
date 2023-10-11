﻿using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase
{
    /// <summary>
    /// データを初期化するユースケースを提供する
    /// </summary>
    public class InitializeUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly PeopleRepository _peopleRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository">Peopleエンティティのリポジトリ</param>
        public InitializeUsecase(PeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 設置値を初期化します。
        /// </summary>
        public void Initialize()
        {
            var people = new People();
            people.AddPerson(new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)));
            people.AddPerson(new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)));
            people.AddPerson(new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)));
            people.AddPerson(new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)));
            people.AddPerson(new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)));
            people.AddPerson(new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)));
            people.AddPerson(new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)));
            people.AddPerson(new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)));
            people.AddPerson(new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)));
            people.AddPerson(new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)));
            people.AddPerson(new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)));
            people.AddPerson(new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)));
            _peopleRepository.SavePeople(people);
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
