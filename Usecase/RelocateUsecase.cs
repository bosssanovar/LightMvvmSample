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

        private readonly OrganizationRepository _organizationRepository;

        private readonly ICheckProblems _problemChecker;

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
        /// <param name="problemChecker">組織人員問題検出</param>
        public RelocateUsecase(OrganizationRepository organizationRepository, ICheckProblems problemChecker)
        {
            _organizationRepository = organizationRepository;
            _problemChecker = problemChecker;
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

            var checkResult = _problemChecker.Check();
            if(checkResult.Count > 0)
            {
                OnArisedProblems?.Invoke(new(checkResult, _problemChecker.UnAssignedPersons, _problemChecker.NoBossOrganizaiotns));
            }

            OnPersonUpdate?.Invoke(person);
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
