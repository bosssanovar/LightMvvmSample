using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase
{
    /// <summary>
    /// 個人情報を更新するユースケースの機能を提供します。
    /// </summary>
    public class UpdatePersonUsecase
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

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository">Peopleエンティティのリポジトリ</param>
        public UpdatePersonUsecase(PeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報を更新します。
        /// </summary>
        /// <param name="person">個人情報</param>
        public void UpdatePerson(Person person)
        {
            var people = _peopleRepository.LoadPeople();

            if (people.Persons.Any(x => x == person))
            {
                person.CopyTo(people.Persons.Single(x => x == person));

                _peopleRepository.SavePeople(people);

                OnUpdatePerson?.Invoke(person);
            }
            else
            {
                // TODO K.I : こっちの場合は何もしない
                //people.AddPerson(person);

                //_peopleRepository.SavePeople(people);

                //OnAddPerson?.Invoke(person);
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
