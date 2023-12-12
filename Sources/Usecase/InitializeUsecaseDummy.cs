using Entity.Organization;
using Entity.Persons;
using Repository;

namespace Usecase
{
    /// <summary>
    /// データを初期化するユースケースを提供する
    /// </summary>
    public class InitializeUsecaseDummy : IInitializeUsecase
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

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="IPeople"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="IOrganization"/>エンティティのリポジトリ</param>
        public InitializeUsecaseDummy(IPeopleRepository peopleRepository, IOrganizationRepository organizationRepository)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public void Initialize()
        {
            var organization = _organizationRepository.LoadOrganization();
            var organizations = organization.GetOrganizationInfos().Select(x => x.Organization).ToList();

            // TODO K.I : ダミーデータを追加
            var people = new People();
            AddPerson(new(new("aaa", "aaaaaa"), new(1000, 1, 2)), 0);
            AddPerson(new(new("bbb", "bbb"), new(1000, 1, 2)), 0, true);
            AddPerson(new(new("c", "cc"), new(1000, 1, 2)), 0);
            AddPerson(new(new("dddd", "d"), new(1000, 1, 2)), 1);

            _peopleRepository.SavePeople(people);
            _organizationRepository.SaveOrganizaion(organization);

            void AddPerson(Person person, int organizationIndex, bool isBoss = false)
            {
                people.AddPerson(person);
                organization.AddNewMember(person);
                if (isBoss)
                {
                    organization.SetBoss(person, organizations[organizationIndex]);
                }
                else
                {
                    organization.RelocateEmployee(person, organizations[organizationIndex]);
                }
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
