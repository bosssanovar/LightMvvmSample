using Entity.Organization;
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
    /// 個人情報を追加するユースケースの機能を提供します。
    /// </summary>
    public class AddPersonUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly PeopleRepository _peopleRepository;

        private readonly OrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報が追加されたことを通知します。
        /// </summary>
        public event Action<Person> OnAddPerson;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="People"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public AddPersonUsecase(PeopleRepository peopleRepository, OrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報を保存します。
        /// </summary>
        /// <param name="person">個人情報</param>
        /// <param name="organization">配属組織</param>
        /// <param name="asBoss">組織長としてか</param>
        public void AddPerson(Person person, OrganizationBase organization, bool asBoss)
        {
            AddToPeople(person);

            AddToOrganization(person, organization, asBoss);

            OnAddPerson?.Invoke(person);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void AddToPeople(Person person)
        {
            var people = _peopleRepository.LoadPeople();

            if (people.IsContain(person))
            {
                throw new ArgumentException("重複登録です。", nameof(person));
            }

            people.AddPerson(person);

            _peopleRepository.SavePeople(people);
        }

        private void AddToOrganization(Person person, OrganizationBase organization, bool asBoss)
        {
            var or = _organizationRepository.LoadOrganization();

            if (asBoss)
            {
                or.SetBoss(person, organization);
            }
            else
            {
                or.RelocateEmployee(person, organization);
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
