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
    /// 個人情報リストを表示するユースケース機能を提供します。
    /// </summary>
    public class PersonListViewUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly PeopleRepository _peopleRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報が更新されたことを通知します。
        /// </summary>
        public event Action<Person> OnUpdatePerson;

        /// <summary>
        /// 個人情報が削除されたことを通知します。
        /// </summary>
        public event Action<Person> OnRemovePerson;

        /// <summary>
        /// 個人情報が追加されたことを通知します。
        /// </summary>
        public event Action<Person> OnAddPerson;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository">Peopleエンティティのリポジトリ</param>
        public PersonListViewUsecase(PeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// Peopleエンティティを取得します。
        /// </summary>
        /// <returns>Peopleエンティティ</returns>
        public People GetPeople() => _peopleRepository.LoadPeople();

        /// <summary>
        /// 個人情報を削除します。
        /// </summary>
        /// <param name="person">個人情報</param>
        public void RemovePerson(Person person)
        {
            var people = _peopleRepository.LoadPeople();

            var removed = people.GetPerson(person);

            people.RemovePerson(person);

            _peopleRepository.SavePeople(people);

            OnRemovePerson?.Invoke(removed);
        }

        /// <summary>
        /// 個人情報を保存します。
        /// </summary>
        /// <param name="person">個人情報</param>
        public void SavePerson(Person person)
        {
            var people = _peopleRepository.LoadPeople();

            if (people.Persons.Any(x => x.HasSameIdentity(person)))
            {
                person.CopyTo(people.Persons.Single(x => x.HasSameIdentity(person)));

                _peopleRepository.SavePeople(people);

                OnUpdatePerson?.Invoke(person);
            }
            else
            {
                people.Persons.Add(person);

                _peopleRepository.SavePeople(people);

                OnAddPerson?.Invoke(person);
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
