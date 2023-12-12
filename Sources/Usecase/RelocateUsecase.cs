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
    public class RelocateUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly IOrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 組織情報一覧を取得します。
        /// </summary>
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

        /// <summary>
        /// 社員の所属組織変更が発生したイベント
        /// </summary>
        public event Action<Person> OnPersonUpdate;

        /// <summary>
        /// 組織人員問題が発生したイベント
        /// </summary>
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

        /// <summary>
        /// 異動します。
        /// </summary>
        /// <param name="person">対象社員</param>
        /// <param name="newOrganization">異動先</param>
        /// <param name="isBoss">組織長として異動の場合　true</param>
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
            if(checkResult.Count > 0)
            {
                OnArisedProblems?.Invoke(new(checkResult, checker.UnAssignedPersons, checker.NoBossOrganizaiotns));
            }

            OnPersonUpdate?.Invoke(person);
        }

        /// <summary>
        /// 所属組織を取得します。
        /// </summary>
        /// <param name="personn">調査対象</param>
        /// <returns>所属組織</returns>
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
