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

        private readonly ICheckProblems _checkProblems;

        private readonly IAssignRepository _assignRepository;

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
        /// <param name="checkProblems">組織人員問題検出</param>
        /// <param name="assignRepository">社員アサインEntity取得リポジトリ</param>
        public FixProblemsUseCase(ICheckProblems checkProblems, IAssignRepository assignRepository)
        {
            _checkProblems = checkProblems;
            _assignRepository = assignRepository;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 社員を組織にアサインします。
        /// </summary>
        /// <param name="person">社員</param>
        /// <param name="organization">組織</param>
        public void Assign(Person person, OrganizationBase organization)
        {
            OnArisedProblems(new(new(), new(), new()));
            OnUpdatePerson(person);
            OnUpdateOrganizaiton(organization);
            throw new NotImplementedException();
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
