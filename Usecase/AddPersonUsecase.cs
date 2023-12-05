using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase.Sub;

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

        private readonly IPeopleRepository _peopleRepository;

        private readonly IOrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報が追加されたことを通知します。
        /// </summary>
        public event Action<Person> OnAddedPerson;

        /// <summary>
        /// 組織構成が変更されたことを通知します。
        /// </summary>
        public event Action OnChangedOrganization;

        /// <summary>
        /// 組織人員問題が発生したことを通知します。
        /// </summary>
        public event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="People"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public AddPersonUsecase(IPeopleRepository peopleRepository, IOrganizationRepository organizationRepository)
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
        public void AddPerson(Person person)
        {
            AddToPeople(person);

            OnAddedPerson?.Invoke(person);
            OnChangedOrganization?.Invoke();

            CheckProblems checker = new(_organizationRepository);
            var checkResult = checker.Check();
            if(checkResult.Count > 0 )
            {
                OnArisedProblems?.Invoke(new(checkResult, checker.UnAssignedPersons, checker.NoBossOrganizaiotns));
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void AddToPeople(Person person)
        {
            var people = _peopleRepository.LoadPeople();
            var organization = _organizationRepository.LoadOrganization();

            if (people.IsContain(person))
            {
                throw new ArgumentException("重複登録です。", nameof(person));
            }

            people.AddPerson(person);
            organization.AddNewMember(person);

            _peopleRepository.SavePeople(people);
            _organizationRepository.SaveOrganizaion(organization);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
