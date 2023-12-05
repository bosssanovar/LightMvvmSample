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
        public InitializeUsecase(IPeopleRepository peopleRepository, IOrganizationRepository organizationRepository)
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

            _peopleRepository.SavePeople(people);
            _organizationRepository.SaveOrganizaion(organization);
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
