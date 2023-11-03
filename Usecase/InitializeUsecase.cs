using Entity.Organization;
using Entity.Persons;
using Repository;

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

        private readonly OrganizationRepository _organizationRepository;

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
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public InitializeUsecase(PeopleRepository peopleRepository, OrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 設置値を初期化します。
        /// </summary>
        public void Initialize()
        {
            var organization = _organizationRepository.LoadOrganization();

            var people = new People();
            AddNewMember(new Person(new NameVO("山田", "太郎"), new BirthdayVO(1990, 5, 5)), people, organization);
            AddNewMember(new Person(new NameVO("佐藤", "一郎"), new BirthdayVO(1985, 10, 2)), people, organization);
            AddNewMember(new Person(new NameVO("伊藤", "次郎"), new BirthdayVO(1990, 5, 5)), people, organization);
            AddNewMember(new Person(new NameVO("加藤", "三郎"), new BirthdayVO(1985, 10, 2)), people, organization);

            _peopleRepository.SavePeople(people);
            _organizationRepository.SaveOrganizaion(organization);
        }

        private static void AddNewMember(Person person, People people, Organization organization)
        {
            people.AddPerson(person);
            organization.AddNewMember(person);
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
