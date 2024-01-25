using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase.Sub;

namespace Usecase
{
    /// <summary>
    /// 社員異動のユースケースを提供します。
    /// </summary>
    public class RelocateUsecase : IRelocateUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly IOrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <inheritdoc/>
        public ReadOnlyCollection<OrganizationInfo> Organizations
        {
            get
            {
                var organization = _organizationRepository.LoadOrganization();
                return organization.GetOrganizationInfos();
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <inheritdoc/>
        public event Action<Person> OnPersonUpdate;

        /// <inheritdoc/>
        public event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="organizationRepository"><see cref="Organization"/>エンティティのリポジトリ</param>
        public RelocateUsecase(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <inheritdoc/>
        public void Relocate(Person person, OrganizationBase newOrganization, bool isBoss)
        {
            var organization = _organizationRepository.LoadOrganization();

            if (!isBoss)
            {
                organization.RelocateEmployee(person, newOrganization);
            }
            else
            {
                organization.SetBoss(person, newOrganization);
            }

            _organizationRepository.SaveOrganizaion(organization);

            var checker = new CheckProblems(_organizationRepository);
            var checkResult = checker.Check();
            if (checkResult.Count > 0)
            {
                OnArisedProblems?.Invoke(new(checkResult, checker.UnAssignedPersons, checker.NoBossOrganizaiotns));
            }

            OnPersonUpdate?.Invoke(person);
        }

        /// <inheritdoc/>
        public OrganizationBase GetAssignedOrganization(Person personn)
        {
            var organization = _organizationRepository.LoadOrganization();

            return organization.GetAssignedOrganization(personn);
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
