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
    /// 組織人員問題を修正するユースケース
    /// </summary>
    public class FixProblemsUseCase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly IOrganizationRepository _organizationRepository;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 組織人員問題発生時イベント
        /// </summary>
        public event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        /// <summary>
        /// 社員情報更新時イベント
        /// </summary>
        public event Action<Person> OnUpdatePerson;

        /// <summary>
        /// 組織情報更新時イベント
        /// </summary>
        public event Action<OrganizationBase> OnUpdateOrganizaiton;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="organizationRepository">社員アサインEntity取得リポジトリ</param>
        public FixProblemsUseCase(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 社員を組織にアサインします。
        /// </summary>
        /// <param name="targetPerson">社員</param>
        /// <param name="destOrganization">組織</param>
        /// <param name="isBoss">組織長としてアサインする場合 true</param>
        public void Assign(Person targetPerson, OrganizationBase destOrganization, bool isBoss)
        {
            var organization = _organizationRepository.LoadOrganization();

            organization.Assign(targetPerson, destOrganization, isBoss);

            _organizationRepository.SaveOrganizaion(organization);

            OnUpdatePerson(targetPerson);
            OnUpdateOrganizaiton(destOrganization);

            var checker = new CheckProblems(_organizationRepository);
            var checkResult = checker.Check();
            if(checkResult.Count > 0)
            {
                OnArisedProblems(new(checkResult, checker.UnAssignedPersons, checker.NoBossOrganizaiotns));
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
