using Entity.Organization;
using Entity.Organization.DataPackets;
using Entity.Persons;
using Entity.Persons.DataPackets;
using Repository;
using Usecase.Sub;

namespace Usecase
{
    /// <summary>
    /// データを保存するユースケース
    /// </summary>
    public class SaveLoadDataUsecase : ISaveLoadDataUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly IPeopleRepository _peopleRepository;

        private readonly IOrganizationRepository _organizationRepository;

        private readonly IDataStore _dataStore;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <inheritdoc/>
        public event Action OnPeopleUpdated;

        /// <inheritdoc/>
        public event Action OnOrganizationUpdated;

        /// <inheritdoc/>
        public event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="peopleRepository"><see cref="People"/>エンティティのリポジトリ</param>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        /// <param name="dataStore"><see cref="IDataStore"/>を実装したクラスインスタンス</param>
        public SaveLoadDataUsecase(
            IPeopleRepository peopleRepository,
            IOrganizationRepository organizationRepository,
            IDataStore dataStore)
        {
            _peopleRepository = peopleRepository;
            _organizationRepository = organizationRepository;
            _dataStore = dataStore;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public async Task Save(string path)
        {
            var people = _peopleRepository.LoadPeople();
            var organization = _organizationRepository.LoadOrganization();

            var peoplePacket = people.ExportPacket();
            var organizationPacket = organization.ExportPacket();

            var packet = new Entity.EntityPacket()
            {
                People = peoplePacket,
                Organization = organizationPacket,
            };

            await _dataStore.SaveData(path, packet);
        }

        /// <inheritdoc/>
        public async Task Load(string path)
        {
            PeoplePacket peoplePacket;
            OrganizationPacket organizationPacket;

            try
            {
                // Get data packets
                var packet = await _dataStore.LoadData(path);
                peoplePacket = packet.People;
                organizationPacket = packet.Organization;
            }
            catch
            {
                throw;
            }

            // Load entities
            var people = _peopleRepository.LoadPeople();
            var organization = _organizationRepository.LoadOrganization();

            // Clear entities
            organization.ClearAll();
            people.ClearAll();

            // Import
            people.ImportPacket(peoplePacket);
            organization.ImportPacket(organizationPacket, people.Persons.ToList());

            // Save entities
            _peopleRepository.SavePeople(people);
            _organizationRepository.SaveOrganizaion(organization);

            // Check Problems
            var checker = new CheckProblems(_organizationRepository);
            var checkResult = checker.Check();

            // Notify
            OnPeopleUpdated?.Invoke();
            OnOrganizationUpdated?.Invoke();
            if (checkResult.Count > 0)
            {
                OnArisedProblems?.Invoke(new(checkResult, checker.UnAssignedPersons, checker.NoBossOrganizaiotns));
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

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
